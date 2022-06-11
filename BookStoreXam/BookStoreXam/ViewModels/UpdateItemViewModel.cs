using BookStoreXam.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookStoreXam.ViewModels
{
    public class UpdateItemViewModel : BaseViewModel
    {
        private string bookname;
        private string price;
        private string category;
        private string author;
 
        public UpdateItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(bookname)
                && !String.IsNullOrWhiteSpace(price)
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
                Id = Guid.NewGuid().ToString(),
                Bookname = Bookname,
                Price = Price,
                Category = Category,
                Author = Author,                
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
