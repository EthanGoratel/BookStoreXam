using BookStoreXam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStoreXam.Services
{
    public class BookDataStore : IDataStore<Book>
    {
        readonly List<Book> items;

        public BookDataStore()
        {
            items = new List<Book>()
            {
                new Book { Id = Guid.NewGuid().ToString(), Bookname = "First item", Author="This is an item description.", Category="cat", Price="1euro" },
                
            };
        }

        public async Task<bool> AddItemAsync(Book item)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            { return true; };

            var client = new HttpClient(handler);
            
            var serial = JsonConvert.SerializeObject(item);
            var json = new StringContent(serial, System.Text.Encoding.UTF8, "application/json");
            var newitem = await client.PostAsync("https://localhost:7177/api/Books", json);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Book item)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            { return true; };

            var client = new HttpClient(handler);
            var serial = JsonConvert.SerializeObject(item);
            var json = new StringContent(serial, System.Text.Encoding.UTF8, "application/json");
            var newitem = await client.PutAsync("https://localhost:7177/api/Books/" + item.Id, json);
            return await Task.FromResult(true);

        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            { return true; };

            var client = new HttpClient(handler);
            var olditem = await client.DeleteAsync("http://localhost:5177/api/Books/" + id);
       

            return await Task.FromResult(true);
        }
 
        public async Task<Book> GetItemAsync(string id)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            { return true; };

            var client = new HttpClient(handler);
            var json = await client.GetStringAsync("http://localhost:5177/api/Books/"+id);
            var Book = JsonConvert.DeserializeObject<Book>(json);
            return Book;
        }

        public async Task<IEnumerable<Book>> GetItemsAsync(bool forceRefresh = false)
        {
            HttpClientHandler handler= new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            { return true; };

            var client = new HttpClient(handler);
            var json = await client.GetStringAsync("http://localhost:5177/api/Books");
            var Books = JsonConvert.DeserializeObject<IList<Book>>(json);
            return Books;
        }
    }
}