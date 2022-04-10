using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Start2.Shared.Model.Account;
using System.Security.Claims;

namespace Start2.Client.Services;

public class CustomAuthenticationProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly StateService stateService;
    private readonly ILocalStorageService localStorageService;

    //private readonly HttpContext httpContext;
    public CustomAuthenticationProvider(
        ILocalStorageService localStorageService,
        //ProtectedSessionStorage protectedSessionStore,
        StateService stateService
        )
    {
        //this.protectedSessionStore = protectedSessionStore;
        this.stateService = stateService;
        this.localStorageService=localStorageService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymous = new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity() { }));
        await Task.FromResult(0);
        if (stateService.Token==null)
        {
            try
            {
                var s = await localStorageService.GetItemAsync<StateService>("stateService");
                if (s != null)
                {
                    stateService.Token = s.Token;
                    stateService.SessionExpires = s.SessionExpires;
                    stateService.User = s.User;
                    stateService.Claims = s.Claims;
                }

            }
            catch 
            {
                stateService.Token = null;
            }
        }
        if (stateService.Token == null
            || stateService.SessionExpires < DateTime.Now
            || stateService.User == null)
        {
            await localStorageService.RemoveItemAsync("stateService");
            stateService.Token = null;
            return anonymous;
        }
        var identity = new ClaimsIdentity(new[]
        {
            new System.Security.Claims.Claim(ClaimTypes.Name, stateService.User.Username),
        }, stateService.User.Username);
        claimsPrincipal = new ClaimsPrincipal(identity);
        return new AuthenticationState(claimsPrincipal);
    }
    public void Notify()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public void LogoutNotify()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        claimsPrincipal = anonymous;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
