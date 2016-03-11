namespace RedZoneHelper2
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
            this.currentMatchesDGV = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.goButton = new System.Windows.Forms.Button();
            this.updateOPRDataButton = new System.Windows.Forms.Button();
            this.getFullRankingsButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.currentMatchesDGV)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentMatchesDGV
            // 
            this.currentMatchesDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentMatchesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.currentMatchesDGV.Location = new System.Drawing.Point(12, 63);
            this.currentMatchesDGV.Name = "currentMatchesDGV";
            this.currentMatchesDGV.Size = new System.Drawing.Size(1184, 654);
            this.currentMatchesDGV.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(335, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 19;
            this.dateTimePicker1.Value = new System.DateTime(2015, 4, 23, 8, 30, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(12, 27);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(174, 23);
            this.goButton.TabIndex = 18;
            this.goButton.Text = "Refresh";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // updateOPRDataButton
            // 
            this.updateOPRDataButton.Location = new System.Drawing.Point(1054, 29);
            this.updateOPRDataButton.Name = "updateOPRDataButton";
            this.updateOPRDataButton.Size = new System.Drawing.Size(131, 23);
            this.updateOPRDataButton.TabIndex = 18;
            this.updateOPRDataButton.Text = "Update OPR Data";
            this.updateOPRDataButton.UseVisualStyleBackColor = true;
            this.updateOPRDataButton.Click += new System.EventHandler(this.updateOPRDataButton_Click);
            // 
            // getFullRankingsButton
            // 
            this.getFullRankingsButton.Location = new System.Drawing.Point(917, 29);
            this.getFullRankingsButton.Name = "getFullRankingsButton";
            this.getFullRankingsButton.Size = new System.Drawing.Size(131, 23);
            this.getFullRankingsButton.TabIndex = 18;
            this.getFullRankingsButton.Text = "Get Full Rankings Data";
            this.getFullRankingsButton.UseVisualStyleBackColor = true;
            this.getFullRankingsButton.Click += new System.EventHandler(this.getFullRankingsButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1208, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 729);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.getFullRankingsButton);
            this.Controls.Add(this.updateOPRDataButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.currentMatchesDGV);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RedZone Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.currentMatchesDGV)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView currentMatchesDGV;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button updateOPRDataButton;
        private System.Windows.Forms.Button getFullRankingsButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

