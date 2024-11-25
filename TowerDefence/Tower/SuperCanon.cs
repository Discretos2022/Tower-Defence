using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Towers
{
    internal class SuperCanon : Tower
    {
        public SuperCanon(int ID, int x = 0, int y = 0) : base(ID, x, y)
        {

            Name = "super canon";

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
                    return 4;
                case 2:
                    return 6;

                case 3:
                    return 8;
                case 4:
                    return 10;
                case 5:
                    return 12;
                case 6:
                    return 14;
                case 7:
                    return 16;
                case 8:
                    return 20;
                case 9:
                    return 25;
                case 10:
                    return 30;

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
                    return 150;
                case 2:
                    return 155;

                case 3:
                    return 160;
                case 4:
                    return 165;
                case 5:
                    return 170;
                case 6:
                    return 175;
                case 7:
                    return 180;
                case 8:
                    return 185;
                case 9:
                    return 190;
                case 10:
                    return 200;

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
                    return 60;
                case 2:
                    return 59;

                case 3:
                    return 58;
                case 4:
                    return 57;
                case 5:
                    return 56;
                case 6:
                    return 55;
                case 7:
                    return 54;
                case 8:
                    return 52;
                case 9:
                    return 50;
                case 10:
                    return 45;

                default: return 0;
            }
        }

        public override int GetPrice()
        {
            switch (Level)
            {
                case 1:
                    return 150;
                case 2:
                    return 200;

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
