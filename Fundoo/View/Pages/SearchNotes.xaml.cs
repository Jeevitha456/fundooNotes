using Fundoo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fundoo.Firebase;
using Fundoo.Interface;

namespace Fundoo.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchNotes : ContentPage
	{
        public List<NotesData> notesData;
		public SearchNotes ()
		{
			InitializeComponent ();
            Data();
            list.ItemsSource = notesData;

        }
        public async void Data()
        {

            FirebaseHelper firebaseHelper = new FirebaseHelper();
            List<NotesData> notes=await firebaseHelper.GetAllNotes();
            notes = notes.Where(a=> a.IsDeleted==false && a.IsArchive==false).ToList();
            notesData = notes;

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                list.ItemsSource = notesData;
            }

            else
            {
                list.ItemsSource = notesData.Where(x => x.Title.ToLower().Contains(e.NewTextValue.ToLower()) 
                
                && x.Notes.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }
    }
}