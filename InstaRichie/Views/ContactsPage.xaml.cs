using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SQLite.Net;
using System.IO;
using Windows.UI.Popups;
using StartFinance.Models;
using System.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactsPage : Page


    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "contactsData.sqlite");


        public ContactsPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            /// Initializing a database
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            // Creating table
            //DateStamp.Date = DateTime.Now; // gets current date and time
            //DateStamp1.Date = DateTime.Now;
            Resuts();
        }

        public void Resuts()
        {
            conn.CreateTable<Contacts>();
            var query = conn.Table<Contacts>();
            ContactsList.ItemsSource = query.ToList();
            ContactID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            CompanyName.Text = "";
            Mobile.Text = "";
        }


        private async void save_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ContactID.Text.ToString() == "")
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
                if (CompanyName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Company Name not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                if (Mobile.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Mobile not Entered", "Oops..!");
                    await dialog.ShowAsync();
                }

                Contacts contact1 = new Contacts()
                {
                    ContactID = int.Parse(ContactID.Text),
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    CompanyName = CompanyName.Text,
                    MobilePhone = (Mobile.Text)
                };
                conn.Query<Contacts>("insert or replace into Contacts (ContactID, FirstName, LastName, CompanyName, MobilePhone) values(" + contact1.ContactID + " , '" + contact1.FirstName + "','" + contact1.LastName + "', '" + contact1.CompanyName + "','" + contact1.MobilePhone + "')");
                Resuts();
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    MessageDialog dialog = new MessageDialog("You forgot to enter the Value or entered an invalid data", "Oops..!");
                    await dialog.ShowAsync();
                }
                else if (ex is SQLiteException)
                {
                    Contacts contact1 = new Contacts()
                    {
                        ContactID = int.Parse(ContactID.Text),
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        CompanyName = CompanyName.Text,
                        MobilePhone = (Mobile.Text)
                    };
                    MessageDialog dialog = new MessageDialog("insert or replace into Contacts (ContactID, FirstName, LastName, CompanyName, MobilePhone) values(" + contact1.ContactID + " , '" + contact1.FirstName + "','" + contact1.LastName + "', '" + contact1.CompanyName + "','" + contact1.MobilePhone + "')");

                    await dialog.ShowAsync();
                }
            }
        }

        private async void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int AccSelection = ((Contacts)ContactsList.SelectedItem).ContactID;
                if (AccSelection == 0)
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<Contacts>();
                    var query1 = conn.Table<Contacts>();
                    var query3 = conn.Query<Contacts>("DELETE FROM Contacts WHERE ContactID ='" + AccSelection + "'");
                    ContactsList.ItemsSource = query1.ToList();
                }

                conn.CreateTable<Contacts>();
                var query = conn.Table<Contacts>();
                ContactsList.ItemsSource = query.ToList();

            }
            catch (NullReferenceException)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }
        }

        private async void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int AccSelection = ((Contacts)ContactsList.SelectedItem).ContactID;
                if (AccSelection == 0)
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<Contacts>();
                    var query1 = conn.Table<Contacts>();
                    Contacts contact1 = query1.First(p => p.ContactID == AccSelection);
                    ContactID.Text = (contact1.ContactID).ToString();
                    FirstName.Text = (contact1.FirstName).ToString();
                    LastName.Text = (contact1.LastName).ToString();
                    CompanyName.Text = (contact1.CompanyName).ToString();
                    Mobile.Text = (contact1.MobilePhone).ToString();

                    //conn.Update(new Contacts()
                    //{
                    //    ContactID = int.Parse(ContactID.Text),
                    //    FirstName = FirstName.Text,
                    //    LastName = LastName.Text,
                    //    CompanyName = CompanyName.Text,
                    //    MobilePhone = int.Parse(Mobile.Text)
                    //});

                }



            }
            catch (NullReferenceException)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }


        }

        private void cancel_click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Views.MainPage));

        private async void itemClickedEvent(object sender, ItemClickEventArgs e)
        {

            try
            {
                int AccSelection = ((Contacts)ContactsList.SelectedItem).ContactID;
                if (AccSelection == 0)
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<Contacts>();
                    var query1 = conn.Table<Contacts>();
                    Contacts contact1 = query1.First(p => p.ContactID == AccSelection);
                    FirstName.Text = (contact1.FirstName).ToString();
                    LastName.Text = (contact1.LastName).ToString();
                    CompanyName.Text = (contact1.CompanyName).ToString();
                    Mobile.Text = (contact1.MobilePhone).ToString();

                    conn.Update(new Contacts()
                    {
                        ContactID = int.Parse(ContactID.Text),
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        CompanyName = CompanyName.Text,
                        MobilePhone = Mobile.Text
                    });

                }



            }
            catch (NullReferenceException)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }
        }
    }
}