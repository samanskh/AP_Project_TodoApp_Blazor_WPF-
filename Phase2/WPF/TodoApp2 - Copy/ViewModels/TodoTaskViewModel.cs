using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TodoApp2.Data;
using TodoApp2.Models;

namespace TodoApp2.ViewModels
{
    public class TodoTaskViewModel : MainViewModel
    {
        private readonly IApiService _apiService;
        private TodoTask _selectedTask;
        private string _newTaskName;
        private bool _isNewTaskStarred;
        private bool _isNewTaskCompleted;
        private int _newListId;
        private DateTime _newTaskDueDate;
        public TodoTaskViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public ObservableCollection<TodoTask> Tasks { get; } = new();
        public DateTime NewTaskDueDate
        {
            get { return _newTaskDueDate; }
            set { _newTaskDueDate = value; RaisePropertyChanged(); }
        }
        public int NewListId
        {
            get { return _newListId; }
            set { _newListId = value; RaisePropertyChanged(); }
        }
        public TodoTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                RaisePropertyChanged();
            }
        }
        public bool IsNewTaskCompleted
        {
            get { return _isNewTaskCompleted; }
            set { _isNewTaskCompleted =  value; RaisePropertyChanged(); }
        }
        public bool IsNewTaskStarred
        {
            get { return _isNewTaskStarred; }
            set { _isNewTaskStarred = value; RaisePropertyChanged(); }
        }
        public string NewTaskName
        {
            get => _newTaskName;
            set
            {
                _newTaskName = value;
                RaisePropertyChanged();
            }
        }

        public async Task LoadTasks()
        {
            if (Tasks.Any()) return;

            var tasks = await _apiService.GetTasks();

            if (tasks != null)
            {
                Tasks.Clear();
                foreach (var task in tasks)
                {
                    Tasks.Add(new TodoTask
                    {
                        Id = task.Id,
                        Name = task.Name,
                        IsStarred = task.IsStarred,
                        IsCompleted = task.IsCompleted,
                        ListId = task.ListId,
                        DueDate = task.DueDate
                    });
                }
            }
        }

        public async Task AddTask()
        {
            var newTask = new TodoTask { Name = NewTaskName , IsStarred = IsNewTaskStarred , IsCompleted = IsNewTaskCompleted , 
            ListId = NewListId , DueDate = NewTaskDueDate};
            await _apiService.AddTask(newTask);
            Tasks.Add(newTask);
            NewTaskName = string.Empty;
            IsNewTaskCompleted = false;
            IsNewTaskStarred = false;
            NewListId = 0;
            NewTaskDueDate = DateTime.Now;

        }

        public async Task UpdateTask()
        {
            await _apiService.UpdateTask(SelectedTask);
        }

        public async Task DeleteTask()
        {
            await _apiService.DeleteTask(SelectedTask.Id);
            Tasks.Remove(SelectedTask);
            SelectedTask = null;
        }
    }
}
