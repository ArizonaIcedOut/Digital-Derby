// AUthor: Eric Pu
// File Name: Button.cs
// Project Name: RapidFire
// Creation Date: May 29th, 2019
// Modified Date: May 29th, 2019
// Description: Class for buttons 

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
    public class Button
    {
        public Texture2D Img;

        public Rectangle Rec;

        public Button(Texture2D img, Rectangle rec)
        {
            Img = img;
            Rec = rec;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: bool indicating if the button was clicked on or not
        /// Description: Checks if the button was pressed by the player
        /// </summary>
        /// <returns></returns>
        public bool CheckButton()
        {
            if (Globals.CheckLeftClick() && Globals.CheckMouseCollision(Rec))
            {
                return true;
            }
            else return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, Rec, Color.White);
        }
    }
}
