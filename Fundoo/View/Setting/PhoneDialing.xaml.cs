using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Fundoo.View.Setting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneDialing : ContentPage
    {
        public PhoneDialing()
        {
            InitializeComponent();
        }

        private async void PhoneDialer_Clicked(object sender, EventArgs e)
        {
            await PlacePhoneCall(EntryNumber.Text);
        }
        
        public async Task PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}