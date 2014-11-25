using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

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

        public CombatText(ArrayList charList)
        {
            floatingPos = new Vector2(0, 0);
            this.charList = charList;
        }

        public void LoadContent(ContentManager Content)
        {
            floatingText = Content.Load<SpriteFont>("StatFont");
        }

        public void GetDrawPosition(Character c)
        {
            //if (c.Pos.X < 10)
            //{
            //    floatingPos.X
            //}

            if (c.Pos.X > 490)
            {
                floatingPos.X = c.Pos.X - 30;
            }

            else
            {
                floatingPos.X = c.Pos.X + 30;
            }

            if (c.Pos.Y < 10)
            {
                floatingPos.Y = c.Pos.Y + 10;
            }

            //else if (c.Pos.Y > 590)
            //{
            //    floatingPos.Y = c.Pos.Y - 10;
            //}

            else
            {
                floatingPos.Y = c.Pos.Y;
            }

        }

        public void ResetCharacterList()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Character currChar = new Character();

            foreach (Character element in charList)
            {
                currChar = element;

                if (currChar.Set)
                {
                    GetDrawPosition(currChar);
                    Console.WriteLine(currChar.CurrDamage);

                    if (currChar.DidDefend)
                    {
                        spriteBatch.DrawString(floatingText, "Defend", floatingPos, Color.Silver);
                        Console.WriteLine("Defend");
                    }

                    else if (currChar.Miss)
                    {
                        spriteBatch.DrawString(floatingText, "Miss", floatingPos, Color.White);
                        Console.WriteLine("Miss");
                    }

                    else if (currChar.CurrDamage > -1)
                    {
                        spriteBatch.DrawString(floatingText, currChar.CurrDamage + "", floatingPos, Color.Red);
                        Console.WriteLine("Damage");
                    }

                    else if (currChar.CurrHealing > -1)
                    {
                        spriteBatch.DrawString(floatingText, currChar.CurrHealing + "", floatingPos, Color.Green);
                        Console.WriteLine("Healing");
                    }
                }
                //ResetCharacterList();
            }
        }
    }
}
