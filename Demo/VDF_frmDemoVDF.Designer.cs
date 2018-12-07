namespace Demo
{
    partial class frmDemoVDF
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
            this.btnLoadfromFile = new System.Windows.Forms.Button();
            this.tViewData = new System.Windows.Forms.TreeView();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.btnAddKey = new System.Windows.Forms.Button();
            this.btnDeleteNode = new System.Windows.Forms.Button();
            this.btnDeleteKey = new System.Windows.Forms.Button();
            this.groupInfo = new System.Windows.Forms.GroupBox();
            this.btnRevertInfo = new System.Windows.Forms.Button();
            this.btnSaveInfo = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnAddRootNode = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadfromFile
            // 
            this.btnLoadfromFile.Location = new System.Drawing.Point(12, 242);
            this.btnLoadfromFile.Name = "btnLoadfromFile";
            this.btnLoadfromFile.Size = new System.Drawing.Size(121, 23);
            this.btnLoadfromFile.TabIndex = 1;
            this.btnLoadfromFile.Text = "Load from File";
            this.btnLoadfromFile.UseVisualStyleBackColor = true;
            this.btnLoadfromFile.Click += new System.EventHandler(this.btnLoadfromFile_Click);
            // 
            // tViewData
            // 
            this.tViewData.HideSelection = false;
            this.tViewData.Location = new System.Drawing.Point(12, 12);
            this.tViewData.Name = "tViewData";
            this.tViewData.Size = new System.Drawing.Size(249, 224);
            this.tViewData.TabIndex = 2;
            this.tViewData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tViewData_AfterSelect);
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(140, 242);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(122, 23);
            this.btnSaveToFile.TabIndex = 3;
            this.btnSaveToFile.Text = "Save To File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Enabled = false;
            this.btnAddNode.Location = new System.Drawing.Point(12, 300);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(121, 23);
            this.btnAddNode.TabIndex = 4;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // btnAddKey
            // 
            this.btnAddKey.Enabled = false;
            this.btnAddKey.Location = new System.Drawing.Point(12, 329);
            this.btnAddKey.Name = "btnAddKey";
            this.btnAddKey.Size = new System.Drawing.Size(121, 23);
            this.btnAddKey.TabIndex = 5;
            this.btnAddKey.Text = "Add Key";
            this.btnAddKey.UseVisualStyleBackColor = true;
            this.btnAddKey.Click += new System.EventHandler(this.btnAddKey_Click);
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.Enabled = false;
            this.btnDeleteNode.Location = new System.Drawing.Point(140, 300);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(121, 23);
            this.btnDeleteNode.TabIndex = 7;
            this.btnDeleteNode.Text = "Delete Node";
            this.btnDeleteNode.UseVisualStyleBackColor = true;
            this.btnDeleteNode.Click += new System.EventHandler(this.btnDeleteNode_Click);
            // 
            // btnDeleteKey
            // 
            this.btnDeleteKey.Enabled = false;
            this.btnDeleteKey.Location = new System.Drawing.Point(140, 329);
            this.btnDeleteKey.Name = "btnDeleteKey";
            this.btnDeleteKey.Size = new System.Drawing.Size(121, 23);
            this.btnDeleteKey.TabIndex = 8;
            this.btnDeleteKey.Text = "Delete Key";
            this.btnDeleteKey.UseVisualStyleBackColor = true;
            this.btnDeleteKey.Click += new System.EventHandler(this.btnDeleteKey_Click);
            // 
            // groupInfo
            // 
            this.groupInfo.Controls.Add(this.btnRevertInfo);
            this.groupInfo.Controls.Add(this.btnSaveInfo);
            this.groupInfo.Controls.Add(this.txtValue);
            this.groupInfo.Controls.Add(this.lblValue);
            this.groupInfo.Controls.Add(this.txtName);
            this.groupInfo.Controls.Add(this.lblName);
            this.groupInfo.Enabled = false;
            this.groupInfo.Location = new System.Drawing.Point(12, 358);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(249, 100);
            this.groupInfo.TabIndex = 9;
            this.groupInfo.TabStop = false;
            this.groupInfo.Text = "Information";
            // 
            // btnRevertInfo
            // 
            this.btnRevertInfo.Enabled = false;
            this.btnRevertInfo.Location = new System.Drawing.Point(50, 71);
            this.btnRevertInfo.Name = "btnRevertInfo";
            this.btnRevertInfo.Size = new System.Drawing.Size(96, 23);
            this.btnRevertInfo.TabIndex = 13;
            this.btnRevertInfo.Text = "Revert";
            this.btnRevertInfo.UseVisualStyleBackColor = true;
            this.btnRevertInfo.Click += new System.EventHandler(this.btnRevertInfo_Click);
            // 
            // btnSaveInfo
            // 
            this.btnSaveInfo.Enabled = false;
            this.btnSaveInfo.Location = new System.Drawing.Point(152, 71);
            this.btnSaveInfo.Name = "btnSaveInfo";
            this.btnSaveInfo.Size = new System.Drawing.Size(91, 23);
            this.btnSaveInfo.TabIndex = 10;
            this.btnSaveInfo.Text = "Save";
            this.btnSaveInfo.UseVisualStyleBackColor = true;
            this.btnSaveInfo.Click += new System.EventHandler(this.btnSaveInfo_Click);
            // 
            // txtValue
            // 
            this.txtValue.Enabled = false;
            this.txtValue.Location = new System.Drawing.Point(50, 45);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(193, 20);
            this.txtValue.TabIndex = 11;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Enabled = false;
            this.lblValue.Location = new System.Drawing.Point(6, 48);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 12;
            this.lblValue.Text = "Value";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(50, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(193, 20);
            this.txtName.TabIndex = 10;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Enabled = false;
            this.lblName.Location = new System.Drawing.Point(6, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "VDF Files (*.vdf;*.acf)|*.vdf;*.acf|All Files (*.*)|*.*";
            // 
            // btnAddRootNode
            // 
            this.btnAddRootNode.Location = new System.Drawing.Point(12, 271);
            this.btnAddRootNode.Name = "btnAddRootNode";
            this.btnAddRootNode.Size = new System.Drawing.Size(250, 23);
            this.btnAddRootNode.TabIndex = 10;
            this.btnAddRootNode.Text = "Add Root Node";
            this.btnAddRootNode.UseVisualStyleBackColor = true;
            this.btnAddRootNode.Click += new System.EventHandler(this.btnAddRootNode_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "VDF File (*.vdf)|*.vdf|ACF File (*.acf)|*.acf";
            // 
            // frmDemoVDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 470);
            this.Controls.Add(this.btnAddRootNode);
            this.Controls.Add(this.groupInfo);
            this.Controls.Add(this.btnDeleteKey);
            this.Controls.Add(this.btnDeleteNode);
            this.Controls.Add(this.btnAddKey);
            this.Controls.Add(this.btnAddNode);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.tViewData);
            this.Controls.Add(this.btnLoadfromFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDemoVDF";
            this.ShowIcon = false;
            this.Text = "VDF Stream Demo";
            this.Load += new System.EventHandler(this.frmDemo_Load);
            this.groupInfo.ResumeLayout(false);
            this.groupInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLoadfromFile;
        private System.Windows.Forms.TreeView tViewData;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Button btnAddKey;
        private System.Windows.Forms.Button btnDeleteNode;
        private System.Windows.Forms.Button btnDeleteKey;
        private System.Windows.Forms.GroupBox groupInfo;
        private System.Windows.Forms.Button btnRevertInfo;
        private System.Windows.Forms.Button btnSaveInfo;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnAddRootNode;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

