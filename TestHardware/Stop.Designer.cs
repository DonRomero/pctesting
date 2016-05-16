namespace pctesting.TestHardware
{
    partial class Stop
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
            this.StopTest = new System.Windows.Forms.Button();
            this.backgroundTest = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // StopTest
            // 
            this.StopTest.Location = new System.Drawing.Point(23, 21);
            this.StopTest.Name = "StopTest";
            this.StopTest.Size = new System.Drawing.Size(111, 23);
            this.StopTest.TabIndex = 0;
            this.StopTest.Text = "Остановить тест";
            this.StopTest.UseVisualStyleBackColor = true;
            this.StopTest.Click += new System.EventHandler(this.StopTest_Click);
            // 
            // backgroundTest
            // 
            this.backgroundTest.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundTest_DoWork);
            this.backgroundTest.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundTest_RunWorkerCompleted);
            // 
            // Stop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(158, 65);
            this.Controls.Add(this.StopTest);
            this.Name = "Stop";
            this.Text = "Stop";
            this.Load += new System.EventHandler(this.Stop_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StopTest;
        private System.ComponentModel.BackgroundWorker backgroundTest;
    }
}