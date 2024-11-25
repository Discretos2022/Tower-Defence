using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Towers
{
    internal class SnowCanon : Tower
    {
        public SnowCanon(int ID, int x = 0, int y = 0) : base(ID, x, y)
        {
            Name = "snowcanon";
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
                    return 0;
                case 2:
                    return 1;

                case 3:
                    return 1;
                case 4:
                    return 1;
                case 5:
                    return 2;
                case 6:
                    return 2;
                case 7:
                    return 2;
                case 8:
                    return 3;
                case 9:
                    return 3;
                case 10:
                    return 5;

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
                    return 190;
                case 9:
                    return 200;
                case 10:
                    return 220;

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
                    return 45;
                case 2:
                    return 44;

                case 3:
                    return 43;
                case 4:
                    return 42;
                case 5:
                    return 41;
                case 6:
                    return 40;
                case 7:
                    return 39;
                case 8:
                    return 38;
                case 9:
                    return 37;
                case 10:
                    return 35;

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


        public override float GetDebufVelocityReduce(int level = -1)
        {
            int lvl;
            if (level == -1)
                lvl = Level;
            else
                lvl = level;

            switch (lvl)
            {
                case 1:
                    return 0.8f;
                case 2:
                    return 0.75f;

                case 3:
                    return 0.70f;
                case 4:
                    return 0.65f;
                case 5:
                    return 0.60f;
                case 6:
                    return 0.55f;
                case 7:
                    return 0.50f;
                case 8:
                    return 0.40f;
                case 9:
                    return 0.30f;
                case 10:
                    return 0.20f;

                default: return 0;
            }
        }


        public override int GetDebufTime(int level = -1)
        {
            int lvl;
            if (level == -1)
                lvl = Level;
            else
                lvl = level;

            switch (lvl)
            {
                case 1:
                    return 60; // 1s
                case 2:
                    return 90;

                case 3:
                    return 120;
                case 4:
                    return 150;
                case 5:
                    return 180;
                case 6:
                    return 210;
                case 7:
                    return 240;
                case 8:
                    return 300;
                case 9:
                    return 360;
                case 10:
                    return 420; // 7s

                default: return 0;
            }
        }


        public override Debuf GetDebuf()
        {
            return new Debuf(480, Debuf.DebufType.Snow, GetDebufVelocityReduce());
        } 

    }
}