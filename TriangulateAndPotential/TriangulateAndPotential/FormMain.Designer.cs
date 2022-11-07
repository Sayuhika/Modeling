namespace TriangulateAndPotential
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.checkBoxIsolines = new System.Windows.Forms.CheckBox();
            this.checkBoxGrid = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownMaxY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxX = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownQ = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownD = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownL = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownR = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGenGrid = new System.Windows.Forms.Button();
            this.buttonGenPhysic = new System.Windows.Forms.Button();
            this.checkBoxFieldLines = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBoxData.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownR)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1484, 961);
            this.splitContainer1.SplitterDistance = 1204;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1204, 961);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.buttonGenPhysic);
            this.splitContainer2.Size = new System.Drawing.Size(276, 961);
            this.splitContainer2.SplitterDistance = 675;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBoxData);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.buttonGenGrid);
            this.splitContainer3.Size = new System.Drawing.Size(276, 675);
            this.splitContainer3.SplitterDistance = 398;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBoxData
            // 
            this.groupBoxData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBoxData.BackgroundImage")));
            this.groupBoxData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBoxData.Controls.Add(this.checkBoxFieldLines);
            this.groupBoxData.Controls.Add(this.checkBoxIsolines);
            this.groupBoxData.Controls.Add(this.checkBoxGrid);
            this.groupBoxData.Controls.Add(this.groupBox1);
            this.groupBoxData.Controls.Add(this.numericUpDownQ);
            this.groupBoxData.Controls.Add(this.label4);
            this.groupBoxData.Controls.Add(this.numericUpDownD);
            this.groupBoxData.Controls.Add(this.label3);
            this.groupBoxData.Controls.Add(this.numericUpDownL);
            this.groupBoxData.Controls.Add(this.label2);
            this.groupBoxData.Controls.Add(this.numericUpDownR);
            this.groupBoxData.Controls.Add(this.label1);
            this.groupBoxData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxData.Location = new System.Drawing.Point(0, 0);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.Size = new System.Drawing.Size(276, 398);
            this.groupBoxData.TabIndex = 0;
            this.groupBoxData.TabStop = false;
            this.groupBoxData.Text = "Data:";
            // 
            // checkBoxIsolines
            // 
            this.checkBoxIsolines.AutoSize = true;
            this.checkBoxIsolines.Checked = true;
            this.checkBoxIsolines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsolines.Location = new System.Drawing.Point(15, 248);
            this.checkBoxIsolines.Name = "checkBoxIsolines";
            this.checkBoxIsolines.Size = new System.Drawing.Size(61, 17);
            this.checkBoxIsolines.TabIndex = 9;
            this.checkBoxIsolines.Text = "Isolines";
            this.checkBoxIsolines.UseVisualStyleBackColor = true;
            this.checkBoxIsolines.CheckedChanged += new System.EventHandler(this.checkBoxIsolines_CheckedChanged);
            // 
            // checkBoxGrid
            // 
            this.checkBoxGrid.AutoSize = true;
            this.checkBoxGrid.Checked = true;
            this.checkBoxGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGrid.Location = new System.Drawing.Point(15, 225);
            this.checkBoxGrid.Name = "checkBoxGrid";
            this.checkBoxGrid.Size = new System.Drawing.Size(45, 17);
            this.checkBoxGrid.TabIndex = 8;
            this.checkBoxGrid.Text = "Grid";
            this.checkBoxGrid.UseVisualStyleBackColor = true;
            this.checkBoxGrid.CheckedChanged += new System.EventHandler(this.checkBoxGrid_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownMaxY);
            this.groupBox1.Controls.Add(this.numericUpDownMaxX);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(15, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 77);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grid Size";
            // 
            // numericUpDownMaxY
            // 
            this.numericUpDownMaxY.Location = new System.Drawing.Point(62, 45);
            this.numericUpDownMaxY.Name = "numericUpDownMaxY";
            this.numericUpDownMaxY.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownMaxY.TabIndex = 11;
            this.numericUpDownMaxY.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numericUpDownMaxX
            // 
            this.numericUpDownMaxX.Location = new System.Drawing.Point(62, 19);
            this.numericUpDownMaxX.Name = "numericUpDownMaxX";
            this.numericUpDownMaxX.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownMaxX.TabIndex = 9;
            this.numericUpDownMaxX.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "MaxY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "MaxX";
            // 
            // numericUpDownQ
            // 
            this.numericUpDownQ.DecimalPlaces = 2;
            this.numericUpDownQ.Location = new System.Drawing.Point(33, 106);
            this.numericUpDownQ.Name = "numericUpDownQ";
            this.numericUpDownQ.Size = new System.Drawing.Size(86, 20);
            this.numericUpDownQ.TabIndex = 7;
            this.numericUpDownQ.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Q";
            // 
            // numericUpDownD
            // 
            this.numericUpDownD.DecimalPlaces = 2;
            this.numericUpDownD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownD.Location = new System.Drawing.Point(33, 80);
            this.numericUpDownD.Name = "numericUpDownD";
            this.numericUpDownD.Size = new System.Drawing.Size(86, 20);
            this.numericUpDownD.TabIndex = 5;
            this.numericUpDownD.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "D";
            // 
            // numericUpDownL
            // 
            this.numericUpDownL.DecimalPlaces = 2;
            this.numericUpDownL.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.numericUpDownL.Location = new System.Drawing.Point(33, 52);
            this.numericUpDownL.Name = "numericUpDownL";
            this.numericUpDownL.Size = new System.Drawing.Size(86, 20);
            this.numericUpDownL.TabIndex = 3;
            this.numericUpDownL.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "L";
            // 
            // numericUpDownR
            // 
            this.numericUpDownR.DecimalPlaces = 2;
            this.numericUpDownR.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.numericUpDownR.Location = new System.Drawing.Point(33, 26);
            this.numericUpDownR.Name = "numericUpDownR";
            this.numericUpDownR.Size = new System.Drawing.Size(86, 20);
            this.numericUpDownR.TabIndex = 1;
            this.numericUpDownR.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "R";
            // 
            // buttonGenGrid
            // 
            this.buttonGenGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGenGrid.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGenGrid.Image = ((System.Drawing.Image)(resources.GetObject("buttonGenGrid.Image")));
            this.buttonGenGrid.Location = new System.Drawing.Point(0, 0);
            this.buttonGenGrid.Name = "buttonGenGrid";
            this.buttonGenGrid.Size = new System.Drawing.Size(276, 273);
            this.buttonGenGrid.TabIndex = 0;
            this.buttonGenGrid.Text = "Create Grid";
            this.buttonGenGrid.UseVisualStyleBackColor = true;
            this.buttonGenGrid.Click += new System.EventHandler(this.buttonGenGrid_Click);
            // 
            // buttonGenPhysic
            // 
            this.buttonGenPhysic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGenPhysic.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGenPhysic.Image = ((System.Drawing.Image)(resources.GetObject("buttonGenPhysic.Image")));
            this.buttonGenPhysic.Location = new System.Drawing.Point(0, 0);
            this.buttonGenPhysic.Name = "buttonGenPhysic";
            this.buttonGenPhysic.Size = new System.Drawing.Size(276, 282);
            this.buttonGenPhysic.TabIndex = 0;
            this.buttonGenPhysic.Text = "Get Physic";
            this.buttonGenPhysic.UseVisualStyleBackColor = true;
            this.buttonGenPhysic.Click += new System.EventHandler(this.buttonGenPhysic_Click);
            // 
            // checkBoxFieldLines
            // 
            this.checkBoxFieldLines.AutoSize = true;
            this.checkBoxFieldLines.Checked = true;
            this.checkBoxFieldLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFieldLines.Location = new System.Drawing.Point(15, 271);
            this.checkBoxFieldLines.Name = "checkBoxFieldLines";
            this.checkBoxFieldLines.Size = new System.Drawing.Size(72, 17);
            this.checkBoxFieldLines.TabIndex = 10;
            this.checkBoxFieldLines.Text = "Field lines";
            this.checkBoxFieldLines.UseVisualStyleBackColor = true;
            this.checkBoxFieldLines.CheckedChanged += new System.EventHandler(this.checkBoxPL_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 961);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMain";
            this.Text = "The Window";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBoxData.ResumeLayout(false);
            this.groupBoxData.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxY;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownQ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGenGrid;
        private System.Windows.Forms.Button buttonGenPhysic;
        private System.Windows.Forms.CheckBox checkBoxIsolines;
        private System.Windows.Forms.CheckBox checkBoxGrid;
        private System.Windows.Forms.CheckBox checkBoxFieldLines;
    }
}

