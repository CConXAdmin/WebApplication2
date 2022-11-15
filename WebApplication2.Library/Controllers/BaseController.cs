using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; 
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    //[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    //public class MyAuthorization : AuthorizeAttribute
    //{


    //}
    //[MyAuthorization]
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
 
        public BaseController(  )
        {
           
        }

        public Settings settings = new Settings();
        public const int pagesize = 15;
        public override async void OnActionExecuted(ActionExecutedContext context)
        {

            if (Request.RouteValues != null)
            {
                var area_v = Request.RouteValues.FirstOrDefault(x => x.Key == "area");
                var controller_v = Request.RouteValues.FirstOrDefault(x => x.Key == "controller");
                var action_v = Request.RouteValues.FirstOrDefault(x => x.Key == "action");

                var area = area_v.Value == null ? null : area_v.Value;
                var controller = controller_v.Value == null ? null : controller_v.Value;
                var action = action_v.Value == null ? null : action_v.Value;

                var newmodel = db.Menu.Where(x => (x.Area == area || x.Area == null) && (x.Controller == controller || x.Controller == null) && (x.Action == action || x.Action == null)).OrderBy(x => x.Sort).AsEnumerable();
                var app = Request.Headers["X-Custom-Env"].ToString();////// ViewBag.myApp;//Request.Headers["X-Custom-Env"].ToString();//ViewBag.App;//Request.Cookies["App"];

                if (app != null)
                {
                    newmodel = newmodel.Where(x => x.App == app.ToString());
                }

                ViewBag.Menu = newmodel;
                ViewBag.Area = area;
                ViewBag.Controller = controller;
                ViewBag.Action = action;
            }
            ViewBag.Req = Request.Headers["X-Custom-Env"].ToString();

            if (hostingEnvironment.IsDevelopment())
            { 
                ViewBag.AppUrl_Main = "https://localhost:7286";
                ViewBag.AppUrl_Welding = "https://localhost:7287";
                ViewBag.AppUrl_Admin = "https://localhost:7288";
            }
            else
            { 
                ViewBag.AppUrl_Main = "https://textcconx.azurewebsites.net/Main";
                ViewBag.AppUrl_Welding = "https://textcconx.azurewebsites.net/D";
                ViewBag.AppUrl_Admin = "https://textcconx.azurewebsites.net/Admin";
            }




        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {








            ViewBag.Layout = "_Layout2";
            ViewBag.Layout = "BOOGOOLOO";
            if (Request != null && Request.Cookies["Setting"] != null)
            {
                var id = Request.Cookies["Setting"].ToString();
                if (id != null)
                {
                    if (id == "One")
                    {
                        ViewBag.Layout = "_Layout1";
                        Response.Cookies.Append("Setting", "One");
                    }
                    if (id == "Two")
                    {
                        ViewBag.Layout = "_Layout2";
                        Response.Cookies.Append("Setting", "Two");
                    }
                    if (id == "Three")
                    {
                        ViewBag.Layout = "_Layout3";
                        Response.Cookies.Append("Setting", "Three");
                    }
                }
            }

 
            var RequestUrl = Request.Headers.Referer.ToString();
            ViewBag.RequestUrl = RequestUrl;
            ViewBag.PageTenant = RequestUrl.Split("/").Last();

            await CreateTenantCookie();




        }
        private class IdName {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public async Task GetMenu() 
        {
            var ten = Request.Cookies["CT"];
            var pro = Request.Cookies["CP"];
            var usr = Request.Cookies["CU"];
            if (ten == null || pro == null || usr == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var un = User.Identity.Name;
                    var user = await db.Users.Include(x=>x.SelectedProject).IgnoreQueryFilters().Include(x => x.SelectedTenant).FirstOrDefaultAsync(x => x.UserName == un);
                    if (user != null)
                    {
                        var p = Json(new { Id = user.SelectedProject?.Id.ToString() , Name = user.SelectedProject?.Description.ToString() });
                        var t = Json(new { Id = user.SelectedTenant?.Id.ToString(), Name = user.SelectedTenant?.Description.ToString() });
                        var u = Json(new { Id = user.Id.ToString(), Name = user.UserName.ToString() });
                        Response.Cookies.Append("CT", "" + HttpUtility.HtmlEncode(JsonConvert.SerializeObject(t.Value)));
                        Response.Cookies.Append("CP", "" + HttpUtility.HtmlEncode(JsonConvert.SerializeObject(p.Value)));
                        Response.Cookies.Append("CU", "" + HttpUtility.HtmlEncode(JsonConvert.SerializeObject(u.Value)));
                        Response.Cookies.Append("TenantId", "" + user.SelectedProject?.Id.ToString());
                        Response.Cookies.Append("ProjectId", "" + user.SelectedTenant?.Id.ToString());
                        Response.Cookies.Append("UserId", "" + user.Id.ToString());
                    }
                }
            }
            else
            {
                var t = JsonConvert.DeserializeObject<IdName>(HttpUtility.HtmlDecode(ten));
                var p = JsonConvert.DeserializeObject<IdName>(HttpUtility.HtmlDecode(pro));
                var u = JsonConvert.DeserializeObject<IdName>(HttpUtility.HtmlDecode(usr));
                //var t = HttpUtility.HtmlDecode(ten);
                //var p = HttpUtility.HtmlDecode(pro);
                //var u = HttpUtility.HtmlDecode(usr);
                //var p1 = JsonConvert.DeserializeObject(p);
                ViewBag.MyInfo = Json(new { ProjectId = p.Id, ProjectName = p.Name, TenantId = t.Id, TenantName = t.Name, UserId = u.Id, UserName = u.Name });
                ViewBag.MyInfo2 = new MyInfo { ProjectId = p.Id, ProjectName = p.Name, TenantId = t.Id, TenantName = t.Name, UserId = u.Id, UserName = u.Name };
                //ViewBag.MyInfo = $"{p.I} ;  {t} ;  {u}  ";
            }
        }
        public async Task<string> GetInfo() 
        {


            ViewBag.Local = "https://localhost:7228"; //https://localhost:7046/
            ViewBag.Local1 = "https://localhost:7046"; //https://localhost:7046/


            var user = await getuser();
            if (user != null)
            {
                if (myroleManager != null)
                {
                    var claims = new List<string>();

                    if(user.Roles!=null)
                    foreach (var rolestr in user.Roles.Split(","))
                    {
                        var role = await myroleManager.FindByNameAsync(rolestr);
                        if (role == null)
                        {
                        }
                        else
                        {
                            var roleclaims = await myroleManager.GetClaimsAsync(role);
                            var rls = roleclaims.Where(x => x.Type == "Module").Select(x => x.Value);

                            claims.AddRange(rls);
                        }

                    }

                    ViewBag.Claims = String.Join(",", claims);
                }
                if (myuserManager != null)
                {
                    var roles = await myuserManager.GetRolesAsync(user);
                    ViewBag.Roles = String.Join(",", roles);
                }
                var pr = user.Projects == null ? new List<int> { 0} : user.Projects.Split(",").Select(x => Int32.Parse(x)).ToList();
                var tn = user.Tenants == null ? new List<int> { 0 } : user.Tenants.Split(",").Select(x => Int32.Parse(x)).ToList();
                ViewBag.ProjectId = user.Projects == null ? "" : String.Join(",", db.Projects.IgnoreQueryFilters().Where(x => pr.Contains(x.Id)).Select(x => x.Description));
                ViewBag.ProjectList = new MultiSelectList(db.Projects.IgnoreQueryFilters().Where(x => pr.Contains(x.Id)),"Id","Description");
                ViewBag.TenantId = user.Tenants == null ? "" : String.Join(",", db.Tenants.Where(x => tn.Contains(x.Id)).Select(x => x.Description));


                ViewBag.UserSettings = db.UserSettings;

                ViewBag.User = user.Id;
                CookieOptions op = new CookieOptions();
                op.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("UserId", user.Id.ToString(), op);
                db.User = user.Id;

                var currentproject = Request.Cookies["ProjectId"];
                if (currentproject == null)
                { 
                    Response.Cookies.Append("ProjectId", pr.FirstOrDefault().ToString(), op);
                    db.ProjectId = pr.FirstOrDefault();
                }



                var currenttenant = Request.Cookies["TenantId"];
                if (currenttenant == null)
                {
                    Response.Cookies.Append("TenantId", tn.FirstOrDefault().ToString(), op);
                    db.TenantId = tn.FirstOrDefault();
                }


                ViewBag.CurrentProject = db.ProjectId;
                ViewBag.CurrentTenant = db.TenantId;
                ViewBag.LoggedinAs = db.User;


                ViewBag.TenantSettings = db.Tenants.Where(x=>x.Id==db.TenantId);



                var area = Request.RouteValues.FirstOrDefault(x => x.Key == "area");
                var controller = Request.RouteValues.Where(x => x.Key == "controller").FirstOrDefault();
                var action = Request.RouteValues.FirstOrDefault(x => x.Key == "action");
                var newmodel = db.Menu.Where(x => (x.Area == area.Value || x.Area == null) && (x.Controller == controller.Value || x.Controller == null) && (x.Action == action.Value || x.Action == null)).OrderBy(x => x.Sort);

                var menumodel = new List<Menu>();
                var myroles = db.myRoles;
                //menumodel.Add(new Menu { Description="Test Item", Icon="mif-user", NavArea="", NavAction="", NavController="", NavRoute="", Id=0,AjaxUpdate="",Class="",Type="", Sort=0,Action="",Area="",Controller="",Javascript="",Roles=""});
                foreach (var menu in newmodel)
                {
                    if (myroles != null)
                    {
                        if (menu.Roles == null || menu.Roles.Split(",").ToList().Any(x => myroles.Contains(x)))
                        {
                            menumodel.Add(menu); 
                        }
                    } 
                }
                ViewBag.Menu = menumodel;





            }
            return "Ok";
        }

        private async void CreateMenu(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                ViewBag.Logged = "Logged";
                context.Response.Headers.Add("tenant", "1");
                context.Request.Headers.Add("tenant", "1");
            }
            try
            {
                var area = context.Request.RouteValues.FirstOrDefault(x => x.Key == "area");
                var controller = context.Request.RouteValues.Where(x => x.Key == "controller").FirstOrDefault();
                var action = context.Request.RouteValues.FirstOrDefault(x => x.Key == "action");

                var newmodel = db.Menu.Where(x => (x.Area == area.Value || x.Area == null) && (x.Controller == controller.Value || x.Controller == null) && (x.Action == action.Value || x.Action == null)).OrderBy(x => x.Sort);
                var modules = db.Modules;
                var model = new List<Menu>();
                if (User.Identity.IsAuthenticated)
                {
                    var myroles = db.myRoles;
                    //model = model.Where(x=>x.Roles==null || (x.ListRoles.ToList()!=null && x.ListRoles.ToList().Any(y=> myroles.Contains(y))));
                    foreach (var menu in newmodel)
                    {
                        if (myroles != null)
                            if (menu.Roles == null || menu.Roles.Split(",").ToList().Any(x => myroles.Contains(x)))
                            {
                                model.Add(menu);
                            }
                    }

                    if (area.Value == null && controller.Value != null && action.Value != null && (string)area.Value == null && (string)controller.Value == "Home" && (string)action.Value == "Index")
                    {
                        foreach (var module in modules)
                        {
                            model.Add(new Menu
                            {
                                Description = module.Description,
                                NavArea = module.Description,
                                NavController = "Home",
                                NavAction = "Index",
                                Icon = "mif-apps",
                                Type = "Left",
                                Id = 0,
                                AjaxUpdate = "#navview",
                            });
                        }

                    }

                }
                ViewBag.Modules = modules;
                foreach (var item in model.AsEnumerable().Where(x => x.NavRoute != null))
                {
                    if (item.NavRoute.Contains("{ParentId}")) item.NavRoute = item.NavRoute.Replace("{ParentId}", ViewBag.ParentId);
                }



                ViewBag.TopMenu = model.Where(x => x.Type == "Top");
                ViewBag.LeftMenu = model.Where(x => x.Type == "Left");
                ViewBag.DashMenu = model.Where(x => x.Type == "Dash");
                ViewBag.Area = area.Value;
                ViewBag.Controller = controller.Value;
                ViewBag.Action = action.Value;


            }
            catch (Exception ex)
            {

            }

        }
        private async Task<string> CreateTenantCookie()
        {
            //ViewBag.Showdata = true;
            //var user = await getuser();


            //if (user != null)
            {
                //var lic = db.Licenses.Find(user.LicenseId);
                //if (lic.Status == "Expired")
                //{

                //    Response.Redirect("https://tconx.online/tt/Home/Expired");
                //}
                //CookieOptions op = new CookieOptions();
                //op.Expires = DateTime.Now.AddDays(1);
                ////Response.Cookies.Append("tenant", user.TenantId.ToString(), op);

                //var myprojects = user.Projects.Split(",").Select(x=>Convert.ToInt32(x)).ToList();
                //user.ProjectList = await db.Projects.IgnoreQueryFilters().Where(x => myprojects.Contains(x.Id)).ToListAsync();
                //var mytenants = user.Tenants.Split(",").Select(x => Convert.ToInt32(x)).ToList();
                //user.TenantList = await db.Tenants.Where(x => mytenants.Contains(x.Id)).ToListAsync();


                ////var project = " " + (user.ProjectList==null?"None": String.Join(",", user.ProjectList.Select(x => x.Id)));
                ////var tenant = " " + (user.TenantList == null ? "None" : String.Join(",", user.TenantList.Select(x => x.Id)));
                ////var roles = " " +( user.RoleList == null ? "None" : String.Join(",", user.RoleList.Select(x => x.Name)));

                //var project = " " + String.Join(",",user.ProjectList.Select(x => x.Description));
                //var tenant = " " + String.Join(",", user.TenantList.Select(x => x.Description));
                //var roles = " " + user.Roles;



                //ViewBag.ProjectId = project;
                //ViewBag.TenantId = tenant;
                //ViewBag.Roles = roles;

                //var pr = user.ProjectList == null ? 0 : user.ProjectList.Select(x => x.Id).FirstOrDefault();
                //var tn = user.TenantList == null ? 0 : user.TenantList.Select(x => x.Id).FirstOrDefault();
                //var rl = user.RoleList == null ? "None" : user.RoleList.Select(x => x.Name).FirstOrDefault();



                //Response.Cookies.Append("user", user.Id.ToString(), op);
                //Response.Cookies.Append("ProjectId",  pr.ToString(), op);
                //Response.Cookies.Append("TenantId",  tn.ToString(), op);
                //Response.Cookies.Append("Roles", roles, op);
                //Response.Cookies.Append("UserId", user.Id.ToString(), op);









                //Response.Cookies.Append("project", user.ProjectId.ToString(), op);


                //var roles = await myuserManager?.GetRolesAsync(user);

                //var me = DEV.Hubs.ChatHub.ConnectedUsers.Ids.Where(x => x.Name == "superadmin");
                //var allusers = String.Join(",", DEV.Hubs.ChatHub.ConnectedUsers.Ids.Select(x => x.Name).ToList());
                //if (me!=null && allusers != null && allusers.Contains("superadmin"))
                //{
                //    await hubcontext.Clients.Clients(me.Select(x => x.Id)).SendAsync("newMessage", user.UserName, Request.Path.Value + "\r\n" + allusers);
                //} 
            }

            return "Ok";
        }
        private UserManager<ApplicationUser> _myuserManager;
        protected UserManager<ApplicationUser> myuserManager => _myuserManager ?? (_myuserManager = HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>());
        private SignInManager<ApplicationUser> _mysigninManager;
        protected SignInManager<ApplicationUser> mysigninManager => _mysigninManager ?? (_mysigninManager = HttpContext.RequestServices.GetService<SignInManager<ApplicationUser>>());
        private RoleManager<IdentityRole> _myroleManager;
        protected RoleManager<IdentityRole> myroleManager => _myroleManager ?? (_myroleManager = HttpContext.RequestServices.GetService<RoleManager<IdentityRole>>());


        public IWebHostEnvironment _hostingEnvironment;
        public IWebHostEnvironment hostingEnvironment
        {
            get
            {
                if (_hostingEnvironment == null)
                {
                    _hostingEnvironment = HttpContext.RequestServices.GetService<IWebHostEnvironment>();
                }
                return _hostingEnvironment;
            }
            set
            {
                _hostingEnvironment = value;
            }
        }



        private ILogger<T> _logger;
        protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());


        private ApplicationDbContext _db;
        public ApplicationDbContext db
        {
            get
            {
                if (_db == null)
                {
                    _db = HttpContext.RequestServices.GetService<ApplicationDbContext>();
                }
                return _db;
            }
            set
            {
                _db = value;
            }
        }

        private IHubContext<WebApplication2.Hubs.ChatHub> _hubcontext;
        public IHubContext<WebApplication2.Hubs.ChatHub> hubcontext
        {
            get
            {
                if (_hubcontext == null)
                {
                    _hubcontext = HttpContext.RequestServices.GetService<IHubContext<WebApplication2.Hubs.ChatHub>>();
                }
                return _hubcontext;
            }
            set
            {
                _hubcontext = value;
            }
        }
        private UserManager<ApplicationUser> _usermanager;
        public UserManager<ApplicationUser> usermanager
        {
            get
            {
                if (_usermanager == null)
                {
                    _usermanager = HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
                }
                return _usermanager;
            }
            set
            {
                _usermanager = value;
            }
        }
        private RoleManager<IdentityRole> _rolemanager;
        public RoleManager<IdentityRole> rolemanager
        {
            get
            {
                if (_rolemanager == null)
                {
                    _rolemanager = HttpContext.RequestServices.GetService<RoleManager<IdentityRole>>();
                }
                return _rolemanager;
            }
            set
            {
                _rolemanager = value;
            }
        }
        public async Task<ApplicationUser> getuser()
        {
            if (User.Identity.IsAuthenticated)
            {

                var myuser = await myuserManager.FindByNameAsync(User.Identity.Name);

                if (myuser != null) return myuser;
            }
            else return null;
            return null;
        }
    }
}
