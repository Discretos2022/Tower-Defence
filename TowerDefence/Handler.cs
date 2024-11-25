using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Towers;

namespace TowerDefence
{
    public static class Handler
    {

        public static List<Monster> monsters = new List<Monster>();
        public static List<Tower> towers = new List<Tower>();
        public static List<Bullet> bullets = new List<Bullet>();

        public static List<Tile> tiles = new List<Tile>();

        public static int[,] MapPlace;

        public static List<Monster> monstersInOrder = new List<Monster>();
        public static int mI;

        public static int bI;


        public static void InitMapPlace(int x, int y)
        {
            MapPlace = new int[y, x];
        }


        public static void Update(GameTime gameTime)
        {

            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Update(gameTime);
            }


            for (mI = 0; mI < monsters.Count; mI++)
            {
                monsters[mI].Update(gameTime);
            }

            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].Update(gameTime);
            }

            for (bI = 0; bI < bullets.Count; bI++)
            {
                bullets[bI].Update(gameTime);
            }

            monstersInOrder = monsters.OrderBy(o => o.Time).ToList();

        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Draw(spriteBatch, gameTime);
            }


            for (mI = 0; mI < monsters.Count; mI++)
            {
                monsters[mI].Draw(spriteBatch, gameTime);
            }

            for (bI = 0; bI < bullets.Count; bI++)
            {
                bullets[bI].Draw(spriteBatch, gameTime);
            }

            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].Draw(spriteBatch, gameTime);
            }

        }

        public static Tower GetCanon(int ID, int x, int y)
        {
            switch (ID)
            {
                case 1: return new Canon(1, x, y);
                case 2: return new SuperCanon(2, x, y);
                case 3: return new ElectroCanon(3, x, y);
                case 4: return new SnowCanon(4, x, y);
                default: return null;
            }
        }


        public static void RemoveMonster(Monster monster)
        {
            mI -= 1;
            monsters.Remove(monster);
        }

        public static void RemoveBullet(Bullet bullet)
        {
            bI -= 1;
            bullets.Remove(bullet);
        }


    }
}
