using WebApplication2.Data;

namespace WebApplication2.Interface
{
    public interface ITenantService
    {
 
        public int GetTenant();
        public int? GetProject();
        public string GetUser();
        public List<string> GetRoles();
    }
}
