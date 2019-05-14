using Firebase.Database;
using Firebase.Database.Query;
using Fundoo.Firebase;
using Fundoo.Interface;
using Fundoo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Collaborator : ContentPage
    {
      
        
        FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
        string value = null;
        ObservableCollection<string> source = new ObservableCollection<string>();
        public Collaborator (string key)
		{
			InitializeComponent ();
            txtMail.ItemsSource = source;
            string uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            EmailData();
            Data();
            this.value = key;
		}

        public async void EmailData()
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
            var currentEmail = await firebase.Child("Persons").Child(uid).Child("userinfo").OnceAsync<SignUpUserData>();
            foreach (var item in currentEmail)
            {
                var currentEmailDetails = item.Object.Email;
                txtCurrentMail.Text = currentEmailDetails;
            }
        }
        string id = null;
        public async void Data()
        {
            var users = await firebase.Child("Persons").OnceAsync<SignUpUserData>();
        
        
            string uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            foreach (var items in users)
            {
               
                if (items.Key.ToString() != uid)
                {
                    var email = await firebase.Child("Persons").Child(items.Key).Child("userinfo").OnceAsync<SignUpUserData>();
                    foreach (var item in email)
                    {
                        var emailDetails = item.Object.Email;
                        // var emailId = item.Key;
                       id = items.Key;
                        source.Add(emailDetails);
                      
                    }
                }

            }
           
        }
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private async void SaveButton(object sender, EventArgs e)
        {
           var collabPerson = txtMail.Text;
            var users = await firebase.Child("Persons").OnceAsync<SignUpUserData>();

            IList<string> mail = new List<string>();

            string uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            foreach (var items in users)
            {
                if (items.Key.ToString() != uid)
                {
                    var email = await firebase.Child("Persons").Child(items.Key).Child("userinfo").OnceAsync<SignUpUserData>();
                    foreach (var item in email)
                    {
                        var emailDetails = item.Object.Email;

                        // var emailId = item.Key;
                        id = items.Key;
                       if(txtMail.Text==emailDetails)
                       {
                            //lstEmails.ItemsSource = emailDetails;
                            NotesData notes = await this.firebaseHelper.GetNotesData(this.value, uid);
                            //// Updates the notes when DeleteNotes method is called
                            notes = new NotesData()
                            {

                                Title = notes.Title,
                                Notes = notes.Notes,
                                ColorNote = notes.ColorNote,
                                LabelData = notes.LabelData,
                                Latitude = notes.Latitude,
                                Longitude = notes.Longitude,
                                Area = notes.Area,
                                IsCollaborated=true

                            };
                            await firebase.Child("Persons").Child(this.id).Child("Notes").Child(value).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, ColorNote = notes.ColorNote, LabelData = notes.LabelData,IsCollaborated=true });
                            await firebase.Child("Persons").Child(uid).Child("Notes").Child(value).PutAsync(new NotesData() { Title = notes.Title, Notes = notes.Notes, ColorNote = notes.ColorNote, LabelData = notes.LabelData, IsCollaborated = true });
                         
                        }
                    }
                }

            }

        }

       
        
    }
}
