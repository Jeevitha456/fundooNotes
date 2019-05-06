// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Master.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.HomePage
{
    using System;
    using System.Collections.Generic;
    using Fundoo.Firebase;
    using Fundoo.Model;
    using Fundoo.View.Pages;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Master class
    /// </summary>
    public partial class Master : MasterDetailPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Master"/> class.
        /// </summary>
        public Master()
        {
            this.InitializeComponent();
           // OnAppearing();
            var image = new TapGestureRecognizer();
            image.Tapped += imageTapped;
            ProfilePic.GestureRecognizers.Add(image);
            this.MasterList = new List<MasterItems>();
            this.MasterList.Add(new MasterItems() { Title = "Notes", Icon = "Note.png", TargetType = typeof(DashBoard) });
            this.MasterList.Add(new MasterItems() { Title = "Reminders", Icon = "Reminders.png", TargetType = typeof(RemindersPage) });
            this.MasterList.Add(new MasterItems() { Title = "Create new label", Icon = "CreateLabel.png", TargetType = typeof(CreatePage) });
            this.MasterList.Add(new MasterItems() { Title = "Archive", Icon = "archiveicon.png", TargetType = typeof(ArchievePage) });
            this.MasterList.Add(new MasterItems() { Title = "Trash", Icon = "Trash.png", TargetType = typeof(TrashPage) });
            this.MasterList.Add(new MasterItems() { Title = "Logout", TargetType = typeof(SignOut) });
            this.navigationDrawerList.ItemsSource = this.MasterList;
            this.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(DashBoard)));
        } 

        public async void imageTapped(object sender,EventArgs e)
        {
            await Navigation.PushModalAsync(new GalleryPage());
        }

        /// <summary>
        /// Gets or sets the menu list.
        /// </summary>
        /// <value>
        /// The menu list.
        /// </value>
        public IList<MasterItems> MasterList { get; set; }

        /// <summary>
        /// Called when [item selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterItems)e.SelectedItem;
            Type page = item.TargetType;
            this.Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            this.IsPresented = false;
        }

        //protected async override void OnAppearing()
        //{
        //    FirebaseHelper firebaseHelper = new FirebaseHelper();
        //    //SignUpUserData user = await firebaseHelper.GetUser();
        //    if(user.imageurl!=null)
        //    {
        //        var imagesource = new UriImageSource { Uri = new Uri(user.imageurl) };

        //        imagesource.CachingEnabled = false;
        //        ProfilePic.Source = imagesource;
        //        ProfilePic.HeightRequest = 70;
        //        ProfilePic.WidthRequest = 70;
        //    }
        //    base.OnAppearing();
        //}
    }
}