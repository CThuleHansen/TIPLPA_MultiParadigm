using System.IO;
using IronScheme;

namespace SchemeLibrary.Loaders.Implementation
{
    /// <summary>
    /// This class simply loads the scheme file as text and evaluates it using IronScheme. 
    /// This is to keep the interop between scheme and C# as simple as possible.
    /// IronScheme has a bit of a footprint for the interop - so we will use this instead.
    /// </summary>
    public class SchemeLoader : ISchemeLoader
    {
        public void Import(string filename)
        {
            if (File.Exists(filename))
            {
                var lib = File.ReadAllText(filename);
                lib.Eval();
            }
        }
    }
}
