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

namespace Racing_GANG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static bool paused;
        public Timer pauseTimer;

        private static List<Animal> animals = new List<Animal>();

        public static Texture2D blankRecImg;

        public static List<Texture2D> placeImg;

        public static float ScrollLine;

        public float maxVel;

        public static float FinishLine;

        public static List<int> places;

        public static SpriteFont bigFont;

        public static List<Button> betButtons;

        public Button startBtn;

        public int currentBet;

        public List<int> bets;

        public static List<Animation> horseAni;

        public static List<Texture2D> horseAniImg;

        public Texture2D bg;

        public ScrollingBg scrollBg;

        public ScreenFade screenFade;

        public bool inMenu;

        public List<Texture2D> pauseTime;

        public Texture2D finishLine;

        public List<Texture2D> buttons;

        public Texture2D placeBets;

        public Dictionary<string, Texture2D> plaques;

        public Texture2D betsImg;

        public Texture2D logo;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            IsMouseVisible = true;
            graphics.ApplyChanges();

            // TODO: use this.Content to load your game content here

            blankRecImg = Content.Load<Texture2D>("Assets/blankrec");
            bigFont = Content.Load<SpriteFont>("Assets/bigfont");

            plaques = new Dictionary<string, Texture2D> { { "123", Content.Load<Texture2D>("Assets/correct1st2nd3rd") },
            { "12", Content.Load<Texture2D>("Assets/correct1st2nd") },
            { "13", Content.Load<Texture2D>("Assets/correct1st3rd") },
            { "23", Content.Load<Texture2D>("Assets/correct2nd3rd") },
            { "1", Content.Load<Texture2D>("Assets/correct1st") },
            { "2", Content.Load<Texture2D>("Assets/correct2nd") },
            { "3", Content.Load<Texture2D>("Assets/correct3rd") }};

            betsImg = Content.Load<Texture2D>("Assets/bets");

            betButtons = new List<Button> { new Button(blankRecImg, new Rectangle(75, 300, 100, 100)),
                                            new Button(blankRecImg, new Rectangle(225, 300, 100, 100)),
                                            new Button(blankRecImg, new Rectangle(375, 300, 100, 100)),
                                            new Button(blankRecImg, new Rectangle(525, 300, 100, 100)),
                                            new Button(blankRecImg, new Rectangle(675, 300, 100, 100)),
                                            new Button(blankRecImg, new Rectangle(825, 300, 100, 100))};

            startBtn = new Button(blankRecImg, new Rectangle(450, 500, 150, 100));

            horseAniImg = new List<Texture2D> { Content.Load<Texture2D>("Assets/horse1ani"),
                                                Content.Load<Texture2D>("Assets/horse2ani"),
                                                Content.Load<Texture2D>("Assets/horse3ani"),
                                                Content.Load<Texture2D>("Assets/horse4ani"),
                                                Content.Load<Texture2D>("Assets/horse5ani"),
                                                Content.Load<Texture2D>("Assets/horse6ani")};

            placeImg = new List<Texture2D> { Content.Load<Texture2D>("Assets/1st"),
                                             Content.Load<Texture2D>("Assets/2nd"),
                                              Content.Load<Texture2D>("Assets/3rd"),
                                              Content.Load<Texture2D>("Assets/4th"),
                                              Content.Load<Texture2D>("Assets/5th"),
                                              Content.Load<Texture2D>("Assets/6th")};

            placeBets = Content.Load<Texture2D>("Assets/placeyourbets");

            logo = Content.Load<Texture2D>("Assets/logo");

            buttons = new List<Texture2D> { Content.Load<Texture2D>("Assets/bet"),
            Content.Load<Texture2D>("Assets/race"),
            Content.Load<Texture2D>("Assets/menu") }; 

            bg = Content.Load<Texture2D>("Assets/bg");

            horseAni = new List<Animation> { new Animation(horseAniImg[0], 4, 1, 4, 1, 2, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true),
                                             new Animation(horseAniImg[1], 4, 1, 4, 1, 2, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true),
                                             new Animation(horseAniImg[2], 4, 1, 4, 1, 2, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true),
                                             new Animation(horseAniImg[3], 4, 1, 4, 1, 2, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true),
                                             new Animation(horseAniImg[4], 4, 1, 4, 1, 2, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true),
                                             new Animation(horseAniImg[5], 4, 1, 4, 1, 2, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true)};

            scrollBg = new ScrollingBg(bg);

            screenFade = new ScreenFade();

            finishLine = Content.Load<Texture2D>("Assets/finish");

            inMenu = true;

            pauseTime = new List<Texture2D> { Content.Load<Texture2D>("Assets/1"),
                                              Content.Load<Texture2D>("Assets/2"),
                                              Content.Load<Texture2D>("Assets/3")};

            Reset();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);


            Globals.UpdateGlobals();

            if (screenFade.Active) screenFade.Update();

            if (Globals.Gamestate == Globals.MENU)
            {
                if (screenFade.Active && inMenu)
                {
                    Reset();
                }

                paused = false;
                if (startBtn.CheckButton() && !screenFade.Active)
                {
                    screenFade.Start(Globals.BET, 1f);
                    inMenu = false;
                }

                maxVel = 0;

                foreach (Animal animal in animals)
                {
                    animal.Update();
                    if (animal.Vel > maxVel)
                    {
                        maxVel = animal.Vel;
                    }
                }

                if (FinishLine >= 800)
                {
                    ScrollLine += maxVel;
                    FinishLine -= maxVel;
                    if (FinishLine <= 6200) scrollBg.Update(maxVel);
                }
                
            }
            else if (Globals.Gamestate == Globals.BET)
            {
                if (screenFade.Active && currentBet == 0)
                {
                    Reset();
                }

                for (int i = 0; i < betButtons.Count; i++)
                {
                    if (betButtons[i].CheckButton() && animals[i].Bet < 1 && currentBet < 3)
                    {
                        if (currentBet == 0) animals[i].Bet = 1;
                        else if (currentBet == 1) animals[i].Bet = 2;
                        else animals[i].Bet = 3;
                        currentBet++;
                        bets.Add(i);
                    }
                }

                if (currentBet >= 3)
                {
                    if (startBtn.CheckButton() && !screenFade.Active)
                    {
                        screenFade.Start(Globals.GAMEPLAY, 1f);
                    }
                }
            }
            else
            {
                if (!screenFade.Active)
                {
                    maxVel = 0;

                    if (places.Count == 0)
                    {
                        if (startBtn.CheckButton())
                        {
                            screenFade.Start(Globals.MENU, 1f);
                            inMenu = true;
                        }
                    }

                    if (paused)
                    {
                        if (pauseTimer.Check())
                        {
                            paused = false;
                        }
                        else pauseTimer.Update();
                    }
                    else
                    {
                        foreach (Animal animal in animals)
                        {
                            animal.Update();
                            if (animal.Vel > maxVel)
                            {
                                maxVel = animal.Vel;
                            }
                        }

                        if (FinishLine >= 800)
                        {
                            ScrollLine += maxVel;
                            FinishLine -= maxVel;
                            if (FinishLine <= 6200) scrollBg.Update(maxVel);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            if (Globals.Gamestate == Globals.MENU)
            {
                spriteBatch.Draw(bg, Globals.GetRec(), Color.White);
                scrollBg.Draw(spriteBatch);

                foreach (Animal animal in animals)
                {
                    animal.Draw(spriteBatch, gameTime);
                }

                startBtn.Img = buttons[0];
                startBtn.Draw(spriteBatch);

                spriteBatch.Draw(finishLine, new Rectangle(Convert.ToInt32(FinishLine), 145, 40, 520), Color.White);

                spriteBatch.Draw(logo, new Rectangle(225, 10, 600, 75), Color.White);
            }
            else if (Globals.Gamestate == Globals.BET)
            {
                spriteBatch.Draw(bg, Globals.GetRec(), Color.White);
                //spriteBatch.DrawString(bigFont, "PLACE YOUR BETS", Globals.CentreMiddle("PLACE YOUR BETS", bigFont, 50), Color.White);
                spriteBatch.Draw(placeBets, new Rectangle(300, 20, 450, 65), Color.White);
                if (currentBet == 0) spriteBatch.Draw(placeImg[0], new Rectangle(450, 100, 150, 100), Color.White);
                else if (currentBet == 1) spriteBatch.Draw(placeImg[1], new Rectangle(450, 100, 150, 100), Color.White);
                else if (currentBet == 2) spriteBatch.Draw(placeImg[2], new Rectangle(450, 100, 150, 100), Color.White);
                else
                {
                    startBtn.Img = buttons[1];
                    startBtn.Draw(spriteBatch);
                }

                for (int i = 0; i < betButtons.Count; i++)
                {
                    if (animals[i].Bet <= 0)
                    {
                        horseAni[i].Update(gameTime);
                        horseAni[i].destRec = betButtons[i].Rec;
                        horseAni[i].Draw(spriteBatch, Color.White, SpriteEffects.None);
                    }
                }
            }
            else
            {
                spriteBatch.Draw(bg, Globals.GetRec(), Color.White);
                scrollBg.Draw(spriteBatch);

                spriteBatch.Draw(finishLine, new Rectangle(Convert.ToInt32(FinishLine), 145, 40, 520), Color.White);

                

                if (paused)
                {
                    if (Math.Ceiling(pauseTimer.Time / 60.0) == 1)
                    {
                        spriteBatch.Draw(pauseTime[0], new Rectangle(475, 300, 100, 100), Color.White);
                    }
                    else if (Math.Ceiling(pauseTimer.Time/60.0) == 2)
                    {
                        spriteBatch.Draw(pauseTime[1], new Rectangle(475, 300, 100, 100), Color.White);
                    }
                    else if(Math.Ceiling(pauseTimer.Time / 60.0) == 3)
                    {
                        spriteBatch.Draw(pauseTime[2], new Rectangle(475, 300, 100, 100), Color.White);
                    }
                }

                if (places.Count == 0)
                {
                    startBtn.Img = buttons[2];
                    startBtn.Draw(spriteBatch);

                    if (animals[bets[0]].Place == 1 && animals[bets[1]].Place == 2 && animals[bets[2]].Place == 3)
                    {
                        spriteBatch.Draw(plaques["123"], new Rectangle(298, 88, 455, 325), Color.White);
                        //spriteBatch.DrawString(bigFont, "1st, 2nd, 3rd CORRECT", Globals.CentreMiddle("1st, 2nd, 3rd CORRECT", bigFont, 100), Color.White);
                        //spriteBatch.DrawString(bigFont, "PAYOUT: $250", Globals.CentreMiddle("PAYOUT: $250", bigFont, 150), Color.White);
                    }
                    if (animals[bets[0]].Place == 1 && animals[bets[1]].Place == 2 && animals[bets[2]].Place != 3)
                    {
                        spriteBatch.Draw(plaques["12"], new Rectangle(298, 88, 455, 325), Color.White);
                    }
                    if (animals[bets[0]].Place == 1 && animals[bets[1]].Place != 2 && animals[bets[2]].Place == 3)
                    {
                        spriteBatch.Draw(plaques["13"], new Rectangle(298, 88, 455, 325), Color.White);
                    }
                    if (animals[bets[0]].Place != 1 && animals[bets[1]].Place == 2 && animals[bets[2]].Place == 3)
                    {
                        spriteBatch.Draw(plaques["23"], new Rectangle(298, 88, 455, 325), Color.White);
                    }
                    if (animals[bets[0]].Place == 1 && animals[bets[1]].Place != 2 && animals[bets[2]].Place != 3)
                    {
                        spriteBatch.Draw(plaques["1"], new Rectangle(298, 88, 455, 325), Color.White);
                    }
                    if (animals[bets[0]].Place != 1 && animals[bets[1]].Place == 2 && animals[bets[2]].Place != 3)
                    {
                        spriteBatch.Draw(plaques["2"], new Rectangle(298,88, 455, 325), Color.White);
                    }
                    if (animals[bets[0]].Place != 1 && animals[bets[1]].Place != 2 && animals[bets[2]].Place == 3)
                    {
                        spriteBatch.Draw(plaques["3"], new Rectangle(298, 88, 455, 325), Color.White);
                    }
                }

                for (int i = 0; i < animals.Count; i++)
                {
                    animals[i].Draw(spriteBatch, gameTime);
                    if (animals[i].Bet == 1) spriteBatch.Draw(placeImg[0], new Rectangle(15, animals[i].Y, 100, 75), Color.White);
                    else if (animals[i].Bet == 2) spriteBatch.Draw(placeImg[1], new Rectangle(15, animals[i].Y, 100, 75), Color.White);
                    else if (animals[i].Bet == 3) spriteBatch.Draw(placeImg[2], new Rectangle(15, animals[i].Y, 100, 75), Color.White);
                }

                spriteBatch.Draw(betsImg, new Rectangle(15, 30, 100, 50), Color.White);
                
            }

            if (screenFade.Active)
            {
                screenFade.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Reset()
        {
            // Creates animals
            animals = new List<Animal>{new Animal(147, 0),
                                       new Animal(225, 1),
                                       new Animal(305, 2),
                                       new Animal(395, 3),
                                       new Animal(485, 4),
                                       new Animal(570, 5) };

            pauseTimer = new Timer(180);
            paused = true;
            maxVel = 0;

            currentBet = 0;
            bets = new List<int>();

            ScrollLine = -800;
            FinishLine = 5000;
            places = new List<int> { 1,2,3,4,5,6 };
        }
    }
}
