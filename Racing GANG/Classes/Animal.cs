using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MONO_TEST;
using Racing_GANG;

namespace MONO_TEST
{
    class Animal
    {
        public int Num;

        public float Vel;
        public float NextVel;

        public bool Finished;

        public int Place;
        public int Bet;

        public bool VelChanged;

        public float VelTransitionInterval;

        public Timer VelChange;
        public Timer VelTransition;

        public float X;
        public int Y;

        public int SizeX;
        public int SizeY;

        public Vector2 VelRange = new Vector2(.5f, .75f);

        public static Random ran = new Random();

        public List<float> Velocities = new List<float> { 3.5f, 4f, 4.5f, 5f};

        public Animal(int y, int n)
        {
            Num = n;

            X = 100;
            Y = y;
            SizeX = 75;
            SizeY = 75;

            VelChanged = false;
            VelChange = new Timer(ran.Next(150, 300));
            VelTransition = new Timer(60);

            UpdateVel();
            Finished = false;
        }

        public void ChangeVel()
        {
            if (VelTransition.Check())
            {
                VelChanged = false;
            }
            else
            {
                Vel += VelTransitionInterval;
                VelTransition.Update();
            }
        }

        public void UpdateVel()
        {
            NextVel = Velocities[ran.Next(0, Velocities.Count - 1)];
            VelChanged = true;
            VelChange = new Timer(ran.Next(150, 300));
            VelTransition.Reset();
            VelTransitionInterval = (NextVel - Vel) / VelTransition.Duration;
        }

        public void Update()
        {
            if (X + SizeX - Game1.ScrollLine > Game1.FinishLine && !Finished)
            {
                Finished = true;
                Place = Game1.places[0];
                Game1.places.RemoveAt(0);
            }

            if (VelChange.Check())
            {
                UpdateVel();
            }
            else if (VelChanged)
            {
                ChangeVel();
            }

            VelChange.Update();
            X += Vel;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Finished)
            {
                spriteBatch.Draw(Game1.placeImg[Place-1], new Rectangle(900, Y, 100, 75), Color.White);
            }

            if (Game1.paused)
            {
                Game1.horseAni[Num].destRec = new Rectangle(Convert.ToInt32(X), Y, SizeX, SizeY);
                Game1.horseAni[Num].Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
            else if (Game1.ScrollLine < 0)
            {
                //spriteBatch.Draw(Game1.blankRecImg, new Rectangle(Convert.ToInt32(X), Y, SizeX, SizeY), Color.White);
                Game1.horseAni[Num].Update(gameTime);
                Game1.horseAni[Num].destRec = new Rectangle(Convert.ToInt32(X), Y, SizeX, SizeY);
                Game1.horseAni[Num].Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
            else
            {
                //spriteBatch.Draw(Game1.blankRecImg, new Rectangle(Convert.ToInt32(X) - Convert.ToInt32(Game1.ScrollLine), Y, SizeX, SizeY), Color.White);
                Game1.horseAni[Num].Update(gameTime);
                Game1.horseAni[Num].destRec = new Rectangle(Convert.ToInt32(X) - Convert.ToInt32(Game1.ScrollLine), Y, SizeX, SizeY);
                Game1.horseAni[Num].Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
        }
    }
}
