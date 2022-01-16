namespace Start2.Shared.Model.Account;
public class LoginToken
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;
    public int UserId { get; set; }
    public virtual User? User { get; set; }

}

