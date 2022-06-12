using BookStoreXam.Models;
using BookStoreXam.Views;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BookStoreXam.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class UpdateItemViewModel : BaseViewModel
    {
        private string itemId;
        private string bookname;
        private string price;
        private string category;
        private string author;

        public string Id { get; set; }
        public Command UpdateCommand { get; }
        public Command CancelCommand { get; }

        public UpdateItemViewModel()
        {
            Title = "Votre Livre";
            UpdateCommand = new Command(OnUpdate, ValidateUpdate);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => UpdateCommand.ChangeCanExecute();
        }


        private bool ValidateUpdate()
        {
            double outprice;
            return !String.IsNullOrWhiteSpace(bookname)
                && !String.IsNullOrWhiteSpace(price)
                && double.TryParse(price, out outprice)
                && !String.IsNullOrWhiteSpace(category)
                && !String.IsNullOrWhiteSpace(author);
        }
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

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Bookname = item.Bookname;
                Price = item.Price;
                Category = item.Category;
                Author = item.Author;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnUpdate()
        {
            Book newItem = new Book()
            {
                Id = itemId,
                Bookname = Regex.Replace(Bookname.Trim(), @"\s+", " ").ToUpper(),
                Price = Price.Replace(',', '.').Trim(),
                Category = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Regex.Replace(Category.Trim(), @"\s+", " ").ToLower()),
                Author = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Regex.Replace(Author.Trim(), @"\s+", " ").ToLower()),                
            };

            await DataStore.UpdateItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");

        }
    }
}

