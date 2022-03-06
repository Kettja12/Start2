using Microsoft.EntityFrameworkCore;
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
        Entry(dashboardItem).State = dashboardItem.Id == 0 
            ? EntityState.Added : EntityState.Modified;
        await SaveChangesAsync();
        return dashboardItem;

    }

}
