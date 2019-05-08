using Firebase.Database;
using Firebase.Database.Query;
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
        ObservableCollection<string> source = new ObservableCollection<string>();
        public Collaborator ()
		{
			InitializeComponent ();
            txtMail.ItemsSource = source;
            Data();
		}
        public async void Data()
        {
            var users = await firebase.Child("Persons").OnceAsync<SignUpUserData>();
           // IList<string> mailid = new List<string>();
            IList<string> mail = new List<string>();
            string uid = DependencyService.Get<IFirebaseAuthenticator>().UserId();

            foreach (var items in users)
            {
                if (items.ToString() != uid)
                {
                    var email = await firebase.Child("Persons").Child(items.Key).Child("userinfo").OnceAsync<SignUpUserData>();
                    foreach (var item in email)
                    {
                        var emailDetails = item.Object.Email;
                        // var emailId = item.Key;

                        source.Add(emailDetails);
                    }
                }

            }
            //List<SignUpUserData> notes = await firebaseHelper.GetAllNotes();
            //notes = notes.Where(a => a.IsDeleted == false && a.IsArchive == false).ToList();
            //this.notesData = notes;
        }
        FirebaseClient firebase = new FirebaseClient("https://fundooapp-50c31.firebaseio.com/");
        private async void TxtMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }
    }
}