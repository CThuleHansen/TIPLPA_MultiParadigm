namespace SchemeLibrary.Loaders 
{
    interface IEvalProxy
    {
        T Evaluate<T>(string procedure, params object[] args);
    }
}
