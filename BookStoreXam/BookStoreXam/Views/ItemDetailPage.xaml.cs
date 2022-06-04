using BookStoreXam.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BookStoreXam.Views
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