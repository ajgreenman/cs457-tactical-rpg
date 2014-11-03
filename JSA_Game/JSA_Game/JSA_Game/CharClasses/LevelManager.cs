using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.CharClasses
{
    static class LevelManager
    {
        
        public static void LevelUp(Character c)
        {
            c.Level++;
            updateStats(c);
        } 

        private static void updateStats(Character c)
        {
            switch (c.Texture)
            {
                case "Archer":
                    updateArcher(c);
                    break;
                case "Cleric":
                    updateCleric(c);
                    break;
                case "Mage":
                    updateMage(c);
                    break;
                case "Thief":
                    updateThief(c);
                    break;
                case "Warrior":
                    updateWarrior(c);
                    break;
                default:
                    break;
            }
        }

        private static void updateArcher(Character c)
        {
            int hp = avgHpMp();
            c.MaxHP += hp;
            c.CurrHp += hp;

            int mp = avgHpMp();
            c.MaxMP += mp;
            c.CurrMp += mp;

            c.Accuracy += strongStat();
            c.Armor += weakStat();
            c.Dodge += avgStat();
            c.Magic += weakStat();
            c.Resist += avgStat();
            c.Strength += strongStat();
        }

        private static void updateCleric(Character c)
        {
            int hp = avgHpMp();
            c.MaxHP += hp;
            c.CurrHp += hp;

            int mp = strongHpMp();
            c.MaxMP += mp;
            c.CurrMp += mp;

            c.Accuracy += avgStat();
            c.Armor += weakStat();
            c.Dodge += avgStat();
            c.Magic += strongStat();
            c.Resist += weakStat();
            c.Strength += avgStat();
        }

        private static void updateMage(Character c)
        {
            int hp = avgHpMp();
            c.MaxHP += hp;
            c.CurrHp += hp;

            int mp = strongHpMp();
            c.MaxMP += mp;
            c.CurrMp += mp;

            c.Accuracy += avgStat();
            c.Armor += weakStat();
            c.Dodge += avgStat();
            c.Magic += strongStat();
            c.Resist += avgStat();
            c.Strength += weakStat();
        }

        private static void updateThief(Character c)
        {
            int hp = avgHpMp();
            c.MaxHP += hp;
            c.CurrHp += hp;

            int mp = avgHpMp();
            c.MaxMP += mp;
            c.CurrMp += mp;

            c.Accuracy += strongStat();
            c.Armor += weakStat();
            c.Dodge += strongStat();
            c.Magic += avgStat();
            c.Resist += weakStat();
            c.Strength += avgStat();
        }

        private static void updateWarrior(Character c)
        {
            int hp = strongHpMp();
            c.MaxHP += hp;
            c.CurrHp += hp;

            int mp = avgHpMp();
            c.MaxMP += mp;
            c.CurrMp += mp;

            c.Accuracy += avgStat();
            c.Armor += avgStat();
            c.Dodge += avgStat();
            c.Magic += weakStat();
            c.Resist += weakStat();
            c.Strength += strongStat();
        }

        private static int weakStat()
        {
            int amount;

            Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(1, 4);

            switch (rand)
            {
                case 1:
                    amount = 0;
                    break;
                case 2:
                    amount = 0;
                    break;
                case 3:
                    amount = 1;
                    break;
                case 4:
                    amount = 2;
                    break;
                default:
                    amount = 0;
                    break;
            }

            return amount;
        }

        private static int avgStat()
        {
            int amount;

            Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(1, 4);

            switch (rand)
            {
                case 1:
                    amount = 0;
                    break;
                case 2:
                    amount = 1;
                    break;
                case 3:
                    amount = 1;
                    break;
                case 4:
                    amount = 2;
                    break;
                default:
                    amount = 1;
                    break;
            }

            return amount;
        }

        private static int strongStat()
        {
            int amount;

            Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(1, 4);

            switch (rand)
            {
                case 1:
                    amount = 0;
                    break;
                case 2:
                    amount = 1;
                    break;
                case 3:
                    amount = 2;
                    break;
                case 4:
                    amount = 3;
                    break;
                default:
                    amount = 2;
                    break;
            }

            return amount;
        }

        private static int weakHpMp()
        {
            int amount;

            Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(1, 2);

            switch (rand)
            {
                case 1:
                    amount = 1;
                    break;
                case 2:
                    amount = 2;
                    break;
                default:
                    amount = 1;
                    break;
            }

            return amount;
        }

        private static int avgHpMp()
        {
            int amount;

            Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(1, 4);

            switch (rand)
            {
                case 1:
                    amount = 0;
                    break;
                case 2:
                    amount = 1;
                    break;
                case 3:
                    amount = 2;
                    break;
                case 4:
                    amount = 4;
                    break;
                default:
                    amount = 2;
                    break;
            }

            return amount;
        }

        private static int strongHpMp()
        {
            int amount;

            Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(1, 4);

            switch (rand)
            {
                case 1:
                    amount = 0;
                    break;
                case 2:
                    amount = 2;
                    break;
                case 3:
                    amount = 3;
                    break;
                case 4:
                    amount = 5;
                    break;
                default:
                    amount = 3;
                    break;
            }
            return amount;
        }
    }
}
