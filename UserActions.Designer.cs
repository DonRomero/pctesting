namespace pctesting
{
    partial class UserActions
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
            this.UsersBox = new System.Windows.Forms.ComboBox();
            this.FrameInfo = new System.Windows.Forms.DataGridView();
            this.CreateReportButton = new System.Windows.Forms.Button();
            this.GeneralReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FrameInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // UsersBox
            // 
            this.UsersBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UsersBox.FormattingEnabled = true;
            this.UsersBox.Location = new System.Drawing.Point(12, 12);
            this.UsersBox.MaxDropDownItems = 20;
            this.UsersBox.Name = "UsersBox";
            this.UsersBox.Size = new System.Drawing.Size(121, 21);
            this.UsersBox.TabIndex = 0;
            // 
            // FrameInfo
            // 
            this.FrameInfo.AllowUserToAddRows = false;
            this.FrameInfo.AllowUserToDeleteRows = false;
            this.FrameInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FrameInfo.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.FrameInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FrameInfo.Location = new System.Drawing.Point(0, 39);
            this.FrameInfo.Name = "FrameInfo";
            this.FrameInfo.ReadOnly = true;
            this.FrameInfo.Size = new System.Drawing.Size(695, 201);
            this.FrameInfo.TabIndex = 1;
            // 
            // CreateReportButton
            // 
            this.CreateReportButton.Location = new System.Drawing.Point(150, 12);
            this.CreateReportButton.Name = "CreateReportButton";
            this.CreateReportButton.Size = new System.Drawing.Size(172, 23);
            this.CreateReportButton.TabIndex = 2;
            this.CreateReportButton.Text = "Составить подробный отчет";
            this.CreateReportButton.UseVisualStyleBackColor = true;
            this.CreateReportButton.Click += new System.EventHandler(this.CreateReportButton_Click);
            // 
            // GeneralReport
            // 
            this.GeneralReport.Location = new System.Drawing.Point(328, 12);
            this.GeneralReport.Name = "GeneralReport";
            this.GeneralReport.Size = new System.Drawing.Size(110, 23);
            this.GeneralReport.TabIndex = 3;
            this.GeneralReport.Text = "Общий отчет";
            this.GeneralReport.UseVisualStyleBackColor = true;
            this.GeneralReport.Click += new System.EventHandler(this.GeneralReport_Click);
            // 
            // UserActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 252);
            this.Controls.Add(this.GeneralReport);
            this.Controls.Add(this.CreateReportButton);
            this.Controls.Add(this.FrameInfo);
            this.Controls.Add(this.UsersBox);
            this.MaximizeBox = false;
            this.Name = "UserActions";
            this.Text = "Действия пользователя";
            this.Load += new System.EventHandler(this.UserActions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FrameInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox UsersBox;
        private System.Windows.Forms.DataGridView FrameInfo;
        private System.Windows.Forms.Button CreateReportButton;
        private System.Windows.Forms.Button GeneralReport;
    }
}