using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Threading;

namespace JSA_Game.HUD
{
    class CombatText
    {
        //Text Font
        SpriteFont floatingText;

        //Text Position
        Vector2 floatingPos;

        //Character List
        ArrayList charList;

        //Draw Timer
        int offset = 0;
        int counter = 1;
        int limit = 2;
        float countDuration = 1f;
        float currentTime = 0f;
        bool stillDrawing = false;

        public CombatText(ArrayList charList)
        {
            floatingPos = new Vector2(0, 0);
            this.charList = charList;
        }

        public void LoadContent(ContentManager Content)
        {
            floatingText = Content.Load<SpriteFont>("FloatingFont");
        }

        public void Update(GameTime gameTime)
        {
            stillDrawing = true;
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;

            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration;
            }

            if (counter >= limit)
            {
                counter = 1;
                stillDrawing = false;
            }
        }


        public void GetDrawPosition(Character c)
        {
            floatingPos.X = c.Pos.X * 100;
            floatingPos.Y = (c.Pos.Y *50) - 50 - offset;
        }

        public void ResetCharacterList(Character c) 
        { 
            c.Set = false;
            offset = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Character currChar = new Character();

            foreach (Character element in charList)
            {
                currChar = element;

                if (currChar.Set)
                {
                    Console.WriteLine(currChar.CurrDamage);

                    if (currChar.DidDefend)
                    {
                        GetDrawPosition(currChar);
                        spriteBatch.DrawString(floatingText, "Defend", floatingPos, Color.Silver);
                        offset += 1;
                        Console.WriteLine("Defend");
                    }

                    else if (currChar.Miss)
                    {
                        GetDrawPosition(currChar);
                        spriteBatch.DrawString(floatingText, "Miss", floatingPos, Color.White);
                        offset += 1;
                        Console.WriteLine("Miss");
                    }

                    else if (currChar.CurrDamage > -1)
                    {
                        GetDrawPosition(currChar);
                        spriteBatch.DrawString(floatingText, currChar.CurrDamage + "", floatingPos, Color.Red);
                        offset += 1;
                        Console.WriteLine("Damage");
                    }

                    else if (currChar.CurrHealing > -1)
                    {
                        GetDrawPosition(currChar);
                        spriteBatch.DrawString(floatingText, currChar.CurrHealing + "", floatingPos, Color.Green);
                        offset += 1;
                        Console.WriteLine("Healing");
                    }
                }
                if (!stillDrawing) { ResetCharacterList(currChar); }
            }
        }
    }
}
