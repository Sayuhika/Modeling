namespace EvolutionOfProbabilityDensity
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chartEoPD = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartF = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownDev = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownExp = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxCurrentF = new System.Windows.Forms.TextBox();
            this.textBoxCurrentTime = new System.Windows.Forms.TextBox();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.trackBarEvolTime = new System.Windows.Forms.TrackBar();
            this.trackBarFourier = new System.Windows.Forms.TrackBar();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxdx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxU = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxdMult = new System.Windows.Forms.TextBox();
            this.textBoxd = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEoPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartF)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEvolTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFourier)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxCurrentF);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxCurrentTime);
            this.splitContainer1.Panel2.Controls.Add(this.checkBox);
            this.splitContainer1.Panel2.Controls.Add(this.trackBarEvolTime);
            this.splitContainer1.Panel2.Controls.Add(this.trackBarFourier);
            this.splitContainer1.Panel2.Controls.Add(this.buttonStartStop);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1260, 763);
            this.splitContainer1.SplitterDistance = 574;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chartEoPD);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartF);
            this.splitContainer2.Size = new System.Drawing.Size(1260, 574);
            this.splitContainer2.SplitterDistance = 627;
            this.splitContainer2.TabIndex = 0;
            // 
            // chartEoPD
            // 
            chartArea1.Name = "ChartArea1";
            this.chartEoPD.ChartAreas.Add(chartArea1);
            this.chartEoPD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEoPD.Location = new System.Drawing.Point(0, 0);
            this.chartEoPD.Name = "chartEoPD";
            this.chartEoPD.Size = new System.Drawing.Size(627, 574);
            this.chartEoPD.TabIndex = 0;
            this.chartEoPD.Text = "chartEoPD";
            // 
            // chartF
            // 
            chartArea2.Name = "ChartArea1";
            this.chartF.ChartAreas.Add(chartArea2);
            this.chartF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartF.Location = new System.Drawing.Point(0, 0);
            this.chartF.Name = "chartF";
            this.chartF.Size = new System.Drawing.Size(629, 574);
            this.chartF.TabIndex = 0;
            this.chartF.Text = "chartF";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownDev);
            this.groupBox2.Controls.Add(this.numericUpDownExp);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(11, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 53);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры гауссова купола";
            // 
            // numericUpDownDev
            // 
            this.numericUpDownDev.DecimalPlaces = 3;
            this.numericUpDownDev.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.numericUpDownDev.Location = new System.Drawing.Point(316, 19);
            this.numericUpDownDev.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDev.Name = "numericUpDownDev";
            this.numericUpDownDev.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownDev.TabIndex = 12;
            this.numericUpDownDev.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDev.ValueChanged += new System.EventHandler(this.numericUpDownDev_ValueChanged);
            // 
            // numericUpDownExp
            // 
            this.numericUpDownExp.DecimalPlaces = 2;
            this.numericUpDownExp.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownExp.Location = new System.Drawing.Point(120, 19);
            this.numericUpDownExp.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownExp.Name = "numericUpDownExp";
            this.numericUpDownExp.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownExp.TabIndex = 11;
            this.numericUpDownExp.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownExp.ValueChanged += new System.EventHandler(this.numericUpDownExp_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Сред.кв. отклонение";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Мат. ожидание";
            // 
            // textBoxCurrentF
            // 
            this.textBoxCurrentF.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCurrentF.Location = new System.Drawing.Point(1186, 6);
            this.textBoxCurrentF.Name = "textBoxCurrentF";
            this.textBoxCurrentF.ReadOnly = true;
            this.textBoxCurrentF.Size = new System.Drawing.Size(61, 31);
            this.textBoxCurrentF.TabIndex = 10;
            // 
            // textBoxCurrentTime
            // 
            this.textBoxCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCurrentTime.Location = new System.Drawing.Point(562, 6);
            this.textBoxCurrentTime.Name = "textBoxCurrentTime";
            this.textBoxCurrentTime.ReadOnly = true;
            this.textBoxCurrentTime.Size = new System.Drawing.Size(61, 31);
            this.textBoxCurrentTime.TabIndex = 9;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(421, 61);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(155, 30);
            this.checkBox.TabIndex = 8;
            this.checkBox.Text = "Режим отображения\r\n\"стационарные функции\"";
            this.checkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // trackBarEvolTime
            // 
            this.trackBarEvolTime.AutoSize = false;
            this.trackBarEvolTime.BackColor = System.Drawing.Color.Silver;
            this.trackBarEvolTime.Location = new System.Drawing.Point(3, 6);
            this.trackBarEvolTime.Name = "trackBarEvolTime";
            this.trackBarEvolTime.Size = new System.Drawing.Size(553, 31);
            this.trackBarEvolTime.TabIndex = 7;
            this.trackBarEvolTime.ValueChanged += new System.EventHandler(this.trackBarEvolTime_ValueChanged);
            // 
            // trackBarFourier
            // 
            this.trackBarFourier.AutoSize = false;
            this.trackBarFourier.BackColor = System.Drawing.Color.Silver;
            this.trackBarFourier.Location = new System.Drawing.Point(626, 6);
            this.trackBarFourier.Name = "trackBarFourier";
            this.trackBarFourier.Size = new System.Drawing.Size(554, 31);
            this.trackBarFourier.TabIndex = 6;
            this.trackBarFourier.ValueChanged += new System.EventHandler(this.trackBarFourier_ValueChanged);
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Font = new System.Drawing.Font("Segoe Script", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartStop.Location = new System.Drawing.Point(625, 43);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(622, 81);
            this.buttonStartStop.TabIndex = 5;
            this.buttonStartStop.Text = "Начать моделирование";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxdx);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxU);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxdMult);
            this.groupBox1.Controls.Add(this.textBoxd);
            this.groupBox1.Location = new System.Drawing.Point(11, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 79);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Начальные данные";
            // 
            // textBoxdx
            // 
            this.textBoxdx.Location = new System.Drawing.Point(316, 19);
            this.textBoxdx.Name = "textBoxdx";
            this.textBoxdx.Size = new System.Drawing.Size(74, 20);
            this.textBoxdx.TabIndex = 4;
            this.textBoxdx.Text = "0,001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Расширение области";
            // 
            // textBoxU
            // 
            this.textBoxU.Location = new System.Drawing.Point(120, 19);
            this.textBoxU.Name = "textBoxU";
            this.textBoxU.Size = new System.Drawing.Size(74, 20);
            this.textBoxU.TabIndex = 0;
            this.textBoxU.Text = "50";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ширина ямы d (%)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Дискретизация по x";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Глубина ямы U";
            // 
            // textBoxdMult
            // 
            this.textBoxdMult.Location = new System.Drawing.Point(316, 48);
            this.textBoxdMult.Name = "textBoxdMult";
            this.textBoxdMult.Size = new System.Drawing.Size(74, 20);
            this.textBoxdMult.TabIndex = 6;
            this.textBoxdMult.Text = "1";
            // 
            // textBoxd
            // 
            this.textBoxd.Location = new System.Drawing.Point(120, 45);
            this.textBoxd.Name = "textBoxd";
            this.textBoxd.Size = new System.Drawing.Size(74, 20);
            this.textBoxd.TabIndex = 2;
            this.textBoxd.Text = "80";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 763);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMain";
            this.Text = "Эволюция плотности вероятности";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEoPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartF)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEvolTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFourier)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxd;
        private System.Windows.Forms.TextBox textBoxdx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxdMult;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEoPD;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartF;
        private System.Windows.Forms.TrackBar trackBarFourier;
        private System.Windows.Forms.Button buttonStartStop;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TrackBar trackBarEvolTime;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.TextBox textBoxCurrentTime;
        private System.Windows.Forms.TextBox textBoxCurrentF;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownDev;
        private System.Windows.Forms.NumericUpDown numericUpDownExp;
    }
}

