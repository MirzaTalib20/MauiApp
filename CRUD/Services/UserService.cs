using CRUD.Models;
using Microsoft.Data.SqlClient;
using System.Net.Http.Json;
using System.Text.Json;

namespace CRUD.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        JsonSerializerOptions _serializerOptions;

        public UserService()
        {

            var handler = new HttpClientHandler
            {
                // For local development, bypass SSL validation
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7250") // Replace with correct URL
            };
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        }

        // Fetch all users
        public async Task<List<User>> GetUsersAsync()
        {
           
            List<User> users = new List<User>();


            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Student/GetStudents");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    users = JsonSerializer.Deserialize<List<User>>(json);
                  
                    return users ?? new List<User>();
                }
            }
            catch (Exception ex)
            {
            }

            return users;
        }
        // Fetch a single user
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/Student/GetStudentsById/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        // Create a new user
        public async Task<bool> AddUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Student/PostCreateStudents", user);
            return response.IsSuccessStatusCode;
        }


     

        // Update an existing user
        public async Task<bool> UpdateUserAsync(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Student/UpdateStudentsById/{user.id}", user);
            return response.IsSuccessStatusCode;
        }

        // Delete a user
        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Student/DeleteStudentById/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
