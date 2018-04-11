using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Shield.Framework.Collections;
using Shield.Framework.Extensibility.Exceptions;

namespace Shield.Framework.Extensibility
{
    public class DependencySolver
    {
        private readonly ListDictionary<string, string> dependencyMatrix = new ListDictionary<string, string>();
        private readonly List<string> knownModules = new List<string>();

        public int ModuleCount
        {
            get { return dependencyMatrix.Count; }
        }

        public void AddModule(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, 
                    "The provided String argument {0} must not be null or empty.", "name"));

            AddToDependencyMatrix(name);
            AddToKnownModules(name);
        }

        public void AddDependency(string dependingModule, string dependentModule)
        {
            if (string.IsNullOrEmpty(dependingModule))
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, 
                    "The provided String argument {0} must not be null or empty.", 
                    nameof(dependingModule)), nameof(dependingModule));

            if (string.IsNullOrEmpty(dependentModule))
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, 
                    "The provided String argument {0} must not be null or empty.",
                                  nameof(dependingModule)), nameof(dependingModule));

            if (!knownModules.Contains(dependingModule))
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, 
                    "The provided String argument {0} must not be null or empty.",
                                  nameof(dependingModule)), nameof(dependingModule));

            AddToDependencyMatrix(dependentModule);
            dependencyMatrix.Add(dependentModule, dependingModule);
        }

        private void AddToDependencyMatrix(string module)
        {
            if (!dependencyMatrix.ContainsKey(module))
                dependencyMatrix.Add(module);
        }

        private void AddToKnownModules(string module)
        {
            if (!knownModules.Contains(module))
                knownModules.Add(module);
        }

        public string[] Solve()
        {
            var skip = new List<string>();
            while (skip.Count < dependencyMatrix.Count)
            {
                var leaves = this.FindLeaves(skip);
                if (leaves.Count == 0 && skip.Count < dependencyMatrix.Count)
                    throw new CyclicDependencyException(
                        "At least one cyclic dependency has been found in the module catalog. Cycles in the module dependencies must be avoided.");

                skip.AddRange(leaves);
            }
            skip.Reverse();

            if (skip.Count > knownModules.Count)
            {
                var moduleNames = this.FindMissingModules(skip);
                throw new ModuleDependencyException(
                    moduleNames, 
                    string.Format(CultureInfo.CurrentCulture,
                        "A module declared a dependency on another module which is not declared to be loaded. Missing module(s): {0}",
                        moduleNames));
            }

            return skip.ToArray();
        }

        private string FindMissingModules(IEnumerable<string> skip)
        {
            var missingModules = "";

            foreach (var module in skip)
            {
                if (knownModules.Contains(module))
                    continue;

                missingModules += ", ";
                missingModules += module;
            }

            return missingModules.Substring(2);
        }        

        private ICollection<string> FindLeaves(ICollection<string> skip)
        {
            var result = new List<string>();

            foreach (var precedent in dependencyMatrix.Keys)
            {
                if (skip.Contains(precedent))
                    continue;

                var count = dependencyMatrix[precedent].Count(
                    dependent => 
                        !skip.Contains(dependent));

                if (count == 0)
                    result.Add(precedent);
            }

            return result;
        }
    }
}
