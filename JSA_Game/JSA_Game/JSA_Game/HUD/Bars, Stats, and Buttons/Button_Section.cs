using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
        //Button Display Values
        public Boolean startDisplay;
        public Boolean actionDisplay;
        public Boolean abilityDisplay;
        public Boolean itemDisplay;
        

        public Button_Section()
        {
            //INIT Display 
            startDisplay = true;
            actionDisplay = false;
            abilityDisplay = false;
            itemDisplay = false;

            //INIT Button1
            Button1Size = new Vector2(125, 25);
            Button1Pos = new Vector2(335, 558);
            Button1Rec = new Rectangle((int)Button1Pos.X, (int)Button1Pos.Y,
                 (int)Button1Size.X, (int)Button1Size.Y);
            Button1Col = new Color(255, 255, 255, 255);

            //INIT Button 2
            Button2Size = new Vector2(125, 25);
            Button2Pos = new Vector2(195, 558);
            Button2Rec = new Rectangle((int)Button2Pos.X, (int)Button2Pos.Y,
                 (int)Button2Size.X, (int)Button2Size.Y);
            Button2Col = new Color(255, 255, 255, 255);

            //INIT Button 3
            Button3Size = new Vector2(125, 25);
            Button3Pos = new Vector2(265, 517);
            Button3Rec = new Rectangle((int)Button3Pos.X, (int)Button3Pos.Y,
                 (int)Button3Size.X, (int)Button3Size.Y);
            Button3Col = new Color(255, 255, 255, 255);

            //INIT Button 4
            Button4Size = new Vector2(125, 25);
            Button4Pos = new Vector2(195, 517);
            Button4Rec = new Rectangle((int)Button4Pos.X, (int)Button4Pos.Y,
                 (int)Button4Size.X, (int)Button2Size.Y);
            Button4Col = new Color(255, 255, 255, 255);

            //INIT Button 5
            Button5Size = new Vector2(125, 25);
            Button5Pos = new Vector2(335, 517);
            Button5Rec = new Rectangle((int)Button5Pos.X, (int)Button5Pos.Y,
                 (int)Button5Size.X, (int)Button5Size.Y);
            Button5Col = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            if (startDisplay)
            {
                Button1Text = Content.Load<Texture2D>("waitButton");
                Button2Text = Content.Load<Texture2D>("itemButton");
                Button3Text = Content.Load<Texture2D>("actionButton");
            }

            if (actionDisplay)
            {
                Button1Text = Content.Load<Texture2D>("attackButton");
                Button2Text = Content.Load<Texture2D>("abilityButton");
                Button3Text = Content.Load<Texture2D>("exertButton");
            }

            if (abilityDisplay)
            {
                Button1Text = Content.Load<Texture2D>("attackButton");
                Button2Text = Content.Load<Texture2D>("abilityButton");
                Button4Text = Content.Load<Texture2D>("exertButton");
                Button5Text = Content.Load<Texture2D>("exertButton");
            }

            if (itemDisplay)
            {
                Button1Text = Content.Load<Texture2D>("attackButton");
                Button2Text = Content.Load<Texture2D>("abilityButton");
                Button4Text = Content.Load<Texture2D>("exertButton");
                Button5Text = Content.Load<Texture2D>("exertButton");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (startDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button3Text, Button3Rec, Button3Col);
            }

            if (actionDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button3Text, Button3Rec, Button3Col);
            }

            if (abilityDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button4Text, Button4Rec, Button4Col);
                spriteBatch.Draw(Button5Text, Button5Rec, Button5Col);
            }

            if (itemDisplay)
            {
                spriteBatch.Draw(Button1Text, Button1Rec, Button1Col);
                spriteBatch.Draw(Button2Text, Button2Rec, Button2Col);
                spriteBatch.Draw(Button4Text, Button4Rec, Button4Col);
                spriteBatch.Draw(Button5Text, Button5Rec, Button5Col);
            }
        }
    }
}