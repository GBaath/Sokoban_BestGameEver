using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Baatht_Adam
{
    public class SnakeMovement
    {
        int timesMoved = 1;
        float countDuration = 0.5f;
        float currentTime = 0f;

        public SnakeMovement(int timesMoved, float countDuration, float currentTime)
        {
            this.timesMoved = timesMoved;
            this.countDuration = countDuration;
            this.currentTime = currentTime;
        }

        public bool DoMoveSnake(float elapsedTime)
        {
            currentTime += elapsedTime;

            if (currentTime < countDuration) return false;
            else
            {
                timesMoved++;
                currentTime -= countDuration;
                return true;
            }
        }

        //public void SnakeMover(Vector2 dirToMove)
        //{
        //    playerDrawPos += dirToMove * CELL_SIZE;
        //    playerDrawPos = new(MathHelper.Clamp(playerDrawPos.X, 0, GAME_WIDTH * 2), MathHelper.Clamp(playerDrawPos.Y, 0, MicroGame.GAME_HEIGHT * 2));
        //    //return moveVector;
        //}
    }
}
