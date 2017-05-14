using System;
using System.Threading;
using System.Windows.Forms;
using StudentGenetic.Properties;
using System.Diagnostics;

namespace StudentGenetic {
    public partial class MainForm : Form {
        /// <summary>
        /// Describe current state of application
        /// </summary>
        enum ProgramState {
            IdleWithoutSolution,
            Working,
            IdleWithSolution
        }

        ProgramState programState; // Internal app status
        Thread simulationThread; // Thread where the simulation is proceeded
        Thread uiUpdateThread; // User interface update thread  
        Simulation simulation;

        /// <summary>
        /// Creates new MainWindow with default simulation parameters.
        /// </summary>
        public MainForm() {
            programState = ProgramState.IdleWithoutSolution;    
            LoadSimulatioDefaults();
            // Generated function
            InitializeComponent();
        }

        /// <summary>
        /// Main simulation thread's body
        /// </summary>
        protected void SimulationTask() {
            while(programState == ProgramState.Working) {
                simulation.NextDay();
            }            
        }

        /// <summary>
        /// Body of user interface update thread
        /// </summary>
        protected void UiUpdateTask()
        {
            // Invokes event on UI thread
            // see http://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c
            this.Invoke((MethodInvoker)delegate {
                ShowIdleUi(false);
            });
            do {
                RefreshUi();
            // Sleeps for some time
            if (programState == ProgramState.Working)
                Thread.Sleep(500);
            } while (programState == ProgramState.Working);
            // Restore UI state if simulation in stopped
            this.Invoke((MethodInvoker)delegate {
                ShowIdleUi(true);
            });
        }

        /// <summary>
        /// Loads simulation default values using values from settings. <see cref="Simulation"/>
        /// </summary>
        private void LoadSimulatioDefaults()
        {
            simulation = new Simulation(Settings.Default.numberOfStudents);
            simulation.SimulationFinishedEvent += new EventHandler(OnSimulationFinished);
            simulation.ExceptedScore = 70;
            simulation.ExceptedKnowlidge = 100;
        }

        /// <summary>
        /// Force stops simulation. Could end in the middle of some event and levees UI in undefined state.
        /// </summary>
        protected void ForceStopSimulatin()
        {
            // Tries to softly stop simulation
            if (programState == ProgramState.Working)
                programState = ProgramState.IdleWithoutSolution;
            // Aborts simulation thread
            if (simulationThread != null && simulationThread.IsAlive)
                simulationThread.Abort();
            // Aborts update UI thread
            if (uiUpdateThread != null && uiUpdateThread.IsAlive)
                uiUpdateThread.Abort();
        }

