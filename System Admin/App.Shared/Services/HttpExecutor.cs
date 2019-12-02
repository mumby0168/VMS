using App.Shared.Context;
using App.Shared.Exceptions;
using App.Shared.Extensions;
using App.Shared.Models;
using App.Shared.Operations;
using App.Shared.Operations.Models;
using Blazored.Toast.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services
{
    public class HttpExecutor : IHttpExecutor
    {
        private readonly ILogger<HttpExecutor> _logger;
        private readonly IToastService _toastService;
        private readonly IOperationsManager _operationsManager;
        private readonly IOperationsService _operationsService;
        private readonly HttpClient _client;

        public HttpExecutor(ILogger<HttpExecutor> logger, IToastService toastService, IOperationsManager operationsManager, IOperationsService operationsService, IHttpClientFactory httpFactory, IUserContext context)
        {
            _logger = logger;
            _toastService = toastService;
            _operationsManager = operationsManager;
            _operationsService = operationsService;
            _client = httpFactory.CreateClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Token}");
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(T);
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"The request failed to : {url} for reason: {e.Message}");
            }

            _toastService.ShowError("The data could not fetched, please try again soon.");

            return default(T);
        }

        public async Task<bool> SendRequestAsync(Func<Task<HttpResponseMessage>> call,string successMessage, Func<Task> onFailure = null, Func<Task> onSuccess = null)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await call();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("A request failed with the reason: " + e.Message);
                _toastService.ShowError(ServiceError.Standard.Reason);
                return false;
            }

            if(response.IsSuccessStatusCode)
            {
                var status = await _operationsManager.GetOperationStatusAsync(response.GetOperationId());
                if(status.Status == OperationStatus.Complete)
                {
                    if (onSuccess is null)
                    {
                        _toastService.ShowSuccess(successMessage);
                        await CheckConnection();
                        return true;
                    }

                    await CheckConnection();
                    await onSuccess.Invoke();

                    return true;
                }
                else if (status is OperationMessageFailed failed)
                {
                    if (onFailure is null)
                    {
                        await CheckConnection();
                        _toastService.ShowError(failed.Reason);
                        return false;                
                    }

                    await CheckConnection();
                    await onFailure?.Invoke();
                    return false;
                }
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await CheckConnection();
                // TODO Re apply for token using refresh token and then recurse on this function.             
                _toastService.ShowError("Request failed as you are not authenticated");
                return false;
            }

            await CheckConnection();
            _toastService.ShowError("Something went wrong :(");
            return false;
        }        


        private Task CheckConnection()
        {
            if (!_operationsManager.IsConnected)
            {
                return _operationsService.CreateConnection();
            }
            else return Task.CompletedTask;
        }
    }
}
