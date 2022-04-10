namespace Start2.Shared.Model.Dashboard;
public class DashboardItem
{
    public string Id { get; set; } = null!;
    public string? Control { get; set; }
    public bool InUse { get; set; }
    public string? UserGroups { get; set; }
    public DateTime Modified { get; set; }

}
