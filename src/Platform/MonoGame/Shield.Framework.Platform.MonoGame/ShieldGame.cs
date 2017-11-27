using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shield.Framework.Extensions;

namespace Shield.Framework.Platform
{
    public sealed class ShieldGame : Game
    {
        public event EventHandler OnPreInitialize;
        public event EventHandler OnInitialize;
        public event EventHandler OnPostInitialize;

        public event EventHandler OnPreLoad;
        public event EventHandler OnLoad;
        public event EventHandler OnPostLoad;

        public event EventHandler OnUnload;

        public event EventHandler<GameTimeEventArgs> OnPreUpdate;
        public event EventHandler<GameTimeEventArgs> OnUpdate;
        public event EventHandler<GameTimeEventArgs> OnPostUpdate;

        public event EventHandler<GameTimeEventArgs> OnPreDraw;
        public event EventHandler<GameTimeEventArgs> OnDraw;
        public event EventHandler<GameTimeEventArgs> OnPostDraw;

        public event EventHandler OnBeginDraw;
        public event EventHandler OnEndDraw;

        public event EventHandler OnPreExit;
        public event EventHandler OnExit;
        public event EventHandler OnPostExit;

        public event EventHandler OnPreActivate;
        public event EventHandler OnActivate;
        public event EventHandler OnPostActivate;

        public event EventHandler OnPreDeactivate;
        public event EventHandler OnDeactivate;
        public event EventHandler OnPostDeactivate;

        private GraphicsDeviceManager m_deviceManager;

        public GraphicsDeviceManager DeviceManager
        {
            get { return m_deviceManager; }
        }

        public ShieldGame(string contentRootDirectory)
        {
            m_deviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = contentRootDirectory;
        }

        protected override void Initialize()
        {
            OnPreInitialize.Raise(this, null);
            OnInitialize.Raise(this, null);
            base.Initialize();
            OnPostInitialize.Raise(this, null);
        }

        protected override void LoadContent()
        {
            OnPreLoad.Raise(this, null);
            OnLoad.Raise(this, null);
            base.LoadContent();
            OnPostLoad.Raise(this, null);
        }

        protected override void UnloadContent()
        {
            OnUnload.Raise(this, null);
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            OnPreUpdate.Raise(this, new GameTimeEventArgs(gameTime));
            OnUpdate.Raise(this, new GameTimeEventArgs(gameTime));
            base.Update(gameTime);
            OnPostUpdate.Raise(this, new GameTimeEventArgs(gameTime));
        }

        protected override bool BeginDraw()
        {
            OnBeginDraw.Raise(this, null);
            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            OnPreDraw.Raise(this, new GameTimeEventArgs(gameTime));
            OnDraw.Raise(this, new GameTimeEventArgs(gameTime));
            base.Draw(gameTime);
            OnPostDraw.Raise(this, new GameTimeEventArgs(gameTime));
        }

        protected override void EndDraw()
        {
            OnEndDraw.Raise(this, null);
            base.EndDraw();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            OnPreExit.Raise(this, args);
            OnExit.Raise(this, args);
            base.OnExiting(sender, args);
            OnPostExit.Raise(this, args);
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            OnPreActivate.Raise(this, args);
            OnActivate.Raise(this, args);
            base.OnActivated(sender, args);
            OnPostActivate.Raise(this, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            OnPreDeactivate.Raise(this, args);
            OnDeactivate.Raise(this, args);
            base.OnDeactivated(sender, args);
            OnPostDeactivate.Raise(this, args);
        }
    }
}
