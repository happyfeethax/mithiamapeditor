using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Aesir5
{
    public partial class FormMonsters : Form
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

        public FormMonsters()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(FormMonsters_MouseWheel);
            this.Text = "Monsters";
        }

        private void FormMonsters_Load(object sender, EventArgs e)
        {
            Reload(false);
            MinimumSize = new Size(MinimumSize.Width + 10, MinimumSize.Height + 10);
            MaximumSize = new Size(MaximumSize.Width + 10, MaximumSize.Height + 10);
            if (this.BackgroundImage == null) this.BackgroundImage = new Bitmap(10 * sizeModifier, 10 * sizeModifier);
            menuStrip.Visible = false;
            if (TileManager.MonstersEpf != null && TileManager.MonstersEpf.Length > 0 && TileManager.MonstersEpf[0] != null)
            {
                hScrollBarMonsters.Maximum = (TileManager.MonstersEpf[0].max / 100) + 9;
            }
            else
            {
                hScrollBarMonsters.Maximum = 9; // Default if not loaded
            }
            RenderMonsters();
        }

        private int GetItemNumber(int x, int y)
        {
            return hScrollBarMonsters.Value*100 + (y*10) + x;
        }

        private void RenderMonsters()
        {
            if (this.BackgroundImage == null) this.BackgroundImage = new Bitmap(10 * sizeModifier, 10 * sizeModifier);
            Graphics g = Graphics.FromImage(this.BackgroundImage);
            g.Clear(Color.LightSlateGray); // Changed background for differentiation

            if (TileManager.MonstersEpf == null || TileManager.MonstersEpf.Length == 0 || TileManager.MonstersEpf[0] == null)
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

                    if (itemNumber < TileManager.MonstersEpf[0].max)
                    {
                        g.DrawImage(ImageRenderer.Singleton.GetMonsterBitmap(itemNumber), x * sizeModifier, y * sizeModifier);
                    }
                }
            }

            g.Dispose();
            this.Invalidate();
        }

        private void hScrollBarMonsters_Scroll(object sender, ScrollEventArgs e)
        {
            selectedItems.Clear();
            RenderMonsters();
        }

        void FormMonsters_Paint(object sender, PaintEventArgs e)
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

        private void FormMonsters_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (hScrollBarMonsters.Value - 1 >= hScrollBarMonsters.Minimum) hScrollBarMonsters.Value--;
            }
            else if (e.Delta < 0)
            {
                if (hScrollBarMonsters.Value + 1 <= hScrollBarMonsters.Maximum) hScrollBarMonsters.Value++;
            }
            hScrollBarMonsters_Scroll(null, null);
        }

        private void FormMonsters_MouseMove(object sender, MouseEventArgs e)
        {
            int newFocusedItemX = e.X / sizeModifier;
            int newFocusedItemY = e.Y / sizeModifier;
            bool refresh = (newFocusedItemX != focusedItem.X || newFocusedItemY != focusedItem.Y);

            if (refresh)
            {
                focusedItem = new Point(newFocusedItemX, newFocusedItemY);
                this.Invalidate();
                int itemNumber = GetItemNumber(newFocusedItemX, newFocusedItemY);
                toolStripStatusLabel.Text = string.Format("Monster item number: {0}", itemNumber);
            }
        }

        private void FormMonsters_MouseClick(object sender, MouseEventArgs e)
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

        private void findMonsterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumberInputForm numberInputForm = new NumberInputForm(@"Enter monster item number");
            if (numberInputForm.ShowDialog(this) == DialogResult.OK)
            {
                NavigateToMonsterItem(numberInputForm.Number);
            }
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGrid = showGridToolStripMenuItem.Checked;
        }

        public void NavigateToMonsterItem(int number)
        {
            if (TileManager.MonstersEpf == null || TileManager.MonstersEpf.Length == 0 || TileManager.MonstersEpf[0] == null) return;
            if (number < 0 || number >= TileManager.MonstersEpf[0].max ) return;

            int sbIndex = number / 100;
            int y = (number - sbIndex * 100) / 10;
            int x = number - sbIndex * 100 - y * 10;

            hScrollBarMonsters.Value = sbIndex;
            selectedItems.Clear();
            selectedItems.Add(new Point(x, y));
            TileManager.ObjectSelection = GetSelection(); 
            TileManager.LastSelection = TileManager.SelectionType.Object; 
            RenderMonsters();
        }

        public void Reload(bool render)
        {
            sizeModifier = ImageRenderer.Singleton.sizeModifier;
            SetClientSizeCore((10 * sizeModifier) -1, (10 * sizeModifier) + menuStrip.Height + statusStrip.Height + hScrollBarMonsters.Height -1); 
            MinimumSize = new Size(ClientSize.Width + 6, ClientSize.Height + 24); 
            MaximumSize = new Size(ClientSize.Width + 6, ClientSize.Height + 24); 
            this.BackgroundImage = null;

            if (TileManager.MonstersEpf != null && TileManager.MonstersEpf.Length > 0 && TileManager.MonstersEpf[0] != null)
            {
                 hScrollBarMonsters.Maximum = (TileManager.MonstersEpf[0].max / 100) + 9;
            }
             else
            {
                hScrollBarMonsters.Maximum = 9;
            }

            if (render) RenderMonsters();
        }
    }
}
