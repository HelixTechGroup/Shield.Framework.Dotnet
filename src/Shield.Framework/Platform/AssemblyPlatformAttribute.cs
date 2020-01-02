#region Usings
using System;
using Shield.Framework.Environment;
#endregion

namespace Shield.Framework.Platform
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class AssemblyPlatformAttribute : Attribute
    {
        #region Properties
        public Type DispatcherType { get; private set; }
        public Type EnvironmentChecker { get; }
        public string Name { get; private set; }
        public int Priority { get; private set; }
        public OperatingSystemType RequiredOS { get; private set; }
        public Type ApplicationType { get; private set; }
        #endregion

        public AssemblyPlatformAttribute(OperatingSystemType requiredRuntimePlatform,
                                         int priority,
                                         string name,
                                         Type applicationType,
                                         Type dispatcherType,
                                         Type environmentChecker = null)
        {
            Name = name;
            ApplicationType = applicationType;
            DispatcherType = dispatcherType;
            EnvironmentChecker = environmentChecker;
            RequiredOS = requiredRuntimePlatform;
            Priority = priority;
        }
    }
}