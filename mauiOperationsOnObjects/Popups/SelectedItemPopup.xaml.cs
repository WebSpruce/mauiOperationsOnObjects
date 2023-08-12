using CommunityToolkit.Maui.Views;
using mauiOperationsOnObjects.ViewModels;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;
using mauiOperationsOnObjects.Pages;

namespace mauiOperationsOnObjects.Popups;

public partial class SelectedItemPopup : Popup
{
	public static SelectedItemPopup instance;
    private newTable newRow = new newTable();
    public SelectedItemPopup(newTable selectedObject)
	{
		InitializeComponent();
		instance = this;
        
        newRow = selectedObject;

        if (selectedObject != null)
        {
            int notNullCount = Enumerable.Range(1, 5)
            .Select(i => "Variable" + i)
            .Select(variableName => selectedObject.GetType().GetProperty(variableName))
            .Where(propertyInfo => propertyInfo != null && propertyInfo.GetValue(selectedObject) != null)
            .Count();
            try
            {
                for (int i = 1; i <= notNullCount; i++)
                {
                    var propertyInfo = selectedObject.GetType().GetProperty("Variable" + (i));
                    if (propertyInfo.GetValue(selectedObject) != null || propertyInfo.GetValue(selectedObject) != "")
                    {
                        string newValue = "";

                        string[] formats = { "yyyy/MM/dd" };
                        DateTime parsedDateTime;

                        PropertyInfo selectedProperty = propertyInfo;

                        Trace.WriteLine($"selected value: {propertyInfo.GetValue(selectedObject).ToString()}");
                        if (propertyInfo.GetValue(selectedObject).ToString() == "True" || propertyInfo.GetValue(selectedObject).ToString() == "False")
                        {
                            Trace.WriteLine($"selecteditem bool");
                            Picker entry = new Picker();
                            entry.ClassId = $"Picker{i}";
                            entry.WidthRequest = 300;
                            entry.FontSize = 12;
                            entry.TextColor = Colors.White;
                            ObservableCollection<bool> boolOptions = new ObservableCollection<bool>() { true, false };
                            entry.ItemsSource = boolOptions;

                            int propertyIndex = i;
                            entry.SelectedIndexChanged += (sender, e) =>
                            {
                                newValue = boolOptions[entry.SelectedIndex].ToString();
                                var propertyInfo2 = newRow.GetType().GetProperty("Variable" + (propertyIndex));

                                if (propertyInfo2 != null)
                                {
                                    propertyInfo2.SetValue(newRow, bool.Parse(newValue));
                                }
                                else
                                {
                                    Trace.WriteLine($"Property not found for Variable{i}.");
                                }
                            };
                            editArea.Children.Add(entry);
                        }
                        else if (DateTime.TryParseExact(propertyInfo.GetValue(selectedObject).ToString(), formats, new CultureInfo("en-US"), DateTimeStyles.None, out parsedDateTime))
                        {
                            Trace.WriteLine($"selecteditem datetime");
                            DatePicker entry = new DatePicker();
                            entry.MinimumDate = new DateTime(1900, 1, 1);
                            entry.ClassId = $"DatePicker{i}";
                            entry.WidthRequest = 300;
                            entry.FontSize = 12;
                            entry.Date = DateTime.Parse(propertyInfo.GetValue(selectedObject).ToString());
                            int propertyIndex = i;
                            entry.DateSelected += (sender, e) =>
                            {
                                DateTime dt = entry.Date;
                                newValue = dt.ToString("yyyy/MM/dd");
                                var propertyInfo2 = newRow.GetType().GetProperty("Variable" + (propertyIndex));

                                if (propertyInfo2 != null)
                                {
                                    propertyInfo2.SetValue(newRow, DateTime.Parse(newValue));
                                }
                                else
                                {
                                    Trace.WriteLine($"Property not found for Variable{i}.");
                                }
                            };
                            editArea.Children.Add(entry);
                        }
                        else if (propertyInfo.GetValue(selectedObject).ToString().All(char.IsDigit))
                        {
                            Trace.WriteLine($"selecteditem double");
                            Entry entry = new Entry();
                            entry.ClassId = $"Entry{i}";
                            entry.WidthRequest = 300;
                            entry.FontSize = 12;
                            entry.TextColor = Colors.White;
                            entry.Text = propertyInfo.GetValue(selectedObject).ToString();
                            entry.Keyboard = Keyboard.Numeric;
                            int propertyIndex = i;
                            entry.TextChanged += (sender, e) =>
                            {
                                Regex regex = new Regex("[^0-9]+");
                                string numericText = regex.Replace(e.NewTextValue, "");
                                ((Entry)sender).Text = numericText;
                                newValue = numericText;

                                var propertyInfo2 = newRow.GetType().GetProperty("Variable" + (propertyIndex));

                                if (propertyInfo2 != null)
                                {
                                    propertyInfo2.SetValue(newRow, double.Parse(newValue));
                                }
                                else
                                {
                                    Trace.WriteLine($"Property not found for Variable{i}.");
                                }
                            };

                            editArea.Children.Add(entry);
                        }
                        else if (!propertyInfo.GetValue(selectedObject).ToString().All(char.IsDigit))
                        {
                            Trace.WriteLine($"selecteditem string");
                            Entry entry = new Entry();
                            entry.ClassId = $"Entry{i}";
                            entry.WidthRequest = 300;
                            entry.FontSize = 12;
                            entry.TextColor = Colors.White;
                            entry.Text = propertyInfo.GetValue(selectedObject).ToString();
                            int propertyIndex = i;
                            entry.TextChanged += (sender, e) =>
                            {
                                newValue = entry.Text;
                                var propertyInfo2 = newRow.GetType().GetProperty("Variable" + (propertyIndex));

                                if (propertyInfo2 != null)
                                {
                                    propertyInfo2.SetValue(newRow, newValue);
                                }
                                else
                                {
                                    Trace.WriteLine($"Property not found for Variable{i}.");
                                }
                            };
                            editArea.Children.Add(entry);
                        }


                    }

                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine($"SelectedItemPopup error: {ex}");
            }
            
            CreateButton(newRow, selectedObject);
        }

        
    }

    private void CreateButton(newTable newRow, newTable selectedObject)
    {
        Button btnAdd = new Button();
        btnAdd.Text = "Close";
        btnAdd.HorizontalOptions = LayoutOptions.Center;
        btnAdd.VerticalOptions = LayoutOptions.Center;
        btnAdd.Padding = new Thickness(5);
        btnAdd.Clicked += (sender, e) =>
        {
            try
            {
                for (int i = 0; i < MainViewModel.instance.ListOfObjects.Count; i++)
                {
                    if (selectedObject == MainViewModel.instance.ListOfObjects[i])
                    {
                        MainViewModel.instance.ListOfObjects[i] = newRow;
                    }
                }
                EditPage.instance.collectionView.SelectedItem = null;
                Close();
            }
            catch(Exception ex)
            {
                Trace.WriteLine($"SelectedItemPopup create button error: {ex}");
            }
            
        };
        editArea.Children.Add(btnAdd);
    }

    public PropertyInfo GetPropertyByIndex(object instance, int index)
    {
        Type type = instance.GetType();
        PropertyInfo[] properties = type.GetProperties();

        if (index >= 0 && index < properties.Length)
        {
            return properties[index];
        }

        return null;
    }


}