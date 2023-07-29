using mauiOperationsOnObjects.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mauiOperationsOnObjects.ViewModels
{
    public class AddViewModel
    {
        private ObservableCollection<newTable> listOfObjectsAdd;
        public ObservableCollection<newTable> ListOfObjectsAdd
        {
            get { return listOfObjectsAdd; }
            set
            {
                if (listOfObjectsAdd != value)
                {
                    listOfObjectsAdd = value;
                    OnPropertyChanged("ListOfObjectsAdd");
                }
            }
        }

        public ICommand AddPageLoadedCommand { get; private set; }
        public AddViewModel instance;
        public AddViewModel()
        {
            instance = this;
            ListOfObjectsAdd = MainViewModel.instance.ListOfObjects;
            AddObjectToCollectionView();
        }
        private ObservableCollection<newTable> tempList = new ObservableCollection<newTable>();
        public void AddObjectToCollectionView()
        {
            try
            {
                switch ((int)MainViewModel.instance.AmountOfColumns)
                {
                    case 1:
                        {
                            if (MainViewModel.instance.newTable.Variable1.GetType() == typeof(int) || MainViewModel.instance.newTable.Variable1.GetType() == typeof(double)) { createEntryIntAndDouble(1); }
                            else if (MainViewModel.instance.newTable.Variable1.GetType() == typeof(string)) { createEntryString(1); }
                            else if (MainViewModel.instance.newTable.Variable1.GetType() == typeof(bool)) { createPickerBool(1); }
                            else if (MainViewModel.instance.newTable.Variable1.GetType() == typeof(DateTime)) { createDatePicker(1); }
                            
                            Trace.WriteLine($"type: {MainViewModel.instance.newTable.Variable1.GetType()}");
                            break;
                        }
                    case 2:
                        {
                            Trace.WriteLine($"type: {MainViewModel.instance.newTable.Variable1.GetType()} and {MainViewModel.instance.newTable.Variable2.GetType()}");
                            break;
                        }
                    case 3:
                        {
                            Trace.WriteLine($"type: {MainViewModel.instance.newTable.Variable1.GetType()} and {MainViewModel.instance.newTable.Variable2.GetType()}");
                            break;
                        }
                    case 4:
                        {
                            Trace.WriteLine($"type: {MainViewModel.instance.newTable.Variable1.GetType()} and {MainViewModel.instance.newTable.Variable2.GetType()}");
                            break;
                        }
                    case 5:
                        {
                            Trace.WriteLine($"type: {MainViewModel.instance.newTable.Variable1.GetType()} and {MainViewModel.instance.newTable.Variable2.GetType()}");
                            break;
                        }
                } 
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"error: {ex}");
            }

        }

        private void createLabel()
        {
            Label lb = new Label();
            lb.Text = $"Add value to table.";
            lb.HorizontalTextAlignment = TextAlignment.Center;
            lb.FontSize = 12;
            AddPage.instance.entries.Children.Add(lb);
        }
        private void createEntryIntAndDouble(int amount)
        {
            for(int i=1; i<=amount; i++)
            {
                createLabel();
                Entry newEntry = new Entry();
                newEntry.ClassId = $"Entry{i}";
                newEntry.Keyboard = Keyboard.Numeric;
                newEntry.WidthRequest = 300;
                newEntry.FontSize = 12;
                newEntry.TextColor = Colors.White;
                newEntry.Text = "";
                newEntry.TextChanged += (sender, e) =>
                {
                    Trace.WriteLine($"entered {newEntry.ClassId}: {newEntry.Text}");
                };
                AddPage.instance.entries.Children.Add(newEntry);

                createButton(newEntry);
            }
        }
        private void createEntryString(int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                createLabel();
                Entry newEntry = new Entry();
                newEntry.ClassId = $"Entry{i}";
                newEntry.WidthRequest = 300;
                newEntry.FontSize = 12;
                newEntry.TextColor = Colors.White;
                newEntry.Text = "";
                newEntry.TextChanged += (sender, e) =>
                {
                    Trace.WriteLine($"entered {newEntry.ClassId}: {newEntry.Text}");
                };
                AddPage.instance.entries.Children.Add(newEntry);

                createButton(newEntry);
            }
        }
        private void createPickerBool(int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                createLabel();
                ObservableCollection<bool> boolOptions = new ObservableCollection<bool>() { true, false };
                Picker newPicker = new Picker();
                newPicker.ItemsSource = boolOptions;
                newPicker.ClassId = $"Picker{i}";
                newPicker.WidthRequest = 300;
                newPicker.FontSize = 12;
                AddPage.instance.entries.Children.Add(newPicker);

                createButton(newPicker);
            }
        }
        private void createDatePicker(int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                createLabel();
                DatePicker newPicker = new DatePicker();
                newPicker.MinimumDate = new DateTime(1900, 1, 1);
                newPicker.ClassId = $"DatePicker{i}";
                newPicker.WidthRequest = 300;
                newPicker.FontSize = 12;
                AddPage.instance.entries.Children.Add(newPicker);

                TimePicker newTimePicker = new TimePicker();
                newTimePicker.ClassId = $"TimePicker{i}";
                newTimePicker.WidthRequest = 300;
                newTimePicker.FontSize = 12;
                AddPage.instance.entries.Children.Add(newTimePicker);

                createButton(newPicker , newTimePicker);
            }
        }
        private void createButton(Entry newEntry)
        {
            Button btnAdd = new Button();
            btnAdd.Text = "Add value to table";
            btnAdd.HorizontalOptions = LayoutOptions.Center;
            btnAdd.VerticalOptions = LayoutOptions.Center;
            btnAdd.Padding = new Thickness(5);
            btnAdd.Clicked += (sender, e) =>
            {
                tempList.Add(new newTable { Variable1 = newEntry.Text });
                ListOfObjectsAdd = tempList;
                MainViewModel.instance.ListOfObjects = ListOfObjectsAdd;
                Trace.WriteLine($"value: {newEntry.Text} - {tempList[0].Variable1} list: {ListOfObjectsAdd[0].Variable1}");
                newEntry.Text = "";
            };
            AddPage.instance.entries.Children.Add(btnAdd);
        }
        private void createButton(Picker newPicker)
        {
            Button btnAdd = new Button();
            btnAdd.Text = "Add value to table";
            btnAdd.HorizontalOptions = LayoutOptions.Center;
            btnAdd.VerticalOptions = LayoutOptions.Center;
            btnAdd.Padding = new Thickness(5);
            btnAdd.Clicked += (sender, e) =>
            {
                tempList.Add(new newTable { Variable1 = newPicker.SelectedItem });
                ListOfObjectsAdd = tempList;
                MainViewModel.instance.ListOfObjects = ListOfObjectsAdd;
                Trace.WriteLine($"value: {newPicker.SelectedItem} - {tempList[0].Variable1} list: {ListOfObjectsAdd[0].Variable1}");
            };
            AddPage.instance.entries.Children.Add(btnAdd);
        }
        private void createButton(DatePicker newPicker, TimePicker newTimePicker)
        {
            Button btnAdd = new Button();
            btnAdd.Text = "Add value to table";
            btnAdd.HorizontalOptions = LayoutOptions.Center;
            btnAdd.VerticalOptions = LayoutOptions.Center;
            btnAdd.Padding = new Thickness(5);
            btnAdd.Clicked += (sender, e) =>
            {
                DateTime selectedFullDate = newPicker.Date + newTimePicker.Time;
                tempList.Add(new newTable { Variable1 = selectedFullDate });
                ListOfObjectsAdd = tempList;
                MainViewModel.instance.ListOfObjects = ListOfObjectsAdd;
                Trace.WriteLine($"value: {selectedFullDate} - {tempList[0].Variable1} list: {ListOfObjectsAdd[0].Variable1}");
            };
            AddPage.instance.entries.Children.Add(btnAdd);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
