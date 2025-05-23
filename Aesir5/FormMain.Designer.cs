namespace Aesir5
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            // Instantiation of all controls - ensuring each is instantiated only ONCE
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripSeparator(); // Separator for File menu
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clothingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monstersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x48ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x36ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x24ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            
            // Suspend layout for main components
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.tileSizeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.windowsToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(603, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.toolStripMenuItem, // This is the separator
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newMapToolStripMenuItem.Text = "&New Map Window";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // toolStripMenuItem (File menu separator)
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(214, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clothingToolStripMenuItem,
            this.monstersToolStripMenuItem,
            this.spellsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // clothingToolStripMenuItem
            // 
            this.clothingToolStripMenuItem.Name = "clothingToolStripMenuItem";
            this.clothingToolStripMenuItem.Size = new System.Drawing.Size(180, 22); // Standard size
            this.clothingToolStripMenuItem.Text = "&Clothing";
            this.clothingToolStripMenuItem.Click += new System.EventHandler(this.clothingToolStripMenuItem_Click);
            // 
            // monstersToolStripMenuItem
            // 
            this.monstersToolStripMenuItem.Name = "monstersToolStripMenuItem";
            this.monstersToolStripMenuItem.Size = new System.Drawing.Size(180, 22); // Standard size
            this.monstersToolStripMenuItem.Text = "&Monsters";
            this.monstersToolStripMenuItem.Click += new System.EventHandler(this.monstersToolStripMenuItem_Click);
            // 
            // spellsToolStripMenuItem
            // 
            this.spellsToolStripMenuItem.Name = "spellsToolStripMenuItem";
            this.spellsToolStripMenuItem.Size = new System.Drawing.Size(180, 22); // Standard size
            this.spellsToolStripMenuItem.Text = "&Spells";
            this.spellsToolStripMenuItem.Click += new System.EventHandler(this.spellsToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.windowsToolStripMenuItem.Text = "&Windows";
            // 
            // tileSizeToolStripMenuItem
            // 
            this.tileSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x48ToolStripMenuItem,
            this.x36ToolStripMenuItem,
            this.x24ToolStripMenuItem});
            this.tileSizeToolStripMenuItem.Name = "tileSizeToolStripMenuItem";
            this.tileSizeToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.tileSizeToolStripMenuItem.Text = "&Tile Size";
            // 
            // x48ToolStripMenuItem
            // 
            this.x48ToolStripMenuItem.CheckOnClick = true;
            this.x48ToolStripMenuItem.Name = "x48ToolStripMenuItem";
            this.x48ToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.x48ToolStripMenuItem.Text = "48 (High Quality/Slowest Speed)";
            this.x48ToolStripMenuItem.Click += new System.EventHandler(this.x48ToolStripMenuItem_Click);
            // 
            // x36ToolStripMenuItem
            // 
            this.x36ToolStripMenuItem.Checked = true;
            this.x36ToolStripMenuItem.CheckOnClick = true;
            this.x36ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.x36ToolStripMenuItem.Name = "x36ToolStripMenuItem";
            this.x36ToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.x36ToolStripMenuItem.Text = "36 (Default)";
            this.x36ToolStripMenuItem.Click += new System.EventHandler(this.x36ToolStripMenuItem_Click);
            // 
            // x24ToolStripMenuItem
            // 
            this.x24ToolStripMenuItem.CheckOnClick = true;
            this.x24ToolStripMenuItem.Name = "x24ToolStripMenuItem";
            this.x24ToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.x24ToolStripMenuItem.Text = "24 (Low Quality/Fastest Speed)";
            this.x24ToolStripMenuItem.Click += new System.EventHandler(this.x24ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionListToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // functionListToolStripMenuItem
            // 
            this.functionListToolStripMenuItem.Name = "functionListToolStripMenuItem";
            this.functionListToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.functionListToolStripMenuItem.Text = "Function List";
            this.functionListToolStripMenuItem.Click += new System.EventHandler(this.functionListToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(603, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(36, 17);
            this.lblStatus.Text = "None";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 466);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Mithia Map Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            
            // Resume layout for main components
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Field declarations (ensure these match the instantiated controls)
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clothingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monstersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spellsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x48ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x36ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x24ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem functionListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        
        // Public fields for other forms - these are not UI controls directly initialized here but part of FormMain
        public FormTile fTile;
        public FormObject fObject;
        public FormMap fMap;
        public Palette256[] tilePal;
        public Palette256[] tileCPal;
        public PaletteTable tileTBL;
        public PaletteTable tileCTBL;
        public ObjectInfo[] objectInfos;
        // public ArchiveInfo dat; // This was commented out in the original, retaining that state
    }
}
