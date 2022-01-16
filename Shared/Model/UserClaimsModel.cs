using Start2.Shared.Model.Account;

namespace Start2.Shared.Model
{
    public class UserClaimsModel
    {
        public int UserId { get; set; }
        public List<Claim>? Claims {get;set;}     
    }
}
