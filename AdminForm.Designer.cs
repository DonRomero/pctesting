namespace pctesting
{
    partial class AdminForm
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
            this.testButton = new System.Windows.Forms.Button();
            this.reportButton = new System.Windows.Forms.Button();
            this.backupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(11, 10);
            this.testButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(188, 24);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Тестировать компьютер";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(11, 39);
            this.reportButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(188, 24);
            this.reportButton.TabIndex = 1;
            this.reportButton.Text = "Сформировать отчёт";
            this.reportButton.UseVisualStyleBackColor = true;
            // 
            // backupButton
            // 
            this.backupButton.Location = new System.Drawing.Point(11, 68);
            this.backupButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(188, 24);
            this.backupButton.TabIndex = 2;
            this.backupButton.Text = "Бекап Базы Данных";
            this.backupButton.UseVisualStyleBackColor = true;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 104);
            this.Controls.Add(this.backupButton);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.testButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Button backupButton;
    }
}