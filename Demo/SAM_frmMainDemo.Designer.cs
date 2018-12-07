namespace Demo
{
    partial class frmMainDemo
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
            this.lstApps = new System.Windows.Forms.ListBox();
            this.groupInfo = new System.Windows.Forms.GroupBox();
            this.btnOpenFileExplore = new System.Windows.Forms.Button();
            this.txtInstallDir = new System.Windows.Forms.TextBox();
            this.txtAppID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblInsDir = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.groupStatus = new System.Windows.Forms.GroupBox();
            this.txtPID = new System.Windows.Forms.TextBox();
            this.lblPID = new System.Windows.Forms.Label();
            this.cboxIsUpdating = new System.Windows.Forms.CheckBox();
            this.cboxIsRunning = new System.Windows.Forms.CheckBox();
            this.btnStartWatchEvents = new System.Windows.Forms.Button();
            this.btnOpenVDFEditor = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.tmrEvents = new System.Windows.Forms.Timer(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLaunchApp = new System.Windows.Forms.Button();
            this.btnExitApp = new System.Windows.Forms.Button();
            this.groupInfo.SuspendLayout();
            this.groupStatus.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstApps
            // 
            this.lstApps.FormattingEnabled = true;
            this.lstApps.Location = new System.Drawing.Point(12, 12);
            this.lstApps.Name = "lstApps";
            this.lstApps.Size = new System.Drawing.Size(524, 225);
            this.lstApps.Sorted = true;
            this.lstApps.TabIndex = 0;
            this.lstApps.SelectedIndexChanged += new System.EventHandler(this.lstApps_SelectedIndexChanged);
            // 
            // groupInfo
            // 
            this.groupInfo.Controls.Add(this.btnOpenFileExplore);
            this.groupInfo.Controls.Add(this.txtInstallDir);
            this.groupInfo.Controls.Add(this.txtAppID);
            this.groupInfo.Controls.Add(this.txtName);
            this.groupInfo.Controls.Add(this.lblInsDir);
            this.groupInfo.Controls.Add(this.lblID);
            this.groupInfo.Controls.Add(this.lblName);
            this.groupInfo.Location = new System.Drawing.Point(12, 297);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(400, 144);
            this.groupInfo.TabIndex = 1;
            this.groupInfo.TabStop = false;
            this.groupInfo.Text = "Information";
            // 
            // btnOpenFileExplore
            // 
            this.btnOpenFileExplore.Enabled = false;
            this.btnOpenFileExplore.Location = new System.Drawing.Point(239, 108);
            this.btnOpenFileExplore.Name = "btnOpenFileExplore";
            this.btnOpenFileExplore.Size = new System.Drawing.Size(144, 23);
            this.btnOpenFileExplore.TabIndex = 6;
            this.btnOpenFileExplore.Text = "Open on File Explorer...";
            this.btnOpenFileExplore.UseVisualStyleBackColor = true;
            this.btnOpenFileExplore.Click += new System.EventHandler(this.btnOpenFileExplore_Click);
            // 
            // txtInstallDir
            // 
            this.txtInstallDir.Location = new System.Drawing.Point(117, 77);
            this.txtInstallDir.Name = "txtInstallDir";
            this.txtInstallDir.ReadOnly = true;
            this.txtInstallDir.Size = new System.Drawing.Size(266, 20);
            this.txtInstallDir.TabIndex = 5;
            // 
            // txtAppID
            // 
            this.txtAppID.Location = new System.Drawing.Point(88, 51);
            this.txtAppID.Name = "txtAppID";
            this.txtAppID.ReadOnly = true;
            this.txtAppID.Size = new System.Drawing.Size(295, 20);
            this.txtAppID.TabIndex = 4;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(50, 25);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(333, 20);
            this.txtName.TabIndex = 3;
            // 
            // lblInsDir
            // 
            this.lblInsDir.AutoSize = true;
            this.lblInsDir.Location = new System.Drawing.Point(6, 80);
            this.lblInsDir.Name = "lblInsDir";
            this.lblInsDir.Size = new System.Drawing.Size(105, 13);
            this.lblInsDir.TabIndex = 2;
            this.lblInsDir.Text = "Installation Directory:";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(6, 54);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(76, 13);
            this.lblID.TabIndex = 1;
            this.lblID.Text = "Application ID:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 28);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // groupStatus
            // 
            this.groupStatus.Controls.Add(this.txtPID);
            this.groupStatus.Controls.Add(this.lblPID);
            this.groupStatus.Controls.Add(this.cboxIsUpdating);
            this.groupStatus.Controls.Add(this.cboxIsRunning);
            this.groupStatus.Enabled = false;
            this.groupStatus.Location = new System.Drawing.Point(418, 297);
            this.groupStatus.Name = "groupStatus";
            this.groupStatus.Size = new System.Drawing.Size(118, 144);
            this.groupStatus.TabIndex = 2;
            this.groupStatus.TabStop = false;
            this.groupStatus.Text = "Status";
            // 
            // txtPID
            // 
            this.txtPID.Location = new System.Drawing.Point(9, 93);
            this.txtPID.Name = "txtPID";
            this.txtPID.ReadOnly = true;
            this.txtPID.Size = new System.Drawing.Size(98, 20);
            this.txtPID.TabIndex = 7;
            // 
            // lblPID
            // 
            this.lblPID.AutoSize = true;
            this.lblPID.Location = new System.Drawing.Point(6, 77);
            this.lblPID.Name = "lblPID";
            this.lblPID.Size = new System.Drawing.Size(67, 13);
            this.lblPID.TabIndex = 7;
            this.lblPID.Text = "PID (Guess):";
            // 
            // cboxIsUpdating
            // 
            this.cboxIsUpdating.AutoSize = true;
            this.cboxIsUpdating.Enabled = false;
            this.cboxIsUpdating.Location = new System.Drawing.Point(6, 48);
            this.cboxIsUpdating.Name = "cboxIsUpdating";
            this.cboxIsUpdating.Size = new System.Drawing.Size(69, 17);
            this.cboxIsUpdating.TabIndex = 1;
            this.cboxIsUpdating.Text = "Updating";
            this.cboxIsUpdating.UseVisualStyleBackColor = true;
            // 
            // cboxIsRunning
            // 
            this.cboxIsRunning.AutoSize = true;
            this.cboxIsRunning.Enabled = false;
            this.cboxIsRunning.Location = new System.Drawing.Point(6, 25);
            this.cboxIsRunning.Name = "cboxIsRunning";
            this.cboxIsRunning.Size = new System.Drawing.Size(66, 17);
            this.cboxIsRunning.TabIndex = 0;
            this.cboxIsRunning.Text = "Running";
            this.cboxIsRunning.UseVisualStyleBackColor = true;
            // 
            // btnStartWatchEvents
            // 
            this.btnStartWatchEvents.Enabled = false;
            this.btnStartWatchEvents.Location = new System.Drawing.Point(197, 268);
            this.btnStartWatchEvents.Name = "btnStartWatchEvents";
            this.btnStartWatchEvents.Size = new System.Drawing.Size(183, 23);
            this.btnStartWatchEvents.TabIndex = 7;
            this.btnStartWatchEvents.Text = "Start watching for Events";
            this.btnStartWatchEvents.UseVisualStyleBackColor = true;
            this.btnStartWatchEvents.Click += new System.EventHandler(this.btnStartWatchEvents_Click);
            // 
            // btnOpenVDFEditor
            // 
            this.btnOpenVDFEditor.Location = new System.Drawing.Point(386, 268);
            this.btnOpenVDFEditor.Name = "btnOpenVDFEditor";
            this.btnOpenVDFEditor.Size = new System.Drawing.Size(150, 23);
            this.btnOpenVDFEditor.TabIndex = 8;
            this.btnOpenVDFEditor.Text = "Open VDF Editor";
            this.btnOpenVDFEditor.UseVisualStyleBackColor = true;
            this.btnOpenVDFEditor.Click += new System.EventHandler(this.btnOpenVDFEditor_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 449);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(547, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(134, 17);
            this.toolStripStatus.Text = "Loading Steam Library...";
            // 
            // tmrEvents
            // 
            this.tmrEvents.Interval = 1000;
            this.tmrEvents.Tick += new System.EventHandler(this.tmrEvents_Tick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(12, 268);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(179, 23);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh Library";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnLaunchApp
            // 
            this.btnLaunchApp.Enabled = false;
            this.btnLaunchApp.Location = new System.Drawing.Point(12, 243);
            this.btnLaunchApp.Name = "btnLaunchApp";
            this.btnLaunchApp.Size = new System.Drawing.Size(274, 23);
            this.btnLaunchApp.TabIndex = 11;
            this.btnLaunchApp.Text = "Launch";
            this.btnLaunchApp.UseVisualStyleBackColor = true;
            this.btnLaunchApp.Click += new System.EventHandler(this.btnLaunchApp_Click);
            // 
            // btnExitApp
            // 
            this.btnExitApp.Enabled = false;
            this.btnExitApp.Location = new System.Drawing.Point(292, 243);
            this.btnExitApp.Name = "btnExitApp";
            this.btnExitApp.Size = new System.Drawing.Size(243, 23);
            this.btnExitApp.TabIndex = 12;
            this.btnExitApp.Text = "Close App";
            this.btnExitApp.UseVisualStyleBackColor = true;
            this.btnExitApp.Click += new System.EventHandler(this.btnExitApp_Click);
            // 
            // frmMainDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 471);
            this.Controls.Add(this.btnExitApp);
            this.Controls.Add(this.btnLaunchApp);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnOpenVDFEditor);
            this.Controls.Add(this.btnStartWatchEvents);
            this.Controls.Add(this.groupStatus);
            this.Controls.Add(this.groupInfo);
            this.Controls.Add(this.lstApps);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMainDemo";
            this.ShowIcon = false;
            this.Text = "Steam Apps Management Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainDemo_FormClosing);
            this.Load += new System.EventHandler(this.frmMainDemo_Load);
            this.groupInfo.ResumeLayout(false);
            this.groupInfo.PerformLayout();
            this.groupStatus.ResumeLayout(false);
            this.groupStatus.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstApps;
        private System.Windows.Forms.GroupBox groupInfo;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblInsDir;
        private System.Windows.Forms.Button btnOpenFileExplore;
        private System.Windows.Forms.TextBox txtInstallDir;
        private System.Windows.Forms.TextBox txtAppID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupStatus;
        private System.Windows.Forms.Button btnStartWatchEvents;
        private System.Windows.Forms.Button btnOpenVDFEditor;
        private System.Windows.Forms.CheckBox cboxIsUpdating;
        private System.Windows.Forms.CheckBox cboxIsRunning;
        private System.Windows.Forms.TextBox txtPID;
        private System.Windows.Forms.Label lblPID;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowse;
        private System.Windows.Forms.Timer tmrEvents;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLaunchApp;
        private System.Windows.Forms.Button btnExitApp;
    }
}