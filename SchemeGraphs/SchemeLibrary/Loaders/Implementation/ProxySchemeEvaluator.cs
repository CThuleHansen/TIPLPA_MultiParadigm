using System.Linq;

namespace SchemeLibrary.Loaders.Implementation
{
    /// <summary>
    /// This class is serves as a proxy for the IronScheme evaluation of scheme. 
    /// </summary>
    public class ProxySchemeEvaluator : ISchemeEvaluator
    {
        public T Evaluate<T>(string procedure, params object[] args)
        {
            var result = IronScheme.RuntimeExtensions.Eval<T>(procedure, args);
            return (T) result; //TODO: InvalidCastException will be thrown if the types doesn't match
        }

        public void Evaluate(string procedure, params object[] args)
        {
            IronScheme.RuntimeExtensions.Eval(procedure, args);                    
        }
    }
}
