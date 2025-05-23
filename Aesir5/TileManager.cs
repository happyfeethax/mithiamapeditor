using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Aesir5
{
    public static class TileManager
    {
        [Flags]
        public enum SelectionType { None = 0x0, Object = 0x1, Tile = 0x2, Pass = 0x4 }
        public static SelectionType LastSelection = SelectionType.None;

        public static Dictionary<Point, int> TileSelection = new Dictionary<Point, int>();
        public static Dictionary<Point, int> ObjectSelection = new Dictionary<Point, int>();
        public static Dictionary<Point, int> PassSelection = new Dictionary<Point, int>();

        public static ToolStripLabel lblStatus { get; set; }
        public static EPFFile[] Epf {get; private set;}
        public static Palette256[] TilePal { get; private set; }
        public static Palette256[] TileCPal { get; private set; }
        public static PaletteTable TileTBL { get; private set; } 
        public static PaletteTable TileCTBL { get; private set; }
        public static ObjectInfo[] ObjectInfos { get; private set; }
        public static EPFFile[] ClothingEpf { get; private set; }
        public static Palette256[] ClothingPal { get; private set; }
        public static PaletteTable ClothingTBL { get; private set; }
        public static EPFFile[] MonstersEpf { get; private set; }
        public static Palette256[] MonstersPal { get; private set; }
        public static PaletteTable MonstersTBL { get; private set; }
        public static EPFFile[] SpellsEpf { get; private set; }
        public static Palette256[] SpellsPal { get; private set; }
        public static PaletteTable SpellsTBL { get; private set; }

        static TileManager()
        {
            Epf = new EPFFile[2];
            ClothingEpf = new EPFFile[1];
            MonstersEpf = new EPFFile[1];
            SpellsEpf = new EPFFile[1];
        }

        public static void Load(string folder)
        {
            LoadTiles();
            LoadTileC();

            TilePal = Palette256.FromFile(folder + "\\data\\tile.pal");
            TileCPal = Palette256.FromFile(folder + "\\data\\tileC.pal");
            TileTBL = new PaletteTable(folder + "\\data\\tile.tbl");
            TileCTBL = new PaletteTable(folder + "\\data\\tileC.tbl");
            ObjectInfos = ObjectInfo.ReadCollection(folder + "\\data\\SObj.tbl");

            // LoadClothing(folder); // Now called directly by FormClothing with specific paths
            // LoadMonsters(folder); // Now called directly by FormMonsters with specific paths
            // LoadSpells(folder);   // Now called directly by FormSpells with specific paths

            // ClothingPal = Palette256.FromFile(folder + "\\data\\clothing.pal"); // Moved to LoadClothing
            // ClothingTBL = new PaletteTable(folder + "\\data\\clothing.tbl"); // Moved to LoadClothing
            // MonstersPal = Palette256.FromFile(folder + "\\data\\monsters.pal"); // Moved to LoadMonsters
            // MonstersTBL = new PaletteTable(folder + "\\data\\monsters.tbl"); // Moved to LoadMonsters
            // SpellsPal = Palette256.FromFile(folder + "\\data\\spells.pal");   // Moved to LoadSpells
            // SpellsTBL = new PaletteTable(folder + "\\data\\spells.tbl");     // Moved to LoadSpells
        }

        private static void LoadTiles()
        {
            lblStatus.Text = @"Loading tiles...";
            string[] file = new string[30];
            string nPath = Application.StartupPath + "\\Data\\";
            for (int a = 0; a < 30; a++)
            {
                file[a] = String.Format("{0}{1}{2}.epf", nPath, "tile", a);

            }
            int count = 0;
            for (int a = 0; a < 30; a++)
            {
                if (File.Exists(file[a]))
                {
                    count += EPFFile.Count(file[a]);
                }
            }

            Epf[0] = EPFFile.Init(count);
            Epf[0].max = count;
            count = 0;
            for (int a = 0; a < 30; a++)
            {
                Application.DoEvents();
                if (File.Exists(file[a]))
                {
                    count = EPFFile.LoadEPF(Epf[0], file[a], count);
                }
            }
            lblStatus.Text = "";
        }
        private static void LoadTileC()
        {
            lblStatus.Text = @"Loading tiles(for objects)...";
            string[] file = new string[30];
            string nPath = Application.StartupPath + "\\Data\\";
            for (int a = 0; a < 30; a++)
            {
                file[a] = String.Format("{0}{1}{2}.epf", nPath, "tilec", a);

            }
            int count = 0;
            for (int a = 0; a < 30; a++)
            {
                if (File.Exists(file[a]))
                {
                    count += EPFFile.Count(file[a]);
                }
            }

            Epf[1] = EPFFile.Init(count);
            count = 0;
            for (int a = 0; a < 30; a++)
            {
                Application.DoEvents();
                if (File.Exists(file[a]))
                {
                    count = EPFFile.LoadEPF(Epf[1], file[a], count);
                }
            }
            lblStatus.Text = "";
        }

        public static void LoadClothing(string[] epfPaths, string palPath, string tblPath)
        {
            lblStatus.Text = @"Loading clothing...";

            // Load PAL and TBL files
            try
            {
                if (File.Exists(palPath)) ClothingPal = Palette256.FromFile(palPath);
                else ClothingPal = null; // Or handle error more explicitly

                if (File.Exists(tblPath)) ClothingTBL = new PaletteTable(tblPath);
                else ClothingTBL = null; // Or handle error
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error loading clothing PAL/TBL: {ex.Message}";
                // Optionally rethrow or handle more gracefully
                ClothingPal = null;
                ClothingTBL = null;
                // return; // Might be too abrupt, depends on desired behavior
            }
            
            lblStatus.Text = @"Loading clothing EPF files...";
            int count = 0;
            if (epfPaths != null)
            {
                foreach (string filePath in epfPaths)
                {
                    if (File.Exists(filePath))
                    {
                        count += EPFFile.Count(filePath);
                    }
                }
            }

            ClothingEpf[0] = EPFFile.Init(count);
            ClothingEpf[0].max = count;
            count = 0; // Reset count for frame offset

            if (epfPaths != null)
            {
                foreach (string filePath in epfPaths)
                {
                    Application.DoEvents();
                    if (File.Exists(filePath))
                    {
                        count = EPFFile.LoadEPF(ClothingEpf[0], filePath, count);
                    }
                }
            }
            lblStatus.Text = "";
        }

        public static void LoadMonsters(string[] epfPaths, string palPath, string tblPath)
        {
            lblStatus.Text = @"Loading monsters...";

            // Load PAL and TBL files
            try
            {
                if (File.Exists(palPath)) MonstersPal = Palette256.FromFile(palPath);
                else MonstersPal = null;

                if (File.Exists(tblPath)) MonstersTBL = new PaletteTable(tblPath);
                else MonstersTBL = null;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error loading monsters PAL/TBL: {ex.Message}";
                MonstersPal = null;
                MonstersTBL = null;
            }

            lblStatus.Text = @"Loading monsters EPF files...";
            int count = 0;
            if (epfPaths != null)
            {
                foreach (string filePath in epfPaths)
                {
                    if (File.Exists(filePath))
                    {
                        count += EPFFile.Count(filePath);
                    }
                }
            }

            MonstersEpf[0] = EPFFile.Init(count);
            MonstersEpf[0].max = count;
            count = 0; // Reset count for frame offset

            if (epfPaths != null)
            {
                foreach (string filePath in epfPaths)
                {
                    Application.DoEvents();
                    if (File.Exists(filePath))
                    {
                        count = EPFFile.LoadEPF(MonstersEpf[0], filePath, count);
                    }
                }
            }
            lblStatus.Text = "";
        }

        public static void LoadSpells(string[] epfPaths, string palPath, string tblPath)
        {
            lblStatus.Text = @"Loading spells...";

            // Load PAL and TBL files
            try
            {
                if (File.Exists(palPath)) SpellsPal = Palette256.FromFile(palPath);
                else SpellsPal = null;

                if (File.Exists(tblPath)) SpellsTBL = new PaletteTable(tblPath);
                else SpellsTBL = null;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error loading spells PAL/TBL: {ex.Message}";
                SpellsPal = null;
                SpellsTBL = null;
            }

            lblStatus.Text = @"Loading spells EPF files...";
            int count = 0;
            if (epfPaths != null)
            {
                foreach (string filePath in epfPaths)
                {
                    if (File.Exists(filePath))
                    {
                        count += EPFFile.Count(filePath);
                    }
                }
            }

            SpellsEpf[0] = EPFFile.Init(count);
            SpellsEpf[0].max = count;
            count = 0; // Reset count for frame offset

            if (epfPaths != null)
            {
                foreach (string filePath in epfPaths)
                {
                    Application.DoEvents();
                    if (File.Exists(filePath))
                    {
                        count = EPFFile.LoadEPF(SpellsEpf[0], filePath, count);
                    }
                }
            }
            lblStatus.Text = "";
        }
    }
}
