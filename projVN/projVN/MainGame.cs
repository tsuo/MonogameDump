using System;
using System.Drawing;

using XNA = Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace projVN
{
    class MainGame : XNA.Game
    {
        private XNA.GraphicsDeviceManager graphics;

        private int width = 1440;
        private int height = 900;

        private SpriteBatch spriteBatch = null;

        SpriteFont font;
        Bitmap map;
        Font f;
        FontMap fmap;
        Texture2D text;
        Texture2D abc;
        public MainGame()
        {
            graphics = new XNA.GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            
            

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("test");
            //map = new Bitmap();
            String teststr = "ABCDEFG";

            abc = Content.Load<Texture2D>("3starters");

            f = new Font(new FontFamily("arial"), 24);
            fmap = new FontMap(GraphicsDevice, f, Brushes.Black);
            using (FileStream str = new FileStream("test.png", FileMode.Open))
                text = Texture2D.FromStream(GraphicsDevice, str);

            //fmap.SaveImage();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(XNA.GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        FontMapEntry testentr;
        protected override void Draw(XNA.GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);

            spriteBatch.Begin();

            //spriteBatch.Draw(font.Texture, new XNA.Rectangle(0,0,width, height), XNA.Color.White);

            // testing font map
            //testentr = fmap.GetCharacterOffset('M');
            //spriteBatch.Draw(fmap.FontMapTexture, new XNA.Rectangle(0,0,50,50), new XNA.Rectangle(testentr.Offset1, 0, testentr.Offset2, testentr.Height), XNA.Color.White);
            spriteBatch.Draw(text, new XNA.Rectangle(0, 0, fmap.TotalMapWidth, fmap.MaxMapHeight), XNA.Color.White);
            //fmap.SaveImage();
            //spriteBatch.Draw(abc, new XNA.Rectangle(0,0, width, height), XNA.Color.White);
            //Console.WriteLine(fmap.FullString);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override bool BeginDraw()
        {
            return base.BeginDraw();
        }
        protected override void EndDraw()
        {
            base.EndDraw();
        }

        protected override void BeginRun()
        {
            base.BeginRun();
        }

        protected override void EndRun()
        {
            base.EndRun();
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
