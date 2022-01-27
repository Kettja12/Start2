using Microsoft.EntityFrameworkCore;
using Start2.Shared.Model.Account;
using Start2.Shared.Model.Dashboard;

namespace Start2.Server.DBContext;

public partial class StartContext : DbContext
{
    public StartContext(DbContextOptions<StartContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Claim>? Claims { get; set; }
    public DbSet<LoginToken> LoginTokens => Set<LoginToken>();
    public DbSet<DashboardItem> DashboardItems => Set<DashboardItem>();

}
