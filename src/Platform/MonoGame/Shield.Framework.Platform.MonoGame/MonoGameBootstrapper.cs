#region Usings
using System;
using Microsoft.Xna.Framework.Graphics;
using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.IO.FileSystems;
#endregion

namespace Shield.Framework.Platform
{
    public abstract class MonoGameBootstrapper : Bootstrapper
    {
        #region Members
        protected string m_contentRootDirectory;
        protected ShieldGame m_game;
        protected GraphicsDevice m_graphicsDevice;
        #endregion

        protected MonoGameBootstrapper() : this("Content") { }

        protected MonoGameBootstrapper(string contentRootDirectory)
        {
            m_contentRootDirectory = contentRootDirectory;
        }

        #region Methods
        public override void Run()
        {
            base.Run();
            m_game.Run();
        }

        public void Exit()
        {
            m_game.Exit();
        }
        #endregion

        protected override void CreatePlatform()
        {
            base.CreatePlatform();
            m_game = new ShieldGame(m_contentRootDirectory);
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            m_container.Register<ILocalApplicationFileSystem, TitleContainerFileSystem>();
            m_container.Register<IMonoGamePlatformServices, MonoGamePlatformServices>();
            m_container.Register<IMonoGameStorageProvider, MonoGameStorageProvider>();
            m_container.Register<IServiceProvider>(m_game.Services);
            m_container.Register(m_game);
            m_container.Register(m_game.DeviceManager);
        }

        protected override void RegisterApplicationEvents()
        {
            base.RegisterApplicationEvents();
            m_game.OnPreInitialize += OnGamePreInitialize;
            m_game.OnInitialize += OnGameInitialize;
            m_game.OnPostInitialize += OnGamePostInitialize;

            m_game.OnPreLoad += OnGamePreLoad;
            m_game.OnLoad += OnGameLoad;
            m_game.OnPostLoad += OnGamePostLoad;

            m_game.OnUnload += OnGameUnload;

            m_game.OnPreUpdate += OnGamePreUpdate;
            m_game.OnUpdate += OnGameUpdate;
            m_game.OnPostUpdate += OnGamePostUpdate;

            m_game.OnPreDraw += OnGamePreDraw;
            m_game.OnDraw += OnGameDraw;
            m_game.OnPostDraw += OnGamePostDraw;

            m_game.OnBeginDraw += OnGameBeginDraw;
            m_game.OnEndDraw += OnGameEndDraw;

            m_game.OnPreExit += OnGamePreExit;
            m_game.OnExit += OnGameExit;
            m_game.OnPostExit += OnGamePostExit;

            m_game.OnPreActivate += OnGamePreActivate;
            m_game.OnActivate += OnGameActivate;
            m_game.OnPostActivate += OnGamePostActivate;

            m_game.OnPreDeactivate += OnGamePreDeactivate;
            m_game.OnDeactivate += OnGameDeactivate;
            m_game.OnPostDeactivate += OnGamePostDeactivate;
        }

        protected override void Dispose(bool disposing)
        {
            m_game.Dispose();
            base.Dispose(disposing);
        }

        #region Initialize                
        protected virtual void OnGamePreInitialize(object sender, EventArgs e) { }

        protected virtual void OnGameInitialize(object sender, EventArgs e) { }

        protected virtual void OnGamePostInitialize(object sender, EventArgs e) { }
        #endregion

        #region Load
        protected virtual void OnGamePreLoad(object sender, EventArgs e) { }

        protected virtual void OnGameLoad(object sender, EventArgs e) { }

        protected virtual void OnGamePostLoad(object sender, EventArgs e) { }

        protected virtual void OnGameUnload(object sender, EventArgs e) { }
        #endregion

        #region Update
        protected virtual void OnGamePreUpdate(object sender, GameTimeEventArgs e) { }

        protected virtual void OnGameUpdate(object sender, GameTimeEventArgs e) { }

        protected virtual void OnGamePostUpdate(object sender, GameTimeEventArgs e) { }
        #endregion

        #region Draw
        protected virtual void OnGamePreDraw(object sender, GameTimeEventArgs e) { }

        protected virtual void OnGameDraw(object sender, GameTimeEventArgs e) { }

        protected virtual void OnGamePostDraw(object sender, GameTimeEventArgs e) { }

        protected virtual void OnGameBeginDraw(object sender, EventArgs e) { }

        protected virtual void OnGameEndDraw(object sender, EventArgs e) { }
        #endregion

        #region Exit
        protected virtual void OnGamePreExit(object sender, EventArgs e) { }

        protected virtual void OnGameExit(object sender, EventArgs e) { }

        protected virtual void OnGamePostExit(object sender, EventArgs e) { }
        #endregion

        #region Activate        
        protected virtual void OnGamePreActivate(object sender, EventArgs e) { }

        protected virtual void OnGameActivate(object sender, EventArgs e) { }

        protected virtual void OnGamePostActivate(object sender, EventArgs e) { }
        #endregion

        #region Deactivate
        protected virtual void OnGamePreDeactivate(object sender, EventArgs e) { }

        protected virtual void OnGameDeactivate(object sender, EventArgs e) { }

        protected virtual void OnGamePostDeactivate(object sender, EventArgs e) { }
        #endregion
    }
}