using Fundoo.Model;
using Fundoo.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fundoo.View.HomePage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : MasterDetailPage
	{
		public Master ()
		{
			InitializeComponent ();
            this.MenuList = new List<MasterItems>();
            this.MenuList.Add(new MasterItems() { Title = "Notes", Icon = "Notes2.png", TargetType = typeof(DashBoard)});
            this.MenuList.Add(new MasterItems() { Title = "Reminders", Icon = "Reminders.png", TargetType = typeof(RemindersPage) });
            this.MenuList.Add(new MasterItems() { Title = "Create new label", Icon = "CreateLabel.png", TargetType = typeof(CreatePage) });
            this.MenuList.Add(new MasterItems() { Title = "Archive", Icon = "Archieve.png", TargetType = typeof(ArchievePage) });
            this.MenuList.Add(new MasterItems() { Title = "Trash", Icon = "Trash.png", TargetType = typeof(TrashPage) });
            this.MenuList.Add(new MasterItems() { Title = "Logout", TargetType = typeof(SignOut) });
            this.navigationDrawerList.ItemsSource = this.MenuList;
            this.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(DashBoard)));
            
		}
       
        /// <summary>
        /// Gets or sets the menu list.
        /// </summary>
        /// <value>
        /// The menu list.
        /// </value>
        public IList<MasterItems> MenuList{ get; set; }

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

    }
}