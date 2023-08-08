using CommunityToolkit.Maui.Views;
using mauiOperationsOnObjects.ViewModels;
using System.Diagnostics;

namespace mauiOperationsOnObjects.Popups;

public partial class SelectedItemPopup : Popup
{
	public StackLayout editArea;

    public SelectedItemPopup(newTable selectedObject)
	{
		InitializeComponent();

		Trace.WriteLine($"selected: {selectedObject.Variable1} - {selectedObject.Variable2} - {selectedObject.Variable3} - {selectedObject.Variable4} - {selectedObject.Variable5}");
	}
}