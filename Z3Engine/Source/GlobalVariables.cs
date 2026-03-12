using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Z3Engine.Source
{
    internal class Global
    {
        // Window

        public static string windowName = "Z3 Engine";
        public static bool
            active = true,
            mouseVisible = true;

        // Debug

        public static string gameVersion = "1.0.0";
        public static bool
            debug = true,
            backculling = false;

        // Game

        public static bool
            paused = false, // Paused
            pauseWhenInactive = true, // Pause when Inactive
            mouseFocus = true; // Mouse Focus (For GUI)
        public static float
            mouseSensitivity = 0.01f;

        // Cheats

        public static bool noClip = false; // No Clipping

        // Assets

        // Images
        public static Texture2D
            noImg;

        // Fonts
        public static SpriteFont
            arial;

        // Load Assets
        public static void Load(ContentManager Content)
        {
            // Load Images
            noImg = Content.Load<Texture2D>("Assets/Images/pixel");

            // Load Fonts
            arial = Content.Load<SpriteFont>("Assets/Fonts/Arial");
        }
    }
}
