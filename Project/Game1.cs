using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Project.Hamsters;
using Project.Cats;

namespace Project
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Variables
        public static Rectangle room;
        public static SpriteFont timerFont;


        public float maxTimerLength = 120;
        public float timer = 120;
        public int timerInt;
        public bool timeIsUp = false;

        private MouseState mouse;
        Cursor cursor = new Cursor(new Vector2(0, 0), true);
        Cursor cursorClicked = new Cursor(new Vector2(0, 0), false);

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public GraphicsDeviceManager ReturnGraphics()
        {
            return graphics;
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
            room = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            cursor.alive = true;
            cursorClicked.alive = false;

            
            Items.Initialize();

            Items.objList.Add(cursor);
            Items.objList.Add(cursorClicked);


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

            //Load Font
            timerFont = Content.Load<SpriteFont>("timerfont");

            // TODO: use this.Content to load your game content here
            foreach (Obj o in Items.objList)
            {
                o.LoadContent(Content);
            }

            foreach (Obj o in Items.tileArray)
            {
                o.LoadContent(Content);

            }

            foreach (Obj o in Items.hamsterList)
            {
                o.LoadContent(Content);
            }

            foreach (Obj o in Items.catList)
            {
                o.LoadContent(Content);
            }
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

            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                cursor.alive = false;
                cursorClicked.alive = true;
            }
            else
            {
                cursor.alive = true;
                cursorClicked.alive = false;
            }

            //Update Timer
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(timer <= 0)
            {
                timeIsUp = true;
            }

            foreach (Obj o in Items.objList)
            {
                o.Update();
            }

            foreach (Obj o in Items.tileArray)
            {
                o.Update();
                if(o.checkHover())
                {
                    Texture2D hoverRect = new Texture2D(graphics.GraphicsDevice, o.getSprite().Width, o.getSprite().Height);
                    spriteBatch.Begin();
                    spriteBatch.Draw(hoverRect, o.position, Color.Red);
                    spriteBatch.End();
                }

            }

            foreach (Obj o in Items.hamsterList)
            {
                o.Update();
            }

            foreach (Obj o in Items.catList)
            {
                o.Update();
            }

           



            base.Update(gameTime);

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

            foreach (Obj o in Items.objList)
            {
                if(!(o.GetType() == typeof(Cursor)))
                {
                    o.Draw(spriteBatch);
                }
                
            }

            foreach (Obj o in Items.tileArray)
            {
                o.Draw(spriteBatch);

            }

            if (!timeIsUp)
            {
                timerInt = (int)timer;


                //Draw "S"
                spriteBatch.DrawString(timerFont, "s", new Vector2(210, 60), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 1);
                spriteBatch.DrawString(timerFont, "s", new Vector2(210, 58), Color.Red, 0, new Vector2(0, 0), 3, SpriteEffects.None, 1);

                //Draw "Time"
                spriteBatch.DrawString(timerFont, timerInt.ToString(), new Vector2(116, 60), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 1);
                spriteBatch.DrawString(timerFont, timerInt.ToString(), new Vector2(114, 58), Color.Red, 0, new Vector2(0, 0), 3, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.DrawString(timerFont, "TIME'S UP", new Vector2(60, 60), Color.Black, 0, new Vector2(0, 0), 3, SpriteEffects.None, 1);
                spriteBatch.DrawString(timerFont, "TIME'S UP", new Vector2(58, 58), Color.Red, 0, new Vector2(0, 0), 3, SpriteEffects.None, 1);
            }

            spriteBatch.End();

            //To Draw UI Components
            spriteBatch.Begin();


            foreach (Obj o in Items.hamsterList)
            {
                o.Draw(spriteBatch);

            }

            foreach (Obj o in Items.catList)
            {
                o.Draw(spriteBatch);
            }

            foreach (Obj o in Items.objList)
            {
                if (o.GetType() == typeof(Cursor))
                {
                    o.Draw(spriteBatch);
                }
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
