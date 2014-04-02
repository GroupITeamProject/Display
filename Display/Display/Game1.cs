using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Display
{
    // This is the class called from Program.cs 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D myTexture;



        //A font class will hold the XML of the font to be printed.
        //SpriteFont font;
        int rnum;
        //A Dictionary takes in a string of words from a file
        Dictionary<string, int> Words = new Dictionary<string, int>();

        //A Dictionary to hold Strings and Rectangles
        Dictionary<string, Rectangle> RectangleDictionary = new Dictionary<string, Rectangle>();

        //A dictionary to Hold the colors for the strings
        Dictionary<string, Color> ColorsDictionary = new Dictionary<string, Color>();
        Dictionary<string, SpriteFont> FontDictionary = new Dictionary<string, SpriteFont>();

        //An int to move words left in the draw method
        int totheleft = 10;

        MouseState myMouse;

        String MovingRec = "";



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
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Random rnd = new Random();
            rnum = rnd.Next(0, 15);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Assigning x to be the XML file Myfont
            //font = Content.Load<SpriteFont>("MyFont");

            myTexture = new Texture2D(GraphicsDevice, 1, 1);
            myTexture.SetData(new Color[] { Color.Tomato });

            // new WebScrap("http://en.wikipedia.org/wiki/Ireland");

            FileAnalyser FA = new FileAnalyser("webOutput.txt");

            Words = FA.getDictionary();

            int rows = 0;
            int cols = 100;

            int wordlimit = 15;
            int wordlimitcurrent = 0;

            foreach (var word in Words)
            {

                if (wordlimit <= wordlimitcurrent)
                {
                    break;
                }

                String recName = word.Key;
                SpriteFont myfont = Content.Load<SpriteFont>("MyFont");
                
                FontDictionary.Add(word.Key, myfont);

                Vector2 StringSize = new Vector2(30, 20);
                foreach (var font in FontDictionary)
                {
                    StringSize = font.Value.MeasureString(recName);
                }

                if (RectangleDictionary.Count == 0)
                {

                    RectangleDictionary.Add(recName,
                        new Rectangle(this.GraphicsDevice.Viewport.Height / 2,
                                        this.GraphicsDevice.Viewport.Width / 2,
                                    (int)StringSize.X,
                                    (int)StringSize.Y));

                    ColorsDictionary.Add(recName, Color.White);
                }
                else
                {

                    RectangleDictionary.Add(recName, new Rectangle(cols, rows, (int)StringSize.X, (int)StringSize.Y));
                    ColorsDictionary.Add(recName, Color.White);
                }

                if (RectangleDictionary.Count == 10)
                {
                    cols += (int)StringSize.X + 100;
                    rows = 0;
                }

                if (RectangleDictionary.Count == 20)
                {
                    cols += (int)StringSize.X + 100;
                    rows = 0;
                }

                if (RectangleDictionary.Count == 30)
                {
                    cols += (int)StringSize.X + 100;
                    rows = 0;
                }

                rows += (int)StringSize.Y + 10;

                wordlimitcurrent++;

            }


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //In here we would unload the files when the back button is pressed.
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit by pressing 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))

                this.Exit();

            myMouse = Mouse.GetState();


            foreach (var rec in RectangleDictionary)
            {
    
                    if (rec.Value.Contains(myMouse.X, myMouse.Y))
                    {
                        ColorsDictionary[rec.Key] = Color.Green;
                    }
                    else
                    {
                        ColorsDictionary[rec.Key] = Color.White;
                    }

                    if (myMouse.LeftButton == ButtonState.Pressed && rec.Value.Contains(myMouse.X, myMouse.Y))
                    {
                        MovingRec = rec.Key;
                    }

                    if (myMouse.LeftButton == ButtonState.Released && MovingRec != "")
                    {
                        if (rec.Key == MovingRec)
                        {
                            String temp = rec.Key;
                            RectangleDictionary.Remove(rec.Key);
                            break;
                        }
                    }
                    //int mousecount = 0;
                    if (myMouse.RightButton == ButtonState.Pressed && rec.Value.Contains(myMouse.X, myMouse.Y))
                    {

                        Vector2 strsiz = new Vector2();
                        //int test = 0;
                        foreach (var font in FontDictionary)
                        {
                            if (rec.Key == font.Key) 
                            {
                                FontDictionary[font.Key] = Content.Load<SpriteFont>("MyFont2");
                                break;
                            }

                            strsiz = FontDictionary[rec.Key].MeasureString(rec.Key);
                            
                            
                        }
                        
                      
                        /*
                        foreach (var font in FontDictionary)
                        {
                            strsiz = font.Value.MeasureString(rec.Key);
                        }
                        */
                        int ox = rec.Value.X;
                        int oy = rec.Value.Y;
                        int newrx = (int)strsiz.X;
                        int newry = (int)strsiz.Y;
                        RectangleDictionary[rec.Key] = new Rectangle(ox, oy, newrx, newry);
                        break;
                    }

            }



            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            /*spriteBatch.DrawString(font, this.GraphicsDevice.Viewport.Height.ToString(),
                                    new Vector2(400, 400), Color.Red);
            spriteBatch.DrawString(font, this.GraphicsDevice.Viewport.Width.ToString(),
                                    new Vector2(400, 410), Color.Red);
             */

            int i = 0;

            int count = 0;

            foreach (var rec in RectangleDictionary)
            {
                Vector2 V = new Vector2(rec.Value.X, rec.Value.Y);
                Vector2 strorigin = new Vector2();
                Vector2 recorigin = new Vector2();
                Vector2 strsiz = new Vector2();
               /* foreach (var font in FontDictionary)
                {
                    strsiz = FontDictionary[rec.Key].MeasureString(rec.Key);
                }*/
                float recangle = 0.0f;
                float strangle = 0.0f;
                int l = rec.Value.Left;
                int b = rec.Value.Bottom;
               /* if (rnum == count)
                {
                    recangle = 0.0f;
                    recorigin = new Vector2();

                    strangle = (float)Math.PI;
                    strorigin = new Vector2((int)strsiz.X, (int)strsiz.Y);
                    

                }*/
                spriteBatch.Draw(myTexture, rec.Value, null, Color.Black, recangle, recorigin, SpriteEffects.None, 0.0f);
               
                    spriteBatch.DrawString(FontDictionary[rec.Key], rec.Key, V, ColorsDictionary[rec.Key],
                        strangle, strorigin, 1.0f, SpriteEffects.None, 0.0f);
                

                //spriteBatch.Draw(myTexture, rec.Value, Color.Black);
                count++;
                //i = i + 10;
            }

            totheleft++;

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
