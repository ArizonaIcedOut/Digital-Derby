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
    public class ScrollingBg
    {
        public Texture2D Image;
        public float X1;
        public float X2;

        public ScrollingBg(Texture2D img)
        {
            Image = img;

            X1 = 0;
            X2 = Globals.ScreenWidth;
        }

        public void Update(float vel)
        {
            X1 -= vel;
            X2 -= vel;

            if (X1 - vel < 0 - Globals.ScreenWidth) X1 = Globals.ScreenWidth;
            if (X2 - vel < 0 - Globals.ScreenWidth) X2 = Globals.ScreenWidth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, new Rectangle(Convert.ToInt32(X1), 0, Globals.ScreenWidth, Globals.ScreenHeight), Color.White);
            spriteBatch.Draw(Image, new Rectangle(Convert.ToInt32(X2), 0, Globals.ScreenWidth, Globals.ScreenHeight), Color.White);
        }
    }
}
