using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IApiService
    {
        Task<IEnumerable<TodoList>> GetTodoLists();
        Task<IEnumerable<TodoTask>> GetTasks();
        Task AddTask(TodoTask task);
        Task UpdateTask(TodoTask task);
        Task DeleteTask(int taskId);
        Task AddList(TodoList list);
        Task DeleteList(int listId);
    }

