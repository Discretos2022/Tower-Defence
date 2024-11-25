using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Screen
    {

        public RenderTarget2D MapTarget;
        public RenderTarget2D FontTarget;

        private bool isSet;

        private Game game;

        public int Width;
        public int Height;

        public Screen(Game _game, int width, int height)
        {
            game = _game;

            Width = width;
            Height = height;

            MapTarget = new RenderTarget2D(_game.GraphicsDevice, width, height);
            FontTarget = new RenderTarget2D(_game.GraphicsDevice, width, height);

        }

        public void Set(RenderTarget2D target)
        {
            game.GraphicsDevice.SetRenderTarget(target);
            isSet = true;
        }

        public void UnSet()
        {
            game.GraphicsDevice.SetRenderTarget(null);
            isSet = true;
        }

        public void Present(Render render, GameTime gameTime, SpriteBatch spriteBatch)
        {

            Rectangle destinationRectangle = CalculateDestinationRectangle();

            render.DrawRenderTarget(MapTarget, destinationRectangle, Color.White, gameTime, spriteBatch);
            render.DrawRenderTarget(FontTarget, destinationRectangle, Color.White, gameTime, spriteBatch);


        }

        public Rectangle CalculateDestinationRectangle()
        {

            Rectangle backbufferBounds = game.GraphicsDevice.PresentationParameters.Bounds;
            float backbufferAspectRatio = (float)backbufferBounds.Width / backbufferBounds.Height;
            float screenAspectRatio = (float)Width / Height;

            float rx = 0f;
            float ry = 0f;
            float rw = backbufferBounds.Width;
            float rh = backbufferBounds.Height;

            if(backbufferAspectRatio > screenAspectRatio)
            {
                rw = rh * screenAspectRatio;
                rx = ((float)backbufferBounds.Width - rw) / 2f;
            }
            else if(backbufferAspectRatio < screenAspectRatio)
            {
                rh = rw / screenAspectRatio;
                ry = ((float)backbufferBounds.Height - rh) / 2f;
            }

            Rectangle result = new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);

            return result;

        }

    }
}
