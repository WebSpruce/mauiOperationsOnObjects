using mauiOperationsOnObjects.Pages;
using mauiOperationsOnObjects.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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

        public AddViewModel instance;
        public AddViewModel()
        {
            instance = this;
            ListOfObjectsAdd = MainViewModel.instance.ListOfObjects;

            AddObjectToCollectionView((int)MainViewModel.instance.AmountOfColumns);
        }
        private ObservableCollection<newTable> listOfData = new ObservableCollection<newTable>();
        private static Dictionary<int, string> tempData = new Dictionary<int, string>
        {
            { 1, ""}, { 2, ""}, { 3, ""}, { 4, ""}, { 5, ""},
        };
        private int keyWithEmptyValue = tempData.FirstOrDefault(pair => pair.Value == "").Key;
        private int index = 0;
        public void AddObjectToCollectionView(int count)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    List<Type> variableType = new List<Type>();
                    switch (count)
                    {
                        case 1:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                break;
                            }
                        case 2:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                break;
                            }
                        case 3:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable3.GetType());
                                break;
                            }
                        case 4:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable3.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable4.GetType());
                                break;
                            }
                        case 5:
                            {
                                variableType.Add(MainViewModel.instance.newTable.Variable1.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable2.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable3.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable4.GetType());
                                variableType.Add(MainViewModel.instance.newTable.Variable5.GetType());
                                break;
                            }
                    }

                    index = variableType.Count;

                    if (variableType[i] == typeof(double))
                    {
                        Entry newEntry = new Entry();
                        CreateLabel(typeof(double));
                        CreateEntries<Entry>(ref index, newEntry, true, ref keyWithEmptyValue);
                        continue;
                    }
                    else if (variableType[i] == typeof(bool))
                    {
                        Picker newEntry = new Picker();
                        CreateLabel(typeof(bool));
                        CreateEntries<Picker>(ref index, newEntry, true, ref keyWithEmptyValue);
                        continue;
                    }
                    else if (variableType[i] == typeof(string))
                    {
                        Entry newEntry = new Entry();
                        CreateLabel(typeof(string));
                        CreateEntries<Entry>(ref index, newEntry, false, ref keyWithEmptyValue);
                        continue;
                    }
                    else if (variableType[i] == typeof(DateTime))
                    {
                        DatePicker newEntry = new DatePicker();
                        CreateLabel(typeof(DatePicker));
                        CreateEntries<DatePicker>(ref index, newEntry, false, ref keyWithEmptyValue);
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
                lb.Text = $"Add value to table (Numbers).";
            }
            else if (typeOfEntry == typeof(bool))
            {
                lb.Text = $"Set value to table (Bool).";
            }
            else if (typeOfEntry == typeof(string))
            {
                lb.Text = $"Set value to table (String).";
            }
            else if (typeOfEntry == typeof(DatePicker))
            {
                lb.Text = $"Set value to table (Date).";
            }
            AddPage.instance.entries.Children.Add(lb);
        }
        private List<Entry> newEntries = new List<Entry>();
        private void CreateEntries<T>(ref int index, T newEntry, bool isNumeric, ref int keyWithEmptyValue)
        {
            try
            {
                int key = keyWithEmptyValue;
                string value = "";
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
                            value = entry.Text;
                            tempData[key - 1] = value;
                        };
                        key += 1;
                        keyWithEmptyValue = key;
                    }
                    else
                    {
                        entry.TextChanged += (sender, e) =>
                        {
                            value = entry.Text;
                            tempData[key] = value;
                        };
                        keyWithEmptyValue++;
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
                        value = entry.Items[entry.SelectedIndex].ToString();
                        tempData[key] = value;
                    };
                    keyWithEmptyValue++;
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
                        value = dt.ToString("yyyy/MM/dd");
                        tempData[key] = value;
                    };
                    keyWithEmptyValue++;
                    newEntry = (T)(object)entry;
                    AddPage.instance.entries.Children.Add((IView)newEntry);
                }
                index -= 1;
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"error create entries: {ex}");
            }


        }
        private void CreateButton(Dictionary<int, string> types)
        {

            Button btnAdd = new Button();
            btnAdd.Text = "Add value to table";
            btnAdd.HorizontalOptions = LayoutOptions.Center;
            btnAdd.VerticalOptions = LayoutOptions.Center;
            btnAdd.Padding = new Thickness(5);
            btnAdd.Clicked += async (sender, e) =>
            {
                try
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
                    if (ListOfObjectsAdd == null)
                    {
                        table.Id = 1;
                    }
                    else 
                    {
                        table.Id = ListOfObjectsAdd[ListOfObjectsAdd.Count - 1].Id + 1;
                    }
                    
                    listOfData.Add(table);

                    ListOfObjectsAdd = listOfData;
                    MainViewModel.instance.ListOfObjects = null;
                    MainViewModel.instance.ListOfObjects = ListOfObjectsAdd;
                    await AddPage.instance.DisplayAlert("Well done!", "Your item has been added.", "OK");
                }
                catch(Exception ex)
                {
                    Trace.WriteLine($"AddViewModel CreateButton error: {ex}");
                }
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
