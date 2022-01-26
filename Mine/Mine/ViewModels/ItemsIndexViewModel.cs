using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Mine.Models;
using Mine.Views;

namespace Mine.ViewModels
{
    public class ItemsIndexViewModel : BaseViewModel
    {
        public ObservableCollection<ItemModel> DataSet { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsIndexViewModel()
        {
            Title = "Items";
            DataSet = new ObservableCollection<ItemModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemCreatePage, ItemModel>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as ItemModel;
                DataSet.Add(newItem);
                await DataStore.CreateAsync(newItem);
            });
        }

        /// <summary>
        /// Delete record from the system (if record exists)
        /// </summary>
        /// <param name="data">Record to Delete</param>
        /// <returns>True if Deleted, False if doesn't exist</returns>
        public async Task<bool> DeleteAsync (ItemModel data)
        {
            // Check if record exists, if it doesn't, returns null
            var record = await ReadAsync(data.Id);
            if (record != null)
            {
                // Remove from local data set cache
                DataSet.Remove(data);

                // Call to remove it from Data Store
                var result = await DataStore.DeleteAsync(data.Id);

                return result;
            }
            return false; 
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DataSet.Clear();
                var items = await DataStore.IndexAsync(true);
                foreach (var item in items)
                {
                    DataSet.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        ///  Read an item from the datastore
        /// </summary>
        /// <param name="id">ID of the Records</param>
        /// <returns>Record from ReadAsync</returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            var result = await DataStore.ReadAsync(id);
            return result;
        }
    }
}