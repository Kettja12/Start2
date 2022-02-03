using Start2.Client.Services;
using Start2.Shared;
using Start2.Shared.Model;
using Start2.Shared.Model.Dashboard;
using Start2.Shared.Model.Reservation;
using System.Net;
using System.Net.Http.Json;

namespace Start2.Client.Controllers
{
    public class ReservationController
    {
        private readonly ApiService apiService;
        private readonly StateService stateService;
        public string Status = string.Empty;

        public ReservationController(ApiService apiService,
            StateService stateService)
        {
            this.apiService = apiService;
            this.stateService = stateService;
        }
        public async Task<List<ReservationNode>?> LoadReservationNodesAsync()
        {
            try
            {

                var response = await apiService.GetServiceAsync(APIServices.ReservationGetReservationNodes);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var r = await response.Content.ReadFromJsonAsync<List<ReservationNode>>();
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

        public async Task<ReservationNode> SaveReservationNodeAsync(ReservationNode node)
        {
            try
            {
                var response = await apiService.PostServiceAsync(
                APIServices.ReservationSaveReservationNode, node);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ReservationNode result = await response.Content.ReadFromJsonAsync<ReservationNode>();
                    Status = "Reservation node save success.";
                    return result;
                }
                Status = "Reservation node save failed.";
                var s = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(s) == false)
                    Status= s;
            }
            catch (Exception e)
            {
                Status = e.Message;
            }
            return null;
        }



    }
}
