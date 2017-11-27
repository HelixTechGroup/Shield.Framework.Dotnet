using System;
using Microsoft.Xna.Framework;

namespace Shield.Framework.Platform
{
    public sealed class GameTimeEventArgs : EventArgs
    {
        private readonly GameTime m_gameTime;

        public GameTime GameTime

        {
            get { return m_gameTime; }
        }

        public GameTimeEventArgs(GameTime gameTime)
        {
            m_gameTime = gameTime;
        }
    }
}