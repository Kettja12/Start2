
using Start2.Shared.Model.Account;

namespace Start2.Shared.Model;
public class LoginResponseModel
{
    public string? Token { get; set; }
    public DateTime SessioneExpires { get; set; }   
    public User? User { get; set; }

}
