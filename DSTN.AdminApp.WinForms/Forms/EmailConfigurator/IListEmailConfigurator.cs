using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;

namespace DSTN.AdminApp.WinForms.Forms.EmailConfigurator;

public interface IListEmailConfigurator : IListForm
{
    IEnumerable<EmailConfiguration> EmailConfigurations { get; set; }
    IEditEmailConfigurator EditorForm { get; set; }
}
