using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Start2.Shared.Model;
using System.Security.Cryptography;
using System.Text;

namespace Start2.Client.Services
{
    public class AccountService 
    {
        private readonly CustomAuthenticationProvider authenticationStateProvider;
        private readonly StateService stateService;
        private readonly ILocalStorageService localStorageService;

        public AccountService(
             ILocalStorageService localStorageService,
            StateService stateService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.localStorageService = localStorageService;
            this.authenticationStateProvider = (CustomAuthenticationProvider) authenticationStateProvider;
            this.stateService = stateService;
        }
        public async Task<bool> LoginAsync(LoginResponseModel loginResponseModel)
        {
            if (loginResponseModel != null 
                && loginResponseModel.User!=null)
            {
                stateService.SessionExpires = loginResponseModel.SessioneExpires;
                stateService.Token = loginResponseModel.Token;
                stateService.User = loginResponseModel.User;

                await localStorageService.SetItemAsync("stateService", stateService);

            }
            authenticationStateProvider.Notify();
            
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            await localStorageService.RemoveItemAsync("stateService");
            stateService.Token = null;
            authenticationStateProvider.LogoutNotify();
            return true; 
        }

        public string CreateToken(string token)
        {
            if (token == "") return Guid.NewGuid().ToString();
            var dataArray = Encoding.ASCII.GetBytes(token);
            HashAlgorithm sha = SHA256.Create();
            byte[] result = sha.ComputeHash(dataArray);
            return Encoding.ASCII.GetString(result);
        }
    }
}
