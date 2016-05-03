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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.testButton = new System.Windows.Forms.Button();
            this.reportButton = new System.Windows.Forms.Button();
            this.backupButton = new System.Windows.Forms.Button();
            this.addUserButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pctestingIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.exitButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(11, 10);
            this.testButton.Margin = new System.Windows.Forms.Padding(2);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(188, 24);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Тестировать компьютер";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(11, 94);
            this.reportButton.Margin = new System.Windows.Forms.Padding(2);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(188, 24);
            this.reportButton.TabIndex = 1;
            this.reportButton.Text = "Сформировать отчёты";
            this.reportButton.UseVisualStyleBackColor = true;
            // 
            // backupButton
            // 
            this.backupButton.Location = new System.Drawing.Point(11, 37);
            this.backupButton.Margin = new System.Windows.Forms.Padding(2);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(188, 24);
            this.backupButton.TabIndex = 2;
            this.backupButton.Text = "Бекап Базы Данных";
            this.backupButton.UseVisualStyleBackColor = true;
            // 
            // addUserButton
            // 
            this.addUserButton.Location = new System.Drawing.Point(11, 66);
            this.addUserButton.Margin = new System.Windows.Forms.Padding(2);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(188, 24);
            this.addUserButton.TabIndex = 3;
            this.addUserButton.Text = "Добавить пользователя";
            this.addUserButton.UseVisualStyleBackColor = true;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pctestingIcon
            // 
            this.pctestingIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.pctestingIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("pctestingIcon.Icon")));
            this.pctestingIcon.Text = "pctesting";
            this.pctestingIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pctestingIcon_MouseDoubleClick);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(11, 123);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(188, 24);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Назад";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 153);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.addUserButton);
            this.Controls.Add(this.backupButton);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.testButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminForm_FormClosing);
            this.Move += new System.EventHandler(this.AdminForm_Move);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon pctestingIcon;
        private System.Windows.Forms.Button exitButton;
    }
}