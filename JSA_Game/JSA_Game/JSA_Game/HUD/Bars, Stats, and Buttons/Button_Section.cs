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
        //Button Image
        public Texture2D Button1Text;
        public Texture2D Button2Text;
        public Texture2D Button3Text;
        public Texture2D Button4Text;
        public Texture2D Button5Text;
        //Size of Button
        public Vector2 Button1Size;
        public Vector2 Button2Size;
        public Vector2 Button3Size;
        public Vector2 Button4Size;
        public Vector2 Button5Size;
        //Button Position
        public Vector2 Button1Pos;
        public Vector2 Button2Pos;
        public Vector2 Button3Pos;
        public Vector2 Button4Pos;
        public Vector2 Button5Pos;
        //Button Rectangle
        public Rectangle Button1Rec;
        public Rectangle Button2Rec;
        public Rectangle Button3Rec;
        public Rectangle Button4Rec;
        public Rectangle Button5Rec;
        //Button Color
        public Color Button1Col;
        public Color Button2Col;
        public Color Button3Col;
        public Color Button4Col;
        public Color Button5Col;
        //Font
        SpriteFont defaultFont;
        //Font Positions
        public Vector2 font1pos;
        public Vector2 font2pos;
        public Vector2 font3pos;
        public Vector2 font4pos;
        public Vector2 font5pos;
        //Generic Constant Names
        public const string attack = "Attack";
        public const string items = "Items";
        public const string exert = "Exert";
        public const string wait = "Wait";
        public const string actions = "Actions";
        public const string abilities = "Abilities";
        //Ability Names
        public string targetAbility1;
        public string targetAbility2;
        public string targetAbility3 = "Ability";
        public string targetAbility4 = "Ability";
        //Item Names
        public string targetItem1 = "Item";
        public string targetItem2 = "Item";
        public string targetItem3 = "Item";
        public string targetItem4 = "Item";
        //Button Display Values
        public enum ButtonState{
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
            font1pos.X = Button1Pos.X + 30;
            font1pos.Y = Button1Pos.Y + 3;

            //INIT Button 2
            Button2Size = new Vector2(125, 25);
            Button2Pos = new Vector2(195, 558);
            Button2Rec = new Rectangle((int)Button2Pos.X, (int)Button2Pos.Y,
                 (int)Button2Size.X, (int)Button2Size.Y);
            Button2Col = new Color(255, 255, 255, 255);
            font2pos.X = Button2Pos.X + 30;
            font2pos.Y = Button2Pos.Y + 3;

            //INIT Button 3
            Button3Size = new Vector2(125, 25);
            Button3Pos = new Vector2(265, 517);
            Button3Rec = new Rectangle((int)Button3Pos.X, (int)Button3Pos.Y,
                 (int)Button3Size.X, (int)Button3Size.Y);
            Button3Col = new Color(255, 255, 255, 255);
            font3pos.X = Button3Pos.X + 30;
            font3pos.Y = Button3Pos.Y + 3;

            //INIT Button 4
            Button4Size = new Vector2(125, 25);
            Button4Pos = new Vector2(195, 517);
            Button4Rec = new Rectangle((int)Button4Pos.X, (int)Button4Pos.Y,
                 (int)Button4Size.X, (int)Button4Size.Y);
            Button4Col = new Color(255, 255, 255, 255);
            font4pos.X = Button4Pos.X + 30;
            font4pos.Y = Button4Pos.Y + 3;

            //INIT Button 5
            Button5Size = new Vector2(125, 25);
            Button5Pos = new Vector2(335, 517);
            Button5Rec = new Rectangle((int)Button5Pos.X, (int)Button5Pos.Y,
                 (int)Button5Size.X, (int)Button5Size.Y);
            Button5Col = new Color(255, 255, 255, 255);
            font5pos.X = Button5Pos.X + 30;
            font5pos.Y = Button5Pos.Y + 3;
        }

        public void CharacterSelect(Character c)
        {
            targetAbility1 = c.Actions[0].Name;
            targetAbility2 = c.Actions[1].Name;
            //targetAbility3 = c.Actions[2].Name;
            //targetAbility4 = c.Actions[3].Name;
            //targetItem1 = c.Inventory[0];
            //targetItem2 = c.Inventory[1];
            //targetItem3 = c.Inventory[2];
            //targetItem4 = c.Inventory[3];
        }

        public void ButtonSelect(KeyboardState keyboard)
        {
            if (bState == ButtonState.startDisplay)
            {
                //Wait
                if (keyboard.IsKeyDown(Keys.D3))
                {
                    Console.WriteLine("Wait Key Pressed");
                }
                //Item
                if (keyboard.IsKeyDown(Keys.D2))
                {
                    Console.WriteLine("Item Key Pressed");
                    bState = ButtonState.itemDisplay;
                }
                //Action
                if (keyboard.IsKeyDown(Keys.D1))
                {
                    Console.WriteLine("Action Key Pressed");
                    bState = ButtonState.actionDisplay;
                }
            }

            if (bState == ButtonState.actionDisplay)
            {
                //Exert
                if (keyboard.IsKeyDown(Keys.D3))
                {
                    //TODO
                }
                //Ability
                else if (keyboard.IsKeyDown(Keys.D2))
                {

                    bState = ButtonState.abilityDisplay;
                }
                //Attack
                else if (keyboard.IsKeyDown(Keys.D1))
                {
                    Selected.setAction(PerformedType.Attack, -1);
                }
            }

            if(bState == ButtonState.abilityDisplay)
            {
                if (keyboard.IsKeyDown(Keys.D4))
                {
                    Selected.setAction(PerformedType.Ability, 3);
                }
                else if (keyboard.IsKeyDown(Keys.D3))
                {
                    Selected.setAction(PerformedType.Ability, 2);
                }
                else if (keyboard.IsKeyDown(Keys.D2))
                {
                    Selected.setAction(PerformedType.Ability, 1);
                }
                else if (keyboard.IsKeyDown(Keys.D1))
                {
                    Selected.setAction(PerformedType.Ability, 0);
                }
            }

            if (bState == ButtonState.itemDisplay)
            {
                if (keyboard.IsKeyDown(Keys.D4))
                {
                    //return 4;
                }
                else if (keyboard.IsKeyDown(Keys.D3))
                {
                    //return 3;
                }
                else if (keyboard.IsKeyDown(Keys.D2))
                {
                    //return 2;
                }
                else if (keyboard.IsKeyDown(Keys.D1))
                {
                    //return 1;
                }
            }

           // return 666;
        }

        public void LoadContent(ContentManager Content)
        {
                Button1Text = Content.Load<Texture2D>("bar_base");
                Button2Text = Content.Load<Texture2D>("bar_base");
                Button3Text = Content.Load<Texture2D>("bar_base");
                Button4Text = Content.Load<Texture2D>("bar_base");
                Button5Text = Content.Load<Texture2D>("bar_base");
                defaultFont = Content.Load<SpriteFont>("StatFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bState == ButtonState.startDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button3Text, Button3Rec, Button3Col);
                spriteBatch.DrawString(defaultFont, wait, font1pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, items, font2pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, actions, font3pos, Color.Blue);
            }

            if (bState == ButtonState.actionDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button3Text, Button3Rec, Button3Col);
                spriteBatch.DrawString(defaultFont, exert, font1pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, abilities, font2pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, attack, font3pos, Color.Blue);
            }

            if (bState == ButtonState.abilityDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button4Text, Button4Rec, Button4Col);
                spriteBatch.Draw(Button5Text, Button5Rec, Button5Col);
                spriteBatch.DrawString(defaultFont, targetAbility1, font1pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetAbility2, font2pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetAbility3, font4pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetAbility4, font5pos, Color.Blue);
            }

            if (bState == ButtonState.itemDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button4Text, Button4Rec, Button4Col);
                spriteBatch.Draw(Button5Text, Button5Rec, Button5Col);
                spriteBatch.DrawString(defaultFont, targetItem1, font1pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetItem2, font2pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetItem3, font4pos, Color.Blue);
                spriteBatch.DrawString(defaultFont, targetItem4, font5pos, Color.Blue);
            }
        }
    }
}