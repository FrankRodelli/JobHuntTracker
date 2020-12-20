using JobHuntTrackerXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace JobHuntTrackerXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}