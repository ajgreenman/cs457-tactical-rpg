using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using JSA_Game.Maps.State;

namespace JSA_Game.HUD
{
    class Button_Section
    {
        Character c;
        //Button Background
        public Texture2D abilityBackground;
        public Texture2D itemBackground;
        public Rectangle backgroundRectangle;
        public Vector2 backgroundPosition;
        public Vector2 backgroundSize;

        //Button Image
        public Texture2D Button1Text;
        public Texture2D Button2Text;
        public Texture2D Button3Text;
        public Texture2D Button4Text;
        //Size of Button
        public Vector2 Button1Size;
        public Vector2 Button2Size;
        public Vector2 Button3Size;
        public Vector2 Button4Size;
        //Button Position
        public Vector2 Button1Pos;
        public Vector2 Button2Pos;
        public Vector2 Button3Pos;
        public Vector2 Button4Pos;
        //Button Rectangle
        public Rectangle Button1Rec;
        public Rectangle Button2Rec;
        public Rectangle Button3Rec;
        public Rectangle Button4Rec;
        //Button Color
        public Color Button1Col;
        public Color Button2Col;
        public Color Button3Col;
        public Color Button4Col;
        //Font
        SpriteFont defaultFont;
        //Font Positions
        public Vector2 font1pos;
        public Vector2 font2pos;
        public Vector2 font3pos;
        public Vector2 font4pos;
        //Generic Constant Names
        public const string attack = "Attack";
        public const string defend = "Defend";
        public const string items = "Items";
        public const string exert = "Exert";
        public const string wait = "Wait";
        public const string move = "Move";
        public const string actions = "Actions";
        public const string abilities = "Abilities";
        //Ability Names
        public string targetAbility1 = "Empty";
        public string targetAbility2 = "Empty";
        public string targetAbility3 = "Empty";
        public string targetAbility4 = "Empty";
        //Item Names
        public string targetItem1 = "Empty";
        public string targetItem2 = "Empty";
        public string targetItem3 = "Empty";
        public string targetItem4 = "Empty";
        
        //Button Display Values
        public enum ButtonState
        {
            startDisplay,
            actionDisplay,
            abilityDisplay,
            itemDisplay
        }
        public ButtonState bState;
        
        public Button_Section()
        {
            //INIT Display 
            bState = ButtonState.startDisplay;

            //INIT Button1
            Button1Size = new Vector2(125, 25);
            Button1Pos = new Vector2(335, 558);
            Button1Rec = new Rectangle((int)Button1Pos.X, (int)Button1Pos.Y,
                 (int)Button1Size.X, (int)Button1Size.Y);
            Button1Col = new Color(255, 255, 255, 255);
            font1pos.X = Button1Pos.X + 5;
            font1pos.Y = Button1Pos.Y + 3;

            //INIT Button 2
            Button2Size = new Vector2(125, 25);
            Button2Pos = new Vector2(195, 558);
            Button2Rec = new Rectangle((int)Button2Pos.X, (int)Button2Pos.Y,
                 (int)Button2Size.X, (int)Button2Size.Y);
            Button2Col = new Color(255, 255, 255, 255);
            font2pos.X = Button2Pos.X + 5;
            font2pos.Y = Button2Pos.Y + 3;

            //INIT Button 3
            Button3Size = new Vector2(125, 25);
            Button3Pos = new Vector2(195, 517);
            Button3Rec = new Rectangle((int)Button3Pos.X, (int)Button3Pos.Y,
                 (int)Button3Size.X, (int)Button3Size.Y);
            Button3Col = new Color(255, 255, 255, 255);
            font3pos.X = Button3Pos.X + 5;
            font3pos.Y = Button3Pos.Y + 3;

            //INIT Button 4
            Button4Size = new Vector2(125, 25);
            Button4Pos = new Vector2(335, 517);
            Button4Rec = new Rectangle((int)Button4Pos.X, (int)Button4Pos.Y,
                 (int)Button4Size.X, (int)Button4Size.Y);
            Button4Col = new Color(255, 255, 255, 255);
            font4pos.X = Button4Pos.X + 5;
            font4pos.Y = Button4Pos.Y + 3;

            //Button Background
            backgroundSize = new Vector2(360, 100);
            backgroundPosition = new Vector2(145, 500);
            backgroundRectangle = new Rectangle((int)backgroundPosition.X, (int)backgroundPosition.Y, (int)backgroundSize.X, (int)backgroundSize.Y);
        }

        public void CharacterSelect(Character c)
        {
            //Getting Character Ability Names
            if (!c.IsEnemy)
            {
                if (c.Actions[0] != null) { targetAbility1 = c.Actions[0].Name; }
                if (c.Actions[1] != null) { targetAbility2 = c.Actions[1].Name; }
                if (c.Actions[2] != null) { targetAbility3 = c.Actions[2].Name; }
                if (c.Actions[3] != null) { targetAbility4 = c.Actions[3].Name; }
                //Getting Character Inventory Names
                if (c.Inventory[0] != null) { targetItem1 = c.Inventory[0].Name; }
                if (c.Inventory[1] != null) { targetItem2 = c.Inventory[1].Name; }
                if (c.Inventory[2] != null) { targetItem3 = c.Inventory[2].Name; }
                if (c.Inventory[3] != null) { targetItem4 = c.Inventory[3].Name; }
            }
            this.c = c;
        }

