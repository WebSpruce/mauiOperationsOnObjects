using mauiOperationsOnObjects.ViewModels;

namespace mauiOperationsOnObjects.Pages;

public partial class HomePage : ContentPage
{
	public StackLayout table;
	public Button BtnCreateTable;

	public static HomePage instance;
	public HomePage()
	{
		InitializeComponent();
        table = tableLayout;
		BtnCreateTable = btnCreateTable;
		instance = this;
        BindingContext = new MainViewModel();
    }
}