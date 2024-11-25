using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Tile
    {

        public Vector2 Position;
        public int ID;
        public int[,] map;

        public Rectangle Img;

        private Color color = new Color(Random.Shared.Next(0, 255), Random.Shared.Next(0, 255), Random.Shared.Next(0, 255));

        public Tile(Vector2 _position, int type, int[,] _map)
        {

            Position = _position;
            ID = type;
            map = _map;

            ID = 1;

            Img = GetImg();


        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(Main.Tile1, Position + new Vector2(32, 28), Img, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

        }


        public Rectangle GetImg()
        {

            int x = (int)Position.Y / 64;
            int y = (int)Position.X / 64;

            if (map[x, y] == 0)
                return new Rectangle(0, 0, 16, 16);

            if (map[x, y] == 1)
            {
                if(y - 1 >= 0 && y + 1 < map.GetLength(1))
                    if (map[x, y - 1] == 1 && map[x, y + 1] == 1)
                        return new Rectangle(34, 0, 16, 16);

                if (x - 1 >= 0 && x + 1 < map.GetLength(0))
                    if (map[x - 1, y] == 1 && map[x + 1, y] == 1) 
                        return new Rectangle(17, 0, 16, 16);


                if (x + 1 < map.GetLength(0) && y + 1 < map.GetLength(1))
                    if (map[x, y + 1] == 1 && map[x + 1, y] == 1)
                        return new Rectangle(0, 17, 16, 16);

                if (x - 1 >= 0 && y + 1 < map.GetLength(1))
                    if (map[x - 1, y] == 1 && map[x, y + 1] == 1)
                        return new Rectangle(0, 34, 16, 16);

                if (x + 1 < map.GetLength(0) && y - 1 >= 0)
                    if (map[x, y - 1] == 1 && map[x + 1, y] == 1)
                        return new Rectangle(17, 17, 16, 16);

                if (x - 1 >= 0 && y - 1 >= 0)
                    if (map[x - 1, y] == 1 && map[x, y - 1] == 1)
                        return new Rectangle(17, 34, 16, 16);



                if (y + 1 < map.GetLength(0))
                    if (map[x, y + 1] == 1)
                        return new Rectangle(34, 0, 16, 16);

            }

            return new Rectangle(0, 0, 16, 16);
        }

    }
}
