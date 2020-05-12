// AUthor: Eric Pu
// File Name: ScreenFade.cs
// Project Name: RapidFire
// Creation Date: May 29th, 2019
// Modified Date: June 1st, 2019
// Description: Class for managing fading screen effects

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;
using MONO_TEST;
using Racing_GANG;

namespace MONO_TEST
{
    /// <summary>
    /// Class for fading screen logic.
    /// </summary>
    public class ScreenFade
    {
        // Properties of the fade effect
        public bool Active;
        public int NextState;
        public float Opacity;
        public bool WonGameed;
        public float Duration;

        public ScreenFade()
        {
            Active = false;
        }

        /// <summary>
        /// Pre: nextState as the gamestate being switched to, duration as the length of the fade
        /// Post: n/a
        /// Description: Begins a new fade effect
        /// </summary>
        /// <param name="nextState"></param>
        /// <param name="duration"></param>
        public void Start(int nextState, float duration)
        {
            // Resets all properties
            NextState = nextState;
            Opacity = 0f;
            Duration = duration;
            WonGameed = false;
            Active = true;
        }

        public void Update()
        {
            // If the gamestate has not WonGameed yet, the screen becomes darker. If it has WonGameed, it will become lighter instead.
            if (!WonGameed)
            {
                Opacity += 1 / (60 * Duration);

                // If the opacity has reached its maximum of 1, the gamestate will WonGame
                if (Opacity >= 1f)
                {
                    Globals.Gamestate = NextState;
                    WonGameed = true;
                }
            }
            else
            {
                Opacity -= 1 / (60 * Duration);

                // If the opacity reaches its minimum of 0, the fade ends
                if (Opacity <= 0f)
                {
                    Active = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws the rectangle of the fade
            spriteBatch.Draw(Game1.blankRecImg, Globals.GetRec(), Color.Black * Opacity);
        }
    }
}
