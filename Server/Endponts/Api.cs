namespace Start2.Server.Endponts;
public partial class Endpoints
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        UserApi.Map(endpoints);
        //SystemApi.Map(endpoints);
        DashboardApi.Map(endpoints);
        ReservationApi.Map(endpoints);
    }

}
