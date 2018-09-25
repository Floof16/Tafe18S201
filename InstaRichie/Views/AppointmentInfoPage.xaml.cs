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
using SQLite.Net;
using SQLite;
using Windows.UI.Popups;
using StartFinance.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppointmentInfoPage : Page
    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");



        public AppointmentInfoPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            /// Initializing a database
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            // Creating table
            //DateStamp.Date = DateTime.Now; // gets current date and time
            //DateStamp1.Date = DateTime.Now;
            Results();
        }

        public void Results()
        {
            conn.CreateTable<Models.AppointmentInfo>();
            var query = conn.Table<Models.AppointmentInfo>();
            Info.ItemsSource = query.ToList();

        }




        private async void save_click(object sender, RoutedEventArgs e)
        {
            try
            {
                // checks if account name is null
                if (AppointmentID.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("ID not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (EventName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Event Name not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Location.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Location not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }





                else
                {   // Inserts the data

                    conn.Insert(new Models.AppointmentInfo()
                    {

                        AppointmentID = int.Parse(AppointmentID.Text),
                        EventName = EventName.Text.ToString(),
                        Location = Location.Text.ToString(),
                        EventDate = eDate,
                        StartTime = StartTime.Time,
                        EndTime = EndTime.Time,
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

                    Frame.Navigate(typeof(Views.AppointmentInfoPage));
                }



            }

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
                    int ID = ((Models.AppointmentInfo)Info.SelectedItem).AppointmentID;
                    var querydel = conn.Query<Models.AppointmentInfo>("DELETE FROM AppointmentInfo WHERE AppointmentID='" + ID + "'");
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
                MessageDialog dialog = new MessageDialog("Nothing was Deleted");
            }
        }



        private void cancel_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.MainPage));
        }

        private async void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AppointmentID.Text == "")
                {
                    MessageDialog dialog = new MessageDialog("ID not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (EventName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("EventName not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Location.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Location not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                
              
                else
                {   // Updates the data
                    MessageDialog ShowConf = new MessageDialog("Are you sure you want to make these changes", "Important");
                    ShowConf.Commands.Add(new UICommand("Yes, Edit")
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

                            int ID = ((Models.AppointmentInfo)Info.SelectedItem).AppointmentID;
                            //var queryEdit = conn.Query<Models.AppointmentInfo>("UPDATE AppointmentInfo SET  EventName='" + EventName.Text.ToString() + "', Location = '" + Location.Text.ToString() +
                            //                                  "', Eventdate ='" + eDate + "', StartTime ='" + StartTime.Time + "',EndTime='" + EndTime.Time+
                            //                                    "WHERE AppointmentID =" + ID );
                            conn.Update(new Models.AppointmentInfo()
                            {

                                AppointmentID = ID,
                                EventName = EventName.Text.ToString(),
                                Location = Location.Text.ToString(),
                                EventDate = eDate,
                                StartTime = StartTime.Time,
                                EndTime = EndTime.Time,
                            });
                            Results();
                        }
                        catch (NullReferenceException)
                        {
                            MessageDialog ClearDialog = new MessageDialog("Please select the item to Edit", "Oops..!");
                            await ClearDialog.ShowAsync();
                        }
                    }
                    else
                    {
                        MessageDialog dialog = new MessageDialog("No Edits Made");
                    }

                }

            }
            catch (Exception ex)
            {   // Exception to display when ID is invalid 
                if (ex is SQLiteException)
                {
                    MessageDialog dialog = new MessageDialog("Incorrect ID", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    Frame rootFrame = Window.Current.Content as Frame;


                    Frame.Navigate(typeof(Views.AppointmentInfoPage));
                }

            }

        }





        //conn.Update(new Contacts()
        //{
        //    ContactID = int.Parse(ContactID.Text),
        //    FirstName = FirstName.Text,
        //    LastName = LastName.Text,
        //    CompanyName = CompanyName.Text,
        //    MobilePhone = int.Parse(Mobile.Text)
        //});
        string eDate;
        private void EventDateMethod(string selectedDate)
        {
            eDate = selectedDate;
        }

        private void dateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != null)
            {

                var date = EventDate.Date;
                DateTime time = date.Value.DateTime;
                var formatedtime = time.ToString("dd/MM/yyyy");
                System.Diagnostics.Debug.WriteLine(formatedtime);
                EventDateMethod(formatedtime);
            }
        }
    }
}


