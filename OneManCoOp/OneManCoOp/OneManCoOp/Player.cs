using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Player : GameObject
    {
        const float JUMP_SPEED = 13;
        const float FRICTION_GROUND = .80f;
        const float FRICTION_AIR = .92f;
        const float ACC_GROUND = 1.5f;
        const float ACC_AIR = .50f;

        public Player(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.player, position, new Vector2(32, 64), 4, new Point(32, 64), 10 / 60f);
        }

        public override void Update()
        {
            //====================== MOVEMENT ================
            Vector2 acceleration = new Vector2(0);
            if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) acceleration.X -= 1;
            if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) acceleration.X += 1;
            if ((Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || Input.newGs.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && !CollidedOnY) acceleration.Y -= 1;
            if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) && !CollidedOnY) acceleration.Y += 1;
            if ((Input.KeyWasJustPressed(Microsoft.Xna.Framework.Input.Keys.Space) || Input.ButtonJustPressed(Microsoft.Xna.Framework.Input.Buttons.A)) && CollidedOnY) Velocity -= new Vector2(0, JUMP_SPEED);

            acceleration.X += Input.newGs.ThumbSticks.Left.X;
            
            if (acceleration != Vector2.Zero) acceleration.Normalize();

            Velocity *= new Vector2((CollidedOnY) ? FRICTION_GROUND : FRICTION_AIR, 1);

            Velocity += acceleration * (CollidedOnY ? ACC_GROUND : ACC_AIR);

            Velocity += new Vector2(0, Game1.GRAVITY);

            Move(true);

            //================= ANIMATION ====================
            if (Velocity.Length() > .05f)
            {
                Sprite.AnimationSpeed = Velocity.Length() / 60;
                if (Velocity.X < 0) Sprite.SpriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                else if (Velocity.X > 0) Sprite.SpriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
            }
            else
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 0;
            }

            if (!CollidedOnY)
            {
                Sprite.Frame = 3;
                Sprite.AnimationSpeed = 0;
            }
        }
    }
}
