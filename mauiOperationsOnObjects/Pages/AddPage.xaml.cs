using mauiOperationsOnObjects.ViewModels;

namespace mauiOperationsOnObjects.Pages;

public partial class AddPage : ContentPage
{
    public StackLayout entries;
    public static AddPage instance;
    public AddPage()
	{
		InitializeComponent();
        entries = entryLayout;
        instance = this;
        BindingContext = new AddViewModel();
    }
}