using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sokoban_Baatht_Adam
{
    public class MicroGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Input input;
        private Board board;
        private Camera camera;
        private SnakeMovement snakeMove;

        private Texture2D whiteSqare;
        private Texture2D snakeHead;
        private SpriteFont font;


        private List<Vector2> snakePositions = new List<Vector2>();
        private Vector2 winPos = Vector2.Zero;

        private int score = 0;

        private bool gameLost = false;
        private float countDuration = 0.15f;

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
            snakePositions.Add(Vector2.Zero);

            _graphics.PreferredBackBufferWidth = GAME_HEIGHT * GAME_UPSCALE_FACTOR;
            _graphics.PreferredBackBufferHeight = GAME_WIDTH * GAME_UPSCALE_FACTOR;
            LoadContent();
            base.Initialize();

            board = new(Content);
            camera = new(board);
            snakeMove = new(1, 0.15f, 0);
            //snakeMove = new(Content);

            char[,] sampleLevel = new char[25, 15];
            board.LoadLevel(sampleLevel);
        }

        protected override void LoadContent()
        {
            whiteSqare = Content.Load<Texture2D>("Sprites/whiteSquare");
            snakeHead = Content.Load<Texture2D>("Sprites/SnakeSmileyHead (1)");
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
                winPos = new(r.Next(0, GAME_WIDTH * 2 / CELL_SIZE), r.Next(0, GAME_HEIGHT * 2 / CELL_SIZE));
                winPos *= CELL_SIZE;
            }
            while (winPos == prevWinpos);
        }
        private bool CheckSameSquare()
        {
            for (int i = 0; i < snakePositions.Count; i++)
            {
                return snakePositions[i] == winPos ? true : false;
            }
            return false;
        }

        private bool CheckForSnakeCollision()
        {
            for (int i = 1; i < snakePositions.Count; i++)
            {
                //Console.WriteLine(i);
                if (snakePositions[0] == snakePositions[i]) return true;
            }
            return false;
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Draw(gameTime);

            if (gameLost) return;
            InputSystem.Update();
            input.Update();

            if (snakeMove.DoMoveSnake((float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                var evenSnakePos = Vector2.Zero;
                var oddSnakePos = Vector2.Zero;
                for (int i = 0; i < snakePositions.Count; i++)
                {
                    if (i == 0)
                    {
                        evenSnakePos = snakePositions[i];
                        snakePositions[i] += input.moveVector * CELL_SIZE;
                    }
                    else if (i % 2 == 0)
                    {
                        evenSnakePos = snakePositions[i];
                        snakePositions[i] = oddSnakePos;
                    }
                    else if (i % 2 == 1)
                    {
                        oddSnakePos = snakePositions[i];
                        snakePositions[i] = evenSnakePos;
                    }
                }

                snakePositions[0] = new(MathHelper.Clamp(snakePositions[0].X, 0, GAME_WIDTH * 2), MathHelper.Clamp(snakePositions[0].Y, 0, GAME_HEIGHT * 2));
            }

            if (CheckForSnakeCollision())
            {
                //snakePositions.Clear();
                Console.WriteLine("You lose");
                gameLost = true;
            }


            base.Update(gameTime);

            if (CheckSameSquare())
            {
                score += 1;

                if (score % 5 == 0)
                {
                    countDuration /= 1.09f;
                    snakeMove = new(1, countDuration, 0);
                }

                var newSnakePos = snakePositions.Last() - input.moveVector;

                snakePositions.Add(newSnakePos);
                Console.WriteLine(snakePositions.Count);
                NewWinPos(winPos);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            board.Draw(_spriteBatch, Vector2.Zero);

            for (int i = snakePositions.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    _spriteBatch.Draw(snakeHead, snakePositions[i], Color.White);
                }
                else
                {
                    _spriteBatch.Draw(whiteSqare, snakePositions[i], Color.Green);
                }
            }

            _spriteBatch.Draw(whiteSqare, winPos, Color.Red);

            if (gameLost) _spriteBatch.DrawString(font, "Game Over", new Vector2(GAME_WIDTH, CELL_SIZE), Color.Black);

            _spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(GAME_WIDTH, CELL_SIZE * 2), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}