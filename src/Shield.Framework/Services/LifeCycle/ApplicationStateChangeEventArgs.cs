#region Usings
using System;
#endregion

namespace Shield.Framework.Services.LifeCycle
{
    public class ApplicationStateChangeEventArgs : EventArgs
    {
        #region Members
        private readonly ApplicationState m_currentState;
        private readonly ApplicationState m_requestedState;
        #endregion

        #region Properties
        public ApplicationState CurrentState
        {
            get { return m_currentState; }
        }

        public ApplicationState RequestedState
        {
            get { return m_requestedState; }
        }
        #endregion

        public ApplicationStateChangeEventArgs(ApplicationState currentState, ApplicationState requestedState)
        {
            m_currentState = currentState;
            m_requestedState = requestedState;
        }
    }
}