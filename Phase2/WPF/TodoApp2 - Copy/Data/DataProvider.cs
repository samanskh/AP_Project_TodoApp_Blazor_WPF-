using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoApp2.Models;

namespace TodoApp2.Data
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;
        public ObservableCollection<TodoTask> TodoTasks { get; set; }
        public ObservableCollection<TodoList> TodoLists { get; set; }

        public ApiService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7229/")
            };
            TodoTasks = new ObservableCollection<TodoTask>();
            TodoLists = new ObservableCollection<TodoList>();
        }

        public async Task<IEnumerable<TodoList>> GetTodoLists()
        {
            var response = await _client.GetFromJsonAsync<IEnumerable<TodoList>>("api/TodoTasks/todoLists");
            return response;
        }

        public async Task<IEnumerable<TodoTask>> GetTasks()
        {
            var response = await _client.GetFromJsonAsync<IEnumerable<TodoTask>>("api/TodoTasks/tasks");
            return response;
        }

        public async Task AddTask(TodoTask task)
        {
            await _client.PostAsJsonAsync("api/TodoTasks/tasks", task);
            await SyncTasksFromServer();
        }

        public async Task UpdateTask(TodoTask task)
        {
            await _client.PutAsJsonAsync($"api/TodoTasks/tasks/{task.Id}", task);
            await SyncTasksFromServer();
        }

        public async Task DeleteTask(int taskId)
        {
            await _client.DeleteAsync($"api/TodoTasks/tasks/{taskId}");
            await SyncTasksFromServer();
        }

        public async Task AddList(TodoList list)
        {
            await _client.PostAsJsonAsync("api/TodoTasks/todoLists", list);
            await SyncListsFromServer();
        }
        public async Task UpdateList(TodoList task)
        {
            await _client.PutAsJsonAsync($"api/TodoTasks/todoLists/{task.Id}", task);
            await SyncTasksFromServer();
        }
        public async Task DeleteList(int listId)
        {
            await _client.DeleteAsync($"api/TodoTasks/todoLists/{listId}");
            await SyncListsFromServer();
        }

        private async Task SyncTasksFromServer()
        {
            var tasks = await GetTasks();
            TodoTasks.Clear();
            foreach (var task in tasks)
            {
                TodoTasks.Add(task);
            }
        }

        private async Task SyncListsFromServer()
        {
            var lists = await GetTodoLists();
            TodoLists.Clear();
            foreach (var list in lists)
            {
                TodoLists.Add(list);
            }
        }
    }
}
