using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public int TenantId { get { return 1; } set { value = 1; } }
        public int? ProjectId { get { return 37; } set { value = 37; } }
        public string User { get; set; }
        public List<string> myRoles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Test> Test { get; set; } = null!;
        public DbSet<Upload> Upload { get; set; } = null!;
        public DbSet<WebApplication2.Data.Devices> Devices { get; set; }
        public DbSet<WebApplication2.Data.Menu> Menu { get; set; }
        public DbSet<WebApplication2.Data.Project> Projects { get; set; }
        public DbSet<WebApplication2.Data.Tenant> Tenants { get; set; }
        public DbSet<WebApplication2.Data.Module> Modules { get; set; }
        public DbSet<WebApplication2.Data.Scan> Scans { get; set; }
        public DbSet<WebApplication2.Data.ModelItem> ModelItems { get; set; }
        public DbSet<WebApplication2.Data.Drawing> Drawings { get; set; }
        public DbSet<WebApplication2.Data.Sheet> Sheets { get; set; }
        public DbSet<WebApplication2.Data.Weld> Welds { get; set; }
        public DbSet<WebApplication2.Data.WeldInfo> WeldInfo { get; set; }
        public DbSet<WebApplication2.Data.Master> Masters { get; set; }
        public DbSet<WebApplication2.Data.MainItem> MainItems { get; set; }
        public DbSet<WebApplication2.Data.Test1MainItem> Test1MainItems { get; set; }
        public DbSet<WebApplication2.Data.Test2MainItem> Test2MainItems { get; set; }
        public DbSet<WebApplication2.Data.Type> Types { get; set; }
        public DbSet<WebApplication2.Data.UserSettings> UserSettings { get; set; }
        public DbSet<WebApplication2.Data.Masters> WMasters { get; set; }
        public DbSet<WebApplication2.Data.Drawings> WDrawings { get; set; }
        public DbSet<WebApplication2.Data.Sheets> WSheets { get; set; }
        public DbSet<WebApplication2.Data.SheetProperties> WSheetProperties { get; set; }
        public DbSet<WebApplication2.Data.Welds> WWelds { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Project>().HasQueryFilter(p => p.TenantId == TenantId);

            builder.Entity<ModelItem>().HasQueryFilter(p => p.Id != ProjectId);

            builder.Entity<UserSettings>().HasQueryFilter(p => p.CU == User);

            builder.Entity<MainItem>().HasQueryFilter(p => p.isDeleted != true);

            builder.Entity<Masters>().HasQueryFilter(p => p.ProjectId == ProjectId);

            //builder.Entity<Drawing>().HasQueryFilter(p => p.ProjectId == ProjectId); 
            //builder.Entity<Sheet>().HasQueryFilter(p => p.ProjectId == ProjectId);
            //builder.Entity<Weld>().HasQueryFilter(p => p.ProjectId == ProjectId);
            //builder.Entity<Menu>().HasQueryFilter(p => p.Roles.Split(",").ToList().Any(x => myRoles.Contains(x))); 
            //builder.Entity<Menu>().HasQueryFilter(p => p.Auth.Count()==0 || p.Auth.Any(x => myRoles.Contains(x)));
            //builder.Entity<Menu>().HasQueryFilter(p => p.Roles.Split(",", StringSplitOptions.None).ToList().Any(x => myRoles.Contains(x)));
            //builder.Entity<Menu>().HasQueryFilter(p => myRoles.Any(x=>   (p.Roles==null?true:p.Roles.Split(",",StringSplitOptions.None).ToList().Contains(x))));
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<ProjectItem>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.ProjectId = ProjectId;
                        break;
                    case EntityState.Modified:
                        entry.Property(x => x.ProjectId).IsModified = false;
                        entry.Entity.ProjectId = ProjectId;
                        break;
                    case EntityState.Deleted:
                        entry.Property(x => x.ProjectId).IsModified = false;
                        entry.Entity.ProjectId = ProjectId;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<MainItem>().ToList())
            {
                switch (entry.State)
                {

                    case EntityState.Modified:

                        if (entry.Entity.isDeleted == true)
                        {
                            entry.Entity.myDescription = "Marked for deletion";
                        }
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        private bool ContainsRoles(string roles)
        {

            return true;
            return roles.Split(",").Any(x => myRoles.Contains(x));

        }

    }
}