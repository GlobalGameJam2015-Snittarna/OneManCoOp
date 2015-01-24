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
            Position = position2;
            type = type2;
        }

        public override void Update()
        {
            
        }
        public void AssignSprite()
        {
            switch(type)
            {
                case Type.Door:
                    Sprite = new Sprite(TextureManager.door, Position, new Vector2(TextureManager.door.Width, TextureManager.door.Height), 6, new Point(64, 64), Sprite.AnimationSpeed);
                    break;
            }
        }
    }
}
