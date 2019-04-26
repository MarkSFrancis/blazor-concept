using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorSampleApp.UI.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorSampleApp.UI.Services
{
    public class TodoService
    {
        private const string ApiUrl = "https://localhost:5594/api/";

        public TodoService(HttpClient http)
        {
            Http = http;
            Http.BaseAddress = new Uri(ApiUrl);
        }

        public HttpClient Http { get; }

        public async Task<List<TodoEntryDtoModel>> Get()
        {
            var results = await Http.GetJsonAsync<List<TodoEntryDtoModel>>("todo");
            return results;
        }

        public async Task<TodoEntryDtoModel> Add(TodoEntryDtoModel todo)
        {
            return await Http.PostJsonAsync<TodoEntryDtoModel>("todo", todo);
        }

        public async Task Complete(string id, DateTime completedOn)
        {
            await Http.SendJsonAsync(new HttpMethod("PATCH"), "todo/" + id, new
            {
                CompletedOn = DateTime.UtcNow
            });
        }

        public async Task Complete(TodoEntryDtoModel todo)
        {
            todo.CompletedOn = DateTime.UtcNow;
            await Complete(todo.Id, todo.CompletedOn.Value);
        }
    }
}