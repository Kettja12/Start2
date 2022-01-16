using Start2.Shared.Model.Account;
namespace Start2.Client.Services;
public class StateService
{
    public string? Token { get; set; }
    public DateTime? SessionExpires { get; set; }

    public bool IsLoaded { get { return User != null; } }
    public User? User { get; set; }

    public void Logout()
    {
        User = null;
    }
}
