﻿using FakinReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakinReader.Services
{
    public class MockDataStore1 : IDataStore<Item>
    {
        #region Constructors
        public MockDataStore1()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item, first data store", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item, first data store", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item, first data store", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item, first data store", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item, first data store", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item, first data store", Description="This is an item description." }
            };
        }
        #endregion Constructors

        #region Fields
        private readonly List<Item> items;
        #endregion Fields

        #region Methods
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
        #endregion Methods
    }
}