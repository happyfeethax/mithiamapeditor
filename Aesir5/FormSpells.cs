using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO; // Required for Path

namespace Aesir5
{
    public partial class FormSpells : Form
    {
        private readonly List<Point> selectedItems = new List<Point>();
        private Point focusedItem = new Point(-1,-1);
        private int sizeModifier;
        private bool showGrid;

        private string[] selectedEpfPaths;
        private string selectedPalPath;
        private string selectedTblPath;
        private Bitmap itemsBitmap;

        public bool ShowGrid
        {
            get { return showGrid; }
            set { showGrid = value; this.Invalidate(); }
        }

        public FormSpells()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(FormSpells_MouseWheel);
            this.Text = "Spells";

            this.lblEpfPathValue.Text = "Not selected";
            this.lblPalPathValue.Text = "Not selected";
            this.lblTblPathValue.Text = "Not selected";
            
            this.sizeModifier = ImageRenderer.Singleton.sizeModifier;
            if (this.sizeModifier <= 0) this.sizeModifier = 36; 

            this.itemsBitmap = new Bitmap(10 * this.sizeModifier, 10 * this.sizeModifier);
        }

        private void FormSpells_Load(object sender, EventArgs e)
        {
            Reload(false); 
            menuStrip.Visible = false; 
        }

        private int GetItemNumber(int x, int y)
        {
            return hScrollBarSpells.Value*100 + (y*10) + x;
        }

