namespace PLogger.UserInterface
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.byIPTab = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addIpFilterButton = new System.Windows.Forms.PictureBox();
            this.targetIpComboBox = new System.Windows.Forms.ComboBox();
            this.operationComboBox = new System.Windows.Forms.ComboBox();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.ipFilterdataGridView = new System.Windows.Forms.DataGridView();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.IPTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilterByIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filterTabControl = new System.Windows.Forms.TabControl();
            this.byIPTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addIpFilterButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipFilterdataGridView)).BeginInit();
            this.filterTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(347, 326);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.Location = new System.Drawing.Point(266, 325);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // byIPTab
            // 
            this.byIPTab.Controls.Add(this.label3);
            this.byIPTab.Controls.Add(this.label2);
            this.byIPTab.Controls.Add(this.label1);
            this.byIPTab.Controls.Add(this.addIpFilterButton);
            this.byIPTab.Controls.Add(this.targetIpComboBox);
            this.byIPTab.Controls.Add(this.operationComboBox);
            this.byIPTab.Controls.Add(this.ipAddressTextBox);
            this.byIPTab.Controls.Add(this.ipFilterdataGridView);
            this.byIPTab.Location = new System.Drawing.Point(4, 22);
            this.byIPTab.Name = "byIPTab";
            this.byIPTab.Padding = new System.Windows.Forms.Padding(3);
            this.byIPTab.Size = new System.Drawing.Size(402, 281);
            this.byIPTab.TabIndex = 0;
            this.byIPTab.Text = "By IP";
            this.byIPTab.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "IP address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Operation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Target IP";
            // 
            // addIpFilterButton
            // 
            this.addIpFilterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addIpFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("addIpFilterButton.Image")));
            this.addIpFilterButton.Location = new System.Drawing.Point(371, 35);
            this.addIpFilterButton.Name = "addIpFilterButton";
            this.addIpFilterButton.Size = new System.Drawing.Size(24, 21);
            this.addIpFilterButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.addIpFilterButton.TabIndex = 6;
            this.addIpFilterButton.TabStop = false;
            this.addIpFilterButton.Click += new System.EventHandler(this.AddIpFilterButton_Click);
            // 
            // targetIpComboBox
            // 
            this.targetIpComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetIpComboBox.FormattingEnabled = true;
            this.targetIpComboBox.Location = new System.Drawing.Point(6, 36);
            this.targetIpComboBox.Name = "targetIpComboBox";
            this.targetIpComboBox.Size = new System.Drawing.Size(121, 21);
            this.targetIpComboBox.TabIndex = 5;
            // 
            // operationComboBox
            // 
            this.operationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operationComboBox.FormattingEnabled = true;
            this.operationComboBox.Location = new System.Drawing.Point(133, 36);
            this.operationComboBox.Name = "operationComboBox";
            this.operationComboBox.Size = new System.Drawing.Size(78, 21);
            this.operationComboBox.TabIndex = 4;
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(217, 36);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(148, 20);
            this.ipAddressTextBox.TabIndex = 3;
            // 
            // ipFilterdataGridView
            // 
            this.ipFilterdataGridView.AllowUserToAddRows = false;
            this.ipFilterdataGridView.AllowUserToDeleteRows = false;
            this.ipFilterdataGridView.AllowUserToResizeRows = false;
            this.ipFilterdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ipFilterdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Delete,
            this.IPTarget,
            this.Operator,
            this.FilterByIP});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ipFilterdataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ipFilterdataGridView.GridColor = System.Drawing.Color.White;
            this.ipFilterdataGridView.Location = new System.Drawing.Point(6, 63);
            this.ipFilterdataGridView.Name = "ipFilterdataGridView";
            this.ipFilterdataGridView.ReadOnly = true;
            this.ipFilterdataGridView.RowHeadersVisible = false;
            this.ipFilterdataGridView.Size = new System.Drawing.Size(390, 212);
            this.ipFilterdataGridView.TabIndex = 2;
            this.ipFilterdataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.IpFilterdataGridView_CellClick);
            this.ipFilterdataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.IpFilterdataGridView_CellMouseEnter);
            // 
            // Delete
            // 
            this.Delete.HeaderText = "";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.ToolTipText = "Delete filter";
            this.Delete.Width = 20;
            // 
            // IPTarget
            // 
            this.IPTarget.DataPropertyName = "IPTarget";
            this.IPTarget.HeaderText = "Target IP";
            this.IPTarget.Name = "IPTarget";
            this.IPTarget.ReadOnly = true;
            // 
            // Operator
            // 
            this.Operator.DataPropertyName = "Operator";
            this.Operator.HeaderText = "Operation";
            this.Operator.Name = "Operator";
            this.Operator.ReadOnly = true;
            // 
            // FilterByIP
            // 
            this.FilterByIP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FilterByIP.DataPropertyName = "FilterByIP";
            this.FilterByIP.HeaderText = "IP Address";
            this.FilterByIP.Name = "FilterByIP";
            this.FilterByIP.ReadOnly = true;
            // 
            // filterTabControl
            // 
            this.filterTabControl.Controls.Add(this.byIPTab);
            this.filterTabControl.Location = new System.Drawing.Point(12, 13);
            this.filterTabControl.Name = "filterTabControl";
            this.filterTabControl.SelectedIndex = 0;
            this.filterTabControl.Size = new System.Drawing.Size(410, 307);
            this.filterTabControl.TabIndex = 2;
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.filterTabControl);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FilterForm_FormClosing);
            this.byIPTab.ResumeLayout(false);
            this.byIPTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addIpFilterButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipFilterdataGridView)).EndInit();
            this.filterTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TabPage byIPTab;
        private System.Windows.Forms.TabControl filterTabControl;
        private System.Windows.Forms.DataGridView ipFilterdataGridView;
        private System.Windows.Forms.ComboBox targetIpComboBox;
        private System.Windows.Forms.ComboBox operationComboBox;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox addIpFilterButton;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilterByIP;
    }
}