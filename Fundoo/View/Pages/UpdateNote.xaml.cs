using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateNote : ContentPage
	{
        string val = null;

		public UpdateNote (string value)
		{
            val = value;
			InitializeComponent ();
            UpdateData();
		}

        public void UpdateData()
        {

        }

    }
}