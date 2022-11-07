namespace Labyrinth
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxLabHigh = new System.Windows.Forms.TextBox();
            this.textBoxLabWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGenAndDraw = new System.Windows.Forms.Button();
            this.buttonResolve = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxEntryH = new System.Windows.Forms.TextBox();
            this.textBoxEntryW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxExitH = new System.Windows.Forms.TextBox();
            this.textBoxExitW = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxLabHigh);
            this.groupBox1.Controls.Add(this.textBoxLabWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры лабиринта";
            // 
            // textBoxLabHigh
            // 
            this.textBoxLabHigh.Location = new System.Drawing.Point(60, 45);
            this.textBoxLabHigh.Name = "textBoxLabHigh";
            this.textBoxLabHigh.Size = new System.Drawing.Size(100, 20);
            this.textBoxLabHigh.TabIndex = 4;
            this.textBoxLabHigh.Text = "4095";
            this.textBoxLabHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxLabHigh.TextChanged += new System.EventHandler(this.textBoxLabHigh_TextChanged);
            // 
            // textBoxLabWidth
            // 
            this.textBoxLabWidth.Location = new System.Drawing.Point(60, 19);
            this.textBoxLabWidth.Name = "textBoxLabWidth";
            this.textBoxLabWidth.Size = new System.Drawing.Size(100, 20);
            this.textBoxLabWidth.TabIndex = 2;
            this.textBoxLabWidth.Text = "4095";
            this.textBoxLabWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxLabWidth.TextChanged += new System.EventHandler(this.textBoxLabWidth_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Высота";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ширина";
            // 
            // buttonGenAndDraw
            // 
            this.buttonGenAndDraw.Location = new System.Drawing.Point(12, 98);
            this.buttonGenAndDraw.Name = "buttonGenAndDraw";
            this.buttonGenAndDraw.Size = new System.Drawing.Size(257, 34);
            this.buttonGenAndDraw.TabIndex = 1;
            this.buttonGenAndDraw.Text = "Сгенерировать и нарисовать лабиринт";
            this.buttonGenAndDraw.UseVisualStyleBackColor = true;
            this.buttonGenAndDraw.Click += new System.EventHandler(this.buttonGenAndDraw_Click);
            // 
            // buttonResolve
            // 
            this.buttonResolve.Enabled = false;
            this.buttonResolve.Location = new System.Drawing.Point(277, 98);
            this.buttonResolve.Name = "buttonResolve";
            this.buttonResolve.Size = new System.Drawing.Size(257, 34);
            this.buttonResolve.TabIndex = 2;
            this.buttonResolve.Text = "Решить лабиринт";
            this.buttonResolve.UseVisualStyleBackColor = true;
            this.buttonResolve.Click += new System.EventHandler(this.buttonResolve_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxEntryH);
            this.groupBox2.Controls.Add(this.textBoxEntryW);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(188, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 80);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Координата входа";
            // 
            // textBoxEntryH
            // 
            this.textBoxEntryH.Location = new System.Drawing.Point(76, 45);
            this.textBoxEntryH.Name = "textBoxEntryH";
            this.textBoxEntryH.Size = new System.Drawing.Size(84, 20);
            this.textBoxEntryH.TabIndex = 4;
            this.textBoxEntryH.Text = "4095";
            this.textBoxEntryH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxEntryW
            // 
            this.textBoxEntryW.Location = new System.Drawing.Point(76, 19);
            this.textBoxEntryW.Name = "textBoxEntryW";
            this.textBoxEntryW.Size = new System.Drawing.Size(84, 20);
            this.textBoxEntryW.TabIndex = 2;
            this.textBoxEntryW.Text = "1";
            this.textBoxEntryW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "По высоте";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "По ширине";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxExitH);
            this.groupBox3.Controls.Add(this.textBoxExitW);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(364, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 80);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Координата выхода";
            // 
            // textBoxExitH
            // 
            this.textBoxExitH.Location = new System.Drawing.Point(76, 45);
            this.textBoxExitH.Name = "textBoxExitH";
            this.textBoxExitH.Size = new System.Drawing.Size(84, 20);
            this.textBoxExitH.TabIndex = 4;
            this.textBoxExitH.Text = "1";
            this.textBoxExitH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxExitW
            // 
            this.textBoxExitW.Location = new System.Drawing.Point(76, 19);
            this.textBoxExitW.Name = "textBoxExitW";
            this.textBoxExitW.Size = new System.Drawing.Size(84, 20);
            this.textBoxExitW.TabIndex = 2;
            this.textBoxExitW.Text = "4095";
            this.textBoxExitW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "По высоте";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "По ширине";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 138);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(520, 23);
            this.progressBar.TabIndex = 7;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 171);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonResolve);
            this.Controls.Add(this.buttonGenAndDraw);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormMain";
            this.Text = "Лабиринт";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxLabHigh;
        private System.Windows.Forms.TextBox textBoxLabWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGenAndDraw;
        private System.Windows.Forms.Button buttonResolve;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxEntryH;
        private System.Windows.Forms.TextBox textBoxEntryW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxExitH;
        private System.Windows.Forms.TextBox textBoxExitW;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

