using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Aesir5
{
    public partial class FormClothing : Form
    {
        private readonly List<Point> selectedItems = new List<Point>();
        private Point focusedItem = new Point(-1,-1);
        private int sizeModifier;
        private bool showGrid;
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
        }

        private void FormClothing_Load(object sender, EventArgs e)
        {
            Reload(false);
            MinimumSize = new Size(MinimumSize.Width + 10, MinimumSize.Height + 10);
            MaximumSize = new Size(MaximumSize.Width + 10, MaximumSize.Height + 10);
            if (this.BackgroundImage == null) this.BackgroundImage = new Bitmap(10 * sizeModifier, 10 * sizeModifier);
            menuStrip.Visible = false;
            if (TileManager.ClothingEpf != null && TileManager.ClothingEpf.Length > 0 && TileManager.ClothingEpf[0] != null)
            {
                hScrollBarClothing.Maximum = (TileManager.ClothingEpf[0].max / 100) + 9;
            }
            else
            {
                hScrollBarClothing.Maximum = 9; // Default if not loaded
            }
            RenderItems();
        }

        private int GetItemNumber(int x, int y)
        {
            return hScrollBarClothing.Value*100 + (y*10) + x;
        }

        private void RenderItems()
        {
            if (this.BackgroundImage == null) this.BackgroundImage = new Bitmap(10 * sizeModifier, 10 * sizeModifier);
            Graphics g = Graphics.FromImage(this.BackgroundImage);
            g.Clear(Color.DarkGray); // Changed background for differentiation

            if (TileManager.ClothingEpf == null || TileManager.ClothingEpf.Length == 0 || TileManager.ClothingEpf[0] == null)
            {
                g.Dispose();
                this.Invalidate();
                return; // Can't render if data isn't loaded
            }

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

            g.Dispose();
            this.Invalidate();
        }

        private void hScrollBarClothing_Scroll(object sender, ScrollEventArgs e)
        {
            selectedItems.Clear();
            RenderItems();
        }

        void FormClothing_Paint(object sender, PaintEventArgs e)
        {
            if (ShowGrid)
            {
                Pen penGrid = new Pen(Color.LightCyan, 1);
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        e.Graphics.DrawRectangle(penGrid, i*sizeModifier, j*sizeModifier, sizeModifier, sizeModifier);
                    }
                }
                penGrid.Dispose();
            }

            if (selectedItems.Count > 0)
            {
                Pen pen = new Pen(Color.Red, 2);
                foreach (var selectedItem in selectedItems)
                {
                    e.Graphics.DrawRectangle(pen, selectedItem.X * sizeModifier, selectedItem.Y * sizeModifier, sizeModifier, sizeModifier);
                }
                pen.Dispose();
            }

            if (focusedItem.X >= 0 && focusedItem.Y >= 0)
            {
                Pen pen = new Pen(Color.Green, 2);
                e.Graphics.DrawRectangle(pen, focusedItem.X * sizeModifier, focusedItem.Y * sizeModifier, sizeModifier, sizeModifier);
                pen.Dispose();
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
            if (ModifierKeys == Keys.Control)
            {
                if (!selectedItems.Contains(selectedItem))
                    selectedItems.Add(selectedItem);
            }
            else
            {
                selectedItems.Clear();
                selectedItems.Add(selectedItem);
            }
            // For now, we'll use ObjectSelection as a placeholder for where clothing selection would go.
            // A dedicated ClothingSelection might be needed later.
            TileManager.ObjectSelection = GetSelection(); 
            TileManager.LastSelection = TileManager.SelectionType.Object; // Or a new SelectionType for Clothing
            this.Invalidate(); // To redraw selection
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
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGrid = showGridToolStripMenuItem.Checked;
        }

        public void NavigateToClothingItem(int number)
        {
            if (TileManager.ClothingEpf == null || TileManager.ClothingEpf.Length == 0 || TileManager.ClothingEpf[0] == null) return;
            if (number < 0 || number >= TileManager.ClothingEpf[0].max ) return;

            int sbIndex = number / 100;
            int y = (number - sbIndex * 100) / 10;
            int x = number - sbIndex * 100 - y * 10;

            hScrollBarClothing.Value = sbIndex;
            selectedItems.Clear();
            selectedItems.Add(new Point(x, y));
            TileManager.ObjectSelection = GetSelection(); // Placeholder
            TileManager.LastSelection = TileManager.SelectionType.Object; // Placeholder
            RenderItems();
        }

        public void Reload(bool render)
        {
            sizeModifier = ImageRenderer.Singleton.sizeModifier;
            SetClientSizeCore((10 * sizeModifier) -1, (10 * sizeModifier) + menuStrip.Height + statusStrip.Height + hScrollBarClothing.Height -1); // Adjusted for all components
            MinimumSize = new Size(ClientSize.Width + 6, ClientSize.Height + 24); // Adjusted based on original FormTile
            MaximumSize = new Size(ClientSize.Width + 6, ClientSize.Height + 24); // Adjusted based on original FormTile
            this.BackgroundImage = null;

            if (TileManager.ClothingEpf != null && TileManager.ClothingEpf.Length > 0 && TileManager.ClothingEpf[0] != null)
            {
                 hScrollBarClothing.Maximum = (TileManager.ClothingEpf[0].max / 100) + 9;
            }
             else
            {
                hScrollBarClothing.Maximum = 9;
            }

            if (render) RenderItems();
        }
    }
}
