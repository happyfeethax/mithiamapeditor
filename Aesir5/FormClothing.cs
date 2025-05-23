using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO; // Required for Path

namespace Aesir5
{
    public partial class FormClothing : Form
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

        public FormClothing()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(FormClothing_MouseWheel);
            this.Text = "Clothing";

            this.lblEpfPathValue.Text = "Not selected";
            this.lblPalPathValue.Text = "Not selected";
            this.lblTblPathValue.Text = "Not selected";
            
            // Get initial sizeModifier from ImageRenderer singleton
            this.sizeModifier = ImageRenderer.Singleton.sizeModifier;
            if (this.sizeModifier <=0) this.sizeModifier = 36; // Sensible default

            this.itemsBitmap = new Bitmap(10 * this.sizeModifier, 10 * this.sizeModifier);
        }

        private void FormClothing_Load(object sender, EventArgs e)
        {
            Reload(false); 
            MinimumSize = new Size(this.ClientSize.Width + 12, this.ClientSize.Height + 32);
            MaximumSize = new Size(this.ClientSize.Width + 12, this.ClientSize.Height + 32); 
            menuStrip.Visible = false; 
        }

        private int GetItemNumber(int x, int y)
        {
            return hScrollBarClothing.Value*100 + (y*10) + x;
        }

        private void RenderItems()
        {
            int currentBitmapWidth = 10 * (sizeModifier > 0 ? sizeModifier : 36);
            int currentBitmapHeight = 10 * (sizeModifier > 0 ? sizeModifier : 36);
            if (this.itemsBitmap == null || this.itemsBitmap.Width != currentBitmapWidth || this.itemsBitmap.Height != currentBitmapHeight)
            {
                if (this.itemsBitmap != null) this.itemsBitmap.Dispose();
                this.itemsBitmap = new Bitmap(currentBitmapWidth, currentBitmapHeight);
            }

            using (Graphics g = Graphics.FromImage(this.itemsBitmap))
            {
                g.Clear(Color.DarkGray); 
                if (TileManager.ClothingEpf != null && TileManager.ClothingEpf.Length > 0 && 
                    TileManager.ClothingEpf[0] != null && TileManager.ClothingEpf[0].max > 0 &&
                    TileManager.ClothingPal != null && TileManager.ClothingTBL != null)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            int itemNumber = GetItemNumber(x, y);
                            if (itemNumber < TileManager.ClothingEpf[0].max)
                            {
                                g.DrawImage(ImageRenderer.Singleton.GetClothingBitmap(itemNumber), x * sizeModifier, y * sizeModifier);
                            }
                        }
                    }
                }
            }
            this.Invalidate();
        }

        private void hScrollBarClothing_Scroll(object sender, ScrollEventArgs e)
        {
            selectedItems.Clear();
            RenderItems();
        }

        void FormClothing_Paint(object sender, PaintEventArgs e)
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

        private void FormClothing_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (hScrollBarClothing.Value - 1 >= hScrollBarClothing.Minimum) hScrollBarClothing.Value--;
            }
            else if (e.Delta < 0)
            {
                if (hScrollBarClothing.Value + 1 <= hScrollBarClothing.Maximum) hScrollBarClothing.Value++;
            }
            hScrollBarClothing_Scroll(null, null);
        }

        private void FormClothing_MouseMove(object sender, MouseEventArgs e)
        {
            int newFocusedItemX = e.X / sizeModifier;
            int newFocusedItemY = e.Y / sizeModifier;
            bool refresh = (newFocusedItemX != focusedItem.X || newFocusedItemY != focusedItem.Y);

            if (refresh)
            {
                focusedItem = new Point(newFocusedItemX, newFocusedItemY);
                this.Invalidate();
                int itemNumber = GetItemNumber(newFocusedItemX, newFocusedItemY);
                toolStripStatusLabel.Text = string.Format("Clothing item number: {0}", itemNumber);
            }
        }

        private void FormClothing_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int newSelectedItemX = e.X / sizeModifier;
            int newSelectedItemY = e.Y / sizeModifier;

            Point selectedItem = new Point(newSelectedItemX, newSelectedItemY);
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control) // Corrected ModifierKeys check
            {
                if (!selectedItems.Contains(selectedItem))
                    selectedItems.Add(selectedItem);
                else
                    selectedItems.Remove(selectedItem); // Allow deselect with Ctrl+Click
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

        private void findClothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumberInputForm numberInputForm = new NumberInputForm(@"Enter clothing item number");
            if (numberInputForm.ShowDialog(this) == DialogResult.OK)
            {
                NavigateToClothingItem(numberInputForm.Number);
            }
            numberInputForm.Dispose();
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGrid = showGridToolStripMenuItem.Checked;
        }

        public void NavigateToClothingItem(int number)
        {
            if (TileManager.ClothingEpf == null || TileManager.ClothingEpf.Length == 0 || 
                TileManager.ClothingEpf[0] == null || TileManager.ClothingEpf[0].max == 0) return;
            if (number < 0 || number >= TileManager.ClothingEpf[0].max ) return;

            int sbIndex = number / 100;
            int y = (number - sbIndex * 100) / 10;
            int x = number - sbIndex * 100 - y * 10;

            hScrollBarClothing.Value = sbIndex;
            selectedItems.Clear();
            selectedItems.Add(new Point(x, y));
            TileManager.ObjectSelection = GetSelection(); 
            TileManager.LastSelection = TileManager.SelectionType.Object; 
            RenderItems();
        }

        public void Reload(bool render)
        {
            sizeModifier = ImageRenderer.Singleton.sizeModifier;
            if (sizeModifier <= 0) sizeModifier = 36; // Ensure sensible default

            int panelHeight = this.panelFileSelection.Height;
            int scrollBarHeight = this.hScrollBarClothing.Height;
            int statusStripHeight = this.statusStrip.Height;
            // ClientSize should be the area for the panel + drawing area + scrollbar + status strip
            // The menuStrip is handled by the Form itself when MainMenuStrip is set.
            int drawingAreaHeight = 10 * sizeModifier;
            int requiredClientHeight = panelHeight + drawingAreaHeight + scrollBarHeight + statusStripHeight;
            int requiredClientWidth = 10 * sizeModifier; 
            
            if (this.ClientSize.Width != requiredClientWidth || this.ClientSize.Height != requiredClientHeight)
            {
                 this.ClientSize = new Size(requiredClientWidth, requiredClientHeight);
            }
            // MinimumSize and MaximumSize are set in Form_Load and should be fine if ClientSize is correct.

            if (this.itemsBitmap != null) this.itemsBitmap.Dispose();
            this.itemsBitmap = new Bitmap(10 * sizeModifier, 10 * sizeModifier);

            if (TileManager.ClothingEpf != null && TileManager.ClothingEpf.Length > 0 && 
                TileManager.ClothingEpf[0] != null && TileManager.ClothingEpf[0].max > 0 &&
                TileManager.ClothingPal != null && TileManager.ClothingTBL != null) // Added PAL/TBL check for scrollbar
            {
                 hScrollBarClothing.Maximum = (TileManager.ClothingEpf[0].max / 100) + 9;
                 hScrollBarClothing.Visible = true;
            }
            else
            {
                hScrollBarClothing.Maximum = 9; 
                hScrollBarClothing.Value = 0;
                hScrollBarClothing.Visible = false; 
            }
            RenderItems(); 
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog epfDialog = new OpenFileDialog())
            {
                epfDialog.Title = "Select EPF File(s)";
                epfDialog.Filter = "EPF files (*.epf)|*.epf|All files (*.*)|*.*";
                epfDialog.Multiselect = true;
                if (epfDialog.ShowDialog() == DialogResult.OK)
                {
                    this.selectedEpfPaths = epfDialog.FileNames;
                    this.lblEpfPathValue.Text = string.Join(", ", epfDialog.SafeFileNames); 
                }
                else { this.selectedEpfPaths = null; this.lblEpfPathValue.Text = "Not selected"; } // Clear if cancelled
            }

            using (OpenFileDialog palDialog = new OpenFileDialog())
            {
                palDialog.Title = "Select PAL File";
                palDialog.Filter = "PAL files (*.pal)|*.pal|All files (*.*)|*.*";
                palDialog.Multiselect = false;
                if (palDialog.ShowDialog() == DialogResult.OK)
                {
                    this.selectedPalPath = palDialog.FileName;
                    this.lblPalPathValue.Text = palDialog.SafeFileName;
                }
                else { this.selectedPalPath = null; this.lblPalPathValue.Text = "Not selected"; } // Clear if cancelled
            }

            using (OpenFileDialog tblDialog = new OpenFileDialog())
            {
                tblDialog.Title = "Select TBL File";
                tblDialog.Filter = "TBL files (*.tbl)|*.tbl|All files (*.*)|*.*";
                tblDialog.Multiselect = false;
                if (tblDialog.ShowDialog() == DialogResult.OK)
                {
                    this.selectedTblPath = tblDialog.FileName;
                    this.lblTblPathValue.Text = tblDialog.SafeFileName;
                }
                else { this.selectedTblPath = null; this.lblTblPathValue.Text = "Not selected"; } // Clear if cancelled
            }

            if (this.selectedEpfPaths != null && this.selectedEpfPaths.Length > 0 && 
                !string.IsNullOrEmpty(this.selectedPalPath) && 
                !string.IsNullOrEmpty(this.selectedTblPath))
            {
                try
                {
                    TileManager.LoadClothing(this.selectedEpfPaths, this.selectedPalPath, this.selectedTblPath);
                    if (TileManager.ClothingEpf != null && TileManager.ClothingEpf.Length > 0 && 
                        TileManager.ClothingEpf[0] != null && TileManager.ClothingEpf[0].max > 0 &&
                        TileManager.ClothingPal != null && TileManager.ClothingTBL != null)
                    {
                        this.Reload(true); 
                    }
                    else
                    {
                        this.lblEpfPathValue.Text = "Load failed or empty";
                        this.lblPalPathValue.Text = "Load failed or empty";
                        this.lblTblPathValue.Text = "Load failed or empty";
                        this.Reload(false); // Clear display
                        MessageBox.Show("Failed to load valid clothing data from selected files, or files were empty.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading files: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.lblEpfPathValue.Text = "Error";
                    this.lblPalPathValue.Text = "Error";
                    this.lblTblPathValue.Text = "Error";
                    this.Reload(false); // Clear display
                }
            }
            else
            {
                // One or more files were not selected, so ensure we show an empty state.
                TileManager.ClothingEpf[0] = null; // Ensure TileManager reflects no loaded data
                TileManager.ClothingPal = null;
                TileManager.ClothingTBL = null;
                this.Reload(false);
            }
        }
    }
}
