using Microsoft.AspNetCore.Identity;

namespace MinAppApi
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return  new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Simvol olmalidir"
            };


        }
        
    }
}
