using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TodoApp2.Data;
using TodoApp2.Models;

namespace TodoApp2.ViewModels
{
    public class TodoListViewModel : MainViewModel
    {
        private readonly IApiService _apiService;
        private TodoList _selectedList;
        private string _newListName;

        public TodoListViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public ObservableCollection<TodoList> TodoLists { get; } = new();

        public TodoList SelectedList
        {
            get => _selectedList;
            set
            {
                _selectedList = value;
                RaisePropertyChanged();
            }
        }

        public string NewListName
        {
            get => _newListName;
            set
            {
                _newListName = value;
                RaisePropertyChanged();
            }
        }

        public async Task LoadTodoLists()
        {
            if (TodoLists.Any()) return;

            var todolists = await _apiService.GetTodoLists();

            if (todolists != null)
            {
                TodoLists.Clear();
                foreach (var todolist in todolists)
                {
                    TodoLists.Add(new TodoList { Id = todolist.Id, Name = todolist.Name });
                }
            }
        }

        public async Task AddList()
        {
            var newList = new TodoList { Name = NewListName };
            await _apiService.AddList(newList);
            TodoLists.Add(newList);
            NewListName = string.Empty;
        }
        public async Task UpdateList()
        {
            await _apiService.UpdateList(SelectedList);
        }
        public async Task DeleteList()
        {
            await _apiService.DeleteList(SelectedList.Id);
            TodoLists.Remove(SelectedList);
            SelectedList = null;
        }
    }
}
