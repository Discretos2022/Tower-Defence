using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace TowerDefence
{
    public class Render
    {

        public Render()
        {

        }


        public void Begin(bool isTextureFileteringEnabled, GameTime gameTime, SpriteBatch spriteBatch, Camera camera = null)
        {

            SamplerState sampler = SamplerState.PointClamp;
            if(isTextureFileteringEnabled)
                sampler = SamplerState.AnisotropicWrap;


            if(camera != null)
            {
                camera.UpdateMatrices();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, sampler, null, RasterizerState.CullNone, null, camera.Translation);
            }
            else
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, sampler, null, RasterizerState.CullNone, null, null);

        }

        public void End(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
        }


        public void DrawRenderTarget(Texture2D tex, Rectangle destination, Color color, GameTime gameTime, SpriteBatch spriteBatch)
        {

            Begin(!Main.PixelPerfect, gameTime, spriteBatch);


            spriteBatch.Draw(tex, destination, null, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);


            End(spriteBatch);
            

        }


    }
}
