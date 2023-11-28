using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vy
{
    public partial class Form1 : Form
    {
        private Timer updateTimer;
        private int refreshInterval = 1000;
        private Process[] processes;
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            LoadProcesses();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Process selectedProcess = processes[listView1.SelectedIndices[0]];
                DisplayProcessDetails(selectedProcess);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            refreshInterval = (int)numericUpDown1.Value;
            updateTimer.Interval = refreshInterval;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void InitializeTimer()
        {
            updateTimer = new Timer();
            updateTimer.Interval = refreshInterval;
            updateTimer.Tick += UpdateProcessList;
            updateTimer.Start();
        }
        private void UpdateProcessList(object sender, EventArgs e)
        {
            LoadProcesses();
        }
        private void LoadProcesses()
        {
            processes = Process.GetProcesses();
            listView1.Items.Clear();
            foreach (Process process in processes)
            {
                ListViewItem item = new ListViewItem(process.ProcessName);
                item.SubItems.Add(process.Id.ToString());
                item.SubItems.Add(process.Responding ? "Yes" : "No");
                listView1.Items.Add(item);
            }
        }
        private void DisplayProcessDetails(Process process)
        {
            listView2.Items.Clear();
            ListViewItem idItem = new ListViewItem("Process ID");
            idItem.SubItems.Add(process.Id.ToString());
            listView2.Items.Add(idItem);
            ListViewItem startTimeItem = new ListViewItem("Start Time");
            startTimeItem.SubItems.Add(process.StartTime.ToString());
            listView2.Items.Add(startTimeItem);
            ListViewItem cpuTimeItem = new ListViewItem("CPU Time");
            cpuTimeItem.SubItems.Add(process.TotalProcessorTime.ToString());
            listView2.Items.Add(cpuTimeItem);
            ListViewItem threadCountItem = new ListViewItem("Thread Count");
            threadCountItem.SubItems.Add(process.Threads.Count.ToString());
            listView2.Items.Add(threadCountItem);
        }
    }
}
