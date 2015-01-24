using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OneManCoOp
{
    class Puzzel : GameObject
    {
        public enum Type { Door };

        public Type type;

        byte tag;
        byte buttonsToOpen;

        bool opening;

        public Puzzel(Vector2 position2, Type type2)
        {
            AssignSprite(position2);
            type = type2;
        }

        public override void Update()
        {
            Sprite.AnimationSpeed = -0.5f;
            switch(type)
            {
                case Type.Door:
                    if(opening)
                    {
                        Sprite.AnimationSpeed = 0.1f;
                    }
                    break;
            }
        }
        public void AssignSprite(Vector2 position)
        {
            switch(type)
            {
                case Type.Door:
                    Sprite = new Sprite(TextureManager.door, position, new Vector2(64, 64), 6, new Point(64, 64), 0);
                    break;
            }
        }
    }
}
