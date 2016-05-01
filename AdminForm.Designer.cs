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
            this.addUserButton = new System.Windows.Forms.Button();
            this.reportLabel = new System.Windows.Forms.Label();
            this.reportComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(15, 12);
            this.testButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(251, 30);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Тестировать компьютер";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(15, 187);
            this.reportButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(251, 30);
            this.reportButton.TabIndex = 1;
            this.reportButton.Text = "Сформировать отчёты";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // backupButton
            // 
            this.backupButton.Location = new System.Drawing.Point(15, 46);
            this.backupButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(251, 30);
            this.backupButton.TabIndex = 2;
            this.backupButton.Text = "Бекап Базы Данных";
            this.backupButton.UseVisualStyleBackColor = true;
            // 
            // addUserButton
            // 
            this.addUserButton.Location = new System.Drawing.Point(15, 81);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(251, 30);
            this.addUserButton.TabIndex = 3;
            this.addUserButton.Text = "Добавить пользователя";
            this.addUserButton.UseVisualStyleBackColor = true;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // reportLabel
            // 
            this.reportLabel.AutoSize = true;
            this.reportLabel.Location = new System.Drawing.Point(12, 138);
            this.reportLabel.Name = "reportLabel";
            this.reportLabel.Size = new System.Drawing.Size(161, 17);
            this.reportLabel.TabIndex = 4;
            this.reportLabel.Text = "Отчёт о пользователе:";
            // 
            // reportComboBox
            // 
            this.reportComboBox.FormattingEnabled = true;
            this.reportComboBox.Location = new System.Drawing.Point(15, 158);
            this.reportComboBox.Name = "reportComboBox";
            this.reportComboBox.Size = new System.Drawing.Size(251, 24);
            this.reportComboBox.TabIndex = 5;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 228);
            this.Controls.Add(this.reportComboBox);
            this.Controls.Add(this.reportLabel);
            this.Controls.Add(this.addUserButton);
            this.Controls.Add(this.backupButton);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.testButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.Label reportLabel;
        private System.Windows.Forms.ComboBox reportComboBox;
    }
}