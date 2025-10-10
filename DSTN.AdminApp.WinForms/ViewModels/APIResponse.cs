namespace DSTN.AdminApp.WinForms.ViewModels
{
    public record APIResponse<T>(T Data, APIValidationResponse ValidatorResponse);
}
