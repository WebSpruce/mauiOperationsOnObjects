using mauiOperationsOnObjects.ViewModels;

namespace mauiOperationsOnObjects.Pages;

public partial class EditPage : ContentPage
{
	public static EditPage instance;
	public CollectionView collectionView;
	public EditPage()
	{
		InitializeComponent();
		collectionView = cview;
		instance = this;
        BindingContext = new EditViewModel();
    }
}