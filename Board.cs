using System;

public class Board
{
    public static Board instance;
    private int width;
    private int height;
    private object level;

    public Board()
	{
        instance = this;

    }
    public void LoadLevel(char[,] level)
    {
        width = level.GetLength(0);
        height = level.GetLength(1);
        this.level = RotateArray(level);
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
}
