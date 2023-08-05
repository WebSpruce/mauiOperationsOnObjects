using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using mauiOperationsOnObjects.Pages;
using System.Dynamic;

namespace mauiOperationsOnObjects.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double amountOfColumns;
        public double AmountOfColumns
        {
            get { return amountOfColumns; }
            set {
                if (amountOfColumns != value)
                {
                    amountOfColumns = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<newTable> listOfObjects;
        public ObservableCollection<newTable> ListOfObjects
        {
            get { return listOfObjects; }
            set
            {
                if (listOfObjects != value)
                {
                    listOfObjects = value;
                    OnPropertyChanged("ListOfObjects");
                }
            }
        }

        public ICommand CreateTableCommand { get; private set; }
        

        private List<string> selectedTypesForColumns = new List<string>();

        public static MainViewModel instance;
        public MainViewModel()
        {
            instance = this;
            CreateTableCommand = new Command(() => CreateCollectionView((int)AmountOfColumns));
        }
        public newTable newTable = new newTable();
        public void CreateCollectionView(int amountOfColumns)
        {
            List<string> types = new List<string>() { "String", "Double", "Datetime", "Bool"};
            Picker[] pickers = new Picker[amountOfColumns];
            for(int i=0; i<amountOfColumns; i++)
            {
                int index = i;
                Label lb = new Label();
                lb.Text = $"Set type of value for {i+1}. column";
                lb.HorizontalTextAlignment = TextAlignment.Center;
                lb.FontSize = 12;
                HomePage.instance.table.Children.Add(lb);

                pickers[i] = new Picker();
                pickers[i].ClassId = $"Picker{i+1}";
                pickers[i].WidthRequest = 100;
                pickers[i].ItemsSource = types;
                pickers[i].SelectedIndexChanged += (sender, args) =>
                {
                    var selectedValue = pickers[index].SelectedItem?.ToString();
                    selectedTypesForColumns.Add(selectedValue);
                    pickers[index].IsEnabled = false;
                };

                HomePage.instance.table.Children.Add(pickers[i]);
            }
            HomePage.instance.BtnCreateTable.IsEnabled = false;

            Button btnCreate = new Button();
            btnCreate.Text = "Create table";
            btnCreate.HorizontalOptions = LayoutOptions.Center;
            btnCreate.VerticalOptions = LayoutOptions.Center;
            btnCreate.Padding = new Thickness(5);
            btnCreate.Clicked += (sender, e) =>
            {
                
                for (int i = 0; i < amountOfColumns; i++)
                {
                    if (pickers[i].SelectedItem != null)
                    {
                        Trace.WriteLine($"{i}. {selectedTypesForColumns[i]}");
                        dynamic value;
                        switch (selectedTypesForColumns[i])
                        {
                            case "String":
                                {
                                    value = "";
                                    break;
                                }
                            case "Double":
                                {
                                    value = 0.0;
                                    break;
                                }
                            case "Bool":
                                {
                                    value = false;
                                    break;
                                }
                            case "Datetime":
                                {
                                    value = DateTime.MinValue;
                                    break;
                                }
                            default:
                                {
                                    Trace.WriteLine("Something is wrong with types.");
                                    value = null;
                                    break;
                                }
                        }
                        switch (pickers[i].ClassId)
                        {
                            case "Picker1":
                                {
                                    newTable.Variable1 = value;
                                    break;
                                }
                            case "Picker2":
                                {
                                    newTable.Variable2 = value;
                                    break;
                                }
                            case "Picker3":
                                {
                                    newTable.Variable3 = value;
                                    break;
                                }
                            case "Picker4":
                                {
                                    newTable.Variable4 = value;
                                    break;
                                }
                            case "Picker5":
                                {
                                    newTable.Variable5 = value;
                                    break;
                                }
                            default:
                                {
                                    Trace.WriteLine("Something is wrong with pickers.");
                                    break;
                                }
                        }
                        HomePage.instance.DisplayAlert("Created table.", $"The table has created. Please, go to Add Window.", "OK");
                        btnCreate.IsEnabled = false;
                    }
                    else
                    {
                        HomePage.instance.DisplayAlert("Wrong","You have not chosen types for columns.","OK");
                        btnCreate.IsEnabled = true;
                    }

                }
            };
            HomePage.instance.table.Children.Add(btnCreate);
        }
        


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class newTable
    {
        public dynamic Variable1 { get; set; }
        public dynamic Variable2 { get; set; }
        public dynamic Variable3 { get; set; }
        public dynamic Variable4 { get; set; }
        public dynamic Variable5 { get; set; }
    }
}
