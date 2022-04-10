using Microsoft.EntityFrameworkCore;
using NUlid;
using Start2.Shared.Model.Dashboard;

namespace Start2.DBContext;
public partial class StartContext
{
    public async Task<List<DashboardItem>?> GetDasboardItemsAsync()
    {
        List<DashboardItem>? items = await DashboardItems.ToListAsync();
        return items;
    }
    public async Task<DashboardItem> SaveDashboardItemAsync(DashboardItem dashboardItem)
    {
        if (dashboardItem.Id == "")
        {
            dashboardItem.Id = Ulid.NewUlid().ToString();
            Entry(dashboardItem).State = EntityState.Added; 
        }
        else
        {
            Entry(dashboardItem).State = EntityState.Modified;
        }

        await SaveChangesAsync();
        return dashboardItem;

    }

}
