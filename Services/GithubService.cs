using System.Net.Http;
using System.Threading.Tasks;
using BlazorSampleApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorSampleApp.Services
{
    public class GithubService
    {
        public GithubService(HttpClient http)
        {
            Http = http;
        }

        public HttpClient Http { get; }

        public async Task<GithubProfileDtoModel> GetProfile(string name)
        {
            var newProfile = await Http.GetJsonAsync<GithubProfileDtoModel>($"https://api.github.com/users/{name}");

            return newProfile;
        }
    }
}