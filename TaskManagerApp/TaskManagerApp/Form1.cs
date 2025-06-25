using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;


namespace TaskManagerApp
{
    public partial class Form1 : Form
    {
        List<TaskItem> tasks = new List<TaskItem>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTasksFromFile();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TaskItem task = new TaskItem
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                Deadline = dtpDeadline.Value,
                IsCompleted = false
            };

            tasks.Add(task);
            RefreshTaskList();
            SaveTasksToFile();
        }
        private void RefreshTaskList()
        {
            listBoxTasks.Items.Clear();
            foreach (var task in tasks)
            {
                listBoxTasks.Items.Add(task);
            }
        }
        private void SaveTasksToFile()
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText("tasks.json", json);
        }
        private void LoadTasksFromFile()
        {
            if (File.Exists("tasks.json"))
            {
                string json = File.ReadAllText("tasks.json");
                tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json);
                RefreshTaskList();
            }
        }
        private void btnMarkDone_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedItem is TaskItem selectedTask)
            {
                selectedTask.IsCompleted = true;
                RefreshTaskList();
                SaveTasksToFile();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedItem is TaskItem selectedTask)
            {
                tasks.Remove(selectedTask);
                RefreshTaskList();
                SaveTasksToFile();
            }
        }
    }
}
