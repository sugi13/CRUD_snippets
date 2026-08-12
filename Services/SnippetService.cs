using CRUDWithFluxor.models.Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace CRUDWithFluxor.Services

{

    public class SnippetService

    {

        private readonly HttpClient _http;
        public SnippetService(HttpClient http)

        {

            _http = http;

        }

        // get users
        public async Task<List<SnippetModel>> GetAllSnippets()

        {

            try

            {

                var response = await _http.GetAsync("/snippets");

                response.EnsureSuccessStatusCode();

                var new_snippets = await response.Content.ReadFromJsonAsync<List<SnippetModel>>();

                return new_snippets ?? new List<SnippetModel>(); // returns snippet data otherwise empty list

            }

            catch (Exception ex)

            {

                // Handle the exception (e.g., log it, rethrow it, etc.)

                Console.WriteLine($"An error occurred while fetching snippets: {ex.Message}");

                return new List<SnippetModel>();

            }

        }

        // 4. create user
        public async Task<SnippetModel?> CreateUser(SnippetModel newSnippet)

        {

            // 5.

            var response = await _http.PostAsJsonAsync("/snippets", newSnippet);

            response.EnsureSuccessStatusCode();

            // 6.

            return await response.Content.ReadFromJsonAsync<SnippetModel>();

        }



    } // delete user

    }