// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoLocation.xaml.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Jeevitha C"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Fundoo.View.Pages
{
    using System;
    using System.Linq;
    using Fundoo.Firebase;
    using Fundoo.Interface;
    using Fundoo.Model;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// GeoLocation class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoLocation : ContentPage
    {
        /// <summary>
        /// The value
        /// </summary>
        private string value = null;

        /// <summary>
        /// Firebase Helper class
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public GeoLocation()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocation"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="notesData">The notes data.</param>
        public GeoLocation(string key)
        {
            this.value = key;
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button Location control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnLocation_Clicked(object sender, EventArgs e)
        {         
            try
            {
                //// Takes the address from the user
                string address = txtAddress.Text;

                //// Checks if address is not null or empty
                if (!string.IsNullOrEmpty(address))
                {          
                    //// Gets the location of the address
                    var locations = await Geocoding.GetLocationsAsync(address);
                    var location = locations?.FirstOrDefault();

                    //// Checks if the location is not null
                    if (location != null)
                    {
                        //// Gets the location latitude and longitude
                        var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                        var placemark = placemarks?.FirstOrDefault();
                        if (placemark != null)
                        {     
                            lblAdminArea.Text = "Admin Area: " + placemark.AdminArea;
                            lblCountryName.Text = "Country Name:" + placemark.CountryName;
                            lblCountryCode.Text = "Country Code:" + placemark.CountryCode;
                            lblLocality.Text = placemark.Locality;
                            lblSubAdminArea.Text = "SubAdmin Area:" + placemark.SubAdminArea;
                            lblSublocality.Text = "SubLocality:" + placemark.SubLocality;
                            lblPostalcode.Text = "PostalCode:" + placemark.PostalCode;
                        }
                        //// Gets the current location
                        var locationArea = await Geolocation.GetLastKnownLocationAsync();

                        //// Checks if the location is not null
                        if (locationArea != null)
                        {
                            lblLatitude.Text = "Latitude: " + locationArea.Latitude.ToString();
                            lblLongitude.Text = "Longitude:" + locationArea.Longitude.ToString();
                        }
                        var userid = DependencyService.Get<IFirebaseAuthenticator>().UserId();
                        NotesData notes = await this.firebaseHelper.GetNotesData(this.value, userid);
                        //// Updates the notes when DeleteNotes method is called
                        notes = new NotesData()
                        {

                            Title = notes.Title,
                            Notes = notes.Notes,
                            ColorNote = notes.ColorNote,
                            LabelData = notes.LabelData,
                            Latitude=notes.Latitude,
                            Longitude=notes.Longitude,
                            Area=notes.Area
                            
                        };
                        //// Updates the data to firebase
                        this.firebaseHelper.AddLocationArea(this.value, notes, lblLocality.Text ,lblLatitude.Text,lblLongitude.Text);
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await this.DisplayAlert("Failed", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await this.DisplayAlert("Failed", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Failed", ex.Message, "OK");
            }
        }
    }
}