using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

public class ApiService : IApiService
{
    private readonly HttpClient _client;

    public ObservableCollection<TodoTask> TodoTasks { get; set; }
    public ObservableCollection<TodoList> TodoLists { get; set; }

    public ApiService(HttpClient httpClient)
    {
        _client = httpClient;
        TodoTasks = new ObservableCollection<TodoTask>();
        TodoLists = new ObservableCollection<TodoList>();
    }

    public async Task<IEnumerable<TodoList>> GetTodoLists()
    {
        var response = await _client.GetFromJsonAsync<IEnumerable<TodoList>>("todoLists");
        return response;
    }

    public async Task<IEnumerable<TodoTask>> GetTasks()
    {
        var response = await _client.GetFromJsonAsync<IEnumerable<TodoTask>>("tasks");
        return response;
    }

    public async Task AddTask(TodoTask task)
    {
        await _client.PostAsJsonAsync("tasks", task);
        await SyncTasksFromServer();
    }

    public async Task UpdateTask(TodoTask task)
    {
        await _client.PutAsJsonAsync($"tasks/{task.Id}", task);
        await SyncTasksFromServer();
    }

    public async Task DeleteTask(int taskId)
    {
        await _client.DeleteAsync($"tasks/{taskId}");
        await SyncTasksFromServer();
    }

    public async Task AddList(TodoList list)
    {
        await _client.PostAsJsonAsync("todoLists", list);
        await SyncListsFromServer();
    }

    public async Task DeleteList(int listId)
    {
        await _client.DeleteAsync($"todoLists/{listId}");
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
