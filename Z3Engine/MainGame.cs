// Z3 Engine is owned by Snowman64 under the GNU General Public License v3.0.
// You are allowed to use this engine for free, but credit must be given.

// Special Thanks:
// 3D Tutorial by Gamefromscratch.com

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Z3Engine.Source;
using Z3Engine.Source.Graphics;
using Z3Engine.Source.States;

namespace Z3Engine
{
    public class MainGame : Game
    {
        // Variables
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Main main;

        // Public Variables
        public static GraphicsDevice publicGraphicsDevice;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Screen Resizing
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new System.EventHandler<System.EventArgs>(WindowResized);
        }

        private void WindowResized(object sender, System.EventArgs e)
        {
            // Update Projection Matrix on Resize
            main.camera.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                GraphicsDevice.Viewport.AspectRatio,
                0.1f,
                1000f);
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Set Public Variables
            publicGraphicsDevice = base.GraphicsDevice;

            // Set Window
            this.Window.Title = Global.windowName;

            // Set State
            main = new Main();
        }

        protected override void LoadContent()
        {
            // Load Assets
            Global.Load(Content);

            // Set SpriteBatch
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Keep Public Variables Updated
            publicGraphicsDevice = base.GraphicsDevice;
            if (Global.mouseVisible != IsMouseVisible) IsMouseVisible = Global.mouseVisible;
            Global.active = this.IsActive;

            // Update Game State
            main.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Draw Game State
            spriteBatch.Begin(SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            DepthStencilState.None,
            RasterizerState.CullNone);
            main.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
