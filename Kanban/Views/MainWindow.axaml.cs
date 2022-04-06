using Avalonia.Controls;
using System;
using Avalonia.Interactivity;
using Kanban.ViewModels;
using System.Collections.Generic;
using ReactiveUI;
using Kanban.Models;
using System.IO;

namespace Kanban.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void AboutClick(object sender, RoutedEventArgs e)
        {

            await new AboutDiagView().ShowDialog<string?>((Window)this.VisualRoot);
        }
        public void DeleteTask(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var context = this.DataContext as MainWindowViewModel;
            int ind = int.Parse((string)btn.Tag);
            Task tsk = btn.DataContext as Task;
            context.Tasks[ind].Remove(tsk);
        }

        public async void AddImage(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Task tsk = btn.DataContext as Task;

            var taskPath = new OpenFileDialog()
            {
                Title = "Open File",
                Filters = null
            }.ShowAsync((Window)this.VisualRoot);
            string[]? path = await taskPath;

            var context = this.DataContext as MainWindowViewModel;
            if (path != null)
                tsk.AddPathToImage(string.Join(@"\", path));
        }

        public async void Save(object sender, RoutedEventArgs e)
        {
            var taskPath = new SaveFileDialog()
            {
                Title = "Save File",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "Текстовый файл (.txt)", Extensions = { "txt" } });
            string? path = await taskPath.ShowAsync((Window)this.VisualRoot);

            var context = this.DataContext as MainWindowViewModel;
            try
            {
                if (path != null)
                    using (StreamWriter file = new StreamWriter(path))
                    {
                        for (int i = 0; i < context.Tasks.Length; i++)
                        {
                            for (int j = 0; j < context.Tasks[i].Count; j++)
                            {
                                string[] str = context.Tasks[i][j].Text.Replace("\r","").Split("\n");
                                file.WriteLine(i.ToString() + " " + str.Length);
                                file.WriteLine(context.Tasks[i][j].Header);
                                file.WriteLine(context.Tasks[i][j].pathName);

                                for (int k = 0; k < str.Length; k++)
                                {
                                    //str[k].Replace("\r", "");
                                    file.WriteLine(str[k]);
                                }
                                    
                                //file.WriteLine();
                            }
                        }

                    }
            }
            catch (Exception ex) { }
            
        }

        public async void Load(object sender, RoutedEventArgs e)
        {
            var taskPath = new OpenFileDialog()
            {
                Title = "Save File",
                Filters = null
            }.ShowAsync((Window)this.VisualRoot);
            string[]? path = await taskPath;

            var context = this.DataContext as MainWindowViewModel;
            try
            {
                if (path != null)
                    using (StreamReader file = new StreamReader(string.Join(@"\", path)))
                    {
                        string? line = "";
                        string header, text = "", pathName;
                        int ind, lineCount;
                        context.ClearTasks();
                        while ((line = file.ReadLine()) != null)
                        {
                            string[] str = line.Split(' ');
                            ind = int.Parse(str[0]);
                            lineCount = int.Parse(str[1]);
                            header = file.ReadLine();
                            pathName = file.ReadLine();
                            for (int j = 0; j < lineCount; j++)
                            {
                                text += file.ReadLine();
                                if (j != lineCount - 1)
                                    text += "\n";
                            }
                            context.Tasks[ind].Add(new Task(header, text, pathName));
                            text = "";
                        }

                    }
            }
            catch (Exception ex) { }
            
        }
    }
}
