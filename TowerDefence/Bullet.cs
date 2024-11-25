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
    public class Bullet
    {

        public int ID;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public Tower tower;

        public Bullet(int ID, Vector2 _position, Vector2 _velocity, Tower _tower, float _rotation = 0f) 
        {
        
            this.ID = ID;
            Position = _position;
            Velocity = _velocity;
            Rotation = _rotation;
            tower = _tower;

        }


        public void Update(GameTime gameTime)
        {

            Position += Velocity;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(Main.ProjectileTex[ID], Position, null, Color.White, Rotation, new Vector2(8, 8), 2f, SpriteEffects.None, 0f);

        }

    }
}
