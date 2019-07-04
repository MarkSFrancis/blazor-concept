using BlazorSampleApp.UI.Configuration;
using BlazorSampleApp.UI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorSampleApp.UI.Services
{
    public class TodoService
    {
        public TodoService(HttpClient http, ConfigService config)
        {
            Http = http;
            Config = config;
        }

        public HttpClient Http { get; }
        public ConfigService Config { get; }

        public async Task<List<TodoEntryDtoModel>> Get()
        {
            List<TodoEntryDtoModel> results = await Http.GetJsonAsync<List<TodoEntryDtoModel>>(await GetApiPath("todo"));
            return results;
        }

        public async Task<TodoEntryDtoModel> Add(TodoEntryDtoModel todo)
        {
            if (todo.CreatedOn == default)
            {
                todo.CreatedOn = DateTime.UtcNow;
            }

            return await Http.PostJsonAsync<TodoEntryDtoModel>(await GetApiPath("todo"), todo);
        }

        public async Task Complete(string id, DateTime completedOn)
        {
            await Http.SendJsonAsync(new HttpMethod("PATCH"), await GetApiPath("todo/" + id), new
            {
                CompletedOn = completedOn
            });
        }

        public async Task Complete(TodoEntryDtoModel todo)
        {
            todo.CompletedOn = DateTime.UtcNow;
            await Complete(todo.Id, todo.CompletedOn.Value);
        }

        private async Task<string> GetApiPath(string relativePath)
        {
            Config config = await Config.GetAsync();

            var apiUrl = config.Uris.Api;

            return apiUrl + relativePath;
        }
    }
}