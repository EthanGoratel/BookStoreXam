using BookStoreXam.Models;
using BookStoreXam.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookStoreXam.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string bookname;
        private string price;
        private string category;
        private string author;
        public Command DeleteItemCommand { get; }

        public Command UpdateItemCommand { get; }
        public string Id { get; set; }

        public string Bookname
        {
            get => bookname;
            set => SetProperty(ref bookname, value);
        }
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public string Author
        {
            get => author;
            set => SetProperty(ref author, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public ItemDetailViewModel()
        {
            Title = "Votre Livre";
            DeleteItemCommand = new Command(OnDeleteItem);
            UpdateItemCommand = new Command(OnUpdateItem);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Bookname = item.Bookname;
                Price = item.Price+"€";
                Category = item.Category;
                Author = item.Author;
                
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnDeleteItem(object obj)
        {
            await DataStore.DeleteItemAsync(ItemId);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnUpdateItem(object obj)
        {
           
            await Shell.Current.GoToAsync($"{nameof(UpdateItemPage)}?{nameof(UpdateItemViewModel.ItemId)}={itemId}");
        }
    }
}
