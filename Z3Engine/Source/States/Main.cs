// The main state of the game.

using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Z3Engine.Source.Graphics;

namespace Z3Engine.Source.States
{
    internal class Main : State
    {
        public State currentState;

        public WorldState worldState;

        public Main()
        {
            // Set States
            worldState = new WorldState();
            worldState.SetWorld();

            // Set Current State
            currentState = worldState;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Update State
            currentState.Update(gameTime);
        }

        public override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw State
            currentState.OnDraw(gameTime, spriteBatch);
        }
    }
}
