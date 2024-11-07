using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            string name = (string)nameTextBox.Text;
        }

        private void desTextBox_TextChanged(object sender, EventArgs e)
        {
            string des = (string)desTextBox.Text;
        }

        private void activeTasks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public class Task
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Id { get; set; }
            public bool TaskStatus { get; set; }
            public Task(string name, string description, int id, bool taskStatus)
            {
                Name = name;
                Description = description;
                Id = id;
                TaskStatus = taskStatus;
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string taskName = nameTextBox.Text;
            string description = desTextBox.Text;
            int id = tasks.Count + 1;
            bool taskDone = false;

            Task newTask = new Task(taskName, description, id, taskDone);
            tasks.Add(newTask);

            // Update the ListBox to display tasks
            activeTasks.Items.Clear(); // Clear existing items
            foreach (var task in tasks)
            {
                activeTasks.Items.Add($"{task.Name} - {task.Description} (ID: {task.Id})");
            }

            Debug.WriteLine($"Created new task: {newTask.Name}, {newTask.Description}, {newTask.Id}, {newTask.TaskStatus}");
        }
    }
}
