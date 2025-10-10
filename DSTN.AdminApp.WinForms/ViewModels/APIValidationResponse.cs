namespace DSTN.AdminApp.WinForms.ViewModels
{
    public record APIValidationResponse(bool IsValid = true, List<string> MessageList = null);
}
