using App.Shared.Exceptions;
using App.Shared.Extensions;
using App.Shared.Models;
using App.Shared.Operations;
using App.Shared.Operations.Models;
using Blazored.Toast.Services;
using Microsoft.Extensions.Logging;
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

        public HttpExecutor(ILogger<HttpExecutor> logger, IToastService toastService, IOperationsManager operationsManager)
        {
            _logger = logger;
            _toastService = toastService;
            _operationsManager = operationsManager;
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
                        return true;
                    }
                    
                    await onSuccess.Invoke();

                    return true;
                }
                else if (status is OperationMessageFailed failed)
                {
                    if (onFailure is null)
                    {
                        _toastService.ShowError(failed.Reason);
                        return false;                
                    }

                    await onFailure?.Invoke();
                    return false;
                }
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // TODO Re apply for token using refresh token and then recurse on this function.             
                _toastService.ShowError("Request failed as you are not authenticated");
                return false;
            }

            _toastService.ShowError("Something went wrong :(");
            return false;
        }        
    }
}
