#region Usings
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shield.Framework.Platform;
#endregion

namespace Shield.Sandbox.MonoGame.Windows
{
    public class SandboxBootstrapper : MonoGameWindowsBootstrapper
    {
        protected override void OnGameInitialize(object sender, EventArgs e)
        {
            var root = PlatformProvider.Services.Storage.Private.RootDirectory;
            var tmp = root.CreateDirectory("tmp");
            var tmp2 = root.CreateFile("a.tmp");
            var tmp3 = root.GetFile("tmp/b.tmp");
            var dirs = root.GetEntities().ToArray();
            base.OnGameInitialize(sender, e);
        }

        protected override void OnGameLoad(object sender, EventArgs e)
        {
            base.OnGameLoad(sender, e);
        }

        protected override void OnGameUnload(object sender, EventArgs e)
        {
            base.OnGameUnload(sender, e);
        }

        protected override void OnGameUpdate(object sender, GameTimeEventArgs e)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.OnGameUpdate(sender, e);
        }

        protected override void OnGameDraw(object sender, GameTimeEventArgs e)
        {
            m_game.GraphicsDevice.Clear(Color.CornflowerBlue);
            base.OnGameDraw(sender, e);
        }
    }
}