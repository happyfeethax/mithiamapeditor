﻿using System;
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

            LoadClothing(folder);
            LoadMonsters(folder);
            LoadSpells(folder);

            ClothingPal = Palette256.FromFile(folder + "\\data\\clothing.pal");
            ClothingTBL = new PaletteTable(folder + "\\data\\clothing.tbl");
            MonstersPal = Palette256.FromFile(folder + "\\data\\monsters.pal");
            MonstersTBL = new PaletteTable(folder + "\\data\\monsters.tbl");
            SpellsPal = Palette256.FromFile(folder + "\\data\\spells.pal");
            SpellsTBL = new PaletteTable(folder + "\\data\\spells.tbl");
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

        private static void LoadClothing(string folder)
        {
            lblStatus.Text = @"Loading clothing...";
            string[] file = new string[30];
            string nPath = Application.StartupPath + "\\Data\\";
            for (int a = 0; a < 30; a++)
            {
                file[a] = String.Format("{0}{1}{2}.epf", nPath, "clothing", a);
            }
            int count = 0;
            for (int a = 0; a < 30; a++)
            {
                if (File.Exists(file[a]))
                {
                    count += EPFFile.Count(file[a]);
                }
            }

            ClothingEpf[0] = EPFFile.Init(count);
            ClothingEpf[0].max = count;
            count = 0;
            for (int a = 0; a < 30; a++)
            {
                Application.DoEvents();
                if (File.Exists(file[a]))
                {
                    count = EPFFile.LoadEPF(ClothingEpf[0], file[a], count);
                }
            }
            lblStatus.Text = "";
        }

        private static void LoadMonsters(string folder)
        {
            lblStatus.Text = @"Loading monsters...";
            string[] file = new string[30];
            string nPath = Application.StartupPath + "\\Data\\";
            for (int a = 0; a < 30; a++)
            {
                file[a] = String.Format("{0}{1}{2}.epf", nPath, "monsters", a);
            }
            int count = 0;
            for (int a = 0; a < 30; a++)
            {
                if (File.Exists(file[a]))
                {
                    count += EPFFile.Count(file[a]);
                }
            }

            MonstersEpf[0] = EPFFile.Init(count);
            MonstersEpf[0].max = count;
            count = 0;
            for (int a = 0; a < 30; a++)
            {
                Application.DoEvents();
                if (File.Exists(file[a]))
                {
                    count = EPFFile.LoadEPF(MonstersEpf[0], file[a], count);
                }
            }
            lblStatus.Text = "";
        }

        private static void LoadSpells(string folder)
        {
            lblStatus.Text = @"Loading spells...";
            string[] file = new string[30];
            string nPath = Application.StartupPath + "\\Data\\";
            for (int a = 0; a < 30; a++)
            {
                file[a] = String.Format("{0}{1}{2}.epf", nPath, "spells", a);
            }
            int count = 0;
            for (int a = 0; a < 30; a++)
            {
                if (File.Exists(file[a]))
                {
                    count += EPFFile.Count(file[a]);
                }
            }

            SpellsEpf[0] = EPFFile.Init(count);
            SpellsEpf[0].max = count;
            count = 0;
            for (int a = 0; a < 30; a++)
            {
                Application.DoEvents();
                if (File.Exists(file[a]))
                {
                    count = EPFFile.LoadEPF(SpellsEpf[0], file[a], count);
                }
            }
            lblStatus.Text = "";
        }
    }
}
