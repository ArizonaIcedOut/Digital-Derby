// AUthor: Eric Pu
// File Name: Globals.cs
// Project Name: RapidFire
// Creation Date: May 22th, 2019
// Modified Date: June 6th, 2019
// Description: Class for managing global properties and functions

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;
using MONO_TEST;

/// <summary>
/// Class for global variables.
/// </summary>
class Globals
{
    // RNG
    public static Random Rng = new Random();

    // List of the words used for typing games
    public static List<string> WordsList = new List<string>();

    // Keyboard and mouse states states for past and present
    public static KeyboardState KbPast = new KeyboardState();

    public static KeyboardState KbCurrent = Keyboard.GetState();

    public static MouseState MousePast = new MouseState();

    public static MouseState MouseCurrent = Mouse.GetState();

    public static int ScreenWidth = 1050;

    public static int ScreenHeight = 700;

    public const int GAMEPLAY = 0;

    public const int MENU = 1;

    public const int BET = 2;

    public static int Gamestate = MENU;

    /// <summary>
    /// Pre: n/a
    /// Post: n/a
    /// Description: Updates the keyboard and mouse states
    /// </summary>
    public static void UpdateGlobals()
    {
        KbPast = KbCurrent;
        KbCurrent = Keyboard.GetState();
        MousePast = MouseCurrent;
        MouseCurrent = Mouse.GetState();
    }

    /// <summary>
    /// Pre: original as the color being inverted
    /// Post: Returns the inverted color
    /// Description: Inverts a given color. Not used in this project, but it's cool
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public static Color InvertColor(Color original)
    {
        return new Color(255 - original.R, 255 - original.G, 255 - original.B);
    }

    /// <summary>
    /// Pre: key as key being checked
    /// Post: Returns if the key is being pressed or not
    /// Description: Checks if a given key is being pressed or not.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool CheckKey(Keys key)
    {
        if (KbCurrent.IsKeyDown(key) && !KbPast.IsKeyDown(key)) return true;
        else return false;
    }

    /// <summary>
    /// Pre: n/a
    /// Post: Returns a bool indicating if left click is being pressed
    /// Description: Checks if the player is currently left clicking
    /// </summary>
    /// <returns></returns>
    public static bool CheckLeftClick()
    {
        if (MouseCurrent.LeftButton == ButtonState.Pressed && MousePast.LeftButton != ButtonState.Pressed) return true;
        else return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rec"></param>
    /// <returns></returns>
    public static bool CheckMouseCollision(Rectangle rec)
    {
        if (rec.X + rec.Width >= MouseCurrent.X && rec.X <= MouseCurrent.X
            && rec.Y + rec.Height >= MouseCurrent.Y && rec.Y <= MouseCurrent.Y) return true;
        else return false;
    }

    /// <summary>
    /// Pre: n/a
    /// Post: Returns the dimensions of the screen as a rectangle
    /// Description: Gives dimensions of the screen.
    /// </summary>
    /// <returns></returns>
    public static Rectangle GetRec()
    {
        return new Rectangle(0, 0, ScreenWidth, ScreenHeight);
    }

    public static Vector2 StartAtMiddle(Vector2 size)
    {
        return new Vector2(ScreenWidth / 2 - size.X / 2, ScreenHeight - size.Y);
    }

    /// <summary>
    /// Pre: rec is a rectangle, text is the text being centred, font is the font used
    /// Post: Returns a Vector2 with centred X and Y value
    /// Description: Centres text given a rectangle, text, and the font.
    /// </summary>
    /// <param name="rec"></param>
    /// <param name="text"></param>
    /// <param name="font"></param>
    /// <returns></returns>
    public static Vector2 CentreText(Rectangle rec, string text, SpriteFont font)
    {
        return new Vector2(rec.X + (rec.Width - font.MeasureString(text).X) / 2, rec.Y + (rec.Height - font.MeasureString(text).Y) / 2);
    }

    public static Vector2 CentreMiddle(string text, SpriteFont font, int y)
    {
        return new Vector2((ScreenWidth - font.MeasureString(text).X) / 2, y);
    }

    /// <summary>
    /// Pre: list as an list of integers
    /// Post: n/a
    /// Description: Randomizes a list of integers
    /// </summary>
    /// <param name="list"></param>
    public static void ShuffleIntList(List<int> list)
    {
        // Sets leng to the length of the list
        int leng = list.Count;

        // If the length is greater than one, 
        while (leng > 1)
        {
            leng--;

            int k = Rng.Next(leng + 1);
            int value = list[k];

            list[k] = list[leng];
            list[leng] = value;
        }
    }

    /// <summary>
    /// Pre: n/a
    /// Post: n/a
    /// Description: Loads in the words used in typing-based games
    /// </summary>
    public static void LoadWords()
    {
        // Goes through each of the words in the words.txt file, and adds it to the list
        using (StreamReader sr = new StreamReader("words.txt"))
        {
            string s = "";

            while ((s = sr.ReadLine()) != null)
            {
                WordsList.Add(s);
            }
        }
    }

    /// <summary>
    /// Pre: n/a
    /// Post: Returns a string with a random word
    /// Description: Generates a random word, used for typing games
    /// </summary>
    /// <returns></returns>
    public static string GetWord()
    {
        return WordsList[Rng.Next(WordsList.Count)];
    }

    /// <summary>
    /// Pre: str as the string being reversed
    /// Post: Returns the reversed string
    /// Description: Reverses the inputted string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ReverseString(string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <summary>
    /// Pre: word as the word being checked
    /// Post: Returns a Keys value of the first letter of the word
    /// Description: Generates a Keys value of the first letter of a given word
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public static Keys GetFirstLetter(string word)
    {
        string letter = word.Substring(0, 1);
        letter = letter.ToUpper();
        return (Keys)Convert.ToChar(letter);
    }

    /// <summary>
    /// Pre: rec1 as the first rectangle, rec2 as the second rectangle
    /// Post: A bool representing if the rectangles collided or not
    /// Description: Checks collision between two rectangles
    /// </summary>
    /// <param name="rec1"></param>
    /// <param name="rec2"></param>
    /// <returns></returns>
    public static bool CheckCollision(Rectangle rec1, Rectangle rec2)
    {
        if (rec1.X + rec1.Width >= rec2.X && rec1.X <= rec2.X + rec2.Width && rec1.Y + rec1.Height >= rec2.Y && rec1.Y <= rec2.Y + rec2.Width) return true;
        else return false;
    }
}
