using BookStoreXam.Models;
using BookStoreXam.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookStoreXam.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Book Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();

            
        }
    }

   
}