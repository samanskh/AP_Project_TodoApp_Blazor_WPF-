using System.Windows;
using TodoApp2.Data;
using TodoApp2.ViewModels;

namespace TodoApp2
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); 

            ListViewModel = new TodoListViewModel(new ApiService()); 
            TaskViewModel = new TodoTaskViewModel(new ApiService()); 


            ListSection.DataContext = ListViewModel;
            TaskSection.DataContext = TaskViewModel;
            TaskSection2.DataContext = TaskViewModel;
            Loaded += MainWindow_Loaded;
        }
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await ListViewModel.LoadTodoLists();
            await TaskViewModel.LoadTasks();
        }

        public TodoListViewModel ListViewModel { get; set; }
        public TodoTaskViewModel TaskViewModel { get; set; }

        private async void AddList_Click(object sender, RoutedEventArgs e)
        {
            await ListViewModel.AddList();
        }

        private async void RemoveList_Click(object sender, RoutedEventArgs e)
        {
            await ListViewModel.DeleteList();
        }

        private async void AddTask_Click(object sender, RoutedEventArgs e)
        {
            await TaskViewModel.AddTask();
        }

        private async void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            await TaskViewModel.UpdateTask();
        }

        private async void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            await TaskViewModel.DeleteTask();
        }
    }
}
