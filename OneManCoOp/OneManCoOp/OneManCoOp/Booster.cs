using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Booster : GameObject
    {
        enum State { Retracted, Retracting, Extending }

        State state;

        public void Extend()
        {
            Sprite.Frame = 1;
            state = State.Extending;
        }

        public Booster(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.booster, position, new Vector2(Tile.SIZE), 5, new Point(32, 32), 0);
            Sprite.Origin = Vector2.Zero;
        }

        public override void Update()
        {
            switch(state)
            {
                case State.Extending:
                    if (Sprite.Frame < Sprite.Frames - 1) Sprite.AnimationSpeed = .5f;
                    else state = State.Retracting;
                    break;
                case State.Retracting:
                    if (Sprite.Frame > 0) Sprite.AnimationSpeed = -.1f;
                    else 
                    {
                        Sprite.AnimationSpeed = 0;
                        state = State.Retracted;
                    }
                    break;
            }
        }
    }
}
