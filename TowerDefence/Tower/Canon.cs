using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Towers
{
    internal class Canon : Tower
    {
        public Canon(int ID, int x = 0, int y = 0) : base(ID, x, y)
        {

            Name = "canon";

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
                    return 2;
                case 2:
                    return 3;

                case 3:
                    return 4;
                case 4:
                    return 5;
                case 5:
                    return 6;
                case 6:
                    return 8;
                case 7:
                    return 10;
                case 8:
                    return 12;
                case 9:
                    return 15;
                case 10:
                    return 20;

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
                    return 100;
                case 2:
                    return 105;

                case 3:
                    return 110;
                case 4:
                    return 115;
                case 5:
                    return 120;
                case 6:
                    return 125;
                case 7:
                    return 130;
                case 8:
                    return 140;
                case 9:
                    return 150;
                case 10:
                    return 170;

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
                    return 40;
                case 2:
                    return 39;

                case 3:
                    return 38;
                case 4:
                    return 37;
                case 5:
                    return 36;
                case 6:
                    return 35;
                case 7:
                    return 34;
                case 8:
                    return 33;
                case 9:
                    return 32;
                case 10:
                    return 30;

                default: return 0;
            }
        }

        public override int GetPrice()
        {
            switch (Level)
            {
                case 1:
                    return 100;
                case 2:
                    return 150;
                case 3:
                    return 200;
                case 4:
                    return 250;
                case 5:
                    return 300;
                case 6:
                    return 400;
                case 7:
                    return 500;
                case 8:
                    return 600;
                case 9:
                    return 800;

                default: return 0;
            }
        }


        public override void InitTower()
        {
            FireAnimation = new Animation(Main.TowerTex[ID], 9, 2, 0.05f);
            Reload = GetTimeOfReload();
            Range = GetRange();
        }

        public override void UpgradeTower()
        {
            FireAnimation = new Animation(Main.TowerTex[ID], 9, 2, 0.05f, Level);
            FireAnimation.Start();
            FireAnimation.Stop();
            Reload = GetTimeOfReload();
            Range = GetRange();
        }

    }
}
