using System.Net.Http;
using System.Net.Http.Json;

namespace WEB_VitalConnectApi.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener lista
        public async Task<List<T>?> GetListAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<T>>(endpoint);
            }
            catch
            {
                return new List<T>();
            }
        }

        // Obtener uno por id
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(endpoint);
            }
            catch
            {
                return default;
            }
        }

        // Crear
        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            return response.IsSuccessStatusCode;
        }

        // Actualizar
        public async Task<bool> PutAsync<T>(string endpoint, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            return response.IsSuccessStatusCode;
        }

        // Eliminar
        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        // Cambiar estado
        public async Task<bool> PatchAsync(string endpoint)
        {
            var response = await _httpClient.PatchAsync(endpoint, null);
            return response.IsSuccessStatusCode;
        }
    }
}