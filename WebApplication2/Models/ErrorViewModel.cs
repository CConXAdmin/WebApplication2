using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class ApplicationUser : IdentityUser
    { 
    }
}