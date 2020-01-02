using System;

namespace Shield.Framework.Environment {
    public interface IRuntimeInformation {
        RuntimeType Runtime { get; }
        Version RuntimeVersion { get; }

        void DetectRuntime();
    }
}