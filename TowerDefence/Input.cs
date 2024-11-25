using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence;

namespace INPUT
{
    class MouseInput
    {

        public static MouseState mouseState;
        public static MouseState oldmouseState;
        private static Rectangle MousePos;

        public static bool IsActived = true;


        public static Point WindowPosition
        {
            get { return mouseState.Position; }
        }


        private static float X;
        private static float Y;

        public MouseInput()
        {
            
        }


        public static void Update()
        {
            oldmouseState = mouseState;
            mouseState = Mouse.GetState();

            MousePos = new Rectangle((int)X / 4, (int)Y, 2, 2);

        }

        public static Vector2 GetScreenPosition(Screen screen)
        {

            Rectangle screenDestinationRectangle = screen.CalculateDestinationRectangle();

            Point windowPosition = WindowPosition;

            float sx = windowPosition.X - screenDestinationRectangle.X;
            float sy = windowPosition.Y - screenDestinationRectangle.Y;

            sx /= screenDestinationRectangle.Width;
            sy /= screenDestinationRectangle.Height;

            sx *= screen.Width;
            sy *= screen.Height;

            return new Vector2(sx, sy);

        }


        public static Rectangle GetRectangle(Screen screen)
        {

            return new Rectangle((int)GetScreenPosition(screen).X, (int)GetScreenPosition(screen).Y, 1, 1);

        }

        public static bool isSimpleClickLeft()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && oldmouseState.LeftButton != ButtonState.Pressed)
                return true;

            return false;
        }

        public static bool isSimpleClickRight()
        {
            if (mouseState.RightButton == ButtonState.Pressed && oldmouseState.RightButton != ButtonState.Pressed)
                return true;

            return false;
        }

        public static void UpdateInput()
        {
            oldmouseState = mouseState;
            mouseState = Mouse.GetState();
        }

    }

    
    class KeyInput
    {

        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

        public KeyInput()
        {

        }


        public static void Update()
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();

        }

        public static KeyboardState getKeyState()
        {
            return keyState;
        }

        public static KeyboardState getOldKeyState()
        {
            return oldKeyState;
        }

        /// <summary>
        /// Discretos 9.7
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool isSimpleClick(Keys keys)
        {
            if (getKeyState().IsKeyDown(keys) && !getOldKeyState().IsKeyDown(keys))
                return true;

            return false;
        }

        /// <summary>
        /// Discretos 9.7
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool isSimpleClick(Keys keys1, Keys keys2)
        {
            if (getKeyState().IsKeyDown(keys1) && !getOldKeyState().IsKeyDown(keys1))
                return true;

            else if (getKeyState().IsKeyDown(keys2) && !getOldKeyState().IsKeyDown(keys2))
                return true;

            return false;
        }


    }


    public class GamePadInput
    {

        private static GamePadState padState1;
        private static GamePadState padState2;
        private static GamePadState padState3;
        private static GamePadState padState4;

        private static GamePadState oldPadState1;
        private static GamePadState oldPadState2;
        private static GamePadState oldPadState3;
        private static GamePadState oldPadState4;


        private static GamePadState[] padState = new GamePadState[4];
        private static GamePadState[] oldPadState = new GamePadState[4];


        public static void Update(PlayerIndex index)
        {
            oldPadState[(int)index] = padState[(int)index];
            padState[(int)index] = GamePad.GetState(index);
        }


        public static GamePadState GetPadState(PlayerIndex index)
        {
            return padState[(int)index];
        }

        public static GamePadState GetOldPadState(PlayerIndex index)
        {
            return oldPadState[(int)index];
        }

        /// <summary>
        /// Discretos 9.7
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool isSimpleClick(PlayerIndex index, Buttons button)
        {
            if (GetPadState(index).IsButtonDown(button) && !GetOldPadState(index).IsButtonDown(button))
                return true;

            return false;
        }

        public static bool isSimpleClick(PlayerIndex index, Buttons button1, Buttons button2)
        {
            if (GetPadState(index).IsButtonDown(button1) && !GetOldPadState(index).IsButtonDown(button1))
                return true;

            if (GetPadState(index).IsButtonDown(button2) && !GetOldPadState(index).IsButtonDown(button2))
                return true;

            return false;
        }


    }


}
