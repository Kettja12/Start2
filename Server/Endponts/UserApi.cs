using Start2.Server.Services;
using Start2.Shared;
using Start2.Shared.Model;
using Start2.Shared.Model.Account;

namespace Start2.Server.Endponts;
public static partial class Endpoints
{
    public static void MapUserApi(IEndpointRouteBuilder endpoints)
    {

        _ = endpoints.MapPost(APIServices.AccountLogin,
            async (AccountServices  services, LoginModel postParams) =>
        {
            if (CheckObject(postParams) == false) return services.UnacceptedContent();
            return await services.LoginAsync(postParams);
        }).AllowAnonymous();

        _ = endpoints.MapPost(APIServices.AccountGetUsers,
            async (AccountServices services, UserSearchModel postParams) =>
        {
            if (CheckObject(postParams) == false) return services.UnacceptedContent();
            return await services.GetUsersAsync(postParams);
        });

        _ = endpoints.MapPost(APIServices.AccountSaveUser,
            async (AccountServices services, User postParams) =>
        {
            if (CheckObject(postParams) == false) return services.UnacceptedContent();
            return await services.SaveUserAsync(postParams);
        });
        _ = endpoints.MapPost(APIServices.AccountSavePassword,
            async (AccountServices services, SavePasswordModel postParams) =>
        {
            if (CheckObject(postParams) == false) return services.UnacceptedContent();
            return await services.SavePasswordAsync(postParams);
        });
    }

}
