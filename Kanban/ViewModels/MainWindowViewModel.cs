using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ReactiveUI;
using System.Reactive;
using Kanban.Models;


namespace Kanban.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Task>[] Tasks { get; set; }
        //public ObservableCollection<Task> Tasks { get; set; }
        public MainWindowViewModel()
        {
            /*Tasks = new ObservableCollection<Task> { new Task("1_1","Сделай это",""),
                                                   new Task("1_2","Сделай то","")};*/
            Tasks = new ObservableCollection<Task>[3] { new ObservableCollection<Task> { new Task("1_1","Сделай это","Kanban/Assets/avalonia-logo.ico"),
                                                                                         new Task("1_2","Сделай то","Kanban/Assets/avalonia-logo.ico")},
                                                        new ObservableCollection<Task> {  new Task("2_1","Сделай это", "Kanban/Assets/avalonia-logo.ico") },
                                                        new ObservableCollection<Task> { new Task("3_1","Сделай это","Kanban/Assets/avalonia-logo.ico"),
                                                                                         new Task("3_2","Сделай то","Kanban/Assets/avalonia-logo.ico")}
            };

            Exit = ReactiveCommand.Create(() => Environment.Exit(0));
            NewKanban = ReactiveCommand.Create(() =>ClearTasks());
            Add = ReactiveCommand.Create<string, int>((column) => AddTask(column));
        }

        public ReactiveCommand<Unit, Unit> Exit { get; }
        public ReactiveCommand<Unit, int> NewKanban { get; }
        public ReactiveCommand<string, int> Add { get; }
        
            public int ClearTasks()
        {
            Tasks[0].Clear();
            Tasks[1].Clear();
            Tasks[2].Clear();
            return 0;
        }
        private int AddTask(string column)
        {
            int ind = int.Parse(column);
            Tasks[ind].Add(new Task("", "", "Kanban/Assets/avalonia-logo.ico"));
            return 0;
        }
        
    }

    
}
