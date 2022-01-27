using Microsoft.EntityFrameworkCore;
using Start2.Shared.Model.Dashboard;

namespace Start2.Server.DBContext;
public partial class StartContext
{
    public async Task<List<DashboardItem>?> GetDasboardItemsAsync()
    {
        List<DashboardItem>? items = await DashboardItems.ToListAsync();
        return items;
    }

}
