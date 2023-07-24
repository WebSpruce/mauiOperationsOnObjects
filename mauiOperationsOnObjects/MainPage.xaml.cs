using mauiOperationsOnObjects.ViewModels;

namespace mauiOperationsOnObjects;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}

}

