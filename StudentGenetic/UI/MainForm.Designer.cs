namespace StudentGenetic {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.autoSimulateButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.nextDayButton = new System.Windows.Forms.Button();
            this.nextSemesterButton = new System.Windows.Forms.Button();
            this.nextGenerationButton = new System.Windows.Forms.Button();
            this.nextWeekButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.generationLabel = new System.Windows.Forms.Label();
            this.dateGroupBox = new System.Windows.Forms.GroupBox();
            this.timetableGroupBox = new System.Windows.Forms.GroupBox();
            this.ttFrLabel = new System.Windows.Forms.Label();
            this.ttThLabel = new System.Windows.Forms.Label();
            this.ttWeLabel = new System.Windows.Forms.Label();
            this.ttTuLabel = new System.Windows.Forms.Label();
            this.ttMoLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.studnetGroupBox = new System.Windows.Forms.GroupBox();
            this.aboutLabel = new System.Windows.Forms.Label();
            this.studentListBox = new System.Windows.Forms.ListBox();
            this.posibilityToRememberLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.posibilityToLernLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.knowladgeLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.studentNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.bestStudentGroupBox = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.bestScoreLabel = new System.Windows.Forms.Label();
            this.bestStudentLinkLabel = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scoreNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.knowladgeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.paramsButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scoreCheckBox = new System.Windows.Forms.CheckBox();
            this.knowladgeCheckBox = new System.Windows.Forms.CheckBox();
            this.eventGroupBox = new System.Windows.Forms.GroupBox();
            this.eventListBox = new System.Windows.Forms.ListBox();
            this.dateGroupBox.SuspendLayout();
            this.timetableGroupBox.SuspendLayout();
            this.studnetGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentNumericUpDown)).BeginInit();
            this.bestStudentGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.knowladgeNumericUpDown)).BeginInit();
            this.eventGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoSimulateButton
            // 
            this.autoSimulateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.autoSimulateButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.autoSimulateButton.Location = new System.Drawing.Point(478, 579);
            this.autoSimulateButton.Name = "autoSimulateButton";
            this.autoSimulateButton.Size = new System.Drawing.Size(162, 23);
            this.autoSimulateButton.TabIndex = 0;
            this.autoSimulateButton.Text = "Start simulation";
            this.autoSimulateButton.UseVisualStyleBackColor = true;
            this.autoSimulateButton.Click += new System.EventHandler(this.SimulateStartButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 541);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(628, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 2;
            // 
            // nextDayButton
            // 
            this.nextDayButton.Location = new System.Drawing.Point(12, 579);
            this.nextDayButton.Name = "nextDayButton";
            this.nextDayButton.Size = new System.Drawing.Size(75, 23);
            this.nextDayButton.TabIndex = 3;
            this.nextDayButton.Text = "Next day";
            this.nextDayButton.UseVisualStyleBackColor = true;
            this.nextDayButton.Click += new System.EventHandler(this.NextDayButton_Click);
            // 
            // nextSemesterButton
            // 
            this.nextSemesterButton.Location = new System.Drawing.Point(190, 578);
            this.nextSemesterButton.Name = "nextSemesterButton";
            this.nextSemesterButton.Size = new System.Drawing.Size(82, 23);
            this.nextSemesterButton.TabIndex = 4;
            this.nextSemesterButton.Text = "Next semester";
            this.nextSemesterButton.UseVisualStyleBackColor = true;
            this.nextSemesterButton.Click += new System.EventHandler(this.NextSemesterButton_Click);
            // 
            // nextGenerationButton
            // 
            this.nextGenerationButton.Location = new System.Drawing.Point(278, 578);
            this.nextGenerationButton.Name = "nextGenerationButton";
            this.nextGenerationButton.Size = new System.Drawing.Size(94, 23);
            this.nextGenerationButton.TabIndex = 5;
            this.nextGenerationButton.Text = "Next generation";
            this.nextGenerationButton.UseVisualStyleBackColor = true;
            this.nextGenerationButton.Click += new System.EventHandler(this.NextGeneticButton_Click);
            // 
            // nextWeekButton
            // 
            this.nextWeekButton.Location = new System.Drawing.Point(93, 579);
            this.nextWeekButton.Name = "nextWeekButton";
            this.nextWeekButton.Size = new System.Drawing.Size(91, 23);
            this.nextWeekButton.TabIndex = 8;
            this.nextWeekButton.Text = "Next week";
            this.nextWeekButton.UseVisualStyleBackColor = true;
            this.nextWeekButton.Click += new System.EventHandler(this.NextWeekButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Date (day/week/semester):";
            // 
            // dateLabel
            // 
            this.dateLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.dateLabel.Location = new System.Drawing.Point(150, 25);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(55, 13);
            this.dateLabel.TabIndex = 10;
            this.dateLabel.Text = "0/00/0";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Generation:";
            // 
            // generationLabel
            // 
            this.generationLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.generationLabel.Location = new System.Drawing.Point(122, 50);
            this.generationLabel.Name = "generationLabel";
            this.generationLabel.Size = new System.Drawing.Size(83, 13);
            this.generationLabel.TabIndex = 12;
            this.generationLabel.Text = "0";
            this.generationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dateGroupBox
            // 
            this.dateGroupBox.Controls.Add(this.label3);
            this.dateGroupBox.Controls.Add(this.generationLabel);
            this.dateGroupBox.Controls.Add(this.label2);
            this.dateGroupBox.Controls.Add(this.dateLabel);
            this.dateGroupBox.Location = new System.Drawing.Point(418, 13);
            this.dateGroupBox.Name = "dateGroupBox";
            this.dateGroupBox.Size = new System.Drawing.Size(222, 78);
            this.dateGroupBox.TabIndex = 13;
            this.dateGroupBox.TabStop = false;
            this.dateGroupBox.Text = "Date";
            // 
            // timetableGroupBox
            // 
            this.timetableGroupBox.Controls.Add(this.ttFrLabel);
            this.timetableGroupBox.Controls.Add(this.ttThLabel);
            this.timetableGroupBox.Controls.Add(this.ttWeLabel);
            this.timetableGroupBox.Controls.Add(this.ttTuLabel);
            this.timetableGroupBox.Controls.Add(this.ttMoLabel);
            this.timetableGroupBox.Controls.Add(this.label8);
            this.timetableGroupBox.Controls.Add(this.label7);
            this.timetableGroupBox.Controls.Add(this.label6);
            this.timetableGroupBox.Controls.Add(this.label5);
            this.timetableGroupBox.Controls.Add(this.label4);
            this.timetableGroupBox.Location = new System.Drawing.Point(418, 97);
            this.timetableGroupBox.Name = "timetableGroupBox";
            this.timetableGroupBox.Size = new System.Drawing.Size(222, 87);
            this.timetableGroupBox.TabIndex = 14;
            this.timetableGroupBox.TabStop = false;
            this.timetableGroupBox.Text = "Timetable";
            // 
            // ttFrLabel
            // 
            this.ttFrLabel.AutoSize = true;
            this.ttFrLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.ttFrLabel.Location = new System.Drawing.Point(170, 41);
            this.ttFrLabel.Name = "ttFrLabel";
            this.ttFrLabel.Size = new System.Drawing.Size(16, 17);
            this.ttFrLabel.TabIndex = 9;
            this.ttFrLabel.Text = "0";
            // 
            // ttThLabel
            // 
            this.ttThLabel.AutoSize = true;
            this.ttThLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.ttThLabel.Location = new System.Drawing.Point(170, 20);
            this.ttThLabel.Name = "ttThLabel";
            this.ttThLabel.Size = new System.Drawing.Size(16, 17);
            this.ttThLabel.TabIndex = 8;
            this.ttThLabel.Text = "0";
            // 
            // ttWeLabel
            // 
            this.ttWeLabel.AutoSize = true;
            this.ttWeLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.ttWeLabel.Location = new System.Drawing.Point(69, 63);
            this.ttWeLabel.Name = "ttWeLabel";
            this.ttWeLabel.Size = new System.Drawing.Size(16, 17);
            this.ttWeLabel.TabIndex = 7;
            this.ttWeLabel.Text = "0";
            // 
            // ttTuLabel
            // 
            this.ttTuLabel.AutoSize = true;
            this.ttTuLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.ttTuLabel.Location = new System.Drawing.Point(69, 41);
            this.ttTuLabel.Name = "ttTuLabel";
            this.ttTuLabel.Size = new System.Drawing.Size(16, 17);
            this.ttTuLabel.TabIndex = 6;
            this.ttTuLabel.Text = "0";
            // 
            // ttMoLabel
            // 
            this.ttMoLabel.AutoSize = true;
            this.ttMoLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.ttMoLabel.Location = new System.Drawing.Point(69, 19);
            this.ttMoLabel.Name = "ttMoLabel";
            this.ttMoLabel.Size = new System.Drawing.Size(16, 17);
            this.ttMoLabel.TabIndex = 5;
            this.ttMoLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Friday:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Thursday:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Wednesday:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Tuesday:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Monday:";
            // 
            // studnetGroupBox
            // 
            this.studnetGroupBox.Controls.Add(this.aboutLabel);
            this.studnetGroupBox.Controls.Add(this.studentListBox);
            this.studnetGroupBox.Controls.Add(this.posibilityToRememberLabel);
            this.studnetGroupBox.Controls.Add(this.scoreLabel);
            this.studnetGroupBox.Controls.Add(this.label14);
            this.studnetGroupBox.Controls.Add(this.label13);
            this.studnetGroupBox.Controls.Add(this.posibilityToLernLabel);
            this.studnetGroupBox.Controls.Add(this.label12);
            this.studnetGroupBox.Controls.Add(this.knowladgeLabel);
            this.studnetGroupBox.Controls.Add(this.label10);
            this.studnetGroupBox.Controls.Add(this.studentNumericUpDown);
            this.studnetGroupBox.Controls.Add(this.label9);
            this.studnetGroupBox.Location = new System.Drawing.Point(12, 13);
            this.studnetGroupBox.Name = "studnetGroupBox";
            this.studnetGroupBox.Size = new System.Drawing.Size(397, 522);
            this.studnetGroupBox.TabIndex = 15;
            this.studnetGroupBox.TabStop = false;
            this.studnetGroupBox.Text = "Students";
            // 
            // aboutLabel
            // 
            this.aboutLabel.AutoSize = true;
            this.aboutLabel.Font = new System.Drawing.Font("Segoe Script", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutLabel.Location = new System.Drawing.Point(17, 119);
            this.aboutLabel.MaximumSize = new System.Drawing.Size(370, 0);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(81, 19);
            this.aboutLabel.TabIndex = 11;
            this.aboutLabel.Text = "About me...";
            // 
            // studentListBox
            // 
            this.studentListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentListBox.FormattingEnabled = true;
            this.studentListBox.ItemHeight = 15;
            this.studentListBox.Location = new System.Drawing.Point(21, 179);
            this.studentListBox.Name = "studentListBox";
            this.studentListBox.Size = new System.Drawing.Size(361, 334);
            this.studentListBox.TabIndex = 10;
            // 
            // posibilityToRememberLabel
            // 
            this.posibilityToRememberLabel.AutoSize = true;
            this.posibilityToRememberLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.posibilityToRememberLabel.Location = new System.Drawing.Point(320, 97);
            this.posibilityToRememberLabel.Name = "posibilityToRememberLabel";
            this.posibilityToRememberLabel.Size = new System.Drawing.Size(62, 17);
            this.posibilityToRememberLabel.TabIndex = 9;
            this.posibilityToRememberLabel.Text = "100.00 %";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F);
            this.scoreLabel.Location = new System.Drawing.Point(329, 73);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(44, 17);
            this.scoreLabel.TabIndex = 8;
            this.scoreLabel.Text = "100 %";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(192, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Ability to remember:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(192, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Score of student:";
            // 
            // posibilityToLernLabel
            // 
            this.posibilityToLernLabel.AutoSize = true;
            this.posibilityToLernLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.posibilityToLernLabel.Location = new System.Drawing.Point(124, 97);
            this.posibilityToLernLabel.Name = "posibilityToLernLabel";
            this.posibilityToLernLabel.Size = new System.Drawing.Size(62, 17);
            this.posibilityToLernLabel.TabIndex = 5;
            this.posibilityToLernLabel.Text = "100.00 %";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Ability to learn:";
            // 
            // knowladgeLabel
            // 
            this.knowladgeLabel.AutoSize = true;
            this.knowladgeLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.knowladgeLabel.Location = new System.Drawing.Point(128, 74);
            this.knowladgeLabel.Name = "knowladgeLabel";
            this.knowladgeLabel.Size = new System.Drawing.Size(44, 17);
            this.knowladgeLabel.TabIndex = 3;
            this.knowladgeLabel.Text = "100 %";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(18, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Knowledge:";
            // 
            // studentNumericUpDown
            // 
            this.studentNumericUpDown.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentNumericUpDown.Location = new System.Drawing.Point(200, 26);
            this.studentNumericUpDown.Name = "studentNumericUpDown";
            this.studentNumericUpDown.Size = new System.Drawing.Size(64, 30);
            this.studentNumericUpDown.TabIndex = 1;
            this.studentNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.studentNumericUpDown.ValueChanged += new System.EventHandler(this.StudentIdUpDown_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 33);
            this.label9.TabIndex = 0;
            this.label9.Text = "Student ID:";
            // 
            // bestStudentGroupBox
            // 
            this.bestStudentGroupBox.Controls.Add(this.label15);
            this.bestStudentGroupBox.Controls.Add(this.bestScoreLabel);
            this.bestStudentGroupBox.Controls.Add(this.bestStudentLinkLabel);
            this.bestStudentGroupBox.Location = new System.Drawing.Point(418, 191);
            this.bestStudentGroupBox.Name = "bestStudentGroupBox";
            this.bestStudentGroupBox.Size = new System.Drawing.Size(222, 67);
            this.bestStudentGroupBox.TabIndex = 16;
            this.bestStudentGroupBox.TabStop = false;
            this.bestStudentGroupBox.Text = "Best student";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(102, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(102, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "(knowledge / score)";
            // 
            // bestScoreLabel
            // 
            this.bestScoreLabel.AutoSize = true;
            this.bestScoreLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestScoreLabel.Location = new System.Drawing.Point(111, 17);
            this.bestScoreLabel.Name = "bestScoreLabel";
            this.bestScoreLabel.Size = new System.Drawing.Size(94, 17);
            this.bestScoreLabel.TabIndex = 1;
            this.bestScoreLabel.Text = "100 % / 100 %";
            // 
            // bestStudentLinkLabel
            // 
            this.bestStudentLinkLabel.AutoSize = true;
            this.bestStudentLinkLabel.Font = new System.Drawing.Font("Segoe Script", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestStudentLinkLabel.Location = new System.Drawing.Point(11, 27);
            this.bestStudentLinkLabel.Name = "bestStudentLinkLabel";
            this.bestStudentLinkLabel.Size = new System.Drawing.Size(81, 20);
            this.bestStudentLinkLabel.TabIndex = 0;
            this.bestStudentLinkLabel.TabStop = true;
            this.bestStudentLinkLabel.Text = "Student 0";
            this.bestStudentLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BestStudentLabel_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scoreNumericUpDown);
            this.groupBox1.Controls.Add(this.knowladgeNumericUpDown);
            this.groupBox1.Controls.Add(this.paramsButton);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.scoreCheckBox);
            this.groupBox1.Controls.Add(this.knowladgeCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(418, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 102);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Goal of simulation:";
            // 
            // scoreNumericUpDown
            // 
            this.scoreNumericUpDown.Location = new System.Drawing.Point(129, 41);
            this.scoreNumericUpDown.Name = "scoreNumericUpDown";
            this.scoreNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.scoreNumericUpDown.TabIndex = 12;
            this.scoreNumericUpDown.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.scoreNumericUpDown.ValueChanged += new System.EventHandler(this.TresholdChanged);
            // 
            // knowladgeNumericUpDown
            // 
            this.knowladgeNumericUpDown.Location = new System.Drawing.Point(129, 17);
            this.knowladgeNumericUpDown.Name = "knowladgeNumericUpDown";
            this.knowladgeNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.knowladgeNumericUpDown.TabIndex = 11;
            this.knowladgeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.knowladgeNumericUpDown.ValueChanged += new System.EventHandler(this.TresholdChanged);
            // 
            // paramsButton
            // 
            this.paramsButton.Location = new System.Drawing.Point(49, 73);
            this.paramsButton.Name = "paramsButton";
            this.paramsButton.Size = new System.Drawing.Size(124, 23);
            this.paramsButton.TabIndex = 0;
            this.paramsButton.Text = "Simulation settings";
            this.paramsButton.UseVisualStyleBackColor = true;
            this.paramsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(191, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 25);
            this.label16.TabIndex = 10;
            this.label16.Text = "%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(99, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = ">";
            // 
            // scoreCheckBox
            // 
            this.scoreCheckBox.AutoSize = true;
            this.scoreCheckBox.Checked = true;
            this.scoreCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scoreCheckBox.Location = new System.Drawing.Point(12, 44);
            this.scoreCheckBox.Name = "scoreCheckBox";
            this.scoreCheckBox.Size = new System.Drawing.Size(54, 17);
            this.scoreCheckBox.TabIndex = 8;
            this.scoreCheckBox.Text = "Score";
            this.scoreCheckBox.UseVisualStyleBackColor = true;
            this.scoreCheckBox.CheckedChanged += new System.EventHandler(this.TresholdChanged);
            // 
            // knowladgeCheckBox
            // 
            this.knowladgeCheckBox.AutoSize = true;
            this.knowladgeCheckBox.Checked = true;
            this.knowladgeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.knowladgeCheckBox.Location = new System.Drawing.Point(12, 20);
            this.knowladgeCheckBox.Name = "knowladgeCheckBox";
            this.knowladgeCheckBox.Size = new System.Drawing.Size(79, 17);
            this.knowladgeCheckBox.TabIndex = 7;
            this.knowladgeCheckBox.Text = "Knowledge";
            this.knowladgeCheckBox.UseVisualStyleBackColor = true;
            this.knowladgeCheckBox.CheckedChanged += new System.EventHandler(this.TresholdChanged);
            // 
            // eventGroupBox
            // 
            this.eventGroupBox.Controls.Add(this.eventListBox);
            this.eventGroupBox.Location = new System.Drawing.Point(418, 372);
            this.eventGroupBox.Name = "eventGroupBox";
            this.eventGroupBox.Size = new System.Drawing.Size(222, 163);
            this.eventGroupBox.TabIndex = 18;
            this.eventGroupBox.TabStop = false;
            this.eventGroupBox.Text = "Common events of simulation";
            // 
            // eventListBox
            // 
            this.eventListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventListBox.FormattingEnabled = true;
            this.eventListBox.ItemHeight = 15;
            this.eventListBox.Location = new System.Drawing.Point(6, 15);
            this.eventListBox.Name = "eventListBox";
            this.eventListBox.Size = new System.Drawing.Size(209, 139);
            this.eventListBox.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 613);
            this.Controls.Add(this.eventGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bestStudentGroupBox);
            this.Controls.Add(this.studnetGroupBox);
            this.Controls.Add(this.timetableGroupBox);
            this.Controls.Add(this.dateGroupBox);
            this.Controls.Add(this.nextWeekButton);
            this.Controls.Add(this.nextGenerationButton);
            this.Controls.Add(this.nextSemesterButton);
            this.Controls.Add(this.nextDayButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.autoSimulateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Student Genetic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.dateGroupBox.ResumeLayout(false);
            this.dateGroupBox.PerformLayout();
            this.timetableGroupBox.ResumeLayout(false);
            this.timetableGroupBox.PerformLayout();
            this.studnetGroupBox.ResumeLayout(false);
            this.studnetGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentNumericUpDown)).EndInit();
            this.bestStudentGroupBox.ResumeLayout(false);
            this.bestStudentGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.knowladgeNumericUpDown)).EndInit();
            this.eventGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button autoSimulateButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button nextDayButton;
        private System.Windows.Forms.Button nextSemesterButton;
        private System.Windows.Forms.Button nextGenerationButton;
        private System.Windows.Forms.Button nextWeekButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label generationLabel;
        private System.Windows.Forms.GroupBox dateGroupBox;
        private System.Windows.Forms.GroupBox timetableGroupBox;
        private System.Windows.Forms.Label ttFrLabel;
        private System.Windows.Forms.Label ttThLabel;
        private System.Windows.Forms.Label ttWeLabel;
        private System.Windows.Forms.Label ttTuLabel;
        private System.Windows.Forms.Label ttMoLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox studnetGroupBox;
        private System.Windows.Forms.NumericUpDown studentNumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label knowladgeLabel;
        private System.Windows.Forms.Label posibilityToRememberLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label posibilityToLernLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox studentListBox;
        private System.Windows.Forms.GroupBox bestStudentGroupBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label bestScoreLabel;
        private System.Windows.Forms.LinkLabel bestStudentLinkLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button paramsButton;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox scoreCheckBox;
        private System.Windows.Forms.CheckBox knowladgeCheckBox;
        private System.Windows.Forms.GroupBox eventGroupBox;
        private System.Windows.Forms.ListBox eventListBox;
        private System.Windows.Forms.NumericUpDown scoreNumericUpDown;
        private System.Windows.Forms.NumericUpDown knowladgeNumericUpDown;
        private System.Windows.Forms.Label aboutLabel;
    }
}

