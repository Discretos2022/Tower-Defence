using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.GameState;
using UTIL;
using static TowerDefence.Towers.Tower;

namespace TowerDefence
{
    public class Monster
    {

        private int pointNum = 1;

        public Vector2 Position;

        private float Velocity = 2f; //Random.Shared.Next(1, 5); // 2f

        private int ID;

        private int[,] points;

        private Vector2 direction;
        private Vector2 point;
        private Vector2 dist;
        private float ratio = 1f;

        public float Rotation = 0f;

        private Cadran TanPos = Cadran.None;


        public int Life = Random.Shared.Next(20, 80); // 1000 // 80
        public int maxLife; // 1000

        public int LifeBarSize = 100;

        public float Time = 0;

        public List<Debuf> debufs = new List<Debuf>();


        public Monster(int _ID, Vector2 _pos, int[,] _points)
        {

            ID = _ID;
            points = _points;
            Position = _pos;

            maxLife = Life;

            point = new Vector2(points[pointNum, 0], points[pointNum, 1]);
            dist = new Vector2(point.X - Position.X, point.Y - Position.Y);
            direction = Vector2.Normalize(dist) * Velocity;


            if(Position.X <= point.X && Position.Y < point.Y)
                TanPos = Cadran.UpLeft;

            if (Position.X >= point.X && Position.Y < point.Y)
                TanPos = Cadran.UpRight;

            if (Position.X < point.X && Position.Y >= point.Y)
                TanPos = Cadran.DownLeft;

            if (Position.X > point.X && Position.Y >= point.Y)
                TanPos = Cadran.DownRight;

            //Console.WriteLine("Direction : " + direction + " | Dist : " + dist + " | Point : " + point + " | Position : " + Position);

        }

        public void Update(GameTime gameTime)
        {

            ratio = 1f;

            for (int i = 0; i < debufs.Count; i++)
            {
                Debuf d = debufs[i];

                d.time -= 1;

                //Console.WriteLine(debufs[0].time);

                if (d.type == Debuf.DebufType.Snow)
                    ratio = d.velocityReduce;

                if (d.type == Debuf.DebufType.Ice)
                    ratio = 0.0f;

                if (d.type == Debuf.DebufType.Fire)
                    if (d.time % 60 == 0)
                        Life -= d.damage;

                if (d.time <= 0)
                    debufs.Remove(d);

            }


            if(TanPos != Cadran.None)
                Position += direction * ratio;

            //Console.WriteLine("Direction : " + direction + " | Dist : " + dist + " | Point : " + point + " | Position : " + Position);

            //Console.WriteLine(TanPos);

            if (TanPos == Cadran.UpLeft)
                if(Position.X + direction.X >= point.X && Position.Y + direction.Y >= point.Y)
                    UpdateDirection();

            if (TanPos == Cadran.UpRight)
                if (Position.X + direction.X <= point.X && Position.Y + direction.Y >= point.Y)
                    UpdateDirection();

            if (TanPos == Cadran.DownLeft)
                if (Position.X + direction.X >= point.X && Position.Y + direction.Y <= point.Y)
                    UpdateDirection();

            if (TanPos == Cadran.DownRight)
                if (Position.X + direction.X <= point.X && Position.Y + direction.Y <= point.Y)
                    UpdateDirection();


            double ScalaProduct = direction.X * 1 + direction.Y * 0;

            

            if ((Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y) * 1) != 0)
            {
                if (direction.Y > 0)
                    Rotation = (float)Math.Acos(ScalaProduct / (Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y) * 1));
                else
                    Rotation = (float)-Math.Acos(ScalaProduct / (Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y) * 1));
            }
            else
                Rotation = 0f;


            if (float.IsNaN(Rotation))
                Rotation = 0f; 



            for (int i = 0; i < Handler.bullets.Count; i++) 
            {

                if (Vector2.Distance(Handler.bullets[i].Position, Position) < 45)
                {
                    Life -= Handler.bullets[i].tower.GetDamage();      //Life -= 5 * Handler.bullets[i].ID * 5; 
                    if (Handler.bullets[i].tower.GetDebuf().type != Debuf.DebufType.None)
                        debufs.Add(Handler.bullets[i].tower.GetDebuf());

                    Handler.RemoveBullet(Handler.bullets[i]);
                }

            }

            if (Life <= 0)
            {
                Handler.RemoveMonster(this); //Handler.monsters.Remove(this);
                Play.Coins += 10;
            }


            Time += Velocity * ratio;



        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (Life > 50)
                LifeBarSize = 200;

            Vector2 pos = new Vector2((int)Position.X, (int)Position.Y);
            spriteBatch.Draw(Main.Tank1, pos, null, Color.White, Rotation, new Vector2(16, 16), 2f, SpriteEffects.None, 0f);


            spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X - LifeBarSize / 4 / 2 - 1, (int)Position.Y - 25 - 1, LifeBarSize / 4 + 2, 5 + 2), new Rectangle(0, 0, 1, 1), Color.Black, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X - LifeBarSize / 4 / 2, (int)Position.Y - 25, LifeBarSize / 4, 5), new Rectangle(0, 0, 1, 1), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X - LifeBarSize / 4 / 2, (int)Position.Y - 25, (Life * LifeBarSize / maxLife) / 4, 2), new Rectangle(0, 0, 1, 1), Color.LightGreen, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X - LifeBarSize / 4 / 2, (int)Position.Y - 25 + 2, (Life * LifeBarSize / maxLife) / 4, 3), new Rectangle(0, 0, 1, 1), Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);


        }


        public void UpdateDirection()
        {

            Position = point;

            pointNum += 1;

            if (pointNum < points.GetLength(0))
            {
                point = new Vector2(points[pointNum, 0], points[pointNum, 1]); // + new Vector2(Random.Shared.Next(-4, 4), Random.Shared.Next(-4, 4));
                dist = new Vector2(point.X - Position.X, point.Y - Position.Y);
                direction = Vector2.Normalize(dist) * Velocity;


                if (Position.X <= point.X && Position.Y < point.Y)
                    TanPos = Cadran.UpLeft;

                if (Position.X > point.X && Position.Y <= point.Y)
                    TanPos = Cadran.UpRight;

                if (Position.X < point.X && Position.Y >= point.Y)
                    TanPos = Cadran.DownLeft;

                if (Position.X >= point.X && Position.Y > point.Y)
                    TanPos = Cadran.DownRight;
            }
            else
            {
                TanPos = Cadran.None;
                Play.PV -= 2;
                Handler.RemoveMonster(this);
            }

            
        }


        public Vector2 GetCenter()
        {
            return Position + new Vector2();
        }


        public enum Cadran
        {
            None,
            UpRight,
            UpLeft,
            DownRight,
            DownLeft,
        }

    }
}
