namespace Shield.Framework.Services.LifeCycle
{
    public class ApplicationStateChangedEventArgs : ApplicationStateChangeEventArgs
    {
        #region Members
        private readonly ApplicationState m_previousState;
        #endregion

        #region Properties
        public ApplicationState PreviousState
        {
            get { return m_previousState; }
        }
        #endregion

        public ApplicationStateChangedEventArgs(ApplicationState currentState, ApplicationState requestedState, ApplicationState previousState) :
            base(currentState, requestedState)
        {
            m_previousState = previousState;
        }
    }
}