using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PdfViewer
{
    /// <summary>
    /// Embedded resource loader.
    /// </summary>
    public static class EmbeddedResourceLoader
    {
        /// <summary>
        /// A list of key value pairs of assemblies and resource names.
        /// </summary>
        static readonly List<KeyValuePair<Assembly, string[]>> _assemblies;

        /// <summary>
        /// Initializes the <see cref="T:Aurora.EmbeddedResourceLoader"/> class.
        /// </summary>
        static EmbeddedResourceLoader()
        {
            _assemblies = new List<KeyValuePair<Assembly, string[]>>();
        }

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        public static void LoadAssembly(Assembly assembly)
        {
            if (_assemblies.Any(kvp => kvp.Key == assembly))
                return;

            _assemblies.Add(new KeyValuePair<Assembly, string[]>(assembly, assembly.GetManifestResourceNames()));
        }

        /// <summary>
        /// Load the specified resource name.
        /// </summary>
        /// <returns>The resource as a stream</returns>
        /// <param name="name">Takes a string representing the name of the embeded resource.</param>
        public static Stream Load(string name)
        {
            Stream stream = null;

            foreach (var kvp in _assemblies)
            {
                var foundResource = kvp.Value.FirstOrDefault(n => n.EndsWith(name, StringComparison.OrdinalIgnoreCase));
                if (foundResource != null)
                {
                    stream = kvp.Key.GetManifestResourceStream(foundResource);
                    if (stream != null)
                        break;
                }
            }

            return stream ?? Stream.Null;
        }
    }
}
