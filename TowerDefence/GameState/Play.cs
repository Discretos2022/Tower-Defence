using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INPUT;
using Microsoft.Xna.Framework.Input;
using static TowerDefence.ButtonV3;
using WRITER;
using TowerDefence.Towers;
using Font;

namespace TowerDefence.GameState
{
    public class Play
    {


        public Vector2 ShopPos;

        public bool placed = true;

        public ButtonV3 CanonButton;
        public ButtonV3 SuperCanonButton;
        public ButtonV3 ElectroCanonButton;
        public ButtonV3 SnowCanonButton;

        public ButtonV3 Upgrade;
        public ButtonV3 Sell;

        public static int Coins = 150;
        public static int PV = 100;

        private Tower selectedTower;

        private bool newCanonButtonSelected = true;

        private bool shopIsClosed = false;


        public Play()
        {

            CanonButton = new ButtonV3();
            SuperCanonButton = new ButtonV3();
            ElectroCanonButton = new ButtonV3();
            SnowCanonButton = new ButtonV3();

            Upgrade = new ButtonV3();
            Sell = new ButtonV3();

            InitButton();

        }

        public void Update(State.GameState state, GameTime gameTime, Screen screen)
        {

            Handler.Update(gameTime);

            UpdateShopPosition();

            if (MouseInput.mouseState.LeftButton == ButtonState.Pressed && MouseInput.oldmouseState.LeftButton != ButtonState.Pressed)
            {
                int mouseX = (int)MouseInput.GetScreenPosition(screen).X;
                int mouseY = (int)MouseInput.GetScreenPosition(screen).Y;

                if (!new Rectangle((int)ShopPos.X, (int)ShopPos.Y, 400, 1200).Contains(mouseX, mouseY))
                {
                    UnselectTower();
                }
                
            }

            for (int i = 0; i < Handler.towers.Count; i++)
            {
                int mouseX = (int)MouseInput.GetScreenPosition(screen).X;
                int mouseY = (int)MouseInput.GetScreenPosition(screen).Y;

                if (Handler.towers[i].isPlaced)
                    if (Handler.towers[i].rect.Contains(mouseX, mouseY))
                        if (MouseInput.mouseState.LeftButton == ButtonState.Pressed && MouseInput.oldmouseState.LeftButton != ButtonState.Pressed)
                        {
                            selectedTower = Handler.towers[i];
                            selectedTower.isSelected = true;
                            shopIsClosed = false;
                        }
            }


            float X = MouseInput.GetScreenPosition(screen).X / 1 + 32;
            float Y = MouseInput.GetScreenPosition(screen).Y / 1 + 36;


            if (placed)
                if (KeyInput.getKeyState().IsKeyDown(Keys.Q) && !KeyInput.getOldKeyState().IsKeyDown(Keys.Q))
                {
                    Handler.towers.Add(Handler.GetCanon(1, (int)X, (int)Y));
                    placed = false;
                }

            if (placed)
                if (KeyInput.getKeyState().IsKeyDown(Keys.W) && !KeyInput.getOldKeyState().IsKeyDown(Keys.W))
                {
                    Handler.towers.Add(Handler.GetCanon(2, (int)X, (int)Y));
                    placed = false;
                }

            if (placed)
                if (KeyInput.getKeyState().IsKeyDown(Keys.E) && !KeyInput.getOldKeyState().IsKeyDown(Keys.E))
                {
                    Handler.towers.Add(Handler.GetCanon(3, (int)X, (int)Y));
                    placed = false;
                }

            if (!placed)
            {

                X = MouseInput.GetScreenPosition(screen).X / 1 + 32;
                Y = MouseInput.GetScreenPosition(screen).Y / 1 + 36;

                Handler.towers[Handler.towers.Count - 1].Place(new Vector2((int)(X / 64) * 64, (int)(Y / 64) * 64 - 4), placed);
                Handler.towers[Handler.towers.Count - 1].onObstacle = false;

                int width = Handler.MapPlace.GetLength(0);
                int height = Handler.MapPlace.GetLength(1);

                if ((int)(Y / 64 - 1) < Handler.MapPlace.GetLength(1) && (int)(X / 64 - 1) < Handler.MapPlace.GetLength(0) && (int)(X / 64) > 0 && (int)(Y / 64) > 0)
                {
                    if (Handler.MapPlace[(int)(X / 64 - 1), (int)(Y / 64 - 1)] != 1)
                    {
                        if (MouseInput.mouseState.LeftButton == ButtonState.Pressed && MouseInput.oldmouseState.LeftButton != ButtonState.Pressed)
                        {
                            placed = true;
                            Handler.towers[Handler.towers.Count - 1].Place(new Vector2((int)(X / 64) * 64, (int)(Y / 64) * 64 - 4), placed);
                            Handler.MapPlace[(int)(X / 64 - 1), (int)(Y / 64 - 1)] = 1;
                        }
                    }
                    else
                        Handler.towers[Handler.towers.Count - 1].onObstacle = true;

                }
                else
                    Handler.towers[Handler.towers.Count - 1].onObstacle = true;



            }

            if (KeyInput.getKeyState().IsKeyDown(Keys.Delete) && !KeyInput.getOldKeyState().IsKeyDown(Keys.Delete))
            {
                Handler.towers.Clear();
            }

            UpdateShopButton(gameTime, screen, X, Y);

            Upgrade.SetPosition(ShopPos.X + 160, 820);
            Upgrade.Update(gameTime, screen);

            if (Upgrade.IsSelected())
                Upgrade.SetTexture(Main.UpgradeButton, new Rectangle(17, 0, 16, 16));
            else
                Upgrade.SetTexture(Main.UpgradeButton, new Rectangle(0, 0, 16, 16));

            if (Upgrade.IsCliqued() && selectedTower != null && selectedTower.Level != 10)
            {
                selectedTower.Level += 1;
                selectedTower.UpgradeTower();
            }


            Sell.SetPosition(ShopPos.X + 220, 820);
            Sell.Update(gameTime, screen);

            if (Sell.IsSelected())
                Sell.SetTexture(Main.SellButton, new Rectangle(17, 0, 16, 16));
            else
                Sell.SetTexture(Main.SellButton, new Rectangle(0, 0, 16, 16));

            if (Sell.IsCliqued() && selectedTower != null)
            {
                Handler.towers.Remove(selectedTower);

                Handler.MapPlace[(int)((selectedTower.Position.X - 32) / 64), (int)((selectedTower.Position.Y - 36) / 64)] = 0;

                selectedTower = null;
            }

            if (CanonButton.IsSelected() || SuperCanonButton.IsSelected() || ElectroCanonButton.IsSelected() || SnowCanonButton.IsSelected())
                newCanonButtonSelected = true;
            else
                newCanonButtonSelected = false;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {

            Handler.Draw(spriteBatch, gameTime);

        }

        public void DrawUI(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {


            //ShopPos = new Vector2(1920 - 70 * 4, 0);

            spriteBatch.Draw(Main.Shop, ShopPos, null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);


            CanonButton.Draw(spriteBatch);
            SuperCanonButton.Draw(spriteBatch);
            ElectroCanonButton.Draw(spriteBatch);
            SnowCanonButton.Draw(spriteBatch);

            if (selectedTower != null && !newCanonButtonSelected)
            {
                if(selectedTower.Level != 10)
                    Upgrade.Draw(spriteBatch);
                Sell.Draw(spriteBatch);

                //Writer.DrawText(Main.UltimateFont, "" + selectedTower.Level, ShopPos + new Vector2(220, 700), Color.Orange, Color.Gold, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, true);

                //Writer.DrawText(FontManager.StatFont, "electrocanon", ShopPos + new Vector2(10, 565), Color.CadetBlue, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.DarkBlue, false);

                DrawCanonName(selectedTower, spriteBatch);

                Color color = Color.Gold;

                if (new int[3] { 1, 2, 3 }.Contains(selectedTower.Level))
                    color = Color.RoyalBlue;
                else if (new int[3] { 4, 5, 6 }.Contains(selectedTower.Level))
                    color = Color.Green;
                else if (new int[3] { 7, 8, 9 }.Contains(selectedTower.Level))
                    color = Color.Orange;
                else if (new int[1] { 10 }.Contains(selectedTower.Level))
                    color = Color.Red;

                if(selectedTower.Level >= 10)
                    Writer.DrawText(FontManager.StatFont, "level:max", ShopPos + new Vector2(10, 605), Color.White, color, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.Gray, false);
                else
                    Writer.DrawText(FontManager.StatFont, "level:" + selectedTower.Level, ShopPos + new Vector2(10, 605), Color.White, color, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.Gray, false);
               

                if (!Upgrade.IsSelected() || selectedTower.Level >= 10)
                {
                    Writer.DrawText(FontManager.StatFont, "damage:" + selectedTower.GetDamage(), ShopPos + new Vector2(10, 635), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                    Writer.DrawText(FontManager.StatFont, "range:" + selectedTower.GetRange(), ShopPos + new Vector2(10, 660), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                    Writer.DrawText(FontManager.StatFont, "reload:" + selectedTower.GetTimeOfReload(), ShopPos + new Vector2(10, 685), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                    if (selectedTower.ID == 4) Writer.DrawText(FontManager.StatFont, "debuf:" + (selectedTower.GetDebufTime() / 60.0f).ToString().Replace(',', '.'), ShopPos + new Vector2(10, 710), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                    if (selectedTower.ID == 4) Writer.DrawText(FontManager.StatFont, "velocity:" + (int)(selectedTower.GetDebufVelocityReduce() * 100), ShopPos + new Vector2(10, 735), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                }
                if ((Upgrade.IsSelected() || Upgrade.IsCliqued()) && selectedTower.Level < 10)
                {
                    Writer.DrawText(FontManager.StatFont, "damage:" + selectedTower.GetDamage() + "+" + (selectedTower.GetDamage(selectedTower.Level + 1) - selectedTower.GetDamage()), ShopPos + new Vector2(10, 635), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                    Writer.DrawText(FontManager.StatFont, "range:" + selectedTower.GetRange() + "+" + (selectedTower.GetRange(selectedTower.Level + 1) - selectedTower.GetRange()), ShopPos + new Vector2(10, 660), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                    Writer.DrawText(FontManager.StatFont, "reload:" + selectedTower.GetTimeOfReload() + "-" + Math.Abs(selectedTower.GetTimeOfReload(selectedTower.Level + 1) - selectedTower.GetTimeOfReload()), ShopPos + new Vector2(10, 685), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);

                    if (selectedTower.ID == 4) Writer.DrawText(FontManager.StatFont, "debuf:" + (selectedTower.GetDebufTime() / 60.0f).ToString().Replace(',', '.') + "+" + (selectedTower.GetDebufTime(selectedTower.Level + 1) / 60.0f - selectedTower.GetDebufTime() / 60.0f).ToString().Replace(',', '.'), ShopPos + new Vector2(10, 710), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                    if (selectedTower.ID == 4) Writer.DrawText(FontManager.StatFont, "velocity:" + (int)(selectedTower.GetDebufVelocityReduce() * 100) + "-" + Math.Abs((int)(selectedTower.GetDebufVelocityReduce(selectedTower.Level + 1) * 100) - (int)(selectedTower.GetDebufVelocityReduce() * 100)), ShopPos + new Vector2(10, 735), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);

                    //Writer.DrawText(FontManager.StatFont, "updgrade:" + "100$", ShopPos + new Vector2(10, 740), Color.Orange, Color.Yellow, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.DarkOrange, false);
                    Writer.DrawText(FontManager.StatFont, "updgrade:" + selectedTower.GetPrice() + "$", ShopPos + new Vector2(10, 780), Color.Green, Color.LightGreen, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.DarkGreen, false);
                }

                if (Sell.IsSelected() || Sell.IsCliqued())
                {
                    Writer.DrawText(FontManager.StatFont, "sell:" + "100$", ShopPos + new Vector2(10, 780), Color.Red, Color.Orange, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.DarkRed, false);
                }



            }

           

            if (CanonButton.IsSelected() || SuperCanonButton.IsSelected() || ElectroCanonButton.IsSelected() || SnowCanonButton.IsSelected())
            {
                Tower t = null;
                if (CanonButton.IsSelected())
                    t = new Canon(1, -100, -100);
                else if (SuperCanonButton.IsSelected())
                    t = new SuperCanon(2, -100, -100);
                else if (ElectroCanonButton.IsSelected())
                    t = new ElectroCanon(3, -100, -100);
                else if (SnowCanonButton.IsSelected())
                    t = new SnowCanon(4, -100, -100);

                DrawCanonName(t, spriteBatch);

                Writer.DrawText(FontManager.StatFont, "level:" + 1, ShopPos + new Vector2(10, 605), Color.White, Color.RoyalBlue, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.Gray, false);
                Writer.DrawText(FontManager.StatFont, "damage:" + t.GetDamage(1), ShopPos + new Vector2(10, 635), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                Writer.DrawText(FontManager.StatFont, "range:" + t.GetRange(1), ShopPos + new Vector2(10, 660), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                Writer.DrawText(FontManager.StatFont, "reload:" + t.GetTimeOfReload(1), ShopPos + new Vector2(10, 685), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                if (t.ID == 4) Writer.DrawText(FontManager.StatFont, "debuf:" + (t.GetDebufTime(1) / 60) + "sec", ShopPos + new Vector2(10, 710), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                if (t.ID == 4) Writer.DrawText(FontManager.StatFont, "velocity:" + t.GetDebufVelocityReduce(1) * 100, ShopPos + new Vector2(10, 735), Color.Gray, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 1f, spriteBatch, Color.Black, false);
                t = null;
            }


            /// Draw Canon on button
            spriteBatch.Draw(Main.BaseTex[1], new Vector2(ShopPos.X + 30 + 30, 250 + 34), null, Color.White, 0f, new Vector2(16, 16), 3f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.TowerTex[1], new Vector2(ShopPos.X + 30 + 30, 250 + 34), new Rectangle(0, 0, 32, 32), Color.White, -(float)(Math.PI / 4), new Vector2(16, 16), 3f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Main.BaseTex[2], new Vector2(ShopPos.X + 110 + 30, 250 + 34), null, Color.White, 0f, new Vector2(16, 16), 3f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.TowerTex[2], new Vector2(ShopPos.X + 110 + 30, 250 + 34), new Rectangle(0, 0, 32, 32), Color.White, -(float)(Math.PI / 4), new Vector2(16, 16), 3f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Main.BaseTex[3], new Vector2(ShopPos.X + 190 + 30, 250 + 34), null, Color.White, 0f, new Vector2(16, 16), 3f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.TowerTex[3], new Vector2(ShopPos.X + 190 + 30, 250 + 34), new Rectangle(0, 0, 32, 32), Color.White, -(float)(Math.PI / 4), new Vector2(16, 16), 3f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Main.BaseTex[4], new Vector2(ShopPos.X + 30 + 30, 250 + 34 + 80), new Rectangle(8, 7, 18, 17), Color.White, 0f, new Vector2(8, 9), 3f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.TowerTex[4], new Vector2(ShopPos.X + 30 + 30, 250 + 34 + 80), new Rectangle(0, 0, 32, 32), Color.White, -(float)(Math.PI / 4), new Vector2(16, 16), 3f, SpriteEffects.None, 0f);


            Writer.DrawText(FontManager.StatFont, "" + Coins + "$", ShopPos + new Vector2(10, 80), Color.Orange, Color.Gold, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, true);
            Writer.DrawText(FontManager.StatFont, "" + PV, ShopPos + new Vector2(10, 120), Color.White, Color.Red, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, true);


            //Writer.DrawText(FontManager.StatFont, "reload:200-20", new Vector2(100, 100), Color.Black, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, 3f, spriteBatch, true);



            /*
            spriteBatch.Draw(Main.Bounds, new Rectangle(32, 28, 16 * 4, 16 * 4), Color.Yellow);

            spriteBatch.Draw(Main.Bounds, new Rectangle(64 * 28 + 32, 28, 16 * 4, 16 * 4), Color.Yellow);

            spriteBatch.Draw(Main.Bounds, new Rectangle(32, 64 * 15 + 28, 16 * 4, 16 * 4), Color.Yellow);

            spriteBatch.Draw(Main.Bounds, new Rectangle(64 * 28 + 32, 64 * 15 + 28, 16 * 4, 16 * 4), Color.Yellow);
            */

        }



        public void UnselectTower()
        {
            if (selectedTower != null)
                selectedTower.isSelected = false;
            selectedTower = null;
        }

        public void UpdateShopPosition()
        {
            if (KeyInput.getKeyState().IsKeyDown(Keys.Tab) && !KeyInput.getOldKeyState().IsKeyDown(Keys.Tab))
                if (shopIsClosed)
                    shopIsClosed = false;
                else
                    shopIsClosed = true;

            if (shopIsClosed)
            {
                if (ShopPos.X < 1920)
                    ShopPos.X += 10;
                else
                    ShopPos.X = 1920;
            }
            else
            {
                if (ShopPos.X > 1920 - 70 * 4)
                    ShopPos.X -= 10;
                else
                    ShopPos.X = 1920 - 70 * 4;
            }
        }

        public void DrawCanonName(Tower t, SpriteBatch spriteBatch)
        {
            if (t.Name == "canon")
                Writer.DrawText(FontManager.StatFont, t.Name, ShopPos + new Vector2(10, 565), Color.Gray, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.DarkGray, false);
            else if (t.Name == "super canon")
                Writer.DrawText(FontManager.StatFont, t.Name, ShopPos + new Vector2(10, 565), Color.Gray, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.DarkBlue, false);
            else if (t.Name == "electrocanon")
                Writer.DrawText(FontManager.StatFont, t.Name, ShopPos + new Vector2(10, 565), Color.Aquamarine, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.DarkBlue, false);
            else if (t.Name == "snowcanon")
                Writer.DrawText(FontManager.StatFont, t.Name, ShopPos + new Vector2(10, 565), Color.LightBlue, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.CadetBlue, false);

            // Color.Gray, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.DarkGray, false);
        }

        public void UpdateShopButton(GameTime gameTime, Screen screen, float X, float Y)
        {

            CanonButton.SetPosition(ShopPos.X + 30, 250);
            CanonButton.Update(gameTime, screen);

            if (CanonButton.IsSelected())
                CanonButton.SetTexture(Main.TowerButton, new Rectangle(33, 0, 32, 32));
            else
                CanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            if (CanonButton.IsCliqued())
            {
                if (placed)
                {
                    Handler.towers.Add(Handler.GetCanon(1, (int)X, (int)Y));
                    placed = false;
                    UnselectTower();
                }
            }


            SuperCanonButton.SetPosition(ShopPos.X + 30 + 80, 250);
            SuperCanonButton.Update(gameTime, screen);

            if (SuperCanonButton.IsSelected())
                SuperCanonButton.SetTexture(Main.TowerButton, new Rectangle(33, 0, 32, 32));
            else
                SuperCanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            if (SuperCanonButton.IsCliqued())
            {
                if (placed)
                {
                    Handler.towers.Add(Handler.GetCanon(2, (int)X, (int)Y));
                    placed = false;
                    UnselectTower();
                }
            }


            ElectroCanonButton.SetPosition(ShopPos.X + 30 + 80 + 80, 250);
            ElectroCanonButton.Update(gameTime, screen);

            if (ElectroCanonButton.IsSelected())
                ElectroCanonButton.SetTexture(Main.TowerButton, new Rectangle(33, 0, 32, 32));
            else
                ElectroCanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            if (ElectroCanonButton.IsCliqued())
            {
                if (placed)
                {
                    Handler.towers.Add(Handler.GetCanon(3, (int)X, (int)Y));
                    placed = false;
                    UnselectTower();
                }
            }


            SnowCanonButton.SetPosition(ShopPos.X + 30, 250 + 80);
            SnowCanonButton.Update(gameTime, screen);

            if (SnowCanonButton.IsSelected())
                SnowCanonButton.SetTexture(Main.TowerButton, new Rectangle(33, 0, 32, 32));
            else
                SnowCanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            if (SnowCanonButton.IsCliqued())
            {
                if (placed)
                {
                    Handler.towers.Add(Handler.GetCanon(4, (int)X, (int)Y));
                    placed = false;
                    UnselectTower();
                }
            }

        }


        public void InitButton()
        {

            CanonButton.SetFont(Main.UltimateFont);
            CanonButton.SetScale(2f);
            CanonButton.SetFrontThickness(3f);
            CanonButton.SetPosition(0, 400);
            CanonButton.SetColor(Color.White, Color.Black);
            CanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            SuperCanonButton.SetFont(Main.UltimateFont);
            SuperCanonButton.SetScale(2f);
            SuperCanonButton.SetFrontThickness(3f);
            SuperCanonButton.SetPosition(0, 400);
            SuperCanonButton.SetColor(Color.White, Color.Black);
            SuperCanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            ElectroCanonButton.SetFont(Main.UltimateFont);
            ElectroCanonButton.SetScale(2f);
            ElectroCanonButton.SetFrontThickness(3f);
            ElectroCanonButton.SetPosition(0, 400);
            ElectroCanonButton.SetColor(Color.White, Color.Black);
            ElectroCanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            SnowCanonButton.SetFont(Main.UltimateFont);
            SnowCanonButton.SetScale(2f);
            SnowCanonButton.SetFrontThickness(3f);
            SnowCanonButton.SetPosition(0, 400);
            SnowCanonButton.SetColor(Color.White, Color.Black);
            SnowCanonButton.SetTexture(Main.TowerButton, new Rectangle(0, 0, 32, 32));

            Upgrade.SetFont(Main.UltimateFont);
            Upgrade.SetScale(3f);
            Upgrade.SetFrontThickness(3f);
            Upgrade.SetPosition(0, 400);
            Upgrade.SetColor(Color.White, Color.Black);
            Upgrade.SetTexture(Main.UpgradeButton, new Rectangle(0, 0, 32, 32));

            Sell.SetFont(Main.UltimateFont);
            Sell.SetScale(3f);
            Sell.SetFrontThickness(3f);
            Sell.SetPosition(0, 400);
            Sell.SetColor(Color.White, Color.Black);
            Sell.SetTexture(Main.SellButton, new Rectangle(0, 0, 32, 32));

        }


    }
}
