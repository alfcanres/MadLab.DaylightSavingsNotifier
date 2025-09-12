namespace DSTN.AdminApp.WinForms.Interfaces
{
    public interface IGenericPresenter
    {
        Task DeleteFromEditorAsync();
        Task DeleteFromListAsync();
        Task CreateNewAsync();
        Task EditSelectedAsync();
        Task LoadListAsync();
        Task SaveAsync();
    }
}