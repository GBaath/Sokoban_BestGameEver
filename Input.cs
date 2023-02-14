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
        public void Update()
        {
            //moveVector = new(0,0);

            if(InputSystem.IsKeyPressed(Keys.W))
            {
                moveVector = new(0, -1);
            }
            else if (InputSystem.IsKeyPressed(Keys.S))
            {
                moveVector = new(0, 1);
            }
            else if (InputSystem.IsKeyPressed(Keys.A))
            {
                moveVector = new(-1, 0);
            }
            else if (InputSystem.IsKeyPressed(Keys.D))
            {
                moveVector = new(1, 0);
            }
        }
    }
}
