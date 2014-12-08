using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace JSA_Game
{
    class Sound
    {
        ContentManager Content;

        private static SoundEffect bow_attack;
        private static SoundEffect fire_spell;
        private static SoundEffect ice_spell;
        private static SoundEffect multishot;
        private static SoundEffect shock_spell;
        private static SoundEffect swoosh;
        private static SoundEffect sword_attack;
        private static SoundEffect battle;
        private static SoundEffect cursor;
        private static SoundEffectInstance cursorInstance;
        private static SoundEffectInstance battleInstance;
        private static SoundEffect title;
        private static SoundEffectInstance titleInstance;
        private static SoundEffect town;
        private static SoundEffectInstance townInstance;
        private static SoundEffect wind;
        private static SoundEffectInstance windInstance;
        private static SoundEffect horn;
        private static SoundEffectInstance hornInstance;
        private static SoundEffect death1;
        private static SoundEffectInstance death1Instance;
        private static SoundEffect death2;
        private static SoundEffectInstance death2Instance;
        private static SoundEffect death3;
        private static SoundEffectInstance death3Instance;
        private static SoundEffect death4;
        private static SoundEffectInstance death4Instance;
        private static SoundEffect death5;
        private static SoundEffectInstance death5Instance;
        private static SoundEffect death6;
        private static SoundEffectInstance death6Instance;
        private static SoundEffect miss;
        private static SoundEffect defend;

        public Sound(ContentManager Content)
        {
            this.Content = Content;

            bow_attack = Content.Load<SoundEffect>("Audio\\bow_attack");
            fire_spell = Content.Load<SoundEffect>("Audio\\fire_spell");
            ice_spell = Content.Load<SoundEffect>("Audio\\ice_spell");
            multishot = Content.Load<SoundEffect>("Audio\\multishot");
            shock_spell = Content.Load<SoundEffect>("Audio\\shock_spell");
            swoosh = Content.Load<SoundEffect>("Audio\\swoosh");
            sword_attack = Content.Load<SoundEffect>("Audio\\sword_attack");
            cursor = Content.Load<SoundEffect>("Audio\\cursor");
            cursorInstance = cursor.CreateInstance();
            battle = Content.Load<SoundEffect>("Audio\\battle");
            battleInstance = battle.CreateInstance();
            battleInstance.Volume = 0.5f;
            battleInstance.IsLooped = true;
            title = Content.Load<SoundEffect>("Audio\\title");
            titleInstance = title.CreateInstance();
            titleInstance.Volume = 0.5f;
            titleInstance.IsLooped = true;
            town = Content.Load<SoundEffect>("Audio\\town");
            townInstance = town.CreateInstance();
            townInstance.Volume = 0.5f;
            townInstance.IsLooped = true;
            wind = Content.Load<SoundEffect>("Audio\\wind");
            windInstance = wind.CreateInstance();
            windInstance.Volume = 0.3f;
            horn = Content.Load<SoundEffect>("Audio\\horn");
            hornInstance = horn.CreateInstance();
            hornInstance.Volume = 0.6f;
            death1 = Content.Load<SoundEffect>("Audio\\death1");
            death1Instance = death1.CreateInstance();
            death1Instance.Volume = 0.5f;
            death2 = Content.Load<SoundEffect>("Audio\\death2");
            death2Instance = death2.CreateInstance();
            death2Instance.Volume = 0.5f;
            death3 = Content.Load<SoundEffect>("Audio\\death3");
            death3Instance = death3.CreateInstance();
            death3Instance.Volume = 0.5f;
            death4 = Content.Load<SoundEffect>("Audio\\death4");
            death4Instance = death4.CreateInstance();
            death4Instance.Volume = 0.5f;
            death5 = Content.Load<SoundEffect>("Audio\\death5");
            death5Instance = death5.CreateInstance();
            death5Instance.Volume = 0.5f;
            death6 = Content.Load<SoundEffect>("Audio\\death6");
            death6Instance = death6.CreateInstance();
            death6Instance.Volume = 0.5f;
            miss = Content.Load<SoundEffect>("Audio\\miss");
            defend = Content.Load<SoundEffect>("Audio\\defend");
        }

        public static void PlaySound(String sound)
        {
            switch (sound)
            {
                case "bow_attack":
                    bow_attack.Play();
                    break;
                case "fire_spell":
                    fire_spell.Play();
                    break;
                case "ice_spell":
                    ice_spell.Play();
                    break;
                case "multishot":
                    multishot.Play();
                    break;
                case "shock_spell":
                    shock_spell.Play();
                    break;
                case "swoosh":
                    swoosh.Play();
                    break;
                case "sword_attack":
                    sword_attack.Play();
                    break;
                case "cursor":
                    PlayAmbientSounds();
                    cursorInstance.Volume = 0.2f;
                    cursorInstance.Play();
                    break;
                case "battle":
                    StopSounds();
                    if (battleInstance.State == SoundState.Stopped)
                    {
                        battleInstance.Play();
                    }
                    break;
                case "title":
                    StopSounds();
                    if (titleInstance.State == SoundState.Stopped)
                    {
                        titleInstance.Play();
                    }
                    break;
                case "town":
                    StopSounds();
                    if (townInstance.State == SoundState.Stopped)
                    {
                        townInstance.Play();
                    }
                    break;
                case "death":
                    PlayDeathSound();
                    break;
                case "miss":
                    miss.Play();
                    break;
                case "defend":
                    defend.Play();
                    break;
            }
        }

        private static void StopSounds()
        {
            if (battleInstance != null)
            {
                battleInstance.Stop();
            }

            if (titleInstance != null)
            {
                titleInstance.Stop();
            }

            if (townInstance != null)
            {
                townInstance.Stop();
            }
        }

        private static void PlayAmbientSounds()
        {
            Random rng1 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int rand1 = rng1.Next(0, 100);
            if (rand1 >= 95)
            {
                // 5% of the time play one of these sound effects.
                Random rng2 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                int rand2 = rng2.Next(0, 100);
                if (rand2 % 2 == 0)
                {
                    hornInstance.Play();
                }
                else
                {
                    windInstance.Play();
                }
            }
        }

        private static void PlayDeathSound()
        {
            Random rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(0, 6);

            switch (rand)
            {
                case 0:
                    death1Instance.Play();
                    break;
                case 1:
                    death2Instance.Play();
                    break;
                case 2:
                    death3Instance.Play();
                    break;
                case 3:
                    death4Instance.Play();
                    break;
                case 4:
                    death5Instance.Play();
                    break;
                case 5:
                    death6Instance.Play();
                    break;
            }
        }
    }
}
