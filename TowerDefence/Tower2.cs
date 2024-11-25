using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Tower2
    {

        public int ID;
        public int Level = 1;

        public bool isPlaced = false;

        public float Rotation;

        public Vector2 Position;

        public Animation FireAnimation;

        public bool isEnemyDetected = false;


        public int Reload;
        public int ReloadTime = 0;

        public int Range = 300;


        public List<Monster> monstersInRange = new List<Monster>();


        public Tower2(int ID)
        {
            this.ID = ID;

            InitTower();

            FireAnimation.Start();
            FireAnimation.Stop();

        }

        public void Update(GameTime gameTime)
        {

            FireAnimation.Update(gameTime);

            if(isPlaced)
                UpdateRotation();


            ReloadTime += 1;

            //Console.WriteLine(ReloadTime);

            if(ReloadTime >= Reload && isEnemyDetected)
            {
                Fire();
            }
            
            //Position = new Vector2(64 * 1, 64 * 1 - 4);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {


            spriteBatch.Draw(Main.BaseTex[ID], Position, null, Color.White, 0f, new Vector2(16, 16), 2f, SpriteEffects.None, 0f);

            FireAnimation.Draw(Color.White, Rotation, 2f, Position, new Vector2(16, 16), spriteBatch, SpriteEffects.None);

            //spriteBatch.Draw(Main.Range, Position, null, Color.White * 0.5f, 0f, new Vector2(150), 2f, SpriteEffects.None, 0f);


        }


        public void UpdateRotation()
        {

            for (int i = 0; i < Handler.monsters.Count; i++)
            {

                Monster e = Handler.monsters[i];

                if(Vector2.Distance(Position, e.Position) <= Range)
                {
                    monstersInRange.Add(e);
                }

            }


            for (int i = 0; i < monstersInRange.Count; i++)
            {
                Monster e = monstersInRange[i];

                if (Vector2.Distance(Position, e.Position) > Range || e.Life <= 0)
                    monstersInRange.Remove(e);
            }




            Vector2 Epos = Vector2.Zero;

            isEnemyDetected = false;

            if (monstersInRange.Count > 0)
            {

                Epos = GetFirstMonster().Position;

                

                if (Vector2.Distance(Position, Epos) < 300)
                {

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
                
        }


        public void Fire()
        {

            FireAnimation.Stop();
            FireAnimation.Reset();
            FireAnimation.StartOne();

            ReloadTime = 0;


            Vector2 Epos = Vector2.Zero;

            if (monstersInRange.Count > 0)
                Epos = GetFirstMonster().Position;

            Vector2 DistNorme = Vector2.Normalize(Epos - Position);

            Vector2 velBullet = DistNorme * 30f;

            //Handler.bullets.Add(new Bullet(ID, Position, velBullet, this, Rotation));


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

            Monster m = monstersInRange[0];

            for (int i = 1; i < monstersInRange.Count; i++)
            {

                if (monstersInRange[i].Time > m.Time)
                    m = monstersInRange[i];

            }

            return m;

        }



        public void InitTower()
        {

            switch (ID)
            {

                case 1:
                    FireAnimation = new Animation(Main.TowerTex[ID], 9, 1, 0.05f);
                    Reload = 20;
                    break;

                case 2:
                    FireAnimation = new Animation(Main.TowerTex[ID], 9, 1, 0.025f);
                    Reload = 60;   //60; // 40
                    break;

            }


        }


        public int GetTimeOfReload()
        {
            switch (ID)
            {

                case 1:

                    if (Level == 1)
                        return 20;
                    else
                        return 0;

                case 2:

                    if (Level == 1)
                        return 20;
                    else
                        return 0;

                


            }

            return 0;

        }


        public int GetDamage()
        {
            switch (ID)
            {

                case 1:

                    if (Level == 1)
                        return 1;
                    else
                        return 0;

                case 2:

                    if (Level == 1)
                        return 4;
                    else
                        return 0;




            }

            return 0;

        }


        public int GetRange()
        {
            switch (ID)
            {

                case 1:

                    if (Level == 1)
                        return 64;
                    else
                        return 0;

                case 2:

                    if (Level == 1)
                        return 128;
                    else
                        return 0;




            }

            return 0;

        }

    }
}
