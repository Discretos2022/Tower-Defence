using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.GameState
{
    public class Menu
    {

        public ButtonV3 Play;


        public Menu()
        {
            Play = new ButtonV3();

            InitButton();

        }

        public void Update(State.GameState state, GameTime gameTime, Screen screen)
        {

            Play.Update(gameTime, screen);

            if (Play.IsSelected())
                Play.SetColor(Color.Gray, Color.Black);
            else
                Play.SetColor(Color.White, Color.Black);

            if (Play.IsCliqued())
                Main.gameState = State.GameState.Playing;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {

            

        }

        public void DrawUI(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {

            spriteBatch.Draw(Main.Banner, new Vector2(1920 / 2 - (114 * 6) / 2, 50), null, Color.White, 0f, Vector2.Zero, 6f, SpriteEffects.None, 0f);

            //spriteBatch.Draw(Main.Bounds, new Rectangle(10, 10, 20, 20), Color.Blue);

            Play.Draw(spriteBatch);


        }



        public void InitButton()
        {

            Play.SetFont(Main.UltimateFont);
            Play.SetText("play");
            Play.SetScale(4f);
            Play.SetFrontThickness(3f);
            Play.IsMajuscule(false);
            Play.SetPosition(0, 400, ButtonV3.Position.centerX);
            Play.SetColor(Color.White, Color.Black);

        }


    }
}
