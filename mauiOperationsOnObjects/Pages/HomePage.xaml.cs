using mauiOperationsOnObjects.ViewModels;

namespace mauiOperationsOnObjects.Pages;

public partial class HomePage : ContentPage
{
	public StackLayout table;
	public Button BtnCreateTable;
	public Slider slider;


    public static HomePage instance;
	public HomePage()
	{
		InitializeComponent();
        table = tableLayout;
		BtnCreateTable = btnCreateTable;
		slider = sliderAmount;

        instance = this;
        BindingContext = new MainViewModel();
    }
}