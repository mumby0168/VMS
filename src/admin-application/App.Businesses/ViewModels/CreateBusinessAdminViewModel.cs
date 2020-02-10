using App.Shared.Http;
using App.Shared.Models;
using App.Shared.Services;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Businesses.ViewModels
{
    public class CreateBusinessAdminViewModel
    {
        private readonly IHttpClient _client;
        private readonly IHttpExecutor _httpExecutor;
        private readonly IModalService _modalService;
        private readonly IToastService _toastService;

        public CreateBusinessAdminViewModel(IHttpClient client, IHttpExecutor httpExecutor, IModalService modalService, IToastService toastService)
        {
            _client = client;
            _httpExecutor = httpExecutor;
            _modalService = modalService;
            _toastService = toastService;
        }
        [EmailAddress]
        public string Email { get; set; }

        public Guid Id { get; set; }

        public async Task SubmitAsync()
        {           
            HttpResponseMessage response = null;
            try
            {
                response = await _client.IdentityClient.PostAsync("admin/create-business-admin", JsonMessage.CreateJsonMessage(new { BusinessId = Id, Email }));
            }
            catch (HttpRequestException)
            {
                _toastService.ShowError(ServiceError.Standard.Reason);
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                _modalService.Close(ModalResult.Cancel());
                _toastService.ShowSuccess($"Email sent to user {Email} to confirm their account");
                Email = string.Empty;
                Id = Guid.Empty;
                return;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = await ServiceError.Deserialize(response);
                _toastService.ShowError(error.Reason);
            }           
        }
    }
}
