using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Display
{
    public enum GameState//gamestates
    {
        Menu,
        Game
    }

    // This is the class called from Program.cs 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //new GraphicsDeviceManager called graphics
        GraphicsDeviceManager graphics;
        float timer; 
        //new SpriteBatch called spriteBatc
        SpriteBatch spriteBatch;

        // new Texture2D called myTexture
        Texture2D myTexture;

        SoundEffect soundEffect;
        SoundEffectInstance soundEffectInstance;

        //int called rnum
        int rnum;
        

        //A Dictionary takes in a string of words from a file
        Dictionary<string, int> Words = new Dictionary<string, int>();

        //A Dictionary to hold Strings and Rectangles
        Dictionary<string, Rectangle> RectangleDictionary = new Dictionary<string, Rectangle>();

        //A Dictionary to Hold the colors for the strings
        Dictionary<string, Color> ColorsDictionary = new Dictionary<string, Color>();

        //A Dictionary to Hold the Fonts for the strings
        Dictionary<string, SpriteFont> FontDictionary = new Dictionary<string, SpriteFont>();

        Dictionary<string, Texture2D> SpriteDictionary = new Dictionary<string, Texture2D>();
        GameState gameState = new GameState();
        //Texture2D

        Texture2D backdraw;
        Texture2D daybut;
        Texture2D nightbut;
        Texture2D ssbut;
        Texture2D musicbut;
        Texture2D mutebut;
        Texture2D homebut;
      
        Rectangle dayrec;
        Rectangle nightrec;
        Rectangle ssrec;
        Rectangle musicrec;
        Rectangle muterec;
        Rectangle homerec;

        bool skyline = false;
        bool castle = false;
        bool mountain = false;

        Color WordCountCol;

        //An int to move words left in the draw method
        int totheleft = 10;


      

        //initialze of rectangles to make up menu
        Rectangle menuCount;
        Rectangle menuColor;
        Rectangle menuFont;
        Rectangle menuRemove;

        //SpritFont called MenuFont for menu
        SpriteFont menufont;
        String menuCountstr = "";
        String menuColorstr = "";
        String menuFontstr = "";
        String menuRemovestr = "";
        String WordCountstr = "";

        //int that counts how many times change color is presssed
        int countcol = 1;
        int butpcount = 1;
        int musicbutpcount = 1;

        //constructer for Game1
        public Game1()
        {
            //graphics equal new GraphicsDevice manager
            graphics = new GraphicsDeviceManager(this);
            //allows content to be loaded such as jpg, spritefont etc.
            Content.RootDirectory = "Content";
        }

        //initialize method
        protected override void Initialize()
        {
            gameState = GameState.Menu;
            //Sets mouse to be visable
            IsMouseVisible = true;
            soundEffect = Content.Load<SoundEffect>("Bass");//load soundeffect Bass.wav
            soundEffectInstance = soundEffect.CreateInstance();
            base.Initialize();

        }


        protected override void LoadContent()
        {
            
            //new randon rnd
            Random rnd = new Random();
            //rnum set to random number between 0 and 15
            rnum = rnd.Next(0, 15);

            //menufont loaded as Menufont.spritefont from the content of the project
            menufont = Content.Load<SpriteFont>("MenuFont");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //mytexture is equal to new  new texture2d of width and height of 1
            myTexture = new Texture2D(GraphicsDevice, 1, 1);

            //set color of mytexture
            myTexture.SetData(new Color[] { Color.White });

            //gets url you pass in
            // new WebScrap("http://en.wikipedia.org/wiki/Ireland");

            //new fileanalyer FA takes in input from webOutput.txt
            FileAnalyser FA = new FileAnalyser("webOutput.txt");

            //Dictionary words gets dictionary from the FileAnalyser
            Words = FA.getDictionary();

            //new ints rows and cols set to 0 and 100 respectivly
            int rows = 0;
            int cols = 100;
            //wordlimit setting how many words can be on the screen
            int wordlimit = 15;
            int ranback = rnd.Next(1, 4);
            //wordlimitcurrent used to check if you have reach word limit
            int wordlimitcurrent = 0;
            if (ranback == 3)
            {
                backdraw = Content.Load<Texture2D>("dayCastle1");
                castle = true;
            }
            else if (ranback == 2)
            {
                backdraw = Content.Load<Texture2D>("dayMountain");
                mountain = true;
            }
            else if (ranback == 1)
            {
                backdraw = Content.Load<Texture2D>("daySkyline1");
                skyline = true;
            }


            nightbut = Content.Load<Texture2D>("moonbtn");
            daybut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
            dayrec = new Rectangle(750, 390, nightbut.Bounds.Height, nightbut.Bounds.Width);
         
            nightrec = new Rectangle(750, 430, nightbut.Bounds.Height, nightbut.Bounds.Width);

            WordCountCol = Color.White;

            ssbut = Content.Load<Texture2D>("cam");
            ssrec = new Rectangle(700, 430, ssbut.Bounds.Height, ssbut.Bounds.Width);

            homebut = Content.Load<Texture2D>("home");
            homerec = new Rectangle(10, 430, homebut.Bounds.Height, homebut.Bounds.Width);

            musicbut = Content.Load<Texture2D>("Music");
            musicrec = new Rectangle(60, 430, musicbut.Bounds.Height, musicbut.Bounds.Width);

            mutebut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
            muterec = new Rectangle(120, 430, musicbut.Bounds.Height, musicbut.Bounds.Width);
            
            //foreach used to go through words dictionary
            foreach (var word in Words)
            {
                //if you reach the wordlimit the dictionary will stop reading in words
                if (wordlimit <= wordlimitcurrent)
                {
                    break;
                }
                //rec name is equal to word.key which is the current word
                String recName = word.Key;
                //myfont loaded as MyFont.spritefont from the content of the project
                SpriteFont myfont = Content.Load<SpriteFont>("MyFont");
                //mycolor is a color which will be used to color the text
                Color mycolor = Color.Black;

                //adds to the font dictionary a new value with the current word and myfont
                FontDictionary.Add(word.Key, myfont);
                //adds to the color dictionary a new value with the current word and mycolor
                ColorsDictionary.Add(word.Key, mycolor);
                //   SpriteDictionary.Add(word.Key, backdraw);

                //Vector2 called StringSize initialized with x and y of 30 and 20
                Vector2 StringSize = new Vector2(30, 20);
                //foreach used to work through font dictionary
                foreach (var font in FontDictionary)
                {
                    //each font with the font key the same as the word key is mesured and put into stringsize
                    StringSize = font.Value.MeasureString(recName);
                }

                //if there is nothing in the rectangle dictionary is 0
                if (RectangleDictionary.Count == 0)
                {
                    //add to the rectangle dictionary with a recname as key an  new rectangle with x and y of half width and height of screen
                    //and height and width of the Stringsize
                    RectangleDictionary.Add(recName,
                        new Rectangle(this.GraphicsDevice.Viewport.Height / 2,
                                        this.GraphicsDevice.Viewport.Width / 2,
                                    (int)StringSize.X,
                                    (int)StringSize.Y));
                }
                //if rectangles are already in dictionary
                else
                {
                    //add to the rectangle dictionary with a recname as key an  new rectangle with x and y of cols and rows
                    //and height and width of the Stringsize
                    RectangleDictionary.Add(recName, new Rectangle(cols, rows, (int)StringSize.X, (int)StringSize.Y));
                   
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
                //wordlimitcurrent adds one to itself every time it loops
                wordlimitcurrent++;

            }


        }


        protected override void UnloadContent()
        {

        }

       

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit by pressing 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))

                this.Exit();

            //new MouseState mymouse
            MouseState myMouse = Mouse.GetState();
            //new keyboardstate state
            KeyboardState state = Keyboard.GetState();
           // soundEffectInstance.Play();
            if (gameState == GameState.Menu)//if gamestate equals menu
            {
                if (state.IsKeyDown(Keys.Enter))//if enter is pressed
                {
                    gameState = GameState.Game;//gamestate equals game
                    soundEffectInstance.Play();
                }

            }
            else if (gameState == GameState.Game)
            {
               // soundEffectInstance.Play();
                if (myMouse.LeftButton == ButtonState.Pressed && homerec.Contains(myMouse.X, myMouse.Y))
                {
                    gameState = GameState.Menu;
                    soundEffectInstance.Stop();
                }
               /* if (myMouse.LeftButton == ButtonState.Pressed && musicrec.Contains(myMouse.X, myMouse.Y))
                {
                    if (musicbutpcount == 1)
                    {
                        musicbut = Content.Load<Texture2D>("mute");
                        soundEffectInstance.Stop();
                        musicbutpcount = 0;
                    }
                    if (musicbutpcount == 0)
                    {
                        musicbut = Content.Load<Texture2D>("Music");
                        soundEffectInstance.Play();
                        musicbutpcount++;
                    }

                }*/
                if (myMouse.LeftButton == ButtonState.Pressed && muterec.Contains(myMouse.X, myMouse.Y))
                {
                    if (musicbutpcount == 0)
                    {
                        soundEffectInstance.Resume();
                        musicbut = Content.Load<Texture2D>("music");
                        mutebut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                        musicbutpcount++;
                    }

                }
                if (myMouse.LeftButton == ButtonState.Pressed && musicrec.Contains(myMouse.X, myMouse.Y))
                {
                    if (musicbutpcount == 1)
                    {
                        soundEffectInstance.Pause();
                        musicbut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                        mutebut = Content.Load<Texture2D>("mute");
                        musicbutpcount = 0;
                    }
                }

                if (myMouse.LeftButton == ButtonState.Pressed && dayrec.Contains(myMouse.X, myMouse.Y))
                {
                    if (skyline == true)
                    {
                        if (butpcount == 0)
                        {
                            backdraw = Content.Load<Texture2D>("daySkyline1");
                            nightbut = Content.Load<Texture2D>("moonbtn");
                            daybut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                            WordCountCol = Color.White;
                            butpcount++;
                        }

                    }
                    if (castle == true)
                    {
                        if (butpcount == 0)
                        {
                            backdraw = Content.Load<Texture2D>("dayCastle1");
                            nightbut = Content.Load<Texture2D>("moonbtn");
                            daybut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                            WordCountCol = Color.White;
                            butpcount++;
                        }

                    }
                    if (mountain == true)
                    {
                        if (butpcount == 0)
                        {
                            backdraw = Content.Load<Texture2D>("dayMountain");
                            nightbut = Content.Load<Texture2D>("moonbtn");
                            daybut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                            WordCountCol = Color.White;
                            butpcount++;
                        }

                    }
                }
                if (myMouse.LeftButton == ButtonState.Pressed && nightrec.Contains(myMouse.X, myMouse.Y))
                {
                    if (skyline == true)
                    {
                        if (butpcount == 1)
                        {
                            backdraw = Content.Load<Texture2D>("nightSkyline");
                            daybut = Content.Load<Texture2D>("sunbtn");
                            nightbut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                            WordCountCol = Color.White;
                            butpcount = 0;

                        }
                    }
                    if (castle == true)
                    {
                        if (butpcount == 1)
                        {
                            backdraw = Content.Load<Texture2D>("nightCastle");
                            daybut = Content.Load<Texture2D>("sunbtn");
                            nightbut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                            WordCountCol = Color.White;

                            butpcount = 0;
                        }
                    }
                    if (mountain == true)
                    {
                        if (butpcount == 1)
                        {
                            backdraw = Content.Load<Texture2D>("nightMountain");
                            daybut = Content.Load<Texture2D>("sunbtn");
                            nightbut = new Texture2D(GraphicsDevice, 5, 5, false, SurfaceFormat.Color);
                            WordCountCol = Color.White;
                            butpcount = 0;
                        }
                    }
                }
                
               






                //foreach used to go through rectangle dictionary
                foreach (var rec in RectangleDictionary)
                {

                    //if right mouse button is pressed and mouse is in rectangle
                    if (myMouse.RightButton == ButtonState.Pressed && rec.Value.Contains(myMouse.X, myMouse.Y))
                    {
                        //new menuitem for showing count of words with string show word count
                        menuCountstr = "Word Count";
                        Vector2 menuCountsiz = menufont.MeasureString(menuCountstr);
                        menuCount = new Rectangle(myMouse.X, myMouse.Y, (int)menuCountsiz.X, (int)menuCountsiz.Y);

                        //new menuitem for color change with string Change color
                        menuColorstr = "Change Color";
                        Vector2 menuColorsiz = menufont.MeasureString(menuColorstr);
                        menuColor = new Rectangle(myMouse.X, myMouse.Y + (int)menuCountsiz.Y, (int)menuColorsiz.X, (int)menuColorsiz.Y);

                        //new menuitem for font change with string Change font
                        menuFontstr = "Change Font";
                        Vector2 menuFontsiz = menufont.MeasureString(menuFontstr);
                        menuFont = new Rectangle(myMouse.X, myMouse.Y + (int)menuCountsiz.Y + (int)menuColorsiz.Y, (int)menuFontsiz.X, (int)menuFontsiz.Y);

                        //new menuitem to remove word with string Remove word
                        menuRemovestr = "Remove Word";
                        Vector2 menuRemovesiz = menufont.MeasureString(menuRemovestr);
                        menuRemove = new Rectangle(myMouse.X, myMouse.Y + (int)menuCountsiz.Y + (int)menuColorsiz.Y + (int)menuFontsiz.Y, (int)menuRemovesiz.X, (int)menuRemovesiz.Y);


                    }
                    if (myMouse.LeftButton == ButtonState.Pressed && menuCount.Contains(myMouse.X, myMouse.Y))
                    {
                        if (rec.Value.Contains(menuCount.X, menuCount.Y))
                        {
                            foreach (var words in Words)
                            {
                                //if the current rectangle equal the current word
                                if (rec.Key == words.Key)
                                {
                                    removestuff();
                                    //write how many times the word in that rectangle appears
                                    WordCountstr = rec.Key + " appears " + words.Value.ToString() + " times";
                                }
                            }
                        }
                    }


                    //if the menuitem color change is pressed 
                    if (myMouse.LeftButton == ButtonState.Pressed && menuColor.Contains(myMouse.X, myMouse.Y))
                    {

                        //checks rectangle contains menu and countcol == 0
                        if (rec.Value.Contains(menuCount.X, menuCount.Y) && countcol == 0)
                        {
                            //method remove stuff
                            this.removestuff();
                            //color of word set to white
                            ColorsDictionary[rec.Key] = Color.Black;
                            //countcol incremented by one
                            countcol++;
                        }
                        //checks rectangle contains menu and countcol == 1
                        if (rec.Value.Contains(menuCount.X, menuCount.Y) && countcol == 1)
                        {
                            this.removestuff();
                            //color of word set to red
                            ColorsDictionary[rec.Key] = Color.Red;
                            countcol++;
                        }
                        //checks rectangle contains menu and countcol == 2
                        if (rec.Value.Contains(menuCount.X, menuCount.Y) && countcol == 2)
                        {
                            this.removestuff();
                            //color of word set to green
                            ColorsDictionary[rec.Key] = Color.Green;
                            countcol++;
                        }
                        //checks rectangle contains menu and countcol == 3
                        if (rec.Value.Contains(menuCount.X, menuCount.Y) && countcol == 3)
                        {
                            this.removestuff();
                            //color of word set to blue
                            ColorsDictionary[rec.Key] = Color.Blue;
                            countcol = 0;
                        }



                    }


                    //if the menuitem font change is pressed 
                    if (myMouse.LeftButton == ButtonState.Pressed && menuFont.Contains(myMouse.X, myMouse.Y))
                    {
                        if (rec.Value.Contains(menuCount.X, menuCount.Y))
                        {
                            this.removestuff();
                            //new stringsiz empty vector2
                            Vector2 strsiz = new Vector2();

                            FontDictionary[rec.Key] = Content.Load<SpriteFont>("MyFont2");
                            strsiz = FontDictionary[rec.Key].MeasureString(rec.Key);
                            //ox and oy are equal to the rec.value.x and y
                            int ox = rec.Value.X;
                            int oy = rec.Value.Y;
                            //newrx and newry equals strsiz x and y
                            int newrx = (int)strsiz.X;
                            int newry = (int)strsiz.Y;
                            //rectangle with rec key is equal to new rectangle with x and y ,ox and oy
                            //and width and height of newrx and newry
                            RectangleDictionary[rec.Key] = new Rectangle(ox, oy, newrx, newry);
                            break;

                        }

                    }
                    //if the menuitem remove word is pressed
                    if (myMouse.LeftButton == ButtonState.Pressed && menuRemove.Contains(myMouse.X, myMouse.Y))
                    {

                        if (rec.Value.Contains(menuCount.X, menuCount.Y))
                        {
                            this.removestuff();
                            //removes word
                            RectangleDictionary.Remove(rec.Key);
                            break;

                        }

                    }
                    //if anywhere on the screen is pressed which is not a box and not the menu
                    if (myMouse.LeftButton == ButtonState.Pressed && rec.Value.Contains(myMouse.X, myMouse.Y) == false && menuColor.Contains(myMouse.X, myMouse.Y) == false && menuCount.Contains(myMouse.X, myMouse.Y) == false && menuFont.Contains(myMouse.X, myMouse.Y) == false && menuRemove.Contains(myMouse.X, myMouse.Y) == false)
                    {
                        //remove stuff method
                        this.removestuff();

                    }


                }

            }
            base.Update(gameTime);

        }
        //remove stuff method
        public void removestuff()
        {
            //sets menuitems back to nothing
            menuCountstr = "";
            menuColorstr = "";
            menuFontstr = "";
            menuRemovestr = "";
            menuCount = new Rectangle();
            menuColor = new Rectangle();
            menuFont = new Rectangle();
            menuRemove = new Rectangle();


        }
        public static void Screenshot(GraphicsDevice device)
        {
            byte[] screenData;

            screenData = new byte[device.PresentationParameters.BackBufferWidth * device.PresentationParameters.BackBufferHeight * 4];

            device.GetBackBufferData<byte>(screenData);

            Texture2D t2d = new Texture2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight, false, device.PresentationParameters.BackBufferFormat);

            t2d.SetData<byte>(screenData);

            int i = 0;
            string name = "ScreenShot" + i.ToString() + ".png";
            while (File.Exists(name))
            {
                i += 1;
                name = "ScreenShot" + i.ToString() + ".png";

            }

            Stream st = new FileStream(name, FileMode.Create);

            t2d.SaveAsPng(st, t2d.Width, t2d.Height);

            st.Close();

            t2d.Dispose();
        } 

        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.Menu)//if gamestate equals menu
            {
                GraphicsDevice.Clear(Color.Tomato);
                spriteBatch.Begin();
                spriteBatch.DrawString(menufont, "Menu", new Vector2(50, 50), Color.Black);
                spriteBatch.End();
            }
            if (gameState == GameState.Game)//if gamestate equals game
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();
                spriteBatch.Draw(backdraw, new Vector2(0, 0), Color.White);
               
                spriteBatch.Draw(daybut, new Vector2(750, 390), Color.White);
                spriteBatch.Draw(myTexture, dayrec, Color.Transparent);
                spriteBatch.Draw(nightbut, new Vector2(750, 430), Color.White);
                spriteBatch.Draw(myTexture, nightrec, Color.Transparent);
              
                spriteBatch.Draw(ssbut, new Vector2(700, 430), Color.White);
                spriteBatch.Draw(myTexture, ssrec, Color.Transparent);

                spriteBatch.Draw(homebut, new Vector2(10, 430), Color.White);
                spriteBatch.Draw(myTexture, homerec, Color.Transparent);
               
                spriteBatch.Draw(musicbut, new Vector2(60, 430), Color.White);
                spriteBatch.Draw(myTexture, musicrec, Color.Transparent);
                spriteBatch.Draw(mutebut, new Vector2(120, 430), Color.White);
                spriteBatch.Draw(myTexture, muterec, Color.Transparent);
               
                int count = 0;
                //foreach to work through rectangle dictionary
                foreach (var rec in RectangleDictionary)
                {
                    //new Vector2 V containing rectangle x and y 
                    Vector2 V = new Vector2(rec.Value.X, rec.Value.Y);
                    //new Vector2s for string origin and rectangle origin
                    Vector2 strorigin = new Vector2();
                    Vector2 recorigin = new Vector2();

                    Vector2 strsiz = new Vector2();
                    foreach (var font in FontDictionary)
                    {
                        strsiz = FontDictionary[rec.Key].MeasureString(rec.Key);
                    }
                    // float rectangle and string angle 
                    float recangle = 0.0f;
                    float strangle = 0.0f;

                    /* if (rnum == count)
                     {
                         recangle = 0.0f;
                         recorigin = new Vector2();

                         strangle = (float)Math.PI;
                         strorigin = new Vector2((int)strsiz.X, (int)strsiz.Y);
                    

                     }*/




                    //Draws all the rectangles
                    spriteBatch.Draw(myTexture, rec.Value, null, Color.Snow, recangle, recorigin, SpriteEffects.None, 0.0f);

                    //Draws all the strings
                    spriteBatch.DrawString(FontDictionary[rec.Key], rec.Key, V, ColorsDictionary[rec.Key],
                            strangle, strorigin, 1.0f, SpriteEffects.None, 0.0f);




                    //color of menu Background
                    Color Menucol = Color.Black;
                    Color Menufontcol = Color.White;

                    //draws menuitems
                    spriteBatch.Draw(myTexture, menuCount, null, Menucol, 0.0f, new Vector2(), SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(menufont, menuCountstr, new Vector2(menuCount.X, menuCount.Y), Menufontcol);
                    spriteBatch.Draw(myTexture, menuColor, null, Menucol, 0.0f, new Vector2(), SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(menufont, menuColorstr, new Vector2(menuColor.X, menuColor.Y), Menufontcol);
                    spriteBatch.Draw(myTexture, menuFont, null, Menucol, 0.0f, new Vector2(), SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(menufont, menuFontstr, new Vector2(menuFont.X, menuFont.Y), Menufontcol);
                    spriteBatch.Draw(myTexture, menuRemove, null, Menucol, 0.0f, new Vector2(), SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(menufont, menuRemovestr, new Vector2(menuRemove.X, menuRemove.Y), Menufontcol);

                    //word count string
                    spriteBatch.DrawString(Content.Load<SpriteFont>("MyFont"), WordCountstr, new Vector2(10, 430), WordCountCol);



                    count++;

                }

                KeyboardState state = Keyboard.GetState();
                MouseState myMouse = Mouse.GetState();
                totheleft++;

                spriteBatch.End();
                if (myMouse.LeftButton == ButtonState.Pressed && ssrec.Contains(myMouse.X, myMouse.Y))
                {
                    Screenshot(GraphicsDevice);
                }
                if (state.IsKeyDown(Keys.S))
                {
                    Screenshot(GraphicsDevice);
                }
            }
            base.Draw(gameTime);
        }
    }
}
