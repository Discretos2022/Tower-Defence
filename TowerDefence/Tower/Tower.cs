using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TowerDefence.Towers
{
    public abstract class Tower
    {

        public Tower(int ID, int x = 0, int y = 0)
        {
            this.ID = ID;

            InitTower();

            FireAnimation.Start();
            FireAnimation.Stop();

            Position = new Vector2(x, y);

        }

        public int ID;
        public int Level = 1;
        public string Name;

        public bool isPlaced = false;

        public float Rotation;

        public Vector2 Position;

        public Animation FireAnimation;

        public bool isEnemyDetected = false;

        public Rectangle rect;


        public int Reload;
        public int ReloadTime = 0;

        public int Range;

        public bool onObstacle = false;
        public bool isSelected = false;


        public List<Monster> monstersInRange = new List<Monster>();

        Monster E = null;


        public void Update(GameTime gameTime)
        {

            FireAnimation.Update(gameTime);

            if (isPlaced)
                UpdateRotation();


            ReloadTime += 1;

            //Console.WriteLine(ReloadTime);

            if (ReloadTime >= Reload && isEnemyDetected)
            {
                Fire();
            }

            //Position = new Vector2(64 * 1, 64 * 1 - 4);

            rect = new Rectangle((int)Position.X - 32, (int)Position.Y - 32, 64, 64);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {


            spriteBatch.Draw(Main.BaseTex[ID], Position, null, Color.White, 0f, new Vector2(16, 16), 2f, SpriteEffects.None, 0f);

            FireAnimation.Draw(Color.White, Rotation, 2f, Position, new Vector2(16, 16), spriteBatch, SpriteEffects.None);

            if (!isPlaced)
            {
                if(!onObstacle)
                    spriteBatch.Draw(Main.Range, Position, null, Color.White * 0.5f, 0f, new Vector2(Range / (2 * (Range / 300.0f))), (Range / 300.0f * 2), SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(Main.Range, Position, null, Color.Red * 0.5f, 0f, new Vector2(Range / (2 * (Range / 300.0f))), (Range / 300.0f * 2), SpriteEffects.None, 0f);

            }

            if(isSelected)
                spriteBatch.Draw(Main.Range, Position, null, Color.White * 0.5f, 0f, new Vector2(Range / (2 * (Range / 300.0f))), (Range / 300.0f * 2), SpriteEffects.None, 0f);

            //spriteBatch.Draw(Main.Bounds, rect, Color.Red);


        }


        public void UpdateRotation()
        {

            Vector2 Epos = Vector2.Zero;

            isEnemyDetected = false;

            E = GetFirstMonster();

            if (E != null)
            {

                Epos = E.Position;

                if (Epos.X >= Position.X && Epos.Y >= Position.Y)
                    Rotation = (float)(-Math.Atan((Position.X - Epos.X) / (Position.Y - Epos.Y)) + Math.PI / 2);


                if (Epos.X < Position.X && Epos.Y > Position.Y)
                    Rotation = (float)(-Math.Atan((Position.X - Epos.X) / (Position.Y - Epos.Y)) + Math.PI / 2);


                if (Epos.X < Position.X && Epos.Y <= Position.Y)
                    Rotation = (float)(-Math.Atan((Position.X - Epos.X) / (Position.Y - Epos.Y)) + Math.PI / 2 + Math.PI);


                if (Epos.X >= Position.X && Epos.Y <= Position.Y)
                    Rotation = (float)(-Math.Atan((Position.X - Epos.X) / (Position.Y - Epos.Y)) + Math.PI / 2 + Math.PI);


                isEnemyDetected = true;


            }

        }


        public void Fire()
        {

            FireAnimation.Stop();
            FireAnimation.Reset();
            FireAnimation.StartOne();

            ReloadTime = 0;


            if (E != null)
            {
                Vector2 Epos = E.Position;

                Vector2 DistNorme = Vector2.Normalize(Epos - Position);

                Vector2 velBullet = DistNorme * 30f; //30f;

                //Handler.bullets.Add(new Bullet(ID, Position + new Vector2(Random.Shared.Next(-1, 1), Random.Shared.Next(-1, 1)), velBullet, this, Rotation));
                Handler.bullets.Add(new Bullet(ID, Position, velBullet, this, Rotation));
            }



        }

        public void Place(Vector2 mousePos, bool placed)
        {

            if (!placed)
                Position = mousePos;
            else
                isPlaced = true;

        }


        public Monster GetFirstMonster()
        {

            Monster m = null;

            for (int i = 0; i < Handler.monstersInOrder.Count; i++)
            {

                if (Vector2.Distance(Position, Handler.monstersInOrder[i].Position) < Range)
                    m = Handler.monstersInOrder[i];

            }

            return m;

        }


        public abstract void InitTower();

        public abstract void UpgradeTower();

        public abstract int GetTimeOfReload(int level = -1);

        public abstract int GetDamage(int level = -1);

        public abstract int GetRange(int level = -1);

        public abstract int GetPrice();

        public virtual float GetDebufVelocityReduce(int level = -1)
        {
            return 0;
        }

        public virtual int GetDebufTime(int level = -1)
        {
            return 0;
        }

        public virtual Debuf GetDebuf()
        {
            return new Debuf(0, Debuf.DebufType.None);
        }

        public class Debuf
        {

            public int time;
            public DebufType type;

            public float velocityReduce;
            public int damage;


            public Debuf(int _time, DebufType _type, float _velocityReduce = 0, int _damage = 0)
            {
                time = _time;
                type = _type;
                velocityReduce = _velocityReduce;
                damage = _damage;
            }


            public enum DebufType
            {
                None = 0,
                Snow = 1,
                Ice = 2,
                Fire = 3,
            }
        }

    }


}
