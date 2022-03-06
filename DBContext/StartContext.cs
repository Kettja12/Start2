using Microsoft.EntityFrameworkCore;
using Start2.Shared.Model.Account;
using Start2.Shared.Model.Dashboard;
using Start2.Shared.Model.Reservation;

namespace Start2.DBContext;

public partial class StartContext : DbContext
{
    public StartContext(DbContextOptions<StartContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Claim>? Claims { get; set; }
    public DbSet<LoginToken> LoginTokens => Set<LoginToken>();
    public DbSet<DashboardItem> DashboardItems => Set<DashboardItem>();
    public DbSet<ReservationNode> ReservationNodes => Set<ReservationNode>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

}
