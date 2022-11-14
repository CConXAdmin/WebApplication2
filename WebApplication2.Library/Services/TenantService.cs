using Microsoft.AspNetCore.Identity;
using WebApplication2.Interface;  
using WebApplication2.Data;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Services
{
    public class TenantService : ITenantService
    {
        private HttpContext _httpContext ;
        private int _currentTenant =0;
        private int? _currentProject=0;
        private string _currentUser="";
        private List<string> _currentRoles = null!;
        private readonly ApplicationDbContext db;
        //private readonly UserManager<ApplicationUser> _userManager;
        public TenantService(IHttpContextAccessor contextAccessor, ApplicationDbContext _db)//, UserManager<ApplicationUser> userManager)
        {
            //_userManager = userManager;
            db = _db;
            _httpContext = contextAccessor.HttpContext;
            if (_httpContext != null)
            {
                if (_httpContext.Request.Cookies["UserId"] != null)
                {
                    SetUser(""+_httpContext.Request.Cookies["UserId"]);

                }
                if (_httpContext.Request.Cookies["ProjectId"] != null)
                {
                    var c = _httpContext.Request.Cookies["ProjectId"];
                    if (c == null || c == "") c = "0";
                    SetProject(Convert.ToInt32(c));

                }
                if (_httpContext.Request.Cookies["TenantId"] != null)
                {
                    var c = _httpContext.Request.Cookies["TenantId"];
                    if (c == null || c == "") c = "0";
                    SetTenant(Convert.ToInt32(c));
                } 
            }
        }

        //public class TenantService : ITenantService
        //{
        //    private HttpContext _httpContext;
        //    private int _currentTenant;
        //    private int _currentProject;
        //    private string _currentUser;
        //    private readonly MainDbContext db;
        //    public TenantService(IHttpContextAccessor contextAccessor, MainDbContext _db)
        //    {
        //        db = _db;
        //        _httpContext = contextAccessor.HttpContext;
        //        if (_httpContext != null)
        //        {
        //            if (_httpContext.Request.Headers.TryGetValue("tenant", out var tenantId))
        //            {
        //                SetTenant(Convert.ToInt32(tenantId));
        //                return;
        //            }
        //            else if (_httpContext.Request.HttpContext.User.Identity.Name!=null)
        //            {
        //                var user = db.Users.Where(x => x.UserName == _httpContext.Request.HttpContext.User.Identity.Name);
        //                if (user != null)
        //                {
        //                    SetTenant((int)user.FirstOrDefault().ProjectId);
        //                    return;
        //                }

        //            }
        //            else if (_httpContext.Request.Cookies["tenant"]!=null)
        //            {
        //                if (_httpContext.Request.Cookies["user"] != null)
        //                {
        //                    SetUser( _httpContext.Request.Cookies["user"] );

        //                }
        //                if (_httpContext.Request.Cookies["project"] != null)
        //                {
        //                    SetProject(Convert.ToInt32(_httpContext.Request.Cookies["project"]));

        //                }
        //                SetTenant(Convert.ToInt32(_httpContext.Request.Cookies["tenant"]));
        //                    return;
        //            }
        //            else
        //            {
        //                SetTenant(1269);
        //                return;
        //                throw new Exception("Invalid Tenant!");
        //            }
        //        }
        //    }
        private void SetTenant(int  tenantId)
        {
            _currentTenant = tenantId;
            //if (_currentTenant == 0) throw new Exception("Invalid Tenant!");
        }
        private void SetProject(int? projectId)
        {
            _currentProject = projectId; 
        }
        private void SetUser(string userId)
        {
            _currentUser = userId;
            SetRolesAsync(userId);
            if (_currentUser == null) throw new Exception("Invalid User!");
        }
        private   void SetRolesAsync(string userId)
        {
            _currentRoles = new List<string>();
            var currentRoles = db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            _currentRoles = db.Roles.Where(x => currentRoles.Contains(x.Id)).Select(x => x.Name).ToList();
            _currentRoles.Add("added");
            if (_currentRoles == null) throw new Exception("No Roles!");
        }

        public int GetTenant()
        {
            return _currentTenant;
        }
        public string GetUser()
        {
            return _currentUser;
        }
        public int? GetProject()
        {
            return _currentProject;
        }
        public List<string> GetRoles()
        {
            return _currentRoles;
        }
    }
}