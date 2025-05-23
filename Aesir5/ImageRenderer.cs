using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Aesir5
{
    public class ImageRenderer : IDisposable
    {
        static readonly int CacheInitialCapacity = 40000;

        bool isDisposed;
        public int sizeModifier = 36;
        Dictionary<int, Bitmap> cachedTiles = new Dictionary<int, Bitmap>(CacheInitialCapacity);
        Dictionary<int, Bitmap> cachedObjects = new Dictionary<int, Bitmap>(CacheInitialCapacity);
        Dictionary<int, Bitmap> cachedClothing = new Dictionary<int, Bitmap>(CacheInitialCapacity);
        Dictionary<int, Bitmap> cachedMonsters = new Dictionary<int, Bitmap>(CacheInitialCapacity);
        Dictionary<int, Bitmap> cachedSpells = new Dictionary<int, Bitmap>(CacheInitialCapacity);
        Bitmap bitmap;
        Bitmap resizeBitmap;

        #region Singleton Member Variables
        // Disallow instance creation.
        private ImageRenderer()
        { }

        static readonly ImageRenderer singleton = new ImageRenderer();

        public static ImageRenderer Singleton
        {
            get { return singleton; }
        }
        #endregion

        #region IDisposable Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool isDisposing)
        {
            if (isDisposed) return;

            if (isDisposing)
            {
                ClearTileCache();
                ClearObjectCache();
                ClearClothingCache();
                ClearMonsterCache();
                ClearSpellCache();
            }

            isDisposed = true;
        }

        ~ImageRenderer()
        {
            Dispose(false);   
        }
        #endregion

        public Bitmap GetTileBitmap(int tile)
        {
            if (cachedTiles.ContainsKey(tile))
                return cachedTiles[tile];

            bitmap = new Bitmap(48, 48, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;
            palette.Entries[0] = Color.Transparent;

            for (int i = 1; i < 256; i++)
            {
                Color color = TileManager.TilePal[TileManager.TileTBL[tile]][i];
                palette.Entries[i] = color;
            }

            bitmap.Palette = palette;
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, 48, 48), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            long top = TileManager.Epf[0].frames[tile].Top;
            long left = TileManager.Epf[0].frames[tile].Left;
            long bottom = TileManager.Epf[0].frames[tile].Bottom;
            long right = TileManager.Epf[0].frames[tile].Right;
            if (left < 0)
                MessageBox.Show(@"left < 0");

            if (top < 0)
                MessageBox.Show(@"top < 0");

            byte[] pixelData = new byte[bitmapdata.Stride * bitmap.Height];
            Marshal.Copy(bitmapdata.Scan0, pixelData, 0, pixelData.Length);

            for (int i = (int)top; i < (int)bottom; i++)
            {
                int index = i * bitmapdata.Stride;
                for (int j = (int)left; j < (int)right; j++)
                {
                    int num3 = TileManager.Epf[0].frames[tile][(int)(i - top), (int)(j - left)];
                    long a = j;
                    pixelData[index + a] = (byte)num3;
                }
            }

            Marshal.Copy(pixelData, 0, bitmapdata.Scan0, pixelData.Length);
            bitmap.UnlockBits(bitmapdata);
            resizeBitmap = new Bitmap(bitmap, sizeModifier, sizeModifier);
            
            cachedTiles[tile] = resizeBitmap;
            return resizeBitmap;
        }

        public Bitmap GetObjectBitmap(int tile)
        {
            if (cachedObjects.ContainsKey(tile))
                return cachedObjects[tile];

            bitmap = new Bitmap(48, 48, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;
            palette.Entries[0] = Color.Transparent;

            for (int i = 1; i < 256; i++)
            {
                Color color = TileManager.TileCPal[TileManager.TileCTBL[tile]][i];
                palette.Entries[i] = color;
            }

            bitmap.Palette = palette;
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, 48, 48), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            long top = TileManager.Epf[1].frames[tile].Top;
            long left = TileManager.Epf[1].frames[tile].Left;
            long bottom = TileManager.Epf[1].frames[tile].Bottom;
            long right = TileManager.Epf[1].frames[tile].Right;
            if (left < 0)
                MessageBox.Show(@"left < 0");

            if (top < 0)
                MessageBox.Show(@"top < 0");

            byte[] pixelData = new byte[bitmapdata.Stride * bitmap.Height];
            Marshal.Copy(bitmapdata.Scan0, pixelData, 0, pixelData.Length);

            for (int i = (int)top; i < (int)bottom; i++)
            {
                int index = i * bitmapdata.Stride;
                for (int j = (int)left; j < (int)right; j++)
                {
                    int num3 = TileManager.Epf[1].frames[tile][(int)(i - top), (int)(j - left)];
                    long a = j;
                    pixelData[index + a] = (byte)num3;
                }
            }

            Marshal.Copy(pixelData, 0, bitmapdata.Scan0, pixelData.Length);
            bitmap.UnlockBits(bitmapdata);
            resizeBitmap = new Bitmap(bitmap, sizeModifier, sizeModifier);

            cachedObjects[tile] = resizeBitmap;
            return resizeBitmap;
        }

        public Bitmap GetClothingBitmap(int id)
        {
            if (cachedClothing.ContainsKey(id))
                return cachedClothing[id];

            bitmap = new Bitmap(48, 48, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;
            palette.Entries[0] = Color.Transparent;

            for (int i = 1; i < 256; i++)
            {
                Color color = TileManager.ClothingPal[TileManager.ClothingTBL[id]][i];
                palette.Entries[i] = color;
            }

            bitmap.Palette = palette;
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, 48, 48), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            long top = TileManager.ClothingEpf[0].frames[id].Top;
            long left = TileManager.ClothingEpf[0].frames[id].Left;
            long bottom = TileManager.ClothingEpf[0].frames[id].Bottom;
            long right = TileManager.ClothingEpf[0].frames[id].Right;
            if (left < 0)
                MessageBox.Show(@"left < 0");

            if (top < 0)
                MessageBox.Show(@"top < 0");

            byte[] pixelData = new byte[bitmapdata.Stride * bitmap.Height];
            Marshal.Copy(bitmapdata.Scan0, pixelData, 0, pixelData.Length);

            for (int i = (int)top; i < (int)bottom; i++)
            {
                int index = i * bitmapdata.Stride;
                for (int j = (int)left; j < (int)right; j++)
                {
                    int num3 = TileManager.ClothingEpf[0].frames[id][(int)(i - top), (int)(j - left)];
                    long a = j;
                    pixelData[index + a] = (byte)num3;
                }
            }

            Marshal.Copy(pixelData, 0, bitmapdata.Scan0, pixelData.Length);
            bitmap.UnlockBits(bitmapdata);
            resizeBitmap = new Bitmap(bitmap, sizeModifier, sizeModifier);

            cachedClothing[id] = resizeBitmap;
            return resizeBitmap;
        }

        public Bitmap GetMonsterBitmap(int id)
        {
            if (cachedMonsters.ContainsKey(id))
                return cachedMonsters[id];

            bitmap = new Bitmap(48, 48, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;
            palette.Entries[0] = Color.Transparent;

            for (int i = 1; i < 256; i++)
            {
                Color color = TileManager.MonstersPal[TileManager.MonstersTBL[id]][i];
                palette.Entries[i] = color;
            }

            bitmap.Palette = palette;
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, 48, 48), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            long top = TileManager.MonstersEpf[0].frames[id].Top;
            long left = TileManager.MonstersEpf[0].frames[id].Left;
            long bottom = TileManager.MonstersEpf[0].frames[id].Bottom;
            long right = TileManager.MonstersEpf[0].frames[id].Right;
            if (left < 0)
                MessageBox.Show(@"left < 0");

            if (top < 0)
                MessageBox.Show(@"top < 0");

            byte[] pixelData = new byte[bitmapdata.Stride * bitmap.Height];
            Marshal.Copy(bitmapdata.Scan0, pixelData, 0, pixelData.Length);

            for (int i = (int)top; i < (int)bottom; i++)
            {
                int index = i * bitmapdata.Stride;
                for (int j = (int)left; j < (int)right; j++)
                {
                    int num3 = TileManager.MonstersEpf[0].frames[id][(int)(i - top), (int)(j - left)];
                    long a = j;
                    pixelData[index + a] = (byte)num3;
                }
            }

            Marshal.Copy(pixelData, 0, bitmapdata.Scan0, pixelData.Length);
            bitmap.UnlockBits(bitmapdata);
            resizeBitmap = new Bitmap(bitmap, sizeModifier, sizeModifier);

            cachedMonsters[id] = resizeBitmap;
            return resizeBitmap;
        }

        public Bitmap GetSpellBitmap(int id)
        {
            if (cachedSpells.ContainsKey(id))
                return cachedSpells[id];

            bitmap = new Bitmap(48, 48, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;
            palette.Entries[0] = Color.Transparent;

            for (int i = 1; i < 256; i++)
            {
                Color color = TileManager.SpellsPal[TileManager.SpellsTBL[id]][i];
                palette.Entries[i] = color;
            }

            bitmap.Palette = palette;
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, 48, 48), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            long top = TileManager.SpellsEpf[0].frames[id].Top;
            long left = TileManager.SpellsEpf[0].frames[id].Left;
            long bottom = TileManager.SpellsEpf[0].frames[id].Bottom;
            long right = TileManager.SpellsEpf[0].frames[id].Right;
            if (left < 0)
                MessageBox.Show(@"left < 0");

            if (top < 0)
                MessageBox.Show(@"top < 0");

            byte[] pixelData = new byte[bitmapdata.Stride * bitmap.Height];
            Marshal.Copy(bitmapdata.Scan0, pixelData, 0, pixelData.Length);

            for (int i = (int)top; i < (int)bottom; i++)
            {
                int index = i * bitmapdata.Stride;
                for (int j = (int)left; j < (int)right; j++)
                {
                    int num3 = TileManager.SpellsEpf[0].frames[id][(int)(i - top), (int)(j - left)];
                    long a = j;
                    pixelData[index + a] = (byte)num3;
                }
            }

            Marshal.Copy(pixelData, 0, bitmapdata.Scan0, pixelData.Length);
            bitmap.UnlockBits(bitmapdata);
            resizeBitmap = new Bitmap(bitmap, sizeModifier, sizeModifier);

            cachedSpells[id] = resizeBitmap;
            return resizeBitmap;
        }

        public void ClearTileCache()
        {
            foreach (Bitmap bitmap in cachedTiles.Values)
                bitmap.Dispose();

            cachedTiles.Clear();
        }

        public void ClearObjectCache()
        {
            foreach (Bitmap bitmap in cachedObjects.Values)
                bitmap.Dispose();

            cachedObjects.Clear();
        }

        public void ClearClothingCache()
        {
            foreach (Bitmap bitmap in cachedClothing.Values)
                bitmap.Dispose();

            cachedClothing.Clear();
        }

        public void ClearMonsterCache()
        {
            foreach (Bitmap bitmap in cachedMonsters.Values)
                bitmap.Dispose();

            cachedMonsters.Clear();
        }

        public void ClearSpellCache()
        {
            foreach (Bitmap bitmap in cachedSpells.Values)
                bitmap.Dispose();

            cachedSpells.Clear();
        }
    }
}
