using StudentGenetic.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentGenetic {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Reloads default settings
        /// </summary>
        /// <param name="sender">Not needed</param>
        /// <param name="e">Not needed</param>
        private void defaultButton_Click(object sender, EventArgs e) {
            Settings.Default.Reset();
            Application.Restart();
        }

        /// <summary>
        /// Loads settings and put them into UI
        /// </summary>
        /// <param name="sender">Not needed</param>
        /// <param name="e">Not needed</param>
        private void SettingsForm_Load(object sender, EventArgs e) {
            timetableMaxHoursInDayNumericUpDown.Value = Settings.Default.timetableMaxHoursInDay;
            timetableHoursInWeekNumericUpDown.Value = Settings.Default.timetableHoursInWeek;
            numberOfStudentsNumericUpDown.Value = Settings.Default.numberOfStudents;
            logSizeNumericUpDown.Value = Settings.Default.logSize;
            coefLearnigNumericUpDown.Value = (decimal)Settings.Default.coefLerning;
            coefSchoolActiveNumericUpDown.Value = (decimal)Settings.Default.coefSchoolActive;
            coefSchoolPasiveNumericUpDown.Value = (decimal)Settings.Default.coefSchoolPasive;
            coefLongTermMemoryNumericUpDown.Value = (decimal)Settings.Default.coefLongTermMemory;
            coefRestNumericUpDown.Value = (decimal)Settings.Default.coefRest;
            timeToHomeNumericUpDown.Value = Settings.Default.timeToHome;
            homeFoodNumericUpDown.Value = Settings.Default.homeFood;
            coefGiveUpNumericUpDown.Value = (decimal)Settings.Default.coefGiveUp;

        }

        /// <summary>
        /// Saves application settings
        /// </summary>
        /// <param name="sender">Not needed</param>
        /// <param name="e">Not needed</param>
        private void saveButton_Click(object sender, EventArgs e) {
            Settings.Default.timetableMaxHoursInDay = (int)timetableMaxHoursInDayNumericUpDown.Value;
            Settings.Default.timetableHoursInWeek = (int)timetableHoursInWeekNumericUpDown.Value;
            Settings.Default.numberOfStudents = (int)numberOfStudentsNumericUpDown.Value;
            Settings.Default.logSize = (int)logSizeNumericUpDown.Value;
            Settings.Default.coefLerning = (double)coefLearnigNumericUpDown.Value;
            Settings.Default.coefSchoolActive = (double)coefSchoolActiveNumericUpDown.Value;
            Settings.Default.coefSchoolPasive = (double)coefSchoolPasiveNumericUpDown.Value;
            Settings.Default.coefLongTermMemory = (double)coefLongTermMemoryNumericUpDown.Value;
            Settings.Default.coefRest = (double)coefRestNumericUpDown.Value;
            Settings.Default.timeToHome = (int)timeToHomeNumericUpDown.Value;
            Settings.Default.homeFood = (int)homeFoodNumericUpDown.Value;
            Settings.Default.coefGiveUp = (double)coefGiveUpNumericUpDown.Value;
            Settings.Default.Save();

            Application.Restart();
        }
    }
}
