namespace SchemeLibrary.Loaders 
{
    public interface ISchemeEvaluator
    {
        T Evaluate<T>(string procedure, params object[] args);
    }
}
