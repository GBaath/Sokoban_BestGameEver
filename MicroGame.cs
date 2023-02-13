using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Sokoban_Baatht_Adam
{
    public class MicroGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Input input;

        private Texture2D whiteSqare;

        private SpriteFont font;

        private Vector2 playerDrawPos = Vector2.Zero;
        private Vector2 winPos = Vector2.Zero;

        private int score = 0;


        public const int CELL_SIZE = 32;

        public const int GAME_WIDTH = 384;
        public const int GAME_HEIGHT = 224;
        public const int GAME_UPSCALE_FACTOR = 4;
        public MicroGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            NewWinPos(winPos);

            _graphics.PreferredBackBufferWidth = GAME_HEIGHT * GAME_UPSCALE_FACTOR;
            _graphics.PreferredBackBufferHeight = GAME_WIDTH * GAME_UPSCALE_FACTOR;
            LoadContent();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            whiteSqare = Content.Load<Texture2D>("Sprites/whiteSquare");
            font = Content.Load<SpriteFont>("Font/Font1");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            input ??= new();


            // TODO: use this.Content to load your game content here
        }

        private void NewWinPos(Vector2 prevWinpos)
        {
            do
            {
                Random r = new();
                winPos = new(r.Next(0, GAME_WIDTH*2 / CELL_SIZE), r.Next(0, GAME_HEIGHT*2 / CELL_SIZE));
                winPos *= CELL_SIZE;
            }
            while (winPos == prevWinpos);
        }
        private bool CheckSameSquare()
        {
            return playerDrawPos == winPos ? true : false;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            InputSystem.Update();
            input.Update();
            playerDrawPos += input.moveVector*CELL_SIZE;
            playerDrawPos = new(MathHelper.Clamp(playerDrawPos.X, 0, GAME_WIDTH*2), MathHelper.Clamp(playerDrawPos.Y, 0, GAME_HEIGHT*2));

            Draw(gameTime);

            base.Update(gameTime);

            if (CheckSameSquare())
            {
                score++;
                NewWinPos(winPos);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(whiteSqare, playerDrawPos, Color.White);
            _spriteBatch.Draw(whiteSqare, winPos, Color.Red);
            _spriteBatch.DrawString(font, "Score: " + score.ToString(),new Vector2(CELL_SIZE,GAME_WIDTH),Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}