using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Data
{
    public class MyInfo
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string TenantId { get; set; }
        public string TenantName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
    [Table("Test", Schema = "DEV")]
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
    [Table("Upload", Schema = "DEV")]
    public class Upload
    {
        public int Id { get; set; }
        public string Filename { get; set; } = "";
    }
    [Table("Devices", Schema = "DEV")]
    public class Devices
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public string Name { get; set; }
        public string PushEndpoint { get; set; }
        public string PushP256DH { get; set; }
        public string PushAuth { get; set; }
    }
    public interface IModelItem
    {
        public string Global { get; set; }
    }
    [Table("ModelItem", Schema = "DEV")]
    public class ModelItem
    {
        public int Id { get; set; }

        public string Description { get; set; } = "";
    }

    public class ApplicationUser : IdentityUser
    {
        public int? CurrentProject { get; set; }
        [ForeignKey("CurrentProject")]
        public virtual Project? SelectedProject { get; set; }
        public int? CurrentTenant { get; set; }
        [ForeignKey("CurrentTenant")]
        public virtual Tenant? SelectedTenant { get; set; }
        public string? Avatar { get; set; } = "";
        public string? Tenants { get; set; }
        public string? Projects { get; set; }
        public string? Roles { get; set; }
        [NotMapped]
        public virtual List<Tenant>? TenantList { get; set; }
        [NotMapped]
        public virtual List<Project>? ProjectList { get; set; }
        [NotMapped]
        public virtual List<ApplicationRole>? RoleList { get; set; }

        public virtual ICollection<UserSettings>? UserSettings { get; set; }

    }
    public class ApplicationRole : IdentityRole
    {
        public int? TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant? Tenant { get; set; }
    }
    [Table("Project", Schema = "DEV")]
    public class Project
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string? Address { get; set; } = "";

        public int? TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant? Tenant { get; set; }
    }
    [Table("Module", Schema = "DEV")]
    public class Module
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string? Url { get; set; } = "";

        public int? TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant? Tenant { get; set; }
    }
    public class ProjectItem : ModelItem
    {

        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }
    }
    [Table("Drawing", Schema = "DEV")]

    public class Drawing : Master
    {
        public bool? Bool1 { get; set; }
        public bool Bool2 { get; set; }
        public virtual ICollection<Sheet>? Sheets { get; set; }

    }
    [Table("Sheet", Schema = "DEV")]
    public class Sheet : Master
    {

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Drawing? Drawing { get; set; }
        public virtual ICollection<Weld>? Welds { get; set; }
    }
    [Table("Weld", Schema = "DEV")]
    public class Weld : Master
    {

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Sheet? Sheet { get; set; }
    }
    [Table("WeldInfo", Schema = "DEV")]
    public class WeldInfo : Weld
    {

        public int? Qty { get; set; }
    }
    [Table("Tenant", Schema = "DEV")]
    public class Tenant
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string? Logo { get; set; } = "";
        public string? Address { get; set; } = "";
        public string? StyleSheet { get; set; } = "";
    }
    [Table("Menu", Schema = "DEV")]
    public class Menu
    {
        public int Id { get; set; }
        public string? App { get; set; } = "";
        public string? Type { get; set; } = "";
        public string? Description { get; set; } = "";
        public string? Area { get; set; } = "";
        public string? Controller { get; set; } = "";
        public string? Action { get; set; } = "";
        public string? NavArea { get; set; } = "";
        public string? NavController { get; set; } = "";
        public string? NavAction { get; set; } = "";
        public string? NavRoute { get; set; } = "";
        public string? Javascript { get; set; } = "";
        public string? AjaxUpdate { get; set; } = "";
        public string? Icon { get; set; } = "";
        public string? Roles { get; set; } = "";
        public string? Class { get; set; } = "";
        public string? Style { get; set; } = "";
        public int? Sort { get; set; }

        public virtual List<string>? ListRoles { get { return Roles == null ? null : Roles.Split(",").ToList(); } }
    }
    [Table("Scan", Schema = "DEV")]
    public class Scan
    {
        public int Id { get; set; }
        public string? Type { get; set; } = "";
        public double? Lat { get; set; } = 0;
        public double? Lng { get; set; } = 0;
        public int? TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant? Tenant { get; set; }
    }
    public interface Descriptor
    {

    }
    public class ProjectEditor : Master, Descriptor
    {
        public int? MyProjectId { get { return this.ProjectId; } set { this.ProjectId = value; } }
        public int? MyTenantId { get { return this.TenantId; } set { this.TenantId = value; } }
        public string? MyDescription { get { return this.Description; } set { this.Description = value; } }

        public string Global { get; set; }
    }


    [Table("Master", Schema = "DEV")]
    public class Master : Item
    {
        public Master()
        {
        }
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }
        public int? TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant? Tenant { get; set; }
        [NotMapped]
        public string SP { get { return this.Description + "/"; } set { SP = value; } }
        public string Global { get; set; } = "";

    }
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string? CU { get; set; }
        [ForeignKey("CU")]
        public virtual ApplicationUser? CreatedBy { get; set; }
        public string? MU { get; set; }
        [ForeignKey("MU")]
        public virtual ApplicationUser? ModifiedBy { get; set; }
        public DateTime? CD { get; set; }
        public DateTime? MD { get; set; }

    }
    [Table("MainItem", Schema = "DEV")]
    public class MainItem
    {
        public int Id { get; set; }
        public string myDescription { get; set; }
        public string Type { get; set; }
        [ForeignKey("Type")]
        public virtual Type MainType { get; set; }

        public bool isDeleted { get; set; } = false;
    }
    [Table("Test1MainItem", Schema = "DEVTest1")]
    public class Test1MainItem : MainItem
    {
        public Test1MainItem()
        {
            Type = "One";
        }
        public string Description { get { return this.myDescription; } set { this.myDescription = value; } }
        public bool tobeDeleted { get { return this.isDeleted; } set { this.isDeleted = value; } }

    }
    [Table("Test2MainItem", Schema = "DEVTest2")]
    public class Test2MainItem : MainItem
    {
        public Test2MainItem()
        {
            Type = "Two";
        }
        public string Description { get { return this.myDescription; } set { this.myDescription = value; } }

        public bool tobeDeleted { get { return this.isDeleted; } set { this.isDeleted = value; } }


    }
    [Table("Type", Schema = "DEV")]
    public class Type
    {

        public string Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public virtual ICollection<MainItem>? MainItems { get; set; }
    }
    [Table("UserSettings", Schema = "DEV")]
    public class UserSettings
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string? CU { get; set; }
        [ForeignKey("CU")]
        public virtual ApplicationUser? CreatedBy { get; set; }

    }
    [Table("Drawings", Schema = "Document")]
    public class Drawings : Masters
    {
        public string? MyDescription { get; set; }
        public virtual ICollection<SheetProperties>? SheetProperties { get; set; }

    }
    [Table("SheetProperties", Schema = "Document")]
    public class SheetProperties : Sheets
    {
        public int? ClassId { get; set; }
        [ForeignKey("ClassId")]
        public virtual Class? Class { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Drawings? Drawings { get; set; }
        public virtual ICollection<Welds>? Welds { get; set; }
        public int? Revision { get; set; }

    }
    [Table("Sheets", Schema = "Document")]
    public class Sheets : Masters
    {
        public string? MyDescription { get; set; }

    }
    [Table("Welds", Schema = "Welding")]
    public class Welds : Masters
    {
        public string? MyDescription { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual SheetProperties? SheetProperties { get; set; }
        public int? WPSId { get; set; }
        [ForeignKey("WPSId")]
        public virtual WPSs? WPSs { get; set; }
        public virtual ICollection<WeldDetails>? WeldDetails { get; set; }

    }
    [Table("WPSs", Schema = "Welding")]
    public class WPSs : Masters
    {
        public string? MyDescription { get; set; }

    }
    [Table("WeldDetails", Schema = "Welding")]
    public class WeldDetails : Masters
    {
        public string? MyDescription { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Welds? Welds { get; set; }
        public int? FitterId { get; set; }
        [ForeignKey("FitterId")]
        public virtual Fitters? Fitters { get; set; }
        public int? WelderId { get; set; }
        [ForeignKey("WelderId")]
        public virtual Welders? Welders { get; set; }
        public int? Consumable1Id { get; set; }
        [ForeignKey("Consumable1Id")]
        public virtual Consumables? Consumable1 { get; set; }
        public int? WeldStatusId { get; set; }
        [ForeignKey("WeldStatusId")]
        public virtual WeldStatuses? WeldStatuses { get; set; }
        public virtual ICollection<WeldDetailNDEs>? WeldDetailNDEs { get; set; }

    }
    [Table("WeldDetailNDEs", Schema = "Welding")]
    public class WeldDetailNDEs : Masters
    {
        public string? MyDescription { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual WeldDetails? WeldDetails { get; set; }
        public int? RequestId { get; set; }
        [ForeignKey("RequestId")]
        public virtual NDERequests? NDERequests { get; set; }
        public int? ReportId { get; set; }
        [ForeignKey("ReportId")]
        public virtual NDEReports? NDEReports { get; set; }
        public int? ViewStateId { get; set; }
        [ForeignKey("ViewStateId")]
        public virtual States? State { get; set; }
        public int? NDEStageId { get; set; }
        [ForeignKey("NDEStageId")]
        public virtual NDEStages? NDEStages { get; set; }

    }
    [Table("NDERequests", Schema = "Welding")]
    public class NDERequests : Masters
    {
        public string? MyDescription { get; set; }
        public int? NDEId { get; set; }
        [ForeignKey("NDEId")]
        public virtual NDEs? NDE { get; set; }

    }
    [Table("NDEReports", Schema = "Welding")]
    public class NDEReports : Masters
    {
        public string? MyDescription { get; set; }
        public int? NDEId { get; set; }
        [ForeignKey("NDEId")]
        public virtual NDEs? NDE { get; set; }

    }
    [Table("Welders", Schema = "Welding")]
    public class Welders : Masters
    {
        public string? MyDescription { get; set; }


    }
    [Table("Fitters", Schema = "Welding")]
    public class Fitters : Masters
    {
        public string? MyDescription { get; set; }


    }
    [Table("Consumables", Schema = "Welding")]
    public class Consumables : Masters
    {
        public string? MyDescription { get; set; }


    }
    [Table("NDEs", Schema = "Welding")]
    public class NDEs
    {
        public int Id { get; set; }
        public string? Description { get; set; }

    }
    [Table("NDEStages", Schema = "Welding")]
    public class NDEStages
    {
        public int Id { get; set; }
        public string? Description { get; set; }

    }
    [Table("WeldStatuses", Schema = "Welding")]
    public class WeldStatuses
    {
        public int Id { get; set; }
        public string? Description { get; set; }

    }
    [Table("States", Schema = "Core")]
    public class States
    {
        public int Id { get; set; }
        public string? Description { get; set; }

    }
    [Table("Classes", Schema = "Welding")]
    public class Class : Masters
    {
        public string? MyDescription { get; set; }

    }
    [Table("Masters", Schema = "Core")]
    public class Masters
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ProjectId { get; set; }
    }
}
