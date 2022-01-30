using Start2.Client.Services;
using Start2.Shared;
using Start2.Shared.Model;
using Start2.Shared.Model.Dashboard;
using System.Net;
using System.Net.Http.Json;

namespace Start2.Client.Controllers
{
    public class DashboardController
    {
        private readonly ApiService apiService;
        private readonly StateService stateService;
        public string Status = string.Empty;

        public DashboardController(ApiService apiService,
            StateService stateService)
        {
            this.apiService = apiService;
            this.stateService = stateService;
        }
        public async Task<List<DashboardItem>?> LoadDasboardItemsAsync()
        {
            try
            {

                var response = await apiService.GetServiceAsync(APIServices.DashboardGetDashboardItems);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var r = await response.Content.ReadFromJsonAsync<List<DashboardItem>>();
                    if (r != null)
                    {
                        return r;
                    }

                }
                var s = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(s)==false)
                    Status = s;
                return null;
            }
            catch (Exception e)
            {
                Status= e.Message;
            }
            Status = "ServiceCall Failed.";
            return null;
        }
    
    }
}
