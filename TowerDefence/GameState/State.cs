using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.GameState
{
    public class State
    {

        private Menu menu;
        private Play play;

        public State(Menu menu, Play play)
        {

            this.menu = menu;
            this.play = play;

        }

        public void Update(GameState state, GameTime gameTime, Screen screen)
        {

            switch (state)
            {
                case GameState.Menu:
                    menu.Update(state, gameTime, screen);
                    break;
                case GameState.Playing:
                    play.Update(state, gameTime, screen);
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, GameState state, Screen screen)
        {

            switch (state)
            {
                case GameState.Menu:
                    menu.Draw(spriteBatch, gameTime, screen);
                    break;
                case GameState.Playing:
                    play.Draw(spriteBatch, gameTime, screen);
                    break;
            }

        }

        public void DrawUI(SpriteBatch spriteBatch, GameTime gameTime, GameState state, Screen screen)
        {

            switch (state)
            {
                case GameState.Menu:
                    menu.DrawUI(spriteBatch, gameTime, screen);
                    break;
                case GameState.Playing:
                    play.DrawUI(spriteBatch, gameTime, screen);
                    break;
            }

        }

        public enum GameState { Menu, Playing }

    }
}
