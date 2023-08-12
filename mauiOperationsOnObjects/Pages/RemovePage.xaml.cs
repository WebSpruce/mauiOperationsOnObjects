using mauiOperationsOnObjects.ViewModels;

namespace mauiOperationsOnObjects.Pages;

public partial class RemovePage : ContentPage
{
    public static RemovePage instance;
    public CollectionView collectionView;
    public RemovePage()
	{
		InitializeComponent();
        collectionView = cview;
        instance = this;
        BindingContext = new RemoveViewModel();
    }
}