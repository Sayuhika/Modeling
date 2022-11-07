namespace DoublePendulum
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxModel = new System.Windows.Forms.PictureBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBoxPS = new System.Windows.Forms.GroupBox();
            this.textBoxV2 = new System.Windows.Forms.TextBox();
            this.textBoxA2 = new System.Windows.Forms.TextBox();
            this.textBoxV1 = new System.Windows.Forms.TextBox();
            this.textBoxA1 = new System.Windows.Forms.TextBox();
            this.labelF2d = new System.Windows.Forms.Label();
            this.labelF2 = new System.Windows.Forms.Label();
            this.labelF1d = new System.Windows.Forms.Label();
            this.labelF1 = new System.Windows.Forms.Label();
            this.labelA2d = new System.Windows.Forms.Label();
            this.labelA2 = new System.Windows.Forms.Label();
            this.labelPlanet = new System.Windows.Forms.Label();
            this.labelA1d = new System.Windows.Forms.Label();
            this.comboBoxPlanets = new System.Windows.Forms.ComboBox();
            this.labelA1 = new System.Windows.Forms.Label();
            this.numericUpDownAccuracy = new System.Windows.Forms.NumericUpDown();
            this.labelAccuracy = new System.Windows.Forms.Label();
            this.labelL2d = new System.Windows.Forms.Label();
            this.numericUpDownL2 = new System.Windows.Forms.NumericUpDown();
            this.labelL2 = new System.Windows.Forms.Label();
            this.labelL1d = new System.Windows.Forms.Label();
            this.numericUpDownL1 = new System.Windows.Forms.NumericUpDown();
            this.labelL1 = new System.Windows.Forms.Label();
            this.labelM2d = new System.Windows.Forms.Label();
            this.numericUpDownM2 = new System.Windows.Forms.NumericUpDown();
            this.labelM2 = new System.Windows.Forms.Label();
            this.labelM1d = new System.Windows.Forms.Label();
            this.numericUpDownM1 = new System.Windows.Forms.NumericUpDown();
            this.labelM1 = new System.Windows.Forms.Label();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.groupBoxPhaseMods = new System.Windows.Forms.GroupBox();
            this.numericUpDownPhasePortrait = new System.Windows.Forms.NumericUpDown();
            this.labelPhasePortrait = new System.Windows.Forms.Label();
            this.numericUpDownPhasePath = new System.Windows.Forms.NumericUpDown();
            this.labelPhasePath = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxPhasePath = new System.Windows.Forms.PictureBox();
            this.pictureBoxPhasePortrait = new System.Windows.Forms.PictureBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.buttonResetSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBoxPS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownL2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownL1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM1)).BeginInit();
            this.groupBoxPhaseMods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhasePortrait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhasePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhasePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhasePortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Size = new System.Drawing.Size(1316, 748);
            this.splitContainer1.SplitterDistance = 687;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pictureBoxModel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(687, 748);
            this.splitContainer2.SplitterDistance = 549;
            this.splitContainer2.TabIndex = 2;
            // 
            // pictureBoxModel
            // 
            this.pictureBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxModel.ErrorImage = global::DoublePendulum.Properties.Resources.Harold;
            this.pictureBoxModel.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxModel.Name = "pictureBoxModel";
            this.pictureBoxModel.Size = new System.Drawing.Size(687, 549);
            this.pictureBoxModel.TabIndex = 0;
            this.pictureBoxModel.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBoxPS);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.buttonResetSettings);
            this.splitContainer3.Panel2.Controls.Add(this.buttonStartStop);
            this.splitContainer3.Panel2.Controls.Add(this.groupBoxPhaseMods);
            this.splitContainer3.Size = new System.Drawing.Size(687, 195);
            this.splitContainer3.SplitterDistance = 423;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBoxPS
            // 
            this.groupBoxPS.Controls.Add(this.textBoxV2);
            this.groupBoxPS.Controls.Add(this.textBoxA2);
            this.groupBoxPS.Controls.Add(this.textBoxV1);
            this.groupBoxPS.Controls.Add(this.textBoxA1);
            this.groupBoxPS.Controls.Add(this.labelF2d);
            this.groupBoxPS.Controls.Add(this.labelF2);
            this.groupBoxPS.Controls.Add(this.labelF1d);
            this.groupBoxPS.Controls.Add(this.labelF1);
            this.groupBoxPS.Controls.Add(this.labelA2d);
            this.groupBoxPS.Controls.Add(this.labelA2);
            this.groupBoxPS.Controls.Add(this.labelPlanet);
            this.groupBoxPS.Controls.Add(this.labelA1d);
            this.groupBoxPS.Controls.Add(this.comboBoxPlanets);
            this.groupBoxPS.Controls.Add(this.labelA1);
            this.groupBoxPS.Controls.Add(this.numericUpDownAccuracy);
            this.groupBoxPS.Controls.Add(this.labelAccuracy);
            this.groupBoxPS.Controls.Add(this.labelL2d);
            this.groupBoxPS.Controls.Add(this.numericUpDownL2);
            this.groupBoxPS.Controls.Add(this.labelL2);
            this.groupBoxPS.Controls.Add(this.labelL1d);
            this.groupBoxPS.Controls.Add(this.numericUpDownL1);
            this.groupBoxPS.Controls.Add(this.labelL1);
            this.groupBoxPS.Controls.Add(this.labelM2d);
            this.groupBoxPS.Controls.Add(this.numericUpDownM2);
            this.groupBoxPS.Controls.Add(this.labelM2);
            this.groupBoxPS.Controls.Add(this.labelM1d);
            this.groupBoxPS.Controls.Add(this.numericUpDownM1);
            this.groupBoxPS.Controls.Add(this.labelM1);
            this.groupBoxPS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPS.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPS.Name = "groupBoxPS";
            this.groupBoxPS.Size = new System.Drawing.Size(423, 195);
            this.groupBoxPS.TabIndex = 0;
            this.groupBoxPS.TabStop = false;
            this.groupBoxPS.Text = "Параметры системы";
            // 
            // textBoxV2
            // 
            this.textBoxV2.Location = new System.Drawing.Point(307, 103);
            this.textBoxV2.Name = "textBoxV2";
            this.textBoxV2.Size = new System.Drawing.Size(62, 20);
            this.textBoxV2.TabIndex = 26;
            this.textBoxV2.Text = "0";
            // 
            // textBoxA2
            // 
            this.textBoxA2.Location = new System.Drawing.Point(307, 51);
            this.textBoxA2.Name = "textBoxA2";
            this.textBoxA2.Size = new System.Drawing.Size(62, 20);
            this.textBoxA2.TabIndex = 2;
            this.textBoxA2.Text = "25";
            // 
            // textBoxV1
            // 
            this.textBoxV1.Location = new System.Drawing.Point(307, 77);
            this.textBoxV1.Name = "textBoxV1";
            this.textBoxV1.Size = new System.Drawing.Size(62, 20);
            this.textBoxV1.TabIndex = 25;
            this.textBoxV1.Text = "0";
            // 
            // textBoxA1
            // 
            this.textBoxA1.Location = new System.Drawing.Point(307, 25);
            this.textBoxA1.Name = "textBoxA1";
            this.textBoxA1.Size = new System.Drawing.Size(62, 20);
            this.textBoxA1.TabIndex = 1;
            this.textBoxA1.Text = "45";
            // 
            // labelF2d
            // 
            this.labelF2d.AutoSize = true;
            this.labelF2d.Location = new System.Drawing.Point(375, 106);
            this.labelF2d.Name = "labelF2d";
            this.labelF2d.Size = new System.Drawing.Size(41, 13);
            this.labelF2d.TabIndex = 23;
            this.labelF2d.Text = "град/c";
            // 
            // labelF2
            // 
            this.labelF2.AutoSize = true;
            this.labelF2.Location = new System.Drawing.Point(195, 106);
            this.labelF2.Name = "labelF2";
            this.labelF2.Size = new System.Drawing.Size(112, 13);
            this.labelF2.TabIndex = 24;
            this.labelF2.Text = "Угловая скорость 2:";
            // 
            // labelF1d
            // 
            this.labelF1d.AutoSize = true;
            this.labelF1d.Location = new System.Drawing.Point(375, 80);
            this.labelF1d.Name = "labelF1d";
            this.labelF1d.Size = new System.Drawing.Size(41, 13);
            this.labelF1d.TabIndex = 20;
            this.labelF1d.Text = "град/с";
            // 
            // labelF1
            // 
            this.labelF1.AutoSize = true;
            this.labelF1.Location = new System.Drawing.Point(195, 80);
            this.labelF1.Name = "labelF1";
            this.labelF1.Size = new System.Drawing.Size(112, 13);
            this.labelF1.TabIndex = 21;
            this.labelF1.Text = "Угловая скорость 1:";
            // 
            // labelA2d
            // 
            this.labelA2d.AutoSize = true;
            this.labelA2d.Location = new System.Drawing.Point(375, 54);
            this.labelA2d.Name = "labelA2d";
            this.labelA2d.Size = new System.Drawing.Size(30, 13);
            this.labelA2d.TabIndex = 17;
            this.labelA2d.Text = "град";
            // 
            // labelA2
            // 
            this.labelA2.AutoSize = true;
            this.labelA2.Location = new System.Drawing.Point(195, 54);
            this.labelA2.Name = "labelA2";
            this.labelA2.Size = new System.Drawing.Size(106, 13);
            this.labelA2.TabIndex = 18;
            this.labelA2.Text = "Угол отклонения 2:";
            // 
            // labelPlanet
            // 
            this.labelPlanet.AutoSize = true;
            this.labelPlanet.Location = new System.Drawing.Point(6, 159);
            this.labelPlanet.Name = "labelPlanet";
            this.labelPlanet.Size = new System.Drawing.Size(139, 13);
            this.labelPlanet.TabIndex = 13;
            this.labelPlanet.Text = "Гравитационные условия:";
            // 
            // labelA1d
            // 
            this.labelA1d.AutoSize = true;
            this.labelA1d.Location = new System.Drawing.Point(375, 28);
            this.labelA1d.Name = "labelA1d";
            this.labelA1d.Size = new System.Drawing.Size(30, 13);
            this.labelA1d.TabIndex = 14;
            this.labelA1d.Text = "град";
            // 
            // comboBoxPlanets
            // 
            this.comboBoxPlanets.FormattingEnabled = true;
            this.comboBoxPlanets.Items.AddRange(new object[] {
            "Меркурий",
            "Венера",
            "Земля",
            "Марс",
            "Юпитер",
            "Сатурн",
            "Уран",
            "Нептун",
            "Плутон"});
            this.comboBoxPlanets.Location = new System.Drawing.Point(151, 156);
            this.comboBoxPlanets.Name = "comboBoxPlanets";
            this.comboBoxPlanets.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPlanets.TabIndex = 1;
            // 
            // labelA1
            // 
            this.labelA1.AutoSize = true;
            this.labelA1.Location = new System.Drawing.Point(195, 28);
            this.labelA1.Name = "labelA1";
            this.labelA1.Size = new System.Drawing.Size(106, 13);
            this.labelA1.TabIndex = 15;
            this.labelA1.Text = "Угол отклонения 1:";
            // 
            // numericUpDownAccuracy
            // 
            this.numericUpDownAccuracy.DecimalPlaces = 5;
            this.numericUpDownAccuracy.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownAccuracy.Location = new System.Drawing.Point(152, 130);
            this.numericUpDownAccuracy.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAccuracy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownAccuracy.Name = "numericUpDownAccuracy";
            this.numericUpDownAccuracy.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownAccuracy.TabIndex = 11;
            this.numericUpDownAccuracy.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // labelAccuracy
            // 
            this.labelAccuracy.AutoSize = true;
            this.labelAccuracy.Location = new System.Drawing.Point(6, 132);
            this.labelAccuracy.Name = "labelAccuracy";
            this.labelAccuracy.Size = new System.Drawing.Size(140, 13);
            this.labelAccuracy.TabIndex = 12;
            this.labelAccuracy.Text = "Точность моделирования:";
            // 
            // labelL2d
            // 
            this.labelL2d.AutoSize = true;
            this.labelL2d.Location = new System.Drawing.Point(171, 106);
            this.labelL2d.Name = "labelL2d";
            this.labelL2d.Size = new System.Drawing.Size(15, 13);
            this.labelL2d.TabIndex = 8;
            this.labelL2d.Text = "м";
            // 
            // numericUpDownL2
            // 
            this.numericUpDownL2.DecimalPlaces = 3;
            this.numericUpDownL2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownL2.Location = new System.Drawing.Point(103, 104);
            this.numericUpDownL2.Name = "numericUpDownL2";
            this.numericUpDownL2.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownL2.TabIndex = 9;
            this.numericUpDownL2.Value = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            // 
            // labelL2
            // 
            this.labelL2.AutoSize = true;
            this.labelL2.Location = new System.Drawing.Point(6, 106);
            this.labelL2.Name = "labelL2";
            this.labelL2.Size = new System.Drawing.Size(84, 13);
            this.labelL2.TabIndex = 10;
            this.labelL2.Text = "Длина нити L2:";
            // 
            // labelL1d
            // 
            this.labelL1d.AutoSize = true;
            this.labelL1d.Location = new System.Drawing.Point(171, 80);
            this.labelL1d.Name = "labelL1d";
            this.labelL1d.Size = new System.Drawing.Size(15, 13);
            this.labelL1d.TabIndex = 5;
            this.labelL1d.Text = "м";
            // 
            // numericUpDownL1
            // 
            this.numericUpDownL1.DecimalPlaces = 3;
            this.numericUpDownL1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownL1.Location = new System.Drawing.Point(103, 78);
            this.numericUpDownL1.Name = "numericUpDownL1";
            this.numericUpDownL1.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownL1.TabIndex = 6;
            this.numericUpDownL1.Value = new decimal(new int[] {
            15,
            0,
            0,
            131072});
            // 
            // labelL1
            // 
            this.labelL1.AutoSize = true;
            this.labelL1.Location = new System.Drawing.Point(6, 80);
            this.labelL1.Name = "labelL1";
            this.labelL1.Size = new System.Drawing.Size(84, 13);
            this.labelL1.TabIndex = 7;
            this.labelL1.Text = "Длина нити L1:";
            // 
            // labelM2d
            // 
            this.labelM2d.AutoSize = true;
            this.labelM2d.Location = new System.Drawing.Point(171, 54);
            this.labelM2d.Name = "labelM2d";
            this.labelM2d.Size = new System.Drawing.Size(18, 13);
            this.labelM2d.TabIndex = 2;
            this.labelM2d.Text = "кг";
            // 
            // numericUpDownM2
            // 
            this.numericUpDownM2.DecimalPlaces = 3;
            this.numericUpDownM2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownM2.Location = new System.Drawing.Point(103, 52);
            this.numericUpDownM2.Name = "numericUpDownM2";
            this.numericUpDownM2.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownM2.TabIndex = 3;
            this.numericUpDownM2.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // labelM2
            // 
            this.labelM2.AutoSize = true;
            this.labelM2.Location = new System.Drawing.Point(6, 54);
            this.labelM2.Name = "labelM2";
            this.labelM2.Size = new System.Drawing.Size(92, 13);
            this.labelM2.TabIndex = 4;
            this.labelM2.Text = "Масса груза M2:";
            // 
            // labelM1d
            // 
            this.labelM1d.AutoSize = true;
            this.labelM1d.Location = new System.Drawing.Point(171, 28);
            this.labelM1d.Name = "labelM1d";
            this.labelM1d.Size = new System.Drawing.Size(18, 13);
            this.labelM1d.TabIndex = 1;
            this.labelM1d.Text = "кг";
            // 
            // numericUpDownM1
            // 
            this.numericUpDownM1.DecimalPlaces = 3;
            this.numericUpDownM1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownM1.Location = new System.Drawing.Point(103, 26);
            this.numericUpDownM1.Name = "numericUpDownM1";
            this.numericUpDownM1.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownM1.TabIndex = 1;
            this.numericUpDownM1.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // labelM1
            // 
            this.labelM1.AutoSize = true;
            this.labelM1.Location = new System.Drawing.Point(6, 28);
            this.labelM1.Name = "labelM1";
            this.labelM1.Size = new System.Drawing.Size(92, 13);
            this.labelM1.TabIndex = 1;
            this.labelM1.Text = "Масса груза M1:";
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartStop.Location = new System.Drawing.Point(6, 148);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(252, 35);
            this.buttonStartStop.TabIndex = 2;
            this.buttonStartStop.Text = "Начать моделирование";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // groupBoxPhaseMods
            // 
            this.groupBoxPhaseMods.Controls.Add(this.numericUpDownPhasePortrait);
            this.groupBoxPhaseMods.Controls.Add(this.labelPhasePortrait);
            this.groupBoxPhaseMods.Controls.Add(this.numericUpDownPhasePath);
            this.groupBoxPhaseMods.Controls.Add(this.labelPhasePath);
            this.groupBoxPhaseMods.Location = new System.Drawing.Point(6, 4);
            this.groupBoxPhaseMods.Name = "groupBoxPhaseMods";
            this.groupBoxPhaseMods.Size = new System.Drawing.Size(251, 77);
            this.groupBoxPhaseMods.TabIndex = 3;
            this.groupBoxPhaseMods.TabStop = false;
            this.groupBoxPhaseMods.Text = "Пареметры отображения фазовых картин";
            // 
            // numericUpDownPhasePortrait
            // 
            this.numericUpDownPhasePortrait.Location = new System.Drawing.Point(210, 52);
            this.numericUpDownPhasePortrait.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownPhasePortrait.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPhasePortrait.Name = "numericUpDownPhasePortrait";
            this.numericUpDownPhasePortrait.Size = new System.Drawing.Size(31, 20);
            this.numericUpDownPhasePortrait.TabIndex = 4;
            this.numericUpDownPhasePortrait.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPhasePortrait.ValueChanged += new System.EventHandler(this.numericUpDownPhasePortrait_ValueChanged);
            // 
            // labelPhasePortrait
            // 
            this.labelPhasePortrait.AutoSize = true;
            this.labelPhasePortrait.Location = new System.Drawing.Point(8, 54);
            this.labelPhasePortrait.Name = "labelPhasePortrait";
            this.labelPhasePortrait.Size = new System.Drawing.Size(186, 13);
            this.labelPhasePortrait.TabIndex = 3;
            this.labelPhasePortrait.Text = "Фазоваый портерт системы звена";
            // 
            // numericUpDownPhasePath
            // 
            this.numericUpDownPhasePath.Location = new System.Drawing.Point(210, 26);
            this.numericUpDownPhasePath.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownPhasePath.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPhasePath.Name = "numericUpDownPhasePath";
            this.numericUpDownPhasePath.Size = new System.Drawing.Size(31, 20);
            this.numericUpDownPhasePath.TabIndex = 2;
            this.numericUpDownPhasePath.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPhasePath.ValueChanged += new System.EventHandler(this.numericUpDownPhasePath_ValueChanged);
            // 
            // labelPhasePath
            // 
            this.labelPhasePath.AutoSize = true;
            this.labelPhasePath.Location = new System.Drawing.Point(8, 28);
            this.labelPhasePath.Name = "labelPhasePath";
            this.labelPhasePath.Size = new System.Drawing.Size(196, 13);
            this.labelPhasePath.TabIndex = 1;
            this.labelPhasePath.Text = "Фазовая траектория системы звена";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ErrorImage = global::DoublePendulum.Properties.Resources.Harold;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(687, 748);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.pictureBoxPhasePath);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.pictureBoxPhasePortrait);
            this.splitContainer4.Size = new System.Drawing.Size(625, 748);
            this.splitContainer4.SplitterDistance = 367;
            this.splitContainer4.TabIndex = 0;
            // 
            // pictureBoxPhasePath
            // 
            this.pictureBoxPhasePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPhasePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPhasePath.ErrorImage = global::DoublePendulum.Properties.Resources.Harold;
            this.pictureBoxPhasePath.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPhasePath.Name = "pictureBoxPhasePath";
            this.pictureBoxPhasePath.Size = new System.Drawing.Size(625, 367);
            this.pictureBoxPhasePath.TabIndex = 0;
            this.pictureBoxPhasePath.TabStop = false;
            // 
            // pictureBoxPhasePortrait
            // 
            this.pictureBoxPhasePortrait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPhasePortrait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPhasePortrait.ErrorImage = global::DoublePendulum.Properties.Resources.Harold;
            this.pictureBoxPhasePortrait.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPhasePortrait.Name = "pictureBoxPhasePortrait";
            this.pictureBoxPhasePortrait.Size = new System.Drawing.Size(625, 377);
            this.pictureBoxPhasePortrait.TabIndex = 1;
            this.pictureBoxPhasePortrait.TabStop = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // buttonResetSettings
            // 
            this.buttonResetSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetSettings.Location = new System.Drawing.Point(6, 82);
            this.buttonResetSettings.Name = "buttonResetSettings";
            this.buttonResetSettings.Size = new System.Drawing.Size(252, 35);
            this.buttonResetSettings.TabIndex = 4;
            this.buttonResetSettings.Text = "Сброс";
            this.buttonResetSettings.UseVisualStyleBackColor = true;
            this.buttonResetSettings.Click += new System.EventHandler(this.buttonResetSettings_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 748);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMain";
            this.Text = "Модель двойного маятника";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModel)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBoxPS.ResumeLayout(false);
            this.groupBoxPS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownL2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownL1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM1)).EndInit();
            this.groupBoxPhaseMods.ResumeLayout(false);
            this.groupBoxPhaseMods.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhasePortrait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhasePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhasePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhasePortrait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxPS;
        private System.Windows.Forms.Label labelM2d;
        private System.Windows.Forms.Label labelM2;
        private System.Windows.Forms.Label labelM1d;
        private System.Windows.Forms.Label labelM1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelPlanet;
        private System.Windows.Forms.Label labelAccuracy;
        private System.Windows.Forms.Label labelL2d;
        private System.Windows.Forms.Label labelL2;
        private System.Windows.Forms.Label labelL1d;
        private System.Windows.Forms.Label labelL1;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox pictureBoxModel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.PictureBox pictureBoxPhasePath;
        private System.Windows.Forms.PictureBox pictureBoxPhasePortrait;
        private System.Windows.Forms.GroupBox groupBoxPhaseMods;
        private System.Windows.Forms.Label labelPhasePortrait;
        private System.Windows.Forms.Label labelPhasePath;
        public System.Windows.Forms.NumericUpDown numericUpDownM2;
        public System.Windows.Forms.NumericUpDown numericUpDownM1;
        public System.Windows.Forms.ComboBox comboBoxPlanets;
        public System.Windows.Forms.NumericUpDown numericUpDownAccuracy;
        public System.Windows.Forms.NumericUpDown numericUpDownL2;
        public System.Windows.Forms.NumericUpDown numericUpDownL1;
        public System.Windows.Forms.NumericUpDown numericUpDownPhasePortrait;
        public System.Windows.Forms.NumericUpDown numericUpDownPhasePath;
        private System.Windows.Forms.Label labelF2d;
        private System.Windows.Forms.Label labelF2;
        private System.Windows.Forms.Label labelF1d;
        private System.Windows.Forms.Label labelF1;
        private System.Windows.Forms.Label labelA2d;
        private System.Windows.Forms.Label labelA2;
        private System.Windows.Forms.Label labelA1d;
        private System.Windows.Forms.Label labelA1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TextBox textBoxV2;
        private System.Windows.Forms.TextBox textBoxA2;
        private System.Windows.Forms.TextBox textBoxV1;
        private System.Windows.Forms.TextBox textBoxA1;
        private System.Windows.Forms.Button buttonResetSettings;
    }
}

