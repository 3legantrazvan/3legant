using Blazored.LocalStorage;

namespace Repositories
{
    public class LocalStorageRepository
    {
        private readonly ILocalStorageService _localStorage;

        public LocalStorageRepository(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _localStorage.GetItemAsync<string>(key);
        }

        public async Task SetValueAsync(string key, string value)
        {
            await _localStorage.SetItemAsync(key, value);
        }

        public async Task RemoveValueAsync(string key)
        {
            await _localStorage.RemoveItemAsync(key);
        }
    }
}
