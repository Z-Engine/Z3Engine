// The template class for a state in the game.
// A state is a different scene.

/*
 * Templates:
 * 
 * To Update:
 * public override void Update(GameTime gameTime)
 * 
 * To Draw:
 * public override void OnDraw(SpriteBatch spriteBatch)
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Z3Engine.Source.Graphics;

namespace Z3Engine.Source.States
{
    internal class State
    {
        // State variables
        public KeyboardState keyboard, previousKeyboard;
        public MouseState mouse, previousMouse;
        public Vector2 mouseDelta;
        const float MAXDELTA = 6;
        public int screenWidth, screenHeight;

        public Camera camera;

        public State()
        {
            // Set Camera
            this.camera = new Camera(MainGame.publicGraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            // Update state variables
            screenWidth = MainGame.publicGraphicsDevice.Viewport.Width;
            screenHeight = MainGame.publicGraphicsDevice.Viewport.Height;

            // Controls
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            // Update View Matrix
            camera.Update(gameTime);

            // Override Update
            OnUpdate(gameTime);

            mouseDelta = new Vector2(Math.Min(MAXDELTA, (previousMouse.X - mouse.X)), Math.Min(MAXDELTA, (previousMouse.Y - mouse.Y)));

            previousKeyboard = keyboard;
            previousMouse = mouse;
        }

        public virtual void OnUpdate(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            OnDraw(gameTime, spriteBatch);
        }

        /// <summary>
        /// The method to override when drawing in the state.
        /// </summary>
        /// /// <param name="gameTime">The GameTime used to render the 3D world.</param>
        /// <param name="spriteBatch">The SpriteBatch used to draw 2D Sprites.</param>
        public virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        // Controls

        public bool KeyPress(Keys key)
        {
            if (keyboard.IsKeyUp(key) && previousKeyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool MouseMoved()
        {
            if (mouseDelta.X != 0 || mouseDelta.Y != 0)
            {
                return true;
            }
            else return false;
        }
    }
}