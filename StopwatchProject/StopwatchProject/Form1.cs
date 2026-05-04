using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StopwatchProject
{
    public partial class Form1 : Form
    {
        bool isPaused;
        int seconds = 0;
        int minutes = 0;
        int hours = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnStart.Visible = true;
            btnCancel.Visible = false;
            lblTimeDisplay.Text = "00:00:00";
            isPaused = true;

            // Run the timer in parallel
            var task = Task.Run(async () =>
            {
                while (Application.OpenForms["Form1"] != null) // Makes sure loop ends if app is closed
                {
                    if (!isPaused)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                        if (seconds >= 60)
                        {
                            seconds = 0;
                            minutes++;
                        }
                        if (minutes >= 60)
                        {
                            minutes = 0;
                            hours++;
                        }

                        // Prevents timer from incrementing
                        if (!isPaused)
                        {
                            lblTimeDisplay.Invoke((MethodInvoker)delegate // Safe UI editing
                            {
                                lblTimeDisplay.Text = $"{hours:00}:{minutes:00}:{seconds:00}";
                            });
                        }
                        else { seconds--; }
                        
                    }
                }
            });
        }

        private void btnStartStop_Click(object sender, EventArgs e) // Unpauses the stopwatch
        {
            btnStart.Visible = false;
            btnCancel.Visible = true;
            isPaused = false;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the time varibles
            seconds = 0;
            minutes = 0;
            hours = 0;
            lblTimeDisplay.Text = $"{hours:00}:{minutes:00}:{seconds:00}";
        }

        private void btnCancel_Click(object sender, EventArgs e) // Pauses the stopwatch
        {
            btnStart.Visible = true;
            btnCancel.Visible = false;
            isPaused = true;
        }
    }
}