        public void ButtonSelect(KeyboardState keyboard)
        {
            if (!c.IsEnemy)
            {
                //Start Display
                if (bState == ButtonState.startDisplay)
                {
                    //Wait
                    if (keyboard.IsKeyDown(Keys.D4)) { Selected.setAction(PerformedType.Wait, -1); }

                    //Item
                    if (keyboard.IsKeyDown(Keys.D3)) { bState = ButtonState.itemDisplay; }

                    //Action
                    if (keyboard.IsKeyDown(Keys.D2)) { bState = ButtonState.actionDisplay; }

                    //Move
                    if (keyboard.IsKeyDown(Keys.D1)) { Selected.setAction(PerformedType.Move, -1); }
                }

                //Action Display
                if (bState == ButtonState.actionDisplay)
                {
                    //Go Back a State
                    if (keyboard.IsKeyDown(Keys.Q)) { bState = ButtonState.startDisplay; }

                    if (exert != null)
                    {
                        //Exert
                        if (keyboard.IsKeyDown(Keys.D4)) { Selected.setAction(PerformedType.Exert, -1); }
                    }

                    //Ability
                    if (keyboard.IsKeyDown(Keys.D3)) { bState = ButtonState.abilityDisplay; }

                    if (keyboard.IsKeyUp(Keys.D2))
                    {
                        //Defend
                        if (keyboard.IsKeyDown(Keys.D2)) { Selected.setAction(PerformedType.Defend, -1); }
                    }

                    //Attack
                    if (keyboard.IsKeyDown(Keys.D1)) { Selected.setAction(PerformedType.Attack, -1); }
                }

                if (bState == ButtonState.abilityDisplay)
                {
                    //Go Back a State
                    if (keyboard.IsKeyDown(Keys.Q)) { bState = ButtonState.actionDisplay; }
                    //Ability 4
                    if (targetAbility4 != "Empty" && keyboard.IsKeyDown(Keys.D3))
                    {
                        Selected.setAction(PerformedType.Ability, 3);
                    }

                    //Ability 3
                    if (targetAbility3 != "Empty" && keyboard.IsKeyDown(Keys.D4))
                    {
                        Selected.setAction(PerformedType.Ability, 2);
                    }
                    //Ability 2
                    if (targetAbility2 != "Empty" && keyboard.IsKeyDown(Keys.D2))
                    {
                        Selected.setAction(PerformedType.Ability, 1);
                    }
                    //Ability 1
                    if (targetAbility1 != "Empty" && keyboard.IsKeyDown(Keys.D1))
                    {
                        Selected.setAction(PerformedType.Ability, 0);
                    }
                }

                if (bState == ButtonState.itemDisplay)
                {
                    //Go Back a State
                    if (keyboard.IsKeyDown(Keys.Q)) { bState = ButtonState.startDisplay; }

                    //Item 4
                    if (targetItem4 != "Empty" && keyboard.IsKeyDown(Keys.D3))
                    {
                        Selected.setAction(PerformedType.Item, 3);
                    }

                    if (keyboard.IsKeyUp(Keys.D3))
                    {
                        //Item 3
                        if (targetItem3 != "Empty" && keyboard.IsKeyDown(Keys.D4))
                        {
                            Selected.setAction(PerformedType.Item, 2);
                        }
                    }

                    //Item 2
                    if (targetItem2 != "Empty" && keyboard.IsKeyDown(Keys.D2))
                    {
                        Selected.setAction(PerformedType.Item, 1);
                    }

                    //Item 1
                    if (targetItem1 != "Empty" && keyboard.IsKeyDown(Keys.D1))
                    {
                        Selected.setAction(PerformedType.Item, 0);
                    }
                }
            }
        }

        public void LoadContent(ContentManager Content)
        {
                Button1Text = Content.Load<Texture2D>("wood Background");
                Button2Text = Content.Load<Texture2D>("wood Background");
                Button3Text = Content.Load<Texture2D>("wood Background");
                Button4Text = Content.Load<Texture2D>("wood Background");
                defaultFont = Content.Load<SpriteFont>("AbilityFont");

                abilityBackground = Content.Load<Texture2D>("scroll");
                itemBackground = Content.Load<Texture2D>("itemPouch");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bState == ButtonState.startDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button3Text, Button3Rec, Button3Col);
                spriteBatch.Draw(Button4Text, Button4Rec, Button4Col);
                spriteBatch.DrawString(defaultFont, wait, font1pos, Color.Black);
                spriteBatch.DrawString(defaultFont, items, font2pos, Color.Black);
                spriteBatch.DrawString(defaultFont, move, font3pos, Color.Black);
                spriteBatch.DrawString(defaultFont, actions, font4pos, Color.Black);
            }

            if (bState == ButtonState.actionDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button3Text, Button3Rec, Button3Col);
                spriteBatch.Draw(Button4Text, Button4Rec, Button4Col);
                spriteBatch.DrawString(defaultFont, exert, font1pos, Color.Black);
                spriteBatch.DrawString(defaultFont, abilities, font2pos, Color.Black);
                spriteBatch.DrawString(defaultFont, attack, font3pos, Color.Black);
                spriteBatch.DrawString(defaultFont, defend, font4pos, Color.Black);
            }

            if (bState == ButtonState.abilityDisplay)
            {
                spriteBatch.Draw(abilityBackground, backgroundRectangle, Color.Wheat); 
                spriteBatch.DrawString(defaultFont, targetAbility3, font1pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetAbility4, font2pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetAbility1, font3pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetAbility2, font4pos, Color.Blue);
            }

            if (bState == ButtonState.itemDisplay)
            {
                spriteBatch.Draw(itemBackground, backgroundRectangle, Color.Wheat); 
                spriteBatch.DrawString(defaultFont, targetItem3, font1pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetItem4, font2pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetItem1, font3pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetItem2, font4pos, Color.Blue);
            }
        }
    }
}