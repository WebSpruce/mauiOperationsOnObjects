using CommunityToolkit.Maui.Views;
using mauiOperationsOnObjects.Pages;
using mauiOperationsOnObjects.Popups;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace mauiOperationsOnObjects.ViewModels
{
    public class EditViewModel
    {
        private ObservableCollection<newTable> listOfObjectsEdit;
        public ObservableCollection<newTable> ListOfObjectsEdit
        {
            get { return listOfObjectsEdit; }
            set
            {
                if (listOfObjectsEdit != value)
                {
                    listOfObjectsEdit = value;
                    OnPropertyChanged("ListOfObjectsEdit");
                }
            }
        }
        private newTable selectedObject;
        public newTable SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (selectedObject != value)
                {
                    selectedObject = value;
                    OnPropertyChanged("SelectedObject");
                    EditPage.instance.ShowPopup(new SelectedItemPopup(SelectedObject));
                    SelectedObject = null;
                    EditPage.instance.collectionView.SelectedItem = null;
                }
            }
        }
        public static EditViewModel instance;
        public EditViewModel()
        {
            instance = this;
            ListOfObjectsEdit = MainViewModel.instance.ListOfObjects;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
