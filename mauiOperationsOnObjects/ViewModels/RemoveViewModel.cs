using mauiOperationsOnObjects.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mauiOperationsOnObjects.ViewModels
{
    public class RemoveViewModel
    {
        private ObservableCollection<newTable> listOfObjectsRemove;
        public ObservableCollection<newTable> ListOfObjectsRemove
        {
            get { return listOfObjectsRemove; }
            set
            {
                if (listOfObjectsRemove != value)
                {
                    listOfObjectsRemove = value;
                    OnPropertyChanged("ListOfObjectsRemove");
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
                }
            }
        }
        public ICommand ItemTappedCommand { get; private set; }

        public static RemoveViewModel instance;
        public RemoveViewModel()
        {
            instance = this;
            ListOfObjectsRemove = MainViewModel.instance.ListOfObjects;

            ItemTappedCommand = new Command(() => RemoveItemQuestion(SelectedObject));
        }
        private async void RemoveItemQuestion(newTable selectedObject)
        {
            if (selectedObject != null)
            {
                bool answer = await RemovePage.instance.DisplayAlert("Are you sure?", "Would you like to remove this item from the list?", "Yes", "No");
                if (answer)
                {
                    ListOfObjectsRemove.Remove(selectedObject);
                    MainViewModel.instance.ListOfObjects = ListOfObjectsRemove;
                }
                else
                {
                    RemovePage.instance.collectionView.SelectedItem = null;
                }
            }
            
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
