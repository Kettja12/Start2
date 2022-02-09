using Start2.Shared;
using System.Net;
using System.Net.Http.Json;

namespace Start2.Client.Services;
public class ApiService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly StateService stateService;
    private readonly ApiServerParameters parameters;

    public ApiService(IHttpClientFactory clientFactory, 
        StateService stateService,
        ApiServerParameters parameters)
    {
        this.clientFactory = clientFactory;
        this.stateService = stateService;
        this.parameters = parameters;
    }

    public async Task<HttpResponseMessage> PostServiceAsync(string service, object dataIn)
    {
        var client = clientFactory.CreateClient("api");
        if (stateService != null &&   string.IsNullOrEmpty(stateService.Token)==false)
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + stateService.Token);
        try
        {
            var response = await client.PostAsJsonAsync(
                parameters.SubFolder+service, dataIn);
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
    public async Task<HttpResponseMessage> GetServiceAsync(string service)
    {
        var client = clientFactory.CreateClient("api");
        if (stateService != null && string.IsNullOrEmpty(stateService.Token) == false)
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + stateService.Token);
        try
        {
            return await client.GetAsync(parameters.SubFolder + service);
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
