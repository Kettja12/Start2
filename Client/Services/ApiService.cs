using System.Net;
using System.Net.Http.Json;

namespace Start2.Client.Services;
public class ApiService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly StateService stateService;
    public ApiService(IHttpClientFactory clientFactory, StateService stateService)
    {
        this.clientFactory = clientFactory;
        this.stateService = stateService;
    }

    public async Task<HttpResponseMessage> PostServiceAsync(string service, object dataIn)
    {
        var client = clientFactory.CreateClient("api");
        if (stateService != null &&   string.IsNullOrEmpty(stateService.Token)==false)
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + stateService.Token);
        try
        {
            var response = await client.PostAsJsonAsync(service, dataIn);
            return response;
        }
        catch (Exception ex)
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(ex.Message)
            };
        }
    }
    public async Task<HttpResponseMessage> GetServiceAsync(string service, string token)
    {
        var client = clientFactory.CreateClient("api");
        if (token != "")
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        try
        {
            return await client.GetAsync(service);
        }
        catch (Exception ex)
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(ex.Message)
            };
        }

    }
}
