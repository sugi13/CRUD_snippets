using CRUDWithFluxor.models.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CRUDWithFluxor.Services
{
    public class UserService 
{
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        // get users         
        public async Task<List<CarDetailModel>> GetUsers()
        {
            try
            {
                var response = await _http.GetAsync("/users");
                response.EnsureSuccessStatusCode();
                var users = await response.Content.ReadFromJsonAsync<List<CarDetailModel>>();
                return users ?? new List<CarDetailModel>(); // returns users data otherwise empty list
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, rethrow it, etc.)
                Console.WriteLine($"An error occurred while fetching users: {ex.Message}");
                return new List<CarDetailModel>();
            }
        }

        // 4. create user 
        public async Task<CarDetailModel?> CreateUser(CarDetailModel newUser)
        {
            // 5.
            var response = await _http.PostAsJsonAsync("/users", newUser);
            response.EnsureSuccessStatusCode();
            // 6.
            return await response.Content.ReadFromJsonAsync<CarDetailModel>();
        }

        // delete user 
        public async Task DeleteUser(int id)
        {
            var response = await _http.DeleteAsync($"/users/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}