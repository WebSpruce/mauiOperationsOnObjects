using mauiOperationsOnObjects.Pages;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
            AddObjectToCollectionView((int)MainViewModel.instance.AmountOfColumns);
        }
        private ObservableCollection<newTable> listOfData = new ObservableCollection<newTable>();
        private Dictionary<int, string> tempData = new Dictionary<int, string>
        {
            { 1, ""}, { 2, ""}, { 3, ""}, { 4, ""}, { 5, ""},
        };
        public void AddObjectToCollectionView(int count)
        {
            try
            {
                for (int i=0; i<count; i++) 
                {
                    List<Type> variableType = new List<Type>(); int index = 0;
                    switch (count)
                    {
                        case 1:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                index = 1;
                                break;
                            }
                        case 2:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                index = 2;
                                break;
                            }
                        case 3:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable3.GetType());
                                index = 3;
                                break;
                            }
                        case 4:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable3.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable4.GetType());
                                index = 4;
                                break;
                            }
                        case 5:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable3.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable4.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable5.GetType());
                                index = 5;
                                break;
                            }
                    }
                    if (variableType[i] == typeof(double))
                    {
                        Entry newEntry = new Entry();
                        CreateLabel(typeof(double));
                        CreateEntryIntAndDouble<Entry>(ref index, newEntry, true);
                        Trace.WriteLine("yep0");
                        continue;
                    }
                    else if(variableType[i] == typeof(bool))
                    {
                        Picker newEntry = new Picker();
                        CreateLabel(typeof(bool));
                        CreateEntryIntAndDouble<Picker>(ref index, newEntry, true);
                        Trace.WriteLine("yep1");
                        continue;
                    }
                    else if(variableType[i] == typeof(string))
                    {
                        Entry newEntry = new Entry();
                        CreateLabel(typeof(string));
                        CreateEntryIntAndDouble<Entry>(ref index, newEntry, false);
                        Trace.WriteLine("yep2");
                        continue;
                    }
                    else if (variableType[i] == typeof(DateTime))
                    {
                        DatePicker newEntry = new DatePicker();
                        CreateLabel(typeof(DatePicker));
                        CreateEntryIntAndDouble<DatePicker>(ref index, newEntry, false);
                        Trace.WriteLine("yep3");
                        continue;
                    }
                }
                CreateButton(tempData);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"error: {ex}");
            }

        }
        
        private void CreateLabel(Type typeOfEntry)
        {
            Label lb = new Label();
            lb.HorizontalTextAlignment = TextAlignment.Center;
            lb.FontSize = 12;
            if (typeOfEntry == typeof(double))
            {
                lb.Text = $"Add value to table (numbers).";
            }
            else if(typeOfEntry == typeof(bool))
            {
                lb.Text = $"Set value to table (bool).";
            }
            else if (typeOfEntry == typeof(string))
            {
                lb.Text = $"Set value to table (string).";
            }
            else if (typeOfEntry == typeof(DatePicker))
            {
                lb.Text = $"Set value to table (DatePicker).";
            }
            AddPage.instance.entries.Children.Add(lb);
        }
        private List<Entry> newEntries = new List<Entry>();
        private void CreateEntryIntAndDouble<T>(ref int index, T newEntry, bool isNumeric)
        {
            try
            {
                if (newEntry.GetType() == typeof(Entry))
                {
                    Entry entry = newEntry as Entry;
                    entry.ClassId = $"Entry{index}";
                    entry.WidthRequest = 300;
                    entry.FontSize = 12;
                    entry.TextColor = Colors.White;
                    entry.Text = "";

                    if (isNumeric)
                    {
                        entry.Keyboard = Keyboard.Numeric;
                        entry.TextChanged += (sender, e) =>
                        {
                            Regex regex = new Regex("[^0-9]+");
                            string numericText = regex.Replace(e.NewTextValue, "");
                            ((Entry)sender).Text = numericText;
                            tempData[1] = entry.Text;
                        };
                    }
                    else
                    {
                        entry.TextChanged += (sender, e) =>
                        {
                            Trace.WriteLine($"entered {entry.ClassId}: {entry.Text}");
                            tempData[2] = entry.Text;
                        };
                    }
                    newEntry = (T)(object)entry;
                    AddPage.instance.entries.Children.Add((IView)newEntry);
                }
                else if (newEntry.GetType() == typeof(Picker))
                {
                    Picker entry = newEntry as Picker;
                    entry.ClassId = $"Picker{index}";
                    entry.WidthRequest = 300;
                    entry.FontSize = 12;
                    entry.TextColor = Colors.White;
                    ObservableCollection<bool> boolOptions = new ObservableCollection<bool>() { true, false };
                    entry.ItemsSource = boolOptions;
                    entry.SelectedIndexChanged += (sender, e) =>
                    {
                        tempData[3] = entry.Items[entry.SelectedIndex].ToString();
                    };
                    newEntry = (T)(object)entry;
                    AddPage.instance.entries.Children.Add((IView)newEntry);
                }
                else if (newEntry.GetType() == typeof(DatePicker))
                {
                    DatePicker entry = newEntry as DatePicker;
                    entry.MinimumDate = new DateTime(1900, 1, 1);
                    entry.ClassId = $"DatePicker{index}";
                    entry.WidthRequest = 300;
                    entry.FontSize = 12;
                    entry.DateSelected += (sender, e) =>
                    {
                        DateTime dt = entry.Date;
                        tempData[4] = dt.ToString("yyyy-MM-dd");
                        Trace.WriteLine($"date: {entry.Date.ToString()} - {dt.ToString("yyyy-MM-dd")}");
                    };
                    newEntry = (T)(object)entry;
                    AddPage.instance.entries.Children.Add((IView)newEntry);
                }


                index -= 1;
            }
            catch(Exception ex)
            {
                Trace.WriteLine($"error create entries: {ex}");
            }
            

        }
        //private void CreateEntryString(int amount)
        //{
        //    for (int i = 1; i <= amount; i++)
        //    {
        //        CreateLabel();
        //        Entry newEntry = new Entry();
        //        newEntry.ClassId = $"Entry{i}";
        //        newEntry.WidthRequest = 300;
        //        newEntry.FontSize = 12;
        //        newEntry.TextColor = Colors.White;
        //        newEntry.Text = "";
        //        newEntry.TextChanged += (sender, e) =>
        //        {
        //            Trace.WriteLine($"entered {newEntry.ClassId}: {newEntry.Text}");
        //        };
        //        AddPage.instance.entries.Children.Add(newEntry);

        //        //CreateButton();
        //    }
        //}
        //private void CreatePickerBool(int amount)
        //{
        //    for (int i = 1; i <= amount; i++)
        //    {
        //        CreateLabel();
        //        ObservableCollection<bool> boolOptions = new ObservableCollection<bool>() { true, false };
        //        Picker newPicker = new Picker();
        //        newPicker.ItemsSource = boolOptions;
        //        newPicker.ClassId = $"Picker{i}";
        //        newPicker.WidthRequest = 300;
        //        newPicker.FontSize = 12;
        //        AddPage.instance.entries.Children.Add(newPicker);

        //        CreateButton(newPicker);
        //    }
        //}
        //private void CreateDatePicker(int amount)
        //{
        //    for (int i = 1; i <= amount; i++)
        //    {
        //        CreateLabel();
        //        DatePicker newPicker = new DatePicker();
        //        newPicker.MinimumDate = new DateTime(1900, 1, 1);
        //        newPicker.ClassId = $"DatePicker{i}";
        //        newPicker.WidthRequest = 300;
        //        newPicker.FontSize = 12;
        //        AddPage.instance.entries.Children.Add(newPicker);

        //        TimePicker newTimePicker = new TimePicker();
        //        newTimePicker.ClassId = $"TimePicker{i}";
        //        newTimePicker.WidthRequest = 300;
        //        newTimePicker.FontSize = 12;
        //        AddPage.instance.entries.Children.Add(newTimePicker);

        //        CreateButton(newPicker, newTimePicker);
        //    }
        //}
        private void CreateButton(Dictionary<int, string> types)
        {
            
            Button btnAdd = new Button();
            btnAdd.Text = "Add value to table";
            btnAdd.HorizontalOptions = LayoutOptions.Center;
            btnAdd.VerticalOptions = LayoutOptions.Center;
            btnAdd.Padding = new Thickness(5);
            btnAdd.Clicked += (sender, e) =>
            {
                newTable table = new newTable();
                for (int i = 1; i <= types.Count; i++)
                {
                    if (types[i] != null && types[i] != "")
                    {
                        var propertyInfo = table.GetType().GetProperty("Variable" + (i));
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(table, types[i]);
                        }
                    }
                }
                listOfData.Add(table);
                ListOfObjectsAdd = listOfData;
                MainViewModel.instance.ListOfObjects = ListOfObjectsAdd;
                Trace.WriteLine($"{ListOfObjectsAdd[0].Variable1} count: {ListOfObjectsAdd.Count} - {MainViewModel.instance.ListOfObjects[0].Variable1}");

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
