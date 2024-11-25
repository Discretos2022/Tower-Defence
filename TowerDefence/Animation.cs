using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Animation
    {

        private Texture2D Texture;

        private readonly List<Rectangle> _sourceRectangle = new List<Rectangle>();
        private readonly int frames;
        private int frame;
        private readonly float frameTime;
        private float frameTimeLeft;
        private bool active = true;

        private bool activeForOne = false;


        public Animation(Texture2D texture, int framesX, int framesY, float frameTime, int row = 1)
        {
            Texture = texture;
            this.frameTime = frameTime;
            frameTimeLeft = frameTime;
            frames = framesX;
            var frameWidth = Texture.Width / framesX;
            var frameHeight = Texture.Height / framesY;


            for(int i = 0; i < frames; i++)
            {
                _sourceRectangle.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
            }

        }

        public void Stop()
        {
            active = false;
            activeForOne = false;   
        }

        public void Start()
        {
            active = true;
        }

        public void StartOne()
        {
            active = true;
            activeForOne = true;
        }

        public void Reset()
        {
            frame = 0;
            frameTimeLeft = frameTime;
        }

        public void Update(GameTime gameTime)
        {

            if (!active) return;

            frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                frame = (frame + 1) % frames;
            }

            if (activeForOne)
            {
                if (frame == _sourceRectangle.Count - 1)
                    Stop();
            }


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Pos, SpriteEffects effect = SpriteEffects.None)
        {
            spriteBatch.Draw(Texture, Pos, _sourceRectangle[frame], Color.White, 0f, Vector2.Zero, 1f, effect, 0f);
        }

        public void Draw(Color color, float Rotation, float Scale, Vector2 Pos, Vector2 Center, SpriteBatch spriteBatch, SpriteEffects effect = SpriteEffects.None)
        {
            spriteBatch.Draw(Texture, Pos, _sourceRectangle[frame], color, Rotation, Center, Scale, effect, 0f);
        }

        public int GetFrame()
        {
            return frame;
        }

        public List<Rectangle> GetSourceRectangle()
        {
            return _sourceRectangle;
        }



    }
}
