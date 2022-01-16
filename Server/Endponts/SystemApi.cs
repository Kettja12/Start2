//using Api;

//namespace Services;
//public static class SystemApi
//{
//    public static void Map(IEndpointRouteBuilder endpoints)
//    {

//        _ = endpoints.MapPost(Api.Services.SystemTest,
//            (ApiServices services, string postParams) =>
//          {
//              return services.TestAsync(postParams);
//          }).AllowAnonymous();

//        _ = endpoints.MapPost(Api.Services.SystemGetSettings,
//            async (ApiServices services, ApiKeySearch postParams) =>
//        {
//            if (postParams == null) return services.NoParamers();
//            return await services.GetSettingsAsync(postParams);
//        });

//        _ = endpoints.MapPost(Api.Services.SystemSaveSetting,
//            async (ApiServices services, ApplicationSettingModel postParams) =>
//        {
//            if (postParams == null) return services.NoParamers();
//            return await services.SaveSettingAsync(postParams);
//        });

//    }

//}
