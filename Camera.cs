using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Sokoban_Baatht_Adam
{
    public class Camera
    {
        private Board board;

        public Camera(Board board)
        {
            this.board = board;
        }

        public Vector2 GetOffset()
        {
            int xOffset = MicroGame.GAME_WIDTH / 2;
            xOffset -= board.width * MicroGame.CELL_SIZE / 2;

            int yOffset = MicroGame.GAME_HEIGHT / 2;
            yOffset -= board.height * MicroGame.CELL_SIZE / 2;

            return new Vector2(xOffset, yOffset);
        }
    }
}
