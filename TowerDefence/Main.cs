using Font;
using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TowerDefence.GameState;
using TowerDefence.Towers;
using WRITER;

namespace TowerDefence
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static bool PixelPerfect = true;

        public static bool Debug = false;

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        private Render render;
        private Screen screen;
        private Camera camera;

        private State state;
        private Menu menu;
        private Play play;
        public static State.GameState gameState = State.GameState.Menu;

        public static Texture2D Bounds;

        public static Texture2D Banner;

        public static Texture2D Shop;

        public static Texture2D Tile1;

        public static Texture2D Tank1;

        public static Texture2D Cursor;

        public static Texture2D Range;

        public static Texture2D TowerButton;

        public static Texture2D UpgradeButton;
        public static Texture2D SellButton;



        public int numOfTower = 4;

        public static Texture2D[] TowerTex;
        public static Texture2D[] BaseTex;
        public static Texture2D[] ProjectileTex;



        public static SpriteFont UltimateFont = null;      //= new SpriteFont(null, null, null, null, 0, 0, null, null);
        Texture2D SuperFont;
        List<Rectangle> glyphRect = new List<Rectangle>();
        List<Rectangle> croppingList = new List<Rectangle>();
        List<char> charList = new List<char>();
        List<Vector3> Vector3List = new List<Vector3>();


        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            graphics.HardwareModeSwitch = false;

        }

        protected override void Initialize()
        {

            Console.WriteLine("Tower Defence 3.0    Copyright © 2023 SIEDEL");

            #region Screen Parameter

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1200;

            graphics.ApplyChanges();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen = new Screen(this, 1920, 1200);
            render = new Render();
            camera = new Camera();


            #endregion


            SuperFont = Content.Load<Texture2D>("Images\\SuperFont");
            TowerButton = Content.Load<Texture2D>("Images/UI/TowerButton");
            UpgradeButton = Content.Load<Texture2D>("Images/UI/UpgradeButton");
            SellButton = Content.Load<Texture2D>("Images/UI/SellButton");

            FontManager.InitFont(FontManager.Font.StatFont, Content.Load<Texture2D>("Images\\StatFont"));

            InitFont();

            play = new Play();
            menu = new Menu();
            state = new State(menu, play);

            Map.LoadMap(1);



            TowerTex = new Texture2D[numOfTower + 1];
            BaseTex = new Texture2D[numOfTower + 1];
            ProjectileTex = new Texture2D[numOfTower + 1];



            //Handler.monsters.Add(new Monster(1, new Vector2(500, 500), Map.GetPoint(1)));
            //Handler.monsters.Add(new Monster(1, new Vector2(400, 500), Map.GetPoint(1)));
            //Handler.monsters.Add(new Monster(1, new Vector2(300, 500), Map.GetPoint(1)));
            //Handler.monsters.Add(new Monster(1, new Vector2(200, 500), Map.GetPoint(1)));
            //Handler.monsters.Add(new Monster(1, new Vector2(101, 500), Map.GetPoint(1)));

            

            base.Initialize();
        }

        protected override void LoadContent()
        {


            Bounds = Content.Load<Texture2D>("Images/Bounds");

            Shop = Content.Load<Texture2D>("Images/Interface/Shop");

            Tile1 = Content.Load<Texture2D>("Images/Tiles/Tile_1");

            Tank1 = Content.Load<Texture2D>("Images/Monster/Tank_1");

            Cursor = Content.Load<Texture2D>("Images/Cursor");
            Banner = Content.Load<Texture2D>("Images/Banner");

            


            for (int i = 1; i <= numOfTower; i++)
            {
                TowerTex[i] = Content.Load<Texture2D>("Images/Tower/Canon_" + i);
                BaseTex[i] = Content.Load<Texture2D>("Images/Tower/Base_Canon_" + i);
                ProjectileTex[i] = Content.Load<Texture2D>("Images/Projectile/Projectile_" + i);
            }


            //Handler.towers.Add(Handler.GetCanon(1));

            Range = Content.Load<Texture2D>("Images/Range");

            


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            FontManager.InitFont(FontManager.Font.StatFont, Content.Load<Texture2D>("Images\\StatFont"));


            KeyInput.Update();
            MouseInput.Update();
            
            /// Fulscreen
            if(KeyInput.getKeyState().IsKeyDown(Keys.F11) && !KeyInput.getOldKeyState().IsKeyDown(Keys.F11))
            {

                if(graphics.IsFullScreen)
                    graphics.IsFullScreen = false;
                else
                    graphics.IsFullScreen = true;

                graphics.ApplyChanges();

                //if (graphics.IsFullScreen)
                //{
                //    graphics.PreferredBackBufferWidth = 1920;
                //    graphics.PreferredBackBufferHeight = 1080;
                //    graphics.ApplyChanges();
                //}
                //else
                //{
                //    graphics.PreferredBackBufferWidth = 1920 / 2;
                //    graphics.PreferredBackBufferHeight = 1080;
                //    graphics.ApplyChanges();
                //}

            }


            if (KeyInput.getKeyState().IsKeyDown(Keys.Enter) && !KeyInput.getOldKeyState().IsKeyDown(Keys.Enter))
                gameState = State.GameState.Playing;

            if (KeyInput.getKeyState().IsKeyDown(Keys.F12) && !KeyInput.getOldKeyState().IsKeyDown(Keys.F12))
                gameState = State.GameState.Menu;

            if (KeyInput.getKeyState().IsKeyDown(Keys.F1) && !KeyInput.getOldKeyState().IsKeyDown(Keys.F1) && Debug)
                Debug = false;
            else if (KeyInput.getKeyState().IsKeyDown(Keys.F1) && !KeyInput.getOldKeyState().IsKeyDown(Keys.F1) && !Debug)
                Debug = true;

            if (KeyInput.getKeyState().IsKeyDown(Keys.Space) && !KeyInput.getOldKeyState().IsKeyDown(Keys.Space))
            {
                Handler.monsters.Add(new Monster(1, new Vector2(Map.GetPoint(1)[0, 0], Map.GetPoint(1)[0, 1]), Map.GetPoint(1)));
            }

            state.Update(gameState, gameTime, screen);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            PixelPerfect = false;


            #region Map

            screen.Set(screen.MapTarget);
            render.Begin(false, gameTime, spriteBatch, camera);
            GraphicsDevice.Clear(Color.CornflowerBlue);


            state.Draw(spriteBatch, gameTime, gameState, screen);

            if(Debug)
                for (int i = 0; i < Map.GetPoint(1).GetLength(0); i++) {

                    Vector2 point = new Vector2(Map.GetPoint(1)[i, 0], Map.GetPoint(1)[i, 1]);
                    spriteBatch.Draw(Main.Bounds, point + new Vector2(-4, -4), null, Color.Red, 0f, new Vector2(0, 0), 8f, SpriteEffects.None, 0f);
                    Writer.DrawText(UltimateFont, "" + i, point + new Vector2(10, 10), Color.Black, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, spriteBatch);


                }


            render.End(spriteBatch); 
            screen.UnSet();

            #endregion


            #region UI

            screen.Set(screen.FontTarget);
            render.Begin(false, gameTime, spriteBatch);
            GraphicsDevice.Clear(new Color(0, 0, 0, 0));


            state.DrawUI(spriteBatch, gameTime, gameState, screen);


            spriteBatch.Draw(Cursor, MouseInput.GetScreenPosition(screen), null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);


            if (gameTime.IsRunningSlowly)
                spriteBatch.Draw(Bounds, new Rectangle(1800, 25, 50, 50), Color.Red);
            else
                spriteBatch.Draw(Bounds, new Rectangle(1800, 25, 50, 50), Color.Green);


            render.End(spriteBatch);
            screen.UnSet();

            #endregion


            screen.Present(render, gameTime, spriteBatch);




            base.Draw(gameTime);

        }





        public void InitFont()
        {
            glyphRect.Add(new Rectangle(600, 0, 9, 16));   //SPACE
            glyphRect.Add(new Rectangle(533, 0, 1, 16));   //!
            glyphRect.Add(new Rectangle(547, 0, 7, 16));   //$
            glyphRect.Add(new Rectangle(583, 0, 1, 16));   //.

            glyphRect.Add(new Rectangle(456, 0, 6, 16));   //0
            glyphRect.Add(new Rectangle(463, 0, 5, 16));   //1
            glyphRect.Add(new Rectangle(469, 0, 7, 16));   //2
            glyphRect.Add(new Rectangle(477, 0, 6, 16));   //3
            glyphRect.Add(new Rectangle(484, 0, 7, 16));   //4
            glyphRect.Add(new Rectangle(492, 0, 7, 16));   //5
            glyphRect.Add(new Rectangle(500, 0, 7, 16));   //6
            glyphRect.Add(new Rectangle(508, 0, 7, 16));   //7
            glyphRect.Add(new Rectangle(516, 0, 6, 16));   //8
            glyphRect.Add(new Rectangle(523, 0, 6, 16));   //9

            glyphRect.Add(new Rectangle(530, 0, 2, 16));   //:
            glyphRect.Add(new Rectangle(535, 0, 7, 16));   //?

            glyphRect.Add(new Rectangle(0, 0, 11, 16));    //A
            glyphRect.Add(new Rectangle(12, 0, 9, 16));    //B
            glyphRect.Add(new Rectangle(22, 0, 11, 16));   //C
            glyphRect.Add(new Rectangle(34, 0, 10, 16));   //D
            glyphRect.Add(new Rectangle(45, 0, 9, 16));    //E
            glyphRect.Add(new Rectangle(55, 0, 9, 16));    //F
            glyphRect.Add(new Rectangle(65, 0, 11, 16));   //G
            glyphRect.Add(new Rectangle(77, 0, 9, 16));    //H
            glyphRect.Add(new Rectangle(87, 0, 7, 16));    //I
            glyphRect.Add(new Rectangle(95, 0, 10, 16));   //J
            glyphRect.Add(new Rectangle(106, 0, 9, 16));   //K
            glyphRect.Add(new Rectangle(116, 0, 8, 16));   //L
            glyphRect.Add(new Rectangle(125, 0, 11, 16));  //M
            glyphRect.Add(new Rectangle(137, 0, 9, 16));   //N
            glyphRect.Add(new Rectangle(147, 0, 9, 16));   //O
            glyphRect.Add(new Rectangle(157, 0, 9, 16));   //P
            glyphRect.Add(new Rectangle(167, 0, 9, 16));   //Q
            glyphRect.Add(new Rectangle(177, 0, 9, 16));   //R
            glyphRect.Add(new Rectangle(187, 0, 9, 16));   //S
            glyphRect.Add(new Rectangle(197, 0, 9, 16));   //T
            glyphRect.Add(new Rectangle(207, 0, 9, 16));   //U
            glyphRect.Add(new Rectangle(217, 0, 11, 16));  //V
            glyphRect.Add(new Rectangle(229, 0, 15, 16));  //W
            glyphRect.Add(new Rectangle(245, 0, 10, 16));  //X
            glyphRect.Add(new Rectangle(256, 0, 11, 16));  //Y
            glyphRect.Add(new Rectangle(268, 0, 9, 16));   //Z

            glyphRect.Add(new Rectangle(278, 0, 6, 16));   //a
            glyphRect.Add(new Rectangle(285, 0, 5, 16));   //b
            glyphRect.Add(new Rectangle(291, 0, 6, 16));   //c
            glyphRect.Add(new Rectangle(298, 0, 5, 16));   //d
            glyphRect.Add(new Rectangle(304, 0, 5, 16));   //e
            glyphRect.Add(new Rectangle(310, 0, 5, 16));   //f
            glyphRect.Add(new Rectangle(316, 0, 6, 16));   //g
            glyphRect.Add(new Rectangle(323, 0, 5, 16));   //h
            glyphRect.Add(new Rectangle(329, 0, 5, 16));   //i
            glyphRect.Add(new Rectangle(335, 0, 5, 16));   //j
            glyphRect.Add(new Rectangle(341, 0, 5, 16));   //k
            glyphRect.Add(new Rectangle(347, 0, 5, 16));   //l
            glyphRect.Add(new Rectangle(352, 0, 7, 16));   //m
            glyphRect.Add(new Rectangle(360, 0, 5, 16));   //n
            glyphRect.Add(new Rectangle(366, 0, 7, 16));   //o
            glyphRect.Add(new Rectangle(374, 0, 5, 16));   //p
            glyphRect.Add(new Rectangle(380, 0, 7, 16));   //q
            glyphRect.Add(new Rectangle(388, 0, 5, 16));   //r
            glyphRect.Add(new Rectangle(394, 0, 6, 16));   //s
            glyphRect.Add(new Rectangle(401, 0, 5, 16));   //t
            glyphRect.Add(new Rectangle(407, 0, 7, 16));   //u
            glyphRect.Add(new Rectangle(414, 0, 7, 16));   //v
            glyphRect.Add(new Rectangle(422, 0, 11, 16));  //w
            glyphRect.Add(new Rectangle(434, 0, 6, 16));   //x
            glyphRect.Add(new Rectangle(441, 0, 7, 16));   //y
            glyphRect.Add(new Rectangle(449, 0, 6, 16));   //z

            glyphRect.Add(new Rectangle(600, 0, 8, 16));   //...


            charList.Add(' ');
            charList.Add('!');
            charList.Add('$');
            charList.Add('.');

            charList.Add('0');
            charList.Add('1');
            charList.Add('2');
            charList.Add('3');
            charList.Add('4');
            charList.Add('5');
            charList.Add('6');
            charList.Add('7');
            charList.Add('8');
            charList.Add('9');

            charList.Add(':');
            charList.Add('?');

            charList.Add('A');
            charList.Add('B');
            charList.Add('C');
            charList.Add('D');
            charList.Add('E');
            charList.Add('F');
            charList.Add('G');
            charList.Add('H');
            charList.Add('I');
            charList.Add('J');
            charList.Add('K');
            charList.Add('L');
            charList.Add('M');
            charList.Add('N');
            charList.Add('O');
            charList.Add('P');
            charList.Add('Q');
            charList.Add('R');
            charList.Add('S');
            charList.Add('T');
            charList.Add('U');
            charList.Add('V');
            charList.Add('W');
            charList.Add('X');
            charList.Add('Y');
            charList.Add('Z');

            charList.Add('a');
            charList.Add('b');
            charList.Add('c');
            charList.Add('d');
            charList.Add('e');
            charList.Add('f');
            charList.Add('g');
            charList.Add('h');
            charList.Add('i');
            charList.Add('j');
            charList.Add('k');
            charList.Add('l');
            charList.Add('m');
            charList.Add('n');
            charList.Add('o');
            charList.Add('p');
            charList.Add('q');
            charList.Add('r');
            charList.Add('s');
            charList.Add('t');
            charList.Add('u');
            charList.Add('v');
            charList.Add('w');
            charList.Add('x');
            charList.Add('y');
            charList.Add('z');

            charList.Add('§');

            int numberCaractere = charList.Count;


            /// NE CHANGE RIEN
            for (int i = 0; i < numberCaractere; i++)
                croppingList.Add(new Rectangle(0, 0, 16, 16));

            Vector3List.Add(new Vector3(0, -4, 0));//SPACE
            Vector3List.Add(new Vector3(0, -8, 0));//!
            Vector3List.Add(new Vector3(0, -3, 0));//$
            Vector3List.Add(new Vector3(0, -8, 0));//.

            Vector3List.Add(new Vector3(0, -3, 0));//0
            Vector3List.Add(new Vector3(0, -4, 0));//1
            Vector3List.Add(new Vector3(0, -2, 0));//2
            Vector3List.Add(new Vector3(0, -3, 0));//3
            Vector3List.Add(new Vector3(0, -2, 0));//4
            Vector3List.Add(new Vector3(0, -2, 0));//5
            Vector3List.Add(new Vector3(0, -2, 0));//6
            Vector3List.Add(new Vector3(0, -2, 0));//7
            Vector3List.Add(new Vector3(0, -3, 0));//8
            Vector3List.Add(new Vector3(0, -3, 0));//9

            Vector3List.Add(new Vector3(0, -7, 0));//:
            Vector3List.Add(new Vector3(0, -3, 0));//?

            Vector3List.Add(new Vector3(0, 2, 0));//A
            Vector3List.Add(new Vector3(0, 0, 0));//B
            Vector3List.Add(new Vector3(0, 2, 0));//C
            Vector3List.Add(new Vector3(0, 1, 0));//D
            Vector3List.Add(new Vector3(0, 0, 0));//E
            Vector3List.Add(new Vector3(0, 0, 0));//F
            Vector3List.Add(new Vector3(0, 2, 0));//G
            Vector3List.Add(new Vector3(0, 0, 0));//H
            Vector3List.Add(new Vector3(0, -2, 0));//I
            Vector3List.Add(new Vector3(0, 1, 0));//J
            Vector3List.Add(new Vector3(0, 0, 0));//K
            Vector3List.Add(new Vector3(0, -1, 0));//L
            Vector3List.Add(new Vector3(0, 2, 0));//M
            Vector3List.Add(new Vector3(0, 0, 0));//N
            Vector3List.Add(new Vector3(0, 0, 0));//O
            Vector3List.Add(new Vector3(0, 0, 0));//P
            Vector3List.Add(new Vector3(0, 0, 0));//Q
            Vector3List.Add(new Vector3(0, 0, 0));//R
            Vector3List.Add(new Vector3(0, 0, 0));//S
            Vector3List.Add(new Vector3(0, 0, 0));//T
            Vector3List.Add(new Vector3(0, 0, 0));//U
            Vector3List.Add(new Vector3(0, 2, 0));//V
            Vector3List.Add(new Vector3(0, 6, 0));//W
            Vector3List.Add(new Vector3(0, 1, 0));//X
            Vector3List.Add(new Vector3(0, 2, 0));//Y
            Vector3List.Add(new Vector3(0, 0, 0));//Z

            Vector3List.Add(new Vector3(0, -3, 0));//a
            Vector3List.Add(new Vector3(0, -4, 0));//b
            Vector3List.Add(new Vector3(0, -3, 0));//c
            Vector3List.Add(new Vector3(0, -4, 0));//d
            Vector3List.Add(new Vector3(0, -4, 0));//e
            Vector3List.Add(new Vector3(0, -4, 0));//f
            Vector3List.Add(new Vector3(0, -3, 0));//g
            Vector3List.Add(new Vector3(0, -4, 0));//h
            Vector3List.Add(new Vector3(0, -4, 0));//i
            Vector3List.Add(new Vector3(0, -4, 0));//j
            Vector3List.Add(new Vector3(0, -4, 0));//k
            Vector3List.Add(new Vector3(0, -5, 0));//l
            Vector3List.Add(new Vector3(0, -2, 0));//m
            Vector3List.Add(new Vector3(0, -4, 0));//n
            Vector3List.Add(new Vector3(0, -2, 0));//o
            Vector3List.Add(new Vector3(0, -4, 0));//p
            Vector3List.Add(new Vector3(0, -2, 0));//q
            Vector3List.Add(new Vector3(0, -4, 0));//r
            Vector3List.Add(new Vector3(0, -3, 0));//s
            Vector3List.Add(new Vector3(0, -4, 0));//t
            Vector3List.Add(new Vector3(0, -3, 0));//u
            Vector3List.Add(new Vector3(0, -2, 0));//v
            Vector3List.Add(new Vector3(0, 2, 0));//w
            Vector3List.Add(new Vector3(0, -3, 0));//x
            Vector3List.Add(new Vector3(0, -2, 0));//y
            Vector3List.Add(new Vector3(0, -3, 0));//z


            Vector3List.Add(new Vector3(0, 0, 0));//...
            /// NE CHANGE RIEN


            UltimateFont = new SpriteFont(SuperFont, glyphRect, croppingList, charList, 0, 12f, Vector3List, '§');


            for (int i = 0; i < numberCaractere; i++)
            {
                glyphRect.Remove(glyphRect[0]);
                charList.Remove(charList[0]);
                croppingList.Remove(croppingList[0]);
                Vector3List.Remove(Vector3List[0]);
            }


        }





    }
}