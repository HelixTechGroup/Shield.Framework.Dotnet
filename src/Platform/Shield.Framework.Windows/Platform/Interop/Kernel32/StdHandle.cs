namespace Shield.Framework.Platform.Interop.Kernel32 {
    public enum StdHandle
    {
        /// <summary>
        ///     The standard input device. Initially, this is the console input buffer, CONIN$.
        /// </summary>
        STD_INPUT_HANDLE = unchecked((int) (uint) -10),

        /// <summary>
        ///     The standard output device. Initially, this is the active console screen buffer, CONOUT$.
        /// </summary>
        STD_OUTPUT_HANDLE = unchecked((int) (uint) -11),

        /// <summary>
        ///     The standard error device. Initially, this is the active console screen buffer, CONOUT$.
        /// </summary>
        STD_ERROR_HANDLE = unchecked((int) (uint) -12)
    }
}