using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Drawing;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        string FilePath = "C:\\Users\\ŠimonŠnyta\\source\\repos\\TaskManager\\TaskManager\\tasks.xml";

        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load tasks when the application starts
            tasks = LoadTasksFromXML(FilePath);
            RefreshTaskLists();

            // Enable custom drawing for the doneTasks ListBox
            doneTasks.DrawMode = DrawMode.OwnerDrawFixed;
            doneTasks.DrawItem += DoneTasks_DrawItem;
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

            // Parameterless constructor for XML serialization
            public Task() { }

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
            SaveToXML(FilePath);

            // Update the ListBoxes
            RefreshTaskLists();

            Debug.WriteLine($"Created new task: {newTask.Name}, {newTask.Description}, {newTask.Id}, {newTask.TaskStatus}");
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            // Check if a task is selected in the activeTasks list
            if (activeTasks.SelectedIndex != -1)
            {
                // Get the selected task text
                string selectedTask = activeTasks.SelectedItem.ToString();

                // Parse the task ID from the selected item string
                int idStart = selectedTask.LastIndexOf("(ID: ") + 5;
                int idEnd = selectedTask.LastIndexOf(")");
                int taskId = int.Parse(selectedTask.Substring(idStart, idEnd - idStart));

                // Find the task in the tasks list using the parsed ID
                Task taskToComplete = tasks.FirstOrDefault(t => t.Id == taskId);
                if (taskToComplete != null)
                {
                    // Update the TaskStatus to true (completed)
                    taskToComplete.TaskStatus = true;

                    // Update the XML file
                    SaveToXML(FilePath);

                    // Refresh the ListBoxes
                    RefreshTaskLists();
                }
            }
        }

        private void RefreshTaskLists()
        {
            activeTasks.Items.Clear();
            doneTasks.Items.Clear();

            // Populate the activeTasks ListBox with incomplete tasks
            foreach (var task in tasks.Where(t => !t.TaskStatus))
            {
                activeTasks.Items.Add($"{task.Name} - {task.Description} (ID: {task.Id})");
            }

            // Populate the doneTasks ListBox with completed tasks
            foreach (var task in tasks.Where(t => t.TaskStatus))
            {
                doneTasks.Items.Add($"{task.Name} - {task.Description} (ID: {task.Id})");
            }
        }

        private void SaveToXML(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, tasks);
                    Debug.WriteLine("Tasks have been successfully saved to XML.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving tasks to XML: {ex.Message}");
            }
        }

        private List<Task> LoadTasksFromXML(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));

                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    Debug.WriteLine("XML file not found.");
                    return new List<Task>(); // Return an empty list if the file doesn't exist
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (List<Task>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading tasks from XML: {ex.Message}");
                return new List<Task>(); // Return an empty list in case of error
            }
        }

        private void DoneTasks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string taskText = doneTasks.Items[e.Index].ToString();

            // Set background and text color for completed tasks
            e.DrawBackground();
            using (Brush textBrush = new SolidBrush(Color.Gray))
            {
                e.Graphics.DrawString(taskText, e.Font, textBrush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void doneTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Prevent selection in the doneTasks ListBox
            doneTasks.ClearSelected();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
