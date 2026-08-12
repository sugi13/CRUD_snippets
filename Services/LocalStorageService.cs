// this line give access to IJSRuntime interface for interacting with JavaScript
// IJSRuntime - is a interface that provides methods to invoke JavaScript functions from .NET code in Blazor applications for using browser localStorage.


using Microsoft.JSInterop; 
using System.Text.Json;

namespace CRUDWithFluxor.Services

{
    public class LocalStorageService
    {
        private readonly IJSRuntime _js;

        public LocalStorageService(IJSRuntime js)
        {
            _js = js;
        }

        // set value
        public async Task SetItemAsync<T>(string key, T value)
        {
            // converting the value into json string and store it in LS
            var json = JsonSerializer.Serialize(value);

           // Console.WriteLine($"Saving data: {json}");


            // VoidAsync - it returns nothing

            await _js.InvokeVoidAsync("localStorage.setItem", key, json);
            // 1. The first args calls the setItem method of the localStorage object in JavaScript.
        }

        // get value
        public async Task<T?> GetItemAsync<T>(string key)
        {

            // getItem - returns data, so InvokeAsync is used.

            var json = await _js.InvokeAsync<string?>(
                "localStorage.getItem",
                key);


           // Console.WriteLine($"Reading LocalStorage: {json}");

            // check if the json is null, then return default value of T, otherwise deserialize the json string into an object of type T and return it.
            if (json == null)
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        // Remove value
        public async Task RemoveItemAsync(string key)
        {
            await _js.InvokeVoidAsync(
                "localStorage.removeItem",
                key);
        }

    }
}
