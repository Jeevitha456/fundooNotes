using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GeoLocation : ContentPage
	{
		public GeoLocation ()
		{
			InitializeComponent ();
		}

        private async void btnLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                string address = txtAddress.Text;
                if (!string.IsNullOrEmpty(address))
                {

               
                    var locations = await Geocoding.GetLocationsAsync(address);
                    var location = locations?.FirstOrDefault();

                    if (location != null)
                    {
                        var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                        var placemark = placemarks?.FirstOrDefault();
                        if (placemark != null)
                        {
                            string add=this.txtAddress.Text;
                            lblAdminArea.Text = "Admin Area: " + placemark.AdminArea;
                            lblCountryName.Text = "Country Name:" + placemark.CountryName;
                            lblCountryCode.Text = "Country Code:" + placemark.CountryCode;
                            lblLocality.Text = "Locality:" + placemark.Locality;
                            lblSubAdminArea.Text = "SubAdmin Area:" + placemark.SubAdminArea;
                            lblSublocality.Text = "SubLocality:" + placemark.SubLocality;
                            lblPostalcode.Text = "PostalCode:" + placemark.PostalCode;
                        }
                        
                    }
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }

        }

        private async void btnLocation1_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    lblLatitude.Text = "Latitude: " + location.Latitude.ToString();
                    lblLongitude.Text = "Longitude:" + location.Longitude.ToString();
                }
                
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }
    }

        //private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //    //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
        //    //var location = await Geolocation.GetLocationAsync(request);

        //    //if (location != null)
        //    //{
        //    //    if (location.IsFromMockProvider)
        //    //    {
        //    //        // location is from a mock provider
        //    //    }
        //    //}
        //}
    }

