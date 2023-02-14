using SharpDX.Direct3D9;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban_Baatht_Adam;
using Microsoft.Xna.Framework.Content;
using SharpDX.MediaFoundation;

public class Board
{
    public static Board instance;
    public int width;
    public int height;
    //private object level;

    private char[,] level;

    private Texture2D whiteSquare;
    public Board(ContentManager c)
    {
        instance = this;
        whiteSquare = c.Load<Texture2D>("Sprites/whiteSquare");

    }
    public void LoadLevel(char[,] level)
    {
        width = level.GetLength(0);
        height = level.GetLength(1);
        //this.level = RotateArray(level);
    }

    private object RotateArray(char[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);

        char[,] result = new char[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j, i] = input[i, j];
            }
        }

        return result;
    }

    public void Draw(SpriteBatch batch, Vector2 camOffset)
    {
        int y = -1;
        for (int i = 0; i < width * height; i++)
        {
            int x = i % width;
            if (x == 0) y += 1;

            Vector2 position = new Vector2(x * MicroGame.CELL_SIZE, y * MicroGame.CELL_SIZE);

            //camOffset.X -= width * MicroGame.CELL_SIZE / 2;
            //camOffset.Y -= height * MicroGame.CELL_SIZE / 2;

            position += camOffset;

            batch.Draw(whiteSquare, position, Color.Beige);

            if ((x + y) % 2 == 0) //For making cells checkered
            {
                batch.Draw(whiteSquare, position, new Color(0, 0, 0, 0.4f));
            }
        }

        //y = -1;

        //for (int i = 0; i < width * height; i++)
        //{
        //    int x = i % width;
        //    if (x == 0) y += 1;

        //    Vector2 position = new Vector2(x * MicroGame.CELL_SIZE, y * MicroGame.CELL_SIZE);
        //    position += camOffset;

        //    //Vector2 playerOffset = new Vector2(0, -player.Height / 2);

        //    switch (level[x, y])
        //    {
        //        case BOX:
        //            batch.Draw(box, position, Color.White);
        //            break;
        //        case WALL:
        //            batch.Draw(wall, position, Color.White);
        //            break;
        //        case PLAYER:
        //            batch.Draw(player, position + playerOffset, Color.White);
        //            break;
        //        case GOAL:
        //            batch.Draw(goal, position, Color.White);
        //            break;
        //        case PLAYER_AND_GOAL:
        //            batch.Draw(goal, position, Color.White);
        //            batch.Draw(player, position + playerOffset, Color.White);
        //            break;
        //        case BOX_AND_GOAL:
        //            batch.Draw(goal, position, Color.White);
        //            batch.Draw(box, position, Color.White);
        //            break;

        //    }
    }
}

