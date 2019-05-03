namespace Fundoo.View.Pages
{
    using System;
    using System.Linq;
    using Fundoo.Firebase;
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
        /// string
        /// </summary>
        private string value = null;

        /// <summary>
        /// Notes Data
        /// </summary>
        private NotesData note = null;

        /// <summary>
        /// GeoLocation
        /// </summary>
        /// <param name="key">the key.</param>
        /// <param name="notesData">notes data.</param>
        public GeoLocation(string key, NotesData notesData)
        {
            this.value = key;
            this.note = notesData;
            this.InitializeComponent();
        }

        /// <summary>
        /// Firebase Helper class
        /// </summary>
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        /// <summary>
        /// Location class
        /// </summary>
        /// <param name="sender">sender object.</param>
        /// <param name="e">events.</param>
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
                            lblLocality.Text = "Locality:" + placemark.Locality;
                            lblSubAdminArea.Text = "SubAdmin Area:" + placemark.SubAdminArea;
                            lblSublocality.Text = "SubLocality:" + placemark.SubLocality;
                            lblPostalcode.Text = "PostalCode:" + placemark.PostalCode;
                        }

                        //// Updates the data to firebase
                       this.firebaseHelper.AddLocationArea(this.value, this.note, lblLocality.Text);
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await this.DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await this.DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Faild", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Button Location1_Clicked
        /// </summary>
        /// <param name="sender">sender object.</param>
        /// <param name="e">events.</param>
        private async void BtnLocation1_Clicked(object sender, EventArgs e)
        {
            try
            {
                //// Gets the current location
                var location = await Geolocation.GetLastKnownLocationAsync();

                //// Checks if the location is not null
                if (location != null)
                {
                    lblLatitude.Text = "Latitude: " + location.Latitude.ToString();
                    lblLongitude.Text = "Longitude:" + location.Longitude.ToString();
                }

                //// Updates the current location in firebase class
                 this.firebaseHelper.AddLocation(this.value, this.note, lblLatitude.Text, lblLongitude.Text);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await this.DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await this.DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Faild", ex.Message, "OK");
            }
        }
    }
}