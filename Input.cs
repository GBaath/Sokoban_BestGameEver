using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sokoban_Baatht_Adam
{
    public class Input
    {
        public Vector2 moveVector;
        private bool movedLastFrame;
        private Keys lastKey;
        public void Update()
        {


            var state = Keyboard.GetState();
            moveVector = new(0,0);

            if(state.IsKeyDown(Keys.W)&&lastKey != Keys.W)
            {
                moveVector = new(0, -1);
                movedLastFrame = true;
                lastKey = Keys.W;
            }
            else if (state.IsKeyDown(Keys.S) && lastKey != Keys.S)
            {
                moveVector = new(0, 1);
                movedLastFrame = true;
                lastKey = Keys.S;
            }
            else if (state.IsKeyDown(Keys.A) && lastKey != Keys.A)
            {
                moveVector = new(-1, 0);
                movedLastFrame = true;
                lastKey = Keys.A;
            }
            else if (state.IsKeyDown(Keys.D) && lastKey != Keys.D)
            {
                moveVector = new(1, 0);
                movedLastFrame = true;
                lastKey = Keys.D;
            }

        }
    }
}
