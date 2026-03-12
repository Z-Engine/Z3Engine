using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Z3Engine.Source.Graphics
{
    internal class StaticSprite
    {
        protected Texture2D texture;
        protected Rectangle destRect;
        protected Color color;

        public StaticSprite(Texture2D texture, Rectangle rect, Color color)
        {
            // Error check
            if (texture == null) texture = Global.noImg;

            // Initialize sprite
            SetTexture(texture);
            SetDestRect(rect);
            SetColor(color);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, color);
        }

        // Getters

        public Texture2D GetTexture()
        {
            return this.texture;
        }

        public Rectangle GetDestRect()
        {
            return this.destRect;
        }

        public Color GetColor()
        {
            return this.color;
        }

        // Setters

        public void SetTexture(Texture2D newTexture)
        {
            this.texture = newTexture;
        }

        public void SetDestRect(Rectangle newRect)
        {
            this.destRect = newRect;
        }

        public void SetColor(Color newColor)
        {
            this.color = newColor;
        }
    }
}
