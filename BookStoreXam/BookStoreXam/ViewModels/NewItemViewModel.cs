using BookStoreXam.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookStoreXam.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string bookname;
        private string price;
        private string category;
        private string author;
 
        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Book newItem = new Book()
            {
                Bookname = Regex.Replace(Bookname.Trim(), @"\s+", " ").ToUpper(),
                Price = Price.Replace(',', '.').Trim(),
                Category = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Regex.Replace(Category.Trim(), @"\s+", " ").ToLower()),
                Author = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Regex.Replace(Author.Trim(), @"\s+", " ").ToLower()),
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        
    }
}
