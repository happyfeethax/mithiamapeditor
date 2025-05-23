namespace Aesir5
{
    partial class FormMonsters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMonsters));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.hScrollBarMonsters = new System.Windows.Forms.HScrollBar();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findMonsterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            
            // New UI Elements for File Selection
            this.panelFileSelection = new System.Windows.Forms.Panel();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.lblEpfPathText = new System.Windows.Forms.Label();
            this.lblPalPathText = new System.Windows.Forms.Label();
            this.lblTblPathText = new System.Windows.Forms.Label();
            this.lblEpfPathValue = new System.Windows.Forms.Label();
            this.lblPalPathValue = new System.Windows.Forms.Label();
            this.lblTblPathValue = new System.Windows.Forms.Label();
            
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelFileSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 271); // Will adjust based on panel
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(250, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // hScrollBarMonsters
            // 
            this.hScrollBarMonsters.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBarMonsters.Location = new System.Drawing.Point(0, 254); // Will adjust based on panel
            this.hScrollBarMonsters.Name = "hScrollBarMonsters";
            this.hScrollBarMonsters.Size = new System.Drawing.Size(250, 17);
            this.hScrollBarMonsters.TabIndex = 4;
            this.hScrollBarMonsters.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarMonsters_Scroll);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(250, 24);
            this.menuStrip.TabIndex = 5;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findMonsterToolStripMenuItem});
            this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // findMonsterToolStripMenuItem
            // 
            this.findMonsterToolStripMenuItem.Name = "findMonsterToolStripMenuItem";
            this.findMonsterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findMonsterToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.findMonsterToolStripMenuItem.Text = "&Find Monster";
            this.findMonsterToolStripMenuItem.Click += new System.EventHandler(this.findMonsterToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.CheckOnClick = true;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGridToolStripMenuItem});
            this.viewToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // showGridToolStripMenuItem
            // 
            this.showGridToolStripMenuItem.CheckOnClick = true;
            this.showGridToolStripMenuItem.Name = "showGridToolStripMenuItem";
            this.showGridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.showGridToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.showGridToolStripMenuItem.Text = "Show &Grid";
            this.showGridToolStripMenuItem.Click += new System.EventHandler(this.showGridToolStripMenuItem_Click);
            //
            // panelFileSelection
            //
            this.panelFileSelection.Controls.Add(this.btnLoadData);
            this.panelFileSelection.Controls.Add(this.lblEpfPathText);
            this.panelFileSelection.Controls.Add(this.lblEpfPathValue);
            this.panelFileSelection.Controls.Add(this.lblPalPathText);
            this.panelFileSelection.Controls.Add(this.lblPalPathValue);
            this.panelFileSelection.Controls.Add(this.lblTblPathText);
            this.panelFileSelection.Controls.Add(this.lblTblPathValue);
            this.panelFileSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFileSelection.Location = new System.Drawing.Point(0, 24); // After menuStrip
            this.panelFileSelection.Name = "panelFileSelection";
            this.panelFileSelection.Size = new System.Drawing.Size(250, 80);
            this.panelFileSelection.TabIndex = 6;
            //
            // btnLoadData
            //
            this.btnLoadData.Location = new System.Drawing.Point(5, 5);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(85, 23);
            this.btnLoadData.TabIndex = 0;
            this.btnLoadData.Text = "Load Data...";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            //
            // lblEpfPathText
            //
            this.lblEpfPathText.AutoSize = true;
            this.lblEpfPathText.Location = new System.Drawing.Point(5, 35);
            this.lblEpfPathText.Name = "lblEpfPathText";
            this.lblEpfPathText.Size = new System.Drawing.Size(61, 13);
            this.lblEpfPathText.Text = "EPF File(s):";
            //
            // lblEpfPathValue
            //
            this.lblEpfPathValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEpfPathValue.AutoEllipsis = true;
            this.lblEpfPathValue.Location = new System.Drawing.Point(95, 35);
            this.lblEpfPathValue.Name = "lblEpfPathValue";
            this.lblEpfPathValue.Size = new System.Drawing.Size(150, 13);
            this.lblEpfPathValue.Text = "Not selected";
            //
            // lblPalPathText
            //
            this.lblPalPathText.AutoSize = true;
            this.lblPalPathText.Location = new System.Drawing.Point(5, 50);
            this.lblPalPathText.Name = "lblPalPathText";
            this.lblPalPathText.Size = new System.Drawing.Size(50, 13);
            this.lblPalPathText.Text = "PAL File:";
            //
            // lblPalPathValue
            //
            this.lblPalPathValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPalPathValue.AutoEllipsis = true;
            this.lblPalPathValue.Location = new System.Drawing.Point(95, 50);
            this.lblPalPathValue.Name = "lblPalPathValue";
            this.lblPalPathValue.Size = new System.Drawing.Size(150, 13);
            this.lblPalPathValue.Text = "Not selected";
            //
            // lblTblPathText
            //
            this.lblTblPathText.AutoSize = true;
            this.lblTblPathText.Location = new System.Drawing.Point(5, 65);
            this.lblTblPathText.Name = "lblTblPathText";
            this.lblTblPathText.Size = new System.Drawing.Size(50, 13);
            this.lblTblPathText.Text = "TBL File:";
            //
            // lblTblPathValue
            //
            this.lblTblPathValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTblPathValue.AutoEllipsis = true;
            this.lblTblPathValue.Location = new System.Drawing.Point(95, 65);
            this.lblTblPathValue.Name = "lblTblPathValue";
            this.lblTblPathValue.Size = new System.Drawing.Size(150, 13);
            this.lblTblPathValue.Text = "Not selected";
            // 
            // FormMonsters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(250, 293); // Original size
            this.ControlBox = false;
            this.Controls.Add(this.panelFileSelection);
            this.Controls.Add(this.hScrollBarMonsters);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip); // menuStrip should be MainMenuStrip
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(256, 317 + 80); // Increased height for panel
            this.MinimizeBox = false;
            this.Name = "FormMonsters";
            this.ShowInTaskbar = false;
            this.Text = "Monsters";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.FormMonsters_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMonsters_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormMonsters_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMonsters_MouseMove);
            
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelFileSelection.ResumeLayout(false);
            this.panelFileSelection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.HScrollBar hScrollBarMonsters;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findMonsterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGridToolStripMenuItem;

        // Declarations for New UI Elements
        private System.Windows.Forms.Panel panelFileSelection;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Label lblEpfPathText;
        private System.Windows.Forms.Label lblPalPathText;
        private System.Windows.Forms.Label lblTblPathText;
        private System.Windows.Forms.Label lblEpfPathValue;
        private System.Windows.Forms.Label lblPalPathValue;
        private System.Windows.Forms.Label lblTblPathValue;
    }
}
