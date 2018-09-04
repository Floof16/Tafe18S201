using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using StartFinance.Models;
using StartFinance.ViewModels;
using Windows.UI.Popups;
using SQLite.Net;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalInfo : Page
    {
       
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");

        public void NewPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            /// Initializing a database
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            Results();
        }
        public PersonalInfo()
        {
            this.InitializeComponent();
        }
        public void Results()
        {
            // Creating table
            conn.CreateTable<Models.PersonalInfo>();
            var query = conn.Table<Models.PersonalInfo>();
            PersonalInfoList.ItemsSource = query.ToList();
        }
        private void cancel_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.MainPage));
        }

        private async void save_click(object sender, RoutedEventArgs e)
        {
            try
            {
                // checks if account name is null
                if (PersonalID.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("ID not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (FirstName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("First Name not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (LastName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Last Name not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (DOB.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Date of Birth not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Gender.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Gender not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Email.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Email not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Mobile.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Mobile not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }


                else
                {   // Inserts the data
                    conn.Insert(new Models.PersonalInfo()
                    {
                        PersonalID = PersonalID.Text.ToString(),
                        FirstName = FirstName.Text.ToString(),
                        LastName = LastName.Text.ToString(),
                        DOB = DOB.Text.ToString(),
                        Gender = Gender.Text.ToString(),
                        Email = Email.Text.ToString(),
                        phoneNumber = Mobile.Text.ToString()
                    });
                    Results();
                }

            }
            catch (Exception ex)
            {   // Exception to display when amount is invalid or not numbers
                 if (ex is SQLiteException)
                {
                    MessageDialog dialog = new MessageDialog("ID already exist, Try Different ID", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {

                    Frame.Navigate(typeof(Views.PersonalInfo));
                }

            }

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Results();
        }

        private async void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog ShowConf = new MessageDialog("Deleting this Profile will delete all attributes inside this profile", "Important");
            ShowConf.Commands.Add(new UICommand("Yes, Delete")
            {
                Id = 0
            });
            ShowConf.Commands.Add(new UICommand("Cancel")
            {
                Id = 1
            });
            ShowConf.DefaultCommandIndex = 0;
            ShowConf.CancelCommandIndex = 1;

            var result = await ShowConf.ShowAsync();
            if ((int)result.Id == 0)
            {
                
                try
                {
                    string ID = ((PersonalInfo)PersonalInfoList.SelectedItem).PersonalID.ToString() ;
                    var querydel = conn.Query<PersonalInfo>("DELETE FROM PersonalInfo WHERE PersonalID='" + ID + "'");
                    Results();
                }
                catch (NullReferenceException)
                {
                    MessageDialog ClearDialog = new MessageDialog("Please select the item to Delete", "Oops..!");
                    await ClearDialog.ShowAsync();
                }
            }
            else
            {
               Frame.Navigate(typeof(Views.PersonalInfo));
            }
        }

        private async void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // checks if account name is null
                if (PersonalID.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("ID not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (FirstName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("First Name not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (LastName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Last Name not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (DOB.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Date of Birth not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Gender.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Gender not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Email.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Email not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Mobile.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Mobile not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }


                else
                {   // Updates the data
                    conn.Update(new Models.PersonalInfo()
                    {
                        PersonalID = PersonalID.Text.ToString(),
                        FirstName = FirstName.Text.ToString(),
                        LastName = LastName.Text.ToString(),
                        DOB = DOB.Text.ToString(),
                        Gender = Gender.Text.ToString(),
                        Email = Email.Text.ToString(),
                        phoneNumber = Mobile.Text.ToString()
                    });
                    Results();
                }

            }
            catch (Exception ex)
            {   // Exception to display when ID is invalid 
                if (ex is SQLiteException)
                {
                    MessageDialog dialog = new MessageDialog("ID already exist, Try Different ID", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    Frame rootFrame = Window.Current.Content as Frame;

                 
                   Frame.Navigate(typeof(Views.PersonalInfo));
                }

            }
        }
    }
}
