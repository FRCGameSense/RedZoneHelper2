namespace RedZoneHelper2
{
    partial class SettingsForm
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
            this.eventCodesBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.yearBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshSeasonDataButton = new System.Windows.Forms.Button();
            this.forceFullUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.addEventButton = new System.Windows.Forms.Button();
            this.eventNameBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).BeginInit();
            this.SuspendLayout();
            // 
            // eventCodesBox
            // 
            this.eventCodesBox.AutoCompleteCustomSource.AddRange(new string[] {
            "hello",
            "goodbye"});
            this.eventCodesBox.Location = new System.Drawing.Point(12, 123);
            this.eventCodesBox.Multiline = true;
            this.eventCodesBox.Name = "eventCodesBox";
            this.eventCodesBox.Size = new System.Drawing.Size(480, 78);
            this.eventCodesBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter the event you want to add, then click \"Add\"";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(339, 453);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(420, 453);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // yearBox
            // 
            this.yearBox.Location = new System.Drawing.Point(15, 28);
            this.yearBox.Maximum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.yearBox.Minimum = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(48, 20);
            this.yearBox.TabIndex = 18;
            this.yearBox.Value = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Select the year.";
            // 
            // refreshSeasonDataButton
            // 
            this.refreshSeasonDataButton.Location = new System.Drawing.Point(69, 25);
            this.refreshSeasonDataButton.Name = "refreshSeasonDataButton";
            this.refreshSeasonDataButton.Size = new System.Drawing.Size(138, 23);
            this.refreshSeasonDataButton.TabIndex = 20;
            this.refreshSeasonDataButton.Text = "Refresh Season Data";
            this.refreshSeasonDataButton.UseVisualStyleBackColor = true;
            this.refreshSeasonDataButton.Click += new System.EventHandler(this.refreshSeasonDataButton_Click);
            // 
            // forceFullUpdateCheckBox
            // 
            this.forceFullUpdateCheckBox.AutoSize = true;
            this.forceFullUpdateCheckBox.Location = new System.Drawing.Point(214, 30);
            this.forceFullUpdateCheckBox.Name = "forceFullUpdateCheckBox";
            this.forceFullUpdateCheckBox.Size = new System.Drawing.Size(110, 17);
            this.forceFullUpdateCheckBox.TabIndex = 21;
            this.forceFullUpdateCheckBox.Text = "Force Full Update";
            this.forceFullUpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // addEventButton
            // 
            this.addEventButton.Location = new System.Drawing.Point(261, 80);
            this.addEventButton.Name = "addEventButton";
            this.addEventButton.Size = new System.Drawing.Size(138, 23);
            this.addEventButton.TabIndex = 20;
            this.addEventButton.Text = "Add Event";
            this.addEventButton.UseVisualStyleBackColor = true;
            this.addEventButton.Click += new System.EventHandler(this.addEventButton_Click);
            // 
            // eventNameBox
            // 
            this.eventNameBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.eventNameBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.eventNameBox.Location = new System.Drawing.Point(15, 82);
            this.eventNameBox.Name = "eventNameBox";
            this.eventNameBox.Size = new System.Drawing.Size(240, 20);
            this.eventNameBox.TabIndex = 22;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.addEventButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 488);
            this.Controls.Add(this.eventNameBox);
            this.Controls.Add(this.forceFullUpdateCheckBox);
            this.Controls.Add(this.addEventButton);
            this.Controls.Add(this.refreshSeasonDataButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eventCodesBox);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox eventCodesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button refreshSeasonDataButton;
        private System.Windows.Forms.CheckBox forceFullUpdateCheckBox;
        private System.Windows.Forms.Button addEventButton;
        private System.Windows.Forms.TextBox eventNameBox;
    }
}