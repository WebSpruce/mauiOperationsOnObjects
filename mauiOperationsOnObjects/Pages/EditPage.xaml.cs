using mauiOperationsOnObjects.ViewModels;

namespace mauiOperationsOnObjects.Pages;

public partial class EditPage : ContentPage
{
	public static EditPage instance;
	public EditPage()
	{
		InitializeComponent();
		instance = this;
        BindingContext = new EditViewModel();
    }
}