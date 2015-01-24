using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Input
    {
        public static KeyboardState newKs, oldKs;
        public static MouseState newMs, oldMs;

        public static Vector2 MousePosition { get { return new Vector2(newMs.X, newMs.Y) + Camera.Position - Camera.Origin; } }

        public static void Initialize()
        {
            newKs = oldKs = Keyboard.GetState();
            newMs = oldMs = Mouse.GetState();
        }

        public static void Update()
        {
            oldKs = newKs;
            oldMs = newMs;
            newKs = Keyboard.GetState();
            newMs = Mouse.GetState();
        }

        public static bool KeyWasJustPressed(Keys key)
        {
            return newKs.IsKeyDown(key) && oldKs.IsKeyUp(key);
        }

        public static bool LeftClickWasJustPressed()
        {
            return newMs.LeftButton == ButtonState.Pressed && oldMs.LeftButton == ButtonState.Released;
        }
    }
}
