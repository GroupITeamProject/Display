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

        //A font class will hold the XML of the font to be printed.
        SpriteFont x;

        //A Dictionary takes in a string of words from a file
        Dictionary<string, int> Words = new Dictionary<string, int>();

        //An int to move words left in the draw method
        int totheleft = 10;
        

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

            //Assigning x to be the XML file Myfont
            x = Content.Load<SpriteFont>("MyFont");

            //The List of Words = the list of words generated from the file.

            FileAnalyser FA = new FileAnalyser("C:\\Users\\Mini-EPIC\\Documents\\EVE\\logs\\Chatlogs\\Rookie_Help_20130120_225718.txt");

            Words = FA.getDictionary();
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
                || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) )

                this.Exit();

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

            //spriteBatch.Draw(texture, new Rectangle(100, 100, 10, 10), Color.White);

            int i = 0;

            foreach(var word in Words)
            {

                spriteBatch.DrawString(x, word.Key, new Vector2(10 + totheleft, i), Color.White,
                    (float)0.0, new Vector2(), 1.0f, SpriteEffects.None, (float)1.0);
                spriteBatch.DrawString(x, word.Value.ToString(), new Vector2(100 + totheleft, i), Color.White,
                    (float)0.0, new Vector2(), 1.0f, SpriteEffects.None, (float)1.0);

                i = i + 10;
            }

            totheleft++;

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
