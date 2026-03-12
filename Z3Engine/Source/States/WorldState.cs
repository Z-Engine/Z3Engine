using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Z3Engine.Source.Graphics;

namespace Z3Engine.Source.States
{
    internal class WorldState : State
    {
        // Geometric Info
        private VertexPositionColor[] triangleVertices;
        private VertexBuffer vertexBuffer;

        private float
            yaw = 0f,
            pitch = 0f;

        private StaticSprite pauseBG;
        private Text debugText;

        public WorldState()
        {
            // Set GUI
            this.debugText = new Text(Global.arial, "", new Vector2(100, 20), Color.White, 1.0f);

            pauseBG = new StaticSprite(Global.noImg, new Rectangle(0, 0, 0, 0), Color.Black * 0.5f);
        }

        public void SetWorld()
        {
            // Create Triangle
            triangleVertices = new VertexPositionColor[3];
            triangleVertices[0] = new VertexPositionColor(new Vector3(0, 20, 0), Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue);

            vertexBuffer = new VertexBuffer(MainGame.publicGraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Keep Sprites Updated
            pauseBG.SetDestRect(new Rectangle(0, 0, screenWidth, screenHeight));

            // Pause Game
            if (Global.active && KeyPress(Keys.Escape)) Global.paused = !Global.paused;

            // Pause Game when Inactive
            if (Global.pauseWhenInactive && !Global.active) Global.paused = true;

            // Set Mouse Focus when Paused
            if (Global.paused) Global.mouseFocus = false;
            else Global.mouseFocus = true;

            // Mouse Focus
            if (Global.mouseFocus)
            {
                Global.mouseVisible = false; // Hide Mouse
                Mouse.SetPosition(screenWidth / 2, screenHeight / 2); // Center Mouse
            }
            // Mouse not Focused
            else
            {
                Global.mouseVisible = true; // Show Mouse
            }

            // Controls
            if (!Global.paused)
            {
                // Movement
                if (Global.noClip) // No Clipping
                {
                    // Flying
                    if (KeyDown(Keys.Space))
                    {
                        camera.position.Y += 1f;
                        camera.target.Y += 1f;
                    }
                    if (KeyDown(Keys.LeftShift))
                    {
                        camera.position.Y -= 1f;
                        camera.target.Y -= 1f;
                    }
                }
                // Regular Movement

                Vector3 forward = camera.target - camera.position;
                forward.Y = 0;

                if (forward != Vector3.Zero)
                    forward.Normalize();

                Vector3 right = Vector3.Cross(forward, Vector3.Up);

                if (right != Vector3.Zero)
                    right.Normalize();

                float speed = 1f;

                if (KeyDown(Keys.W))
                {
                    camera.position += forward * speed;
                    camera.target += forward * speed;
                }

                if (KeyDown(Keys.S))
                {
                    camera.position -= forward * speed;
                    camera.target -= forward * speed;
                }

                if (KeyDown(Keys.A))
                {
                    camera.position -= right * speed;
                    camera.target -= right * speed;
                }

                if (KeyDown(Keys.D))
                {
                    camera.position += right * speed;
                    camera.target += right * speed;
                }

                // Mouse Camera
                if (MouseMoved())
                {
                    // Update Camera Rotation
                    yaw -= (mouse.X - screenWidth / 2) * Global.mouseSensitivity;
                    pitch -= (mouse.Y - screenHeight / 2) * Global.mouseSensitivity;
                    pitch = MathHelper.Clamp(pitch, -1.55f, 1.55f);

                    Vector3 direction;

                    direction.X = (float)(Math.Cos(pitch) * Math.Sin(yaw));
                    direction.Y = (float)(Math.Sin(pitch));
                    direction.Z = (float)(Math.Cos(pitch) * Math.Cos(yaw));

                    if (direction != Vector3.Zero)
                        direction.Normalize();

                    // Update Camera Target
                    camera.target = camera.position + direction;
                }

                // Debug Controls
                if (Global.debug)
                {
                    // No Clip
                    if (KeyPress(Keys.V)) Global.noClip = !Global.noClip;
                }
                // Debug Disabled
                else
                {
                    // Disable No Clip
                    if (Global.noClip) Global.noClip = false;
                }

                // Update debug text
                debugText.setText("X: " + Math.Round(camera.position.X, 2) + "\nY: " + Math.Round(camera.position.Y, 2) + "\nZ: " + Math.Round(camera.position.Z, 2));
            }
        }

        public override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw Game
            camera.Draw(gameTime, vertexBuffer);

            // Draw GUI
            if (Global.paused) pauseBG.Draw(spriteBatch);
            if (Global.debug) debugText.Draw(spriteBatch);
        }
    }
}
