namespace SchemeLibrary
{
    interface IEvalProxy
    {
        T Evaluate<T>(string procedure, params object[] args);
    }
}