        /// <summary>
        /// Stops simulation and select best student as selected student in UI
        /// </summary>
        /// <param name="sender">Unneeded</param>
        /// <param name="e">Unneeded</param>
        protected void OnSimulationFinished(object sender, EventArgs e)
        {
            programState = ProgramState.IdleWithSolution;
            // performs operation on UI thread 
            // see http://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c
            this.Invoke((MethodInvoker)delegate {
                studentNumericUpDown.Value = simulation.LifeEnv.FindBestStudentId();
                RefreshUi();
            });
            MessageBox.Show("Simulation finished", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Updates values in user interface.
        /// </summary>
        protected void RefreshUi()
        {
            // Finds student selected in UI
            Student student = simulation.LifeEnv.GetStudent((int)studentNumericUpDown.Value);
            // Performs operation on UI thread
            // see http://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c
            Invoke((MethodInvoker)delegate {
                // ProcessBar
                progressBar.Value = simulation.LifeEnv.DayOfLife;
                // Date
                dateLabel.Text = $"{simulation.Calendar.DayOfWeek}/{simulation.Calendar.Week}/{simulation.Calendar.Semester}";
                generationLabel.Text = simulation.Calendar.Generation.ToString();
                // Timetable
                ttMoLabel.Text = simulation.LifeEnv.Timetable[0].ToString();
                ttTuLabel.Text = simulation.LifeEnv.Timetable[1].ToString();
                ttWeLabel.Text = simulation.LifeEnv.Timetable[2].ToString();
                ttThLabel.Text = simulation.LifeEnv.Timetable[3].ToString();
                ttFrLabel.Text = simulation.LifeEnv.Timetable[4].ToString();
                // Detail of selected student
                knowladgeLabel.Text = $"{student.KnowladgePercentage} %";
                scoreLabel.Text = $"{student.Points.Score} %";
                posibilityToLernLabel.Text = $"{student.PosibilityToLern * 100:N2} %";
                posibilityToRememberLabel.Text = $"{student.PosibilityToRemember * 100:N2} %";
                aboutLabel.Text = student.Gens.ToString();
                // Logs
                studentListBox.DataSource = student.GetLog();
                studentListBox.SelectedIndex = studentListBox.Items.Count - 1;
                // Best student
                bestStudentLinkLabel.Text = $"Student {simulation.LifeEnv.FindBestStudentId()}";
                student = simulation.LifeEnv.GetStudent(simulation.LifeEnv.FindBestStudentId());
                bestScoreLabel.Text = $"{student.KnowladgePercentage} % / {student.Points.Score} %";
            });
        }

        /// <summary>
        /// Adapts UI to working or idle simulation state. 
        /// </summary>
        /// <param name="state">if true UI is idle state, if false UI is in working state</param>
        private void ShowIdleUi(bool state) {
            if(state) {
                progressBar.Maximum = Core.DAYS_IN_WEEK * Core.WEEKS_IN_SEMESTER * Core.SEMESTERS_IN_LIFE + Core.DAYS_IN_WEEK * Core.SEMESTERS_IN_LIFE;
                progressBar.Value = simulation.LifeEnv.DayOfLife;
                progressBar.Style = ProgressBarStyle.Blocks;
                if(programState == ProgramState.IdleWithoutSolution)
                    autoSimulateButton.Text = "Start simulation";
                else
                {
                    Debug.Assert(programState == ProgramState.IdleWithSolution);
                    autoSimulateButton.Text = "Restart simulation";
                }
            } else {
                Debug.Assert(programState == ProgramState.Working);
                progressBar.Style = ProgressBarStyle.Marquee;
                autoSimulateButton.Text = "Pause simulation";
            }
            nextDayButton.Enabled = state;
            nextGenerationButton.Enabled = state;
            nextSemesterButton.Enabled = state;
            nextWeekButton.Enabled = state;
        }

        /// <summary>
        /// Handles log messages from LifeEnviroment
        /// </summary>
        /// <param name="sender">Not needed</param>
        /// <param name="args">Log message</param>
        protected void OnEnviromentLog(object sender, LogEventArgs args)
        {
            // Performs operation on UI thread
            // see http://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c
            this.Invoke((MethodInvoker)delegate
            {
                if (eventListBox.Items.Count > Settings.Default.logSize)
                    eventListBox.Items.RemoveAt(0);
                eventListBox.Items.Add(args.What);
                eventListBox.SelectedIndex = eventListBox.Items.Count - 1;
            });
        }

        // User interface events

        /// <summary>
        /// Fired when values where simulation stop are changed
        /// </summary>
        /// <param name="sender">Not needed</param>
        /// <param name="e">Not needed</param>
        private void TresholdChanged(object sender, EventArgs e)
        {
            if (knowladgeCheckBox.Checked)
                simulation.ExceptedKnowlidge = (int)knowladgeNumericUpDown.Value;
            else
                simulation.ExceptedKnowlidge = 0;
            if (scoreCheckBox.Checked)
                simulation.ExceptedScore = (int)scoreNumericUpDown.Value;
            else
                simulation.ExceptedScore = (int)scoreNumericUpDown.Value;

        }

        private void SimulateStartButton_Click(object sender, EventArgs e) {
            switch(programState) {
                case ProgramState.IdleWithoutSolution:
                    programState = ProgramState.Working;
                    // Starts UI refreshing
                    uiUpdateThread = new Thread(UiUpdateTask);
                    uiUpdateThread.Priority = ThreadPriority.BelowNormal;
                    uiUpdateThread.Start();
                    // Starts simulation
                    simulationThread = new Thread(SimulationTask);
                    simulationThread.Name = "ThreadOfLife";
                    simulationThread.Start();
                    break;
                case ProgramState.Working:
                    programState = ProgramState.IdleWithoutSolution;
                    RefreshUi();
                    break;
                case ProgramState.IdleWithSolution:
                    LoadSimulatioDefaults();
                    goto case ProgramState.IdleWithoutSolution;
                default:
                    throw new NotImplementedException("Unknown ProgramState");
            }
            
        }

        private void NextGeneticButton_Click(object sender, EventArgs e) {
            uint generation = simulation.Calendar.Generation;
            while (generation == simulation.Calendar.Generation)
                simulation.NextDay();
            RefreshUi();
        }

        private void NextSemesterButton_Click(object sender, EventArgs e) {
            uint semester = simulation.Calendar.Semester;
            while (semester == simulation.Calendar.Semester)
                simulation.NextDay();
            RefreshUi();
        }

        private void NextWeekButton_Click(object sender, EventArgs e) {
            uint week = simulation.Calendar.Week;
            while (week == simulation.Calendar.Week)
                simulation.NextDay();
            RefreshUi();
        }

        private void NextDayButton_Click(object sender, EventArgs e) {
            simulation.NextDay();
            RefreshUi();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            studentNumericUpDown.Maximum = Settings.Default.numberOfStudents - 1;
            // Regular semester including examination
            progressBar.Maximum = Core.DAYS_IN_WEEK * Core.WEEKS_IN_SEMESTER * Core.SEMESTERS_IN_LIFE + Core.DAYS_IN_WEEK * Core.SEMESTERS_IN_LIFE;
            knowladgeNumericUpDown.Value = simulation.ExceptedKnowlidge;
            scoreNumericUpDown.Value = simulation.ExceptedScore;
            // Loads simulation values into UI
            RefreshUi();
            // Logs binding
            simulation.LifeEnv.LogEvent += OnEnviromentLog;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            ForceStopSimulatin();
            simulation.LifeEnv.LogEvent -= OnEnviromentLog;
        }

        private void StudentIdUpDown_ValueChanged(object sender, EventArgs e) {
            if(programState != ProgramState.Working)
                RefreshUi();
        }

        private void BestStudentLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            studentNumericUpDown.Value = simulation.LifeEnv.FindBestStudentId();
            if(programState != ProgramState.Working)
                RefreshUi();
        }

        private void SettingsButton_Click(object sender, EventArgs e) {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog(this);
        }
    }
}
