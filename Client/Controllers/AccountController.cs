using Blazored.LocalStorage;
using Start2.Client.Services;
using Start2.Shared;
using Start2.Shared.Model;
using Start2.Shared.Model.Account;
using System.Net;
using System.Net.Http.Json;

namespace Start2.Client.Controllers
{
    public class AccountController
    {
        private readonly ApiService apiService;
        private readonly AccountService accountService;
        private readonly StateService stateService;
     
        public string Status = string.Empty;

        public AccountController(ApiService apiService,
            AccountService accountService,
            StateService stateService)
        {
            this.apiService = apiService;
            this.accountService = accountService;
            this.stateService = stateService;
        }
        public async Task<string> LoginAsync(LoginModel LoginParams)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginParams.Username)
                   || string.IsNullOrEmpty(LoginParams.Password))
                {
                    return "Username or password missing.";
                }
                var response = await apiService.PostServiceAsync(APIServices.AccountLogin, LoginParams);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    LoginResponseModel? r = await response.Content.ReadFromJsonAsync<LoginResponseModel>();
                    if (r != null)
                    {
                        await accountService.LoginAsync(r);
                        return string.Empty;
                    }

                }
                var s = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(s)==false)
                    return s;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "ServiceCall Failed.";
        }
        public async Task<string> SaveActiveUserAsync(
            string? oldPassword,
            string? newPassword,
            string? passwordVerification)
        {
            try
            {
                if (stateService.User == null)
                {
                    return "No user information to save.";

                }

                if (newPassword != passwordVerification)
                {
                    return "Password and password verification are not same.";
                }

                if (string.IsNullOrEmpty(newPassword) == false
                    && string.IsNullOrEmpty(oldPassword))
                {
                    return "Old password missing.";
                }
                var response = await apiService.PostServiceAsync(
                APIServices.AccountSaveUser, stateService.User);
                var firstResult = await response.Content.ReadFromJsonAsync<User>();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (string.IsNullOrEmpty(newPassword) == false)
                    {
                        var p = new SavePasswordModel()
                        {
                            OldPassword = oldPassword,
                            NewPassword = newPassword,
                            Username = stateService.User.Username
                        };
                        response = await apiService.PostServiceAsync(
                            APIServices.AccountSavePassword, p);
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return "User save success.";
                    }
                }
                var s = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(s)==false)
                    return s;
                return "User save failed.";

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public async Task<User?> SaveUserAsync(
            string? password,
            string? passwordVerification,
            User selectedUser)
        {
            try
            {
                if (password != passwordVerification)
                {
                    Status = "Password and password verification are not same.";
                    return null;
                }
                var response = await apiService
                    .PostServiceAsync(APIServices.AccountSaveUser, selectedUser);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    if (user != null)
                    {

                        if (string.IsNullOrEmpty(password) == false)
                        {
                            var p = new SavePasswordModel()
                            {
                                NewPassword = password,
                                Username = user.Username
                            };
                            response = await apiService.PostServiceAsync(
                                APIServices.AccountSavePassword, p);
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Status= "User save success.";
                            return user;
                        }
                    }
                }
                Status = "User save failed."; 
                var s = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(s) == false)
                    Status =s.Replace('"',' ').Trim();
                return null;
            }
            catch (Exception e)
            {
                Status =e.Message;
                return null;
            }

        }

        public async Task<List<User>?> SearchUserList(UserSearchModel userSearchModel)
        {
            try
            {
                if (apiService != null)
                {
                    if (userSearchModel.Searchkey == "1"
                        && string.IsNullOrEmpty(userSearchModel.Searchfield))
                    {
                        Status="You must enter username for search.";
                        return null;
                    }
                    var response = await apiService.PostServiceAsync(APIServices.AccountGetUsers, userSearchModel);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        List<User>? result = await response.Content.ReadFromJsonAsync<List<User>>();
                        if (result != null)
                        {
                            if (result.Any()) return result;

                        }
                        Status = "Users not found  with these conditions.";
                        return null;
                    }
                    var s = response.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrEmpty(s) == false)
                        Status = s;
                }
                return null;
            }
            catch (Exception e)
            {
                Status =e.Message;
            }
            return null;

        }

        public async Task<string> SaveActiveUserAsync()
        {
            try
            {
                if (stateService.User == null)
                {
                    return "No user information to save.";

                }
                var response = await apiService.PostServiceAsync(
                APIServices.AccountSaveUser, stateService.User);
                var firstResult = await response.Content.ReadFromJsonAsync<User>();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await accountService.UpdateState();
                    return "User save success.";

                }
                var s = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(s) == false)
                    return s;
                return "User save failed.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}
