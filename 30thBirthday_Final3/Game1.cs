using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _30thBirthday_Final3
{
    enum Scenes
    {
        PRESS_SPACE,
        CANDLEGAME,
        ONECANDLE,
        TWOCANDLES,
        THREECANDLES,
        FOUR,
        FIVECANDLES,
        SIXCANDLES,
        SEVENCANDLESMORE,
        EIGHTCANDLES,
        NINECANDLES,
        TENCANDLESOUT,
        ELEVENCANDLES,
        TWELVECANDLES,
        THIRTEEN
    };
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Texture2D bg_ps;
        public Texture2D birthdaycandles;
        public Texture2D candlebackground;
        Texture2D[] flickertextures;
        private Scenes activeScene;
        public bool keyboardState;
        KeyboardState spaceUnpressed;
        int activeFrame;
        int counter;
        public int sceneNumber;
        public Texture2D[] background;
        public Texture2D thecreature, ageText, birthdayText, addonText, spaceText, theCake;
        public Vector2 cakePosition;
        public Vector2 cakeOrigin;
        public float rotationAngle;
        public Song cakeSpin;
        public SoundEffect candleBlow;
        public SoundEffect knocking;
        public Song happybirthday;
        public Song violins;
        public SpriteFont theEnd;
        public Vector2 theEndPOS;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            activeScene = Scenes.PRESS_SPACE;
            sceneNumber = 0;
            keyboardState = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;         // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 1040;
            _graphics.ApplyChanges();
            base.Initialize();
            spaceUnpressed = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bg_ps = Content.Load<Texture2D>("press_space_screen");
            birthdaycandles = Content.Load<Texture2D>("birthdaycandles_bw");
            Texture2D texture1 = Content.Load<Texture2D>("flame1");
            candle1 c1;
            c1 = new candle1(texture1, Vector2.Zero);
            flickertextures = new Texture2D[3];
            flickertextures[0] = Content.Load<Texture2D>("flame2_1");
            flickertextures[1] = Content.Load<Texture2D>("flame2_2");
            flickertextures[2] = Content.Load<Texture2D>("flame2_3");
            candlebackground = Content.Load<Texture2D>("30thbirthday_bg");
            thecreature = Content.Load<Texture2D>("thecreatureface");
            background = new Texture2D[13];
            background[0] = Content.Load<Texture2D>("30thbirthday_bg");
            background[1] = Content.Load<Texture2D>("30thbirthday_bg1");
            background[2] = Content.Load<Texture2D>("30thbirthday_bg2c");
            background[3] = Content.Load<Texture2D>("30thbirthday_bg3c");
            background[4] = Content.Load<Texture2D>("30thbirthday_bg4c");
            background[5] = Content.Load<Texture2D>("30thbirthday_bg5c");
            background[6] = Content.Load<Texture2D>("30thbirthday_bg6c");
            background[7] = Content.Load<Texture2D>("30thbirthday_bg7c");
            background[8] = Content.Load<Texture2D>("30thbirthday_bg8c");
            background[9] = Content.Load<Texture2D>("30thbirthday_bg9c");
            background[10] = Content.Load<Texture2D>("30thbirthday_bg10c");
            background[11] = Content.Load<Texture2D>("30thbirthday_bg11c");
            background[12] = Content.Load<Texture2D>("30thbirthday_bg12c");
            ageText = Content.Load<Texture2D>("30th_txt");
            cakeSpin = Content.Load<Song>("Media/02. Title Screen");
            knocking = Content.Load<SoundEffect>("Media/knocking_effect");
            happybirthday = Content.Load<Song>("Media/happybirthday_girl");
            violins = Content.Load<Song>("Media/violin_mp3");
            theEnd = Content.Load<SpriteFont>("theEnd");

            MediaPlayer.Play(cakeSpin);
            candleBlow = Content.Load<SoundEffect>("Media/blowsound_wav");
            theCake = Content.Load<Texture2D>("cake_forspinning");
            Viewport viewport = _graphics.GraphicsDevice.Viewport;

            // Set the Texture origin to be the center of the texture.
            cakeOrigin.X = theCake.Width / 2;
            cakeOrigin.Y = theCake.Height / 2;

            // Set the position of the texture to be the center of the screen.
            cakePosition.X = viewport.Width / 2;
            cakePosition.Y = viewport.Height / 2;

            // Set the position of THE END text

            theEndPOS = new Vector2(200, 150);

            birthdayText = Content.Load<Texture2D>("birthday_txt");
            addonText = Content.Load<Texture2D>("press_space_txt_addon");
            spaceText = Content.Load<Texture2D>("press_space_txt");
            // TODO: use this.Content to load your game content here


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Rotation of sprite??

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds*15;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle %= circle;

            KeyboardState spacedPressed = Keyboard.GetState();



            counter++;                  // Counter for animated flames of candles
            if (counter > 29)
            {
                counter = 0;
                activeFrame++;

                if (activeFrame > flickertextures.Length - 1)
                {
                    activeFrame = 0;
                }
                KeyboardState spacePressed = Keyboard.GetState();
            }
            switch (activeScene)
                {
                    case Scenes.PRESS_SPACE:

                    {
                        if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                        {
                            activeScene = Scenes.CANDLEGAME;
                            MediaPlayer.Play(happybirthday);
                            MediaPlayer.IsRepeating = true;
                        }
                    }
                            break;

                    case Scenes.CANDLEGAME:
                    {
                         if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                                    {

                                        activeScene = Scenes.ONECANDLE;
                                        candleBlow.Play();
                                    }
                        break;
                    }
                    case Scenes.ONECANDLE:
                    {
                        if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                        {
                            activeScene = Scenes.TWOCANDLES;
                            candleBlow.Play();
                        }
                        break;
                    }


                    case Scenes.TWOCANDLES:
                    {
                        if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                        {
                            activeScene = Scenes.THREECANDLES;
                            candleBlow.Play();
                        }
                        break;
                    }

                    case Scenes.THREECANDLES:
                    {
                        if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                        {
                            activeScene = Scenes.FOUR;
                            candleBlow.Play();
                        }
                        break;
                    }
                case Scenes.FOUR:

                    {
                        if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                        {
                            activeScene = Scenes.FIVECANDLES;
                            candleBlow.Play();
                        }
                        break;
                    }
                case Scenes.FIVECANDLES:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.SIXCANDLES;
                            candleBlow.Play();
                    }

                    break;
                    case Scenes.SIXCANDLES:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.SEVENCANDLESMORE;
                            candleBlow.Play();

                    }

                    break;
                    case Scenes.SEVENCANDLESMORE:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.EIGHTCANDLES;
                            candleBlow.Play();
                            MediaPlayer.Stop();
                    }

                    break;
                    case Scenes.EIGHTCANDLES:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.NINECANDLES;
                            knocking.Play();
                            candleBlow.Play();
                    }

                    break;
                    case Scenes.NINECANDLES:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.TENCANDLESOUT;
                            candleBlow.Play();
                        }

                    break;
                    case Scenes.TENCANDLESOUT:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.ELEVENCANDLES;
                            candleBlow.Play();


                    }
                    break;
                    case Scenes.ELEVENCANDLES:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.TWELVECANDLES;
                            candleBlow.Play();
                    }

                    break;
                    case Scenes.TWELVECANDLES:

                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                            activeScene = Scenes.THIRTEEN;
                            MediaPlayer.Play(violins);
                    }

                    break;
                    case Scenes.THIRTEEN:
                    if (spacedPressed.IsKeyDown(Keys.Space) && !spaceUnpressed.IsKeyDown(Keys.Space))
                    {
                        activeScene = Scenes.PRESS_SPACE;

                    }
                    break;
            }
            spaceUnpressed = spacedPressed;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

                switch (activeScene)
                {

                    case Scenes.PRESS_SPACE:


                        _spriteBatch.Begin();
                        _spriteBatch.Draw(bg_ps, new Rectangle(0, 0, 1280, 1040), Color.White);   // Draws the background for the main screen
                        _spriteBatch.Draw(birthdayText, new Rectangle(120, 260, 1133, 136), Color.White);
                        _spriteBatch.Draw(ageText, new Rectangle(30, 80, 804, 162), Color.White);
                        _spriteBatch.Draw(addonText, new Rectangle(80, 850, 1088, 54), Color.White);
                        _spriteBatch.Draw(spaceText, new Rectangle(140, 850, 974, 52), Color.White);
                        _spriteBatch.Draw(theCake, new Vector2(600, 600), null, Color.White, rotationAngle, cakeOrigin, 0.8f, SpriteEffects.None, 1f);
                        _spriteBatch.End();

                        break;

                    case Scenes.CANDLEGAME:

                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[1], new Rectangle(0, 0, 1280, 1040), Color.White);
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(600, 568, 16, 37), Color.White); // Flame on H
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(640, 560, 16, 37), Color.White); // Flame on A
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(680, 559, 16, 37), Color.White); // Flame on P
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(735, 563, 16, 37), Color.White); // Flame on P
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(791, 568, 16, 37), Color.White); // Flame on Y

                        // Now we write BIRTHDAY
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(543, 616, 16, 37), Color.White); // Flame on B
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(567, 633, 16, 37), Color.White); // Flame on I
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y// draws the background
                        _spriteBatch.End();

                        break;

                    case Scenes.ONECANDLE:

                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[1], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(640, 560, 16, 37), Color.White); // Flame on A
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(680, 559, 16, 37), Color.White); // Flame on P
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(735, 563, 16, 37), Color.White); // Flame on P
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(791, 568, 16, 37), Color.White); // Flame on Y

                        // Now we write BIRTHDAY
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(543, 616, 16, 37), Color.White); // Flame on B
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(567, 633, 16, 37), Color.White); // Flame on I
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y// draws the background

                        _spriteBatch.End();
                        break;

                    case Scenes.TWOCANDLES:

                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[2], new Rectangle(0, 0, 1280, 1040), Color.White);
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(680, 559, 16, 37), Color.White); // Flame on P
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(735, 563, 16, 37), Color.White); // Flame on P
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(791, 568, 16, 37), Color.White); // Flame on Y

                        // Now we write BIRTHDAY
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(543, 616, 16, 37), Color.White); // Flame on B
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(567, 633, 16, 37), Color.White); // Flame on I
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//
                        _spriteBatch.End();
                        break;

                    case Scenes.THREECANDLES:

                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[3], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    _spriteBatch.Draw(background[2], new Rectangle(0, 0, 1280, 1040), Color.White);
                    _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(735, 563, 16, 37), Color.White); // Flame on P
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(791, 568, 16, 37), Color.White); // Flame on Y

                    // Now we write BIRTHDAY
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(543, 616, 16, 37), Color.White); // Flame on B
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(567, 633, 16, 37), Color.White); // Flame on I
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//
                    _spriteBatch.End();
                        break;

                    case Scenes.FOUR:

                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[4], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(791, 568, 16, 37), Color.White); // Flame on Y

                    // Now we write BIRTHDAY
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(543, 616, 16, 37), Color.White); // Flame on B
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(567, 633, 16, 37), Color.White); // Flame on I
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//
                    _spriteBatch.End();
                        break;

                    case Scenes.FIVECANDLES:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[5], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    // Now we write BIRTHDAY
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(543, 616, 16, 37), Color.White); // Flame on B
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(567, 633, 16, 37), Color.White); // Flame on I
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                    _spriteBatch.End();
                        break;

                    case Scenes.SIXCANDLES:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[6], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);

                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(567, 633, 16, 37), Color.White); // Flame on I
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                    // The CREATURE APPEARS //

                    _spriteBatch.Draw(thecreature, new Rectangle(10, 350, 249, 268), Color.White * 0.1f);
                    _spriteBatch.End();
                        break;
                     
                    case Scenes.SEVENCANDLESMORE:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[7], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(609, 639, 16, 37), Color.White); // Flame on R
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                    _spriteBatch.Draw(thecreature, new Rectangle(10, 350, 249, 268), Color.White * 0.1f);
                    _spriteBatch.End();
                        break;

                    case Scenes.EIGHTCANDLES:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[8], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(654, 640, 16, 37), Color.White); // Flame on T
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                    _spriteBatch.Draw(thecreature, new Rectangle(10, 350, 249, 268), Color.White * 0.2f);
                    _spriteBatch.End();
                        break;

                    case Scenes.NINECANDLES:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[9], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);

                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(689, 642, 16, 37), Color.White); // Flame on H
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                    _spriteBatch.Draw(thecreature, new Rectangle(10, 350, 249, 268), Color.White * 0.2f);
                    _spriteBatch.End();
                        break;

                    case Scenes.TENCANDLESOUT:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[10], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(744, 635, 16, 37), Color.White); // Flame on D
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                    _spriteBatch.Draw(thecreature, new Rectangle(10, 350, 249, 268), Color.White * 0.3f);
                    _spriteBatch.End();
                        break;

                    case Scenes.ELEVENCANDLES:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[11], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(793, 625, 16, 37), Color.White); // Flame on A
                    _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                    _spriteBatch.Draw(thecreature, new Rectangle(10, 350, 249, 268), Color.White * 0.3f);
                    _spriteBatch.End();
                        break;

                    case Scenes.TWELVECANDLES:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(background[12], new Rectangle(0, 0, 1280, 1040), Color.White);               // draws the background
                        _spriteBatch.Draw(birthdaycandles, new Rectangle(535, 590, 324, 123), Color.White);
                        _spriteBatch.Draw(flickertextures[activeFrame], new Rectangle(827, 610, 16, 37), Color.White); // Flame on Y//

                        _spriteBatch.Draw(thecreature, new Rectangle(10, 350, 249, 268), Color.White * 0.3f);
                        _spriteBatch.End();
                        break;

                    case Scenes.THIRTEEN:
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(thecreature, new Rectangle(160, 205, 1280, 1040), Color.White);
                        string output = "The End.";
                        Vector2 FontOrigin = theEnd.MeasureString(output) / 2;
                        _spriteBatch.DrawString(theEnd, output, theEndPOS, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                        _spriteBatch.End();
                        break;
                    }

                        base.Draw(gameTime);
                    }
                }
            }

