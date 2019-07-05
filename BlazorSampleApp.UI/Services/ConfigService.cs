using BlazorSampleApp.UI.Configuration;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorSampleApp.UI.Services
{
    public class ConfigService
    {
        public ConfigService(HttpClient client)
        {
            _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);
            _client = client;
        }

        private readonly HttpClient _client;

        private readonly ManualResetEventSlim _cacheLoadedEvent = new ManualResetEventSlim();
        private readonly object _isLoadingOrLoadedLock = new object();
        private volatile bool _isLoadingOrLoaded = false;

        private Config _cachedConfig;

        public async Task<Config> GetAsync()
        {
            bool loadFromExternalSource;
            lock (_isLoadingOrLoadedLock)
            {
                loadFromExternalSource = !_isLoadingOrLoaded;
                if (!_isLoadingOrLoaded)
                {
                    _isLoadingOrLoaded = true;
                }
            }

            if (loadFromExternalSource)
            {
                await GetFromRemote();
                _cacheLoadedEvent.Set();
            }

            _cacheLoadedEvent.Wait();
            return _cachedConfig;
        }

        private async Task GetFromRemote()
        {
            var result = await _client.GetJsonAsync<Config>("appsettings.json");

            _cachedConfig = result;
        }
    }
}
