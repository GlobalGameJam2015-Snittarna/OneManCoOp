using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OneManCoOp
{
    class Puzzel : GameObject
    {
        public enum Type { Door };

        public Type type;

        byte tag;
        byte buttonsToOpen;
        byte buttonsPressed;

        bool opening;

        public Puzzel(Vector2 position2, Type type2, byte tag2, byte buttonsToOpen2)
        {
            buttonsToOpen = buttonsToOpen2;
            tag = tag2;
            AssignSprite(position2);
            type = type2;
        }

        public override void Update()
        {
            Debug.Print(buttonsPressed.ToString());
            switch(type)
            {
                case Type.Door:
                    if(opening)
                    {
                        Sprite.AnimationSpeed = 0.1f;
                    }
                    foreach(Button b in Game1.buttons)
                    {
                        
                        if (!b.AddPress && b.BeingPressed)
                        {
                            buttonsPressed += 1;
                            b.AddPress = true;
                        }
                        
                    }
                    //if (Sprite.Frame >= 3) Sprite.AnimationSpeed = 0;
                    Sprite.AnimationSpeed = (Sprite.Frame >= 5) ? Sprite.AnimationSpeed = 0 : Sprite.AnimationSpeed = Sprite.AnimationSpeed;
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
