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
        public ICommand CreateTableCommand { get; private set; }

        public ObservableCollection<string> list;

        public static MainViewModel instance;
        public MainViewModel()
        {
            instance = this;
            CreateTableCommand = new Command(() => CreateCollectionView((int)AmountOfColumns));
        }
        public void CreateCollectionView(int amountOfColumns)
        {
            //list = new ObservableCollection<string>();
            List<string> types = new List<string>() { "string", "int", "double", "image", "date", "bool"};
            for(int i=1; i<=amountOfColumns; i++)
            {
                Label lb = new Label();
                lb.Text = $"Set type of value for {i}. column";
                lb.HorizontalTextAlignment = TextAlignment.Center;
                lb.FontSize = 12;
                HomePage.instance.table.Children.Add(lb);

                Picker picker = new Picker();
                picker.WidthRequest = 100;
                picker.ItemsSource = types;
                picker.SelectedIndexChanged += (sender, args) =>
                {
                    var selectedValue = picker.SelectedItem?.ToString();
                    Debug.WriteLine($"Selected value for column {picker.Id} : {selectedValue}");
                };

                HomePage.instance.table.Children.Add(picker);
            }
            HomePage.instance.BtnCreateTable.IsEnabled = false;
            
            Button btnCreate = new Button();
            btnCreate.Text = "Create table";
            btnCreate.HorizontalOptions = LayoutOptions.Center;
            btnCreate.VerticalOptions = LayoutOptions.Center;
            btnCreate.Padding = new Thickness(5);
            btnCreate.Clicked += (sender, e) => { Trace.WriteLine("CLICKED"); };
            HomePage.instance.table.Children.Add(btnCreate);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
