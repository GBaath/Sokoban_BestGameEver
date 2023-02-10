﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sokoban_Baatht_Adam
{
    public class MicroGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Input input;

        private Texture2D whiteSqare;

        private Vector2 drawPos = Vector2.Zero;


        public const int CELL_SIZE = 16;

        public const int GAME_WIDTH = 320;
        public const int GAME_HEIGHT = 180;
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
            LoadContent();
            base.Initialize();
            _graphics.PreferredBackBufferWidth = GAME_HEIGHT * GAME_UPSCALE_FACTOR;
            _graphics.PreferredBackBufferHeight = GAME_WIDTH * GAME_UPSCALE_FACTOR;

        }

        protected override void LoadContent()
        {
            whiteSqare = Content.Load<Texture2D>("Sprites/whiteSquare");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            input ??= new();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            input.Update();
            drawPos += input.moveVector*CELL_SIZE;
            Draw(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(whiteSqare, drawPos, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}