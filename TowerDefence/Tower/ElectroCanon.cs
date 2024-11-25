using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Towers
{
    internal class ElectroCanon : Tower
    {
        public ElectroCanon(int ID, int x = 0, int y = 0) : base(ID, x, y)
        {

            Name = "electrocanon";
        }



        public override int GetDamage(int level = -1)
        {
            int lvl;
            if (level == -1)
                lvl = Level;
            else
                lvl = level;

            switch (lvl)
            {
                case 1:
                    return 6;
                case 2:
                    return 8; //6;

                case 3:
                    return 10;
                case 4:
                    return 12;
                case 5:
                    return 14;
                case 6:
                    return 16;
                case 7:
                    return 20;
                case 8:
                    return 25;
                case 9:
                    return 30;
                case 10:
                    return 40;

                default: return 0;
            }
        }

        public override int GetRange(int level = -1)
        {
            int lvl;
            if (level == -1)
                lvl = Level;
            else
                lvl = level;

            switch (lvl)
            {
                case 1:
                    return 160;
                case 2:
                    return 165;

                case 3:
                    return 170;
                case 4:
                    return 175;
                case 5:
                    return 180;
                case 6:
                    return 190;
                case 7:
                    return 200;
                case 8:
                    return 220;
                case 9:
                    return 240;
                case 10:
                    return 280;

                default: return 0;
            }
        }

        public override int GetTimeOfReload(int level = -1)
        {
            int lvl;
            if (level == -1)
                lvl = Level;
            else
                lvl = level;

            switch (lvl)
            {
                case 1:
                    return 70;
                case 2:
                    return 69;

                case 3:
                    return 68;
                case 4:
                    return 67;
                case 5:
                    return 66;
                case 6:
                    return 65;
                case 7:
                    return 64;
                case 8:
                    return 62;
                case 9:
                    return 60;
                case 10:
                    return 55;

                default: return 0;
            }
        }

        public override int GetPrice()
        {
            switch (Level)
            {
                case 1:
                    return 200;
                case 2:
                    return 250;

                default: return 0;
            }
        }


        public override void InitTower()
        {
            FireAnimation = new Animation(Main.TowerTex[ID], 9, 1, 0.05f);
            Reload = GetTimeOfReload();
            Range = GetRange();
        }

        public override void UpgradeTower()
        {
            Reload = GetTimeOfReload();
            Range = GetRange();
        }
    }
}