using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aesir5
{
    public partial class FormMain : Form
    {
        public int sizeModifier;

        private FormClothing fClothing;
        private FormMonsters fMonsters;
        private FormSpells fSpells;

        public FormMain()
        {
            InitializeComponent();
            MdiChildActivate += FormMain_MdiChildActivate;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Extract files if required
            StartupUtilities.lblStatus = lblStatus;
            StartupUtilities.CreateMapEditorRegistryKey();
            if(!StartupUtilities.ExtractFiles(Application.StartupPath)) return;

            // Load tiles
            TileManager.lblStatus = lblStatus;
            TileManager.Load(Application.StartupPath);

            // Set forms to be MDI and show them
            fTile = new FormTile { MdiParent = this };
            fObject = new FormObject { MdiParent = this };
            fTile.Show();
            fObject.Show();
            new FormMap(this).Show();
        }

        private void FormMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormMap)
            {
                FormMap formMap = (FormMap)ActiveMdiChild;
                //formMap.MinimapWindow.Location = new Point(Width - 280, 20);
                formMap.MinimapWindow.SetImage(formMap.pnlImage.Image);
                formMap.MinimapWindow.Visible = formMap.IsMinimapVisible;
                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMinimap && mdiChild != formMap.MinimapWindow)
                        mdiChild.Visible = false;
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageRenderer.Singleton.Dispose();
        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormMap(this).Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you wish to exit?", "Exit Confirmation", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                foreach (Form mdiChild in MdiChildren) mdiChild.Close();
                if (MdiChildren.Length == 0) Close();
            }
        }

        private void x48ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            foreach (Form mdiChild in MdiChildren)
            {
                if (mdiChild is FormMap)
                {
                    result = MessageBox.Show("This will cause the map to be rendered again. Proceed?", "Resize Tiles", MessageBoxButtons.YesNo);
                    break;
                }
            }

            if (result == DialogResult.Yes)
            {
                x36ToolStripMenuItem.Checked = false;
                x24ToolStripMenuItem.Checked = false;
                ImageRenderer.Singleton.ClearTileCache();
                ImageRenderer.Singleton.ClearObjectCache();
                ImageRenderer.Singleton.sizeModifier = 48;
                fTile.Reload(true);
                fObject.Reload(true);

                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMap)
                    {
                        FormMap map = (FormMap)mdiChild;
                        map.Reload(true);
                    }
                }
            }
        }

        private void x36ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            foreach (Form mdiChild in MdiChildren)
            {
                if (mdiChild is FormMap)
                {
                    result = MessageBox.Show("This will cause the map to be rendered again. Proceed?", "Resize Tiles", MessageBoxButtons.YesNo);
                    break;
                }
            }

            if (result == DialogResult.Yes)
            {
                x48ToolStripMenuItem.Checked = false;
                x24ToolStripMenuItem.Checked = false;
                ImageRenderer.Singleton.ClearTileCache();
                ImageRenderer.Singleton.ClearObjectCache();
                ImageRenderer.Singleton.sizeModifier = 36;
                fTile.Reload(true);
                fObject.Reload(true);

                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMap)
                    {
                        FormMap map = (FormMap)mdiChild;
                        map.Reload(true);
                    }
                }
            }
        }

        private void x24ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            foreach (Form mdiChild in MdiChildren)
            {
                if (mdiChild is FormMap)
                {
                    result = MessageBox.Show("This will cause the map to be rendered again. Proceed?", "Resize Tiles", MessageBoxButtons.YesNo);
                    break;
                }
            }

            if (result == DialogResult.Yes)
            {
                x48ToolStripMenuItem.Checked = false;
                x36ToolStripMenuItem.Checked = false;
                ImageRenderer.Singleton.ClearTileCache();
                ImageRenderer.Singleton.ClearObjectCache();
                ImageRenderer.Singleton.sizeModifier = 24;
                fTile.Reload(true);
                fObject.Reload(true);

                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMap)
                    {
                        FormMap map = (FormMap)mdiChild;
                        map.Reload(true);
                    }
                }
            }
        }

        private void functionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormTile)
                MessageBox.Show("Left-Click: Select Tile\nCtrl + Left-Click: Select Multiple Tiles\nMouse-Wheel Up: Scroll Left\nMouse-Wheel Down: Scroll Right",
                    "Tile Window Help");
            else if (ActiveMdiChild is FormObject)
                MessageBox.Show("Left-Click: Select Object\nCtrl + Left-Click: Select Multiple Objects\nLeft-Click + Drag: Select Contiguous Objects\n" +
                    "Mouse-Wheel Up: Scroll Left\nMouse-Wheel Down: Scroll Right", "Object Window Help");
            else if (ActiveMdiChild is FormMap)
                MessageBox.Show("Left-Click: Paint Tile/Object Selection/Change Pass\nLeft-Click + Drag: Paint Contiguously\nCtrl + Left-Click: Copy Single Tile\n" +
                    "Ctrl + Left-Click + Drag: Copy Contiguous Tiles\nAlt + Left-Click: Copy Single Object\nAlt + Left-Click + Drag: Copy Contiguous Objects\n" +
                    "Shift + Left-Click: Copy Single Tile/Object\nShift + Left-Click + Drag: Copy Contiguous Tiles/Objects\nRight-Click: Fill Area\n" +
                    "Ctrl + Right-Click: Fill Map\nMouse-Wheel Up: Scroll Left\nMouse-Wheel Down: Scroll Right\nCtrl + Mouse-Wheel Up: Scroll Up\n" +
                    "Ctrl + Mouse-Wheel Down: Scroll Down", "Map Window Help");
        }

        private void clothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // FormClothing will be created in a future step.
            // For now, this would cause a compile error if FormClothing doesn't exist.
            // if (fClothing == null || fClothing.IsDisposed)
            // {
            //     fClothing = new FormClothing { MdiParent = this };
            //     fClothing.Show();
            // }
            // else
            // {
            //     fClothing.Activate();
            // }
            // FormClothing formClothing = new FormClothing { MdiParent = this };
            // formClothing.Show();
            if (this.fClothing == null || this.fClothing.IsDisposed)
            {
                this.fClothing = new FormClothing { MdiParent = this };
                this.fClothing.Show();
            }
            else
            {
                this.fClothing.Activate();
            }
        }

        private void monstersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // FormMonsters will be created in a future step.
            // if (fMonsters == null || fMonsters.IsDisposed)
            // {
            //     fMonsters = new FormMonsters { MdiParent = this };
            //     fMonsters.Show();
            // }
            // else
            // {
            //     fMonsters.Activate();
            // }
            // FormMonsters formMonsters = new FormMonsters { MdiParent = this };
            // formMonsters.Show();
            if (this.fMonsters == null || this.fMonsters.IsDisposed)
            {
                this.fMonsters = new FormMonsters { MdiParent = this };
                this.fMonsters.Show();
            }
            else
            {
                this.fMonsters.Activate();
            }
        }

        private void spellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // FormSpells will be created in a future step.
            // if (fSpells == null || fSpells.IsDisposed)
            // {
            //     fSpells = new FormSpells { MdiParent = this };
            //     fSpells.Show();
            // }
            // else
            // {
            //     fSpells.Activate();
            // }
            // FormSpells formSpells = new FormSpells { MdiParent = this };
            // formSpells.Show();
            if (this.fSpells == null || this.fSpells.IsDisposed)
            {
                this.fSpells = new FormSpells { MdiParent = this };
                this.fSpells.Show();
            }
            else
            {
                this.fSpells.Activate();
            }
        }
    }
}