        private void RenderSpells()
        {
            if (this.itemsBitmap == null || 
                this.itemsBitmap.Width != 10 * (sizeModifier > 0 ? sizeModifier : 36) || 
                this.itemsBitmap.Height != 10 * (sizeModifier > 0 ? sizeModifier : 36))
            {
                 if (this.itemsBitmap != null) this.itemsBitmap.Dispose();
                 this.itemsBitmap = new Bitmap(10 * (sizeModifier > 0 ? sizeModifier : 36), 10 * (sizeModifier > 0 ? sizeModifier : 36));
            }

            using (Graphics g = Graphics.FromImage(this.itemsBitmap))
            {
                g.Clear(Color.DarkSlateBlue); 
                if (TileManager.SpellsEpf != null && TileManager.SpellsEpf.Length > 0 && 
                    TileManager.SpellsEpf[0] != null && TileManager.SpellsEpf[0].max > 0 &&
                    TileManager.SpellsPal != null && TileManager.SpellsTBL != null)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            int itemNumber = GetItemNumber(x, y);
                            if (itemNumber < TileManager.SpellsEpf[0].max)
                            {
                                g.DrawImage(ImageRenderer.Singleton.GetSpellBitmap(itemNumber), x * sizeModifier, y * sizeModifier);
                            }
                        }
                    }
                }
            }
            this.Invalidate();
        }

        private void hScrollBarSpells_Scroll(object sender, ScrollEventArgs e)
        {
            selectedItems.Clear();
            RenderSpells();
        }

        void FormSpells_Paint(object sender, PaintEventArgs e)
        {
            if (this.itemsBitmap != null)
            {
                e.Graphics.DrawImage(this.itemsBitmap, 0, 0);
            }

            if (ShowGrid)
            {
                using (Pen penGrid = new Pen(Color.LightCyan, 1))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            e.Graphics.DrawRectangle(penGrid, i*sizeModifier, j*sizeModifier, sizeModifier, sizeModifier);
                        }
                    }
                }
            }

            if (selectedItems.Count > 0)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    foreach (var selectedItem in selectedItems)
                    {
                        e.Graphics.DrawRectangle(pen, selectedItem.X * sizeModifier, selectedItem.Y * sizeModifier, sizeModifier, sizeModifier);
                    }
                }
            }

            if (focusedItem.X >= 0 && focusedItem.Y >= 0)
            {
                using (Pen pen = new Pen(Color.Green, 2))
                {
                    e.Graphics.DrawRectangle(pen, focusedItem.X * sizeModifier, focusedItem.Y * sizeModifier, sizeModifier, sizeModifier);
                }
            }
        }

        private void FormSpells_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (hScrollBarSpells.Value - 1 >= hScrollBarSpells.Minimum) hScrollBarSpells.Value--;
            }
            else if (e.Delta < 0)
            {
                if (hScrollBarSpells.Value + 1 <= hScrollBarSpells.Maximum) hScrollBarSpells.Value++;
            }
            hScrollBarSpells_Scroll(null, null);
        }

        private void FormSpells_MouseMove(object sender, MouseEventArgs e)
        {
            int newFocusedItemX = e.X / sizeModifier;
            int newFocusedItemY = e.Y / sizeModifier;
            bool refresh = (newFocusedItemX != focusedItem.X || newFocusedItemY != focusedItem.Y);

            if (refresh)
            {
                focusedItem = new Point(newFocusedItemX, newFocusedItemY);
                this.Invalidate();
                int itemNumber = GetItemNumber(newFocusedItemX, newFocusedItemY);
                toolStripStatusLabel.Text = string.Format("Spell item number: {0}", itemNumber);
            }
        }

        private void FormSpells_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int newSelectedItemX = e.X / sizeModifier;
            int newSelectedItemY = e.Y / sizeModifier;

            Point selectedItem = new Point(newSelectedItemX, newSelectedItemY);
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (!selectedItems.Contains(selectedItem))
                    selectedItems.Add(selectedItem);
                else
                    selectedItems.Remove(selectedItem);
            }
            else
            {
                selectedItems.Clear();
                selectedItems.Add(selectedItem);
            }
            TileManager.ObjectSelection = GetSelection(); 
            TileManager.LastSelection = TileManager.SelectionType.Object; 
            this.Invalidate(); 
        }

        public Dictionary<Point, int> GetSelection()
        {
            Dictionary<Point, int> dictionary = new Dictionary<Point, int>();
            if (selectedItems.Count == 0) return dictionary;

            int xMin = selectedItems[0].X, yMin = selectedItems[0].Y;

            foreach (Point selectedItem in selectedItems)
            {
                if (xMin > selectedItem.X) xMin = selectedItem.X;
                if (yMin > selectedItem.Y) yMin = selectedItem.Y;
            }

            foreach (Point selectedItem in selectedItems)
            {
                dictionary.Add(new Point(selectedItem.X - xMin, selectedItem.Y - yMin), 
                    GetItemNumber(selectedItem.X, selectedItem.Y));
            }
            return dictionary;
        }

        private void findSpellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (NumberInputForm numberInputForm = new NumberInputForm(@"Enter spell item number"))
            {
                if (numberInputForm.ShowDialog(this) == DialogResult.OK)
                {
                    NavigateToSpellItem(numberInputForm.Number);
                }
            }
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGrid = showGridToolStripMenuItem.Checked;
        }

        public void NavigateToSpellItem(int number)
        {
            if (TileManager.SpellsEpf == null || TileManager.SpellsEpf.Length == 0 || 
                TileManager.SpellsEpf[0] == null || TileManager.SpellsEpf[0].max == 0) return;
            if (number < 0 || number >= TileManager.SpellsEpf[0].max ) return;

            int sbIndex = number / 100;
            int y = (number - sbIndex * 100) / 10;
            int x = number - sbIndex * 100 - y * 10;

            hScrollBarSpells.Value = sbIndex;
            selectedItems.Clear();
            selectedItems.Add(new Point(x, y));
            TileManager.ObjectSelection = GetSelection(); 
            TileManager.LastSelection = TileManager.SelectionType.Object; 
            RenderSpells();
        }

        public void Reload(bool render)
        {
            sizeModifier = ImageRenderer.Singleton.sizeModifier;
            if (sizeModifier <= 0) sizeModifier = 36;

            int panelHeight = this.panelFileSelection.Height;
            int scrollBarHeight = this.hScrollBarSpells.Height;
            int statusStripHeight = this.statusStrip.Height;
            int drawingAreaHeight = 10 * sizeModifier;
            int requiredClientHeight = panelHeight + drawingAreaHeight + scrollBarHeight + statusStripHeight;
            int requiredClientWidth = 10 * sizeModifier; 
            
            if (this.ClientSize.Width != requiredClientWidth || this.ClientSize.Height != requiredClientHeight)
            {
                 this.ClientSize = new Size(requiredClientWidth, requiredClientHeight);
            }
            MinimumSize = new Size(this.ClientSize.Width + 12, this.ClientSize.Height + 32); 
            MaximumSize = new Size(this.ClientSize.Width + 12, this.ClientSize.Height + 32); 

            if (this.itemsBitmap != null && 
                (this.itemsBitmap.Width != 10 * sizeModifier || this.itemsBitmap.Height != 10 * sizeModifier))
            {
                this.itemsBitmap.Dispose();
                this.itemsBitmap = null; 
            }
            if (this.itemsBitmap == null)
            {
                 this.itemsBitmap = new Bitmap(10 * sizeModifier, 10 * sizeModifier);
            }

            if (TileManager.SpellsEpf != null && TileManager.SpellsEpf.Length > 0 && 
                TileManager.SpellsEpf[0] != null && TileManager.SpellsEpf[0].max > 0 &&
                TileManager.SpellsPal != null && TileManager.SpellsTBL != null)
            {
                 hScrollBarSpells.Maximum = (TileManager.SpellsEpf[0].max / 100) + 9;
                 hScrollBarSpells.Visible = true;
            }
            else
            {
                hScrollBarSpells.Maximum = 9; 
                hScrollBarSpells.Value = 0;
                hScrollBarSpells.Visible = false; 
            }
            RenderSpells(); 
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog epfDialog = new OpenFileDialog())
            {
                epfDialog.Title = "Select EPF File(s) for Spells";
                epfDialog.Filter = "EPF files (*.epf)|*.epf|All files (*.*)|*.*";
                epfDialog.Multiselect = true;
                if (epfDialog.ShowDialog() == DialogResult.OK)
                {
                    this.selectedEpfPaths = epfDialog.FileNames;
                    this.lblEpfPathValue.Text = string.Join(", ", epfDialog.SafeFileNames);
                }
                else { this.selectedEpfPaths = null; this.lblEpfPathValue.Text = "Not selected"; }
            }

            using (OpenFileDialog palDialog = new OpenFileDialog())
            {
                palDialog.Title = "Select PAL File for Spells";
                palDialog.Filter = "PAL files (*.pal)|*.pal|All files (*.*)|*.*";
                palDialog.Multiselect = false;
                if (palDialog.ShowDialog() == DialogResult.OK)
                {
                    this.selectedPalPath = palDialog.FileName;
                    this.lblPalPathValue.Text = palDialog.SafeFileName;
                }
                 else { this.selectedPalPath = null; this.lblPalPathValue.Text = "Not selected"; }
            }

            using (OpenFileDialog tblDialog = new OpenFileDialog())
            {
                tblDialog.Title = "Select TBL File for Spells";
                tblDialog.Filter = "TBL files (*.tbl)|*.tbl|All files (*.*)|*.*";
                tblDialog.Multiselect = false;
                if (tblDialog.ShowDialog() == DialogResult.OK)
                {
                    this.selectedTblPath = tblDialog.FileName;
                    this.lblTblPathValue.Text = tblDialog.SafeFileName;
                }
                 else { this.selectedTblPath = null; this.lblTblPathValue.Text = "Not selected"; }
            }

            if (this.selectedEpfPaths != null && this.selectedEpfPaths.Length > 0 &&
                !string.IsNullOrEmpty(this.selectedPalPath) &&
                !string.IsNullOrEmpty(this.selectedTblPath))
            {
                try
                {
                    TileManager.LoadSpells(this.selectedEpfPaths, this.selectedPalPath, this.selectedTblPath);
                    if (TileManager.SpellsEpf != null && TileManager.SpellsEpf.Length > 0 &&
                        TileManager.SpellsEpf[0] != null && TileManager.SpellsEpf[0].max > 0 &&
                        TileManager.SpellsPal != null && TileManager.SpellsTBL != null)
                    {
                        this.Reload(true);
                    }
                    else
                    {
                        this.lblEpfPathValue.Text = "Load failed or empty";
                        this.lblPalPathValue.Text = "Load failed or empty";
                        this.lblTblPathValue.Text = "Load failed or empty";
                        this.Reload(false); 
                        MessageBox.Show("Failed to load valid spell data from selected files, or files were empty.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading spell files: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.lblEpfPathValue.Text = "Error";
                    this.lblPalPathValue.Text = "Error";
                    this.lblTblPathValue.Text = "Error";
                    this.Reload(false);
                }
            }
            else
            {
                if(TileManager.SpellsEpf != null && TileManager.SpellsEpf.Length > 0) TileManager.SpellsEpf[0] = null; 
                TileManager.SpellsPal = null;
                TileManager.SpellsTBL = null;
                this.Reload(false);
            }
        }
    }
}
