namespace OcupancyDetection
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.RichTextBox();
            this.populationSize = new System.Windows.Forms.TextBox();
            this.maxGenerations = new System.Windows.Forms.TextBox();
            this.crossoverRate = new System.Windows.Forms.TextBox();
            this.mutationRate = new System.Windows.Forms.TextBox();
            this.C = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(730, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(26, 77);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(796, 239);
            this.output.TabIndex = 1;
            this.output.Text = "";
            // 
            // populationSize
            // 
            this.populationSize.Location = new System.Drawing.Point(48, 30);
            this.populationSize.Name = "populationSize";
            this.populationSize.Size = new System.Drawing.Size(73, 20);
            this.populationSize.TabIndex = 2;
            this.populationSize.Text = "30";
            // 
            // maxGenerations
            // 
            this.maxGenerations.Location = new System.Drawing.Point(164, 30);
            this.maxGenerations.Name = "maxGenerations";
            this.maxGenerations.Size = new System.Drawing.Size(73, 20);
            this.maxGenerations.TabIndex = 3;
            this.maxGenerations.Text = "1000";
            // 
            // crossoverRate
            // 
            this.crossoverRate.Location = new System.Drawing.Point(277, 30);
            this.crossoverRate.Name = "crossoverRate";
            this.crossoverRate.Size = new System.Drawing.Size(73, 20);
            this.crossoverRate.TabIndex = 4;
            this.crossoverRate.Text = "0.90";
            // 
            // mutationRate
            // 
            this.mutationRate.Location = new System.Drawing.Point(398, 30);
            this.mutationRate.Name = "mutationRate";
            this.mutationRate.Size = new System.Drawing.Size(73, 20);
            this.mutationRate.TabIndex = 5;
            this.mutationRate.Text = "0.1";
            // 
            // C
            // 
            this.C.Location = new System.Drawing.Point(515, 30);
            this.C.Name = "C";
            this.C.Size = new System.Drawing.Size(73, 20);
            this.C.TabIndex = 6;
            this.C.Text = "0.1";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(624, 30);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(73, 20);
            this.textBox6.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 342);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.C);
            this.Controls.Add(this.mutationRate);
            this.Controls.Add(this.crossoverRate);
            this.Controls.Add(this.maxGenerations);
            this.Controls.Add(this.populationSize);
            this.Controls.Add(this.output);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.TextBox populationSize;
        private System.Windows.Forms.TextBox maxGenerations;
        private System.Windows.Forms.TextBox crossoverRate;
        private System.Windows.Forms.TextBox mutationRate;
        private System.Windows.Forms.TextBox C;
        private System.Windows.Forms.TextBox textBox6;
    }
}

