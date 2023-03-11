using System;

namespace Observer
{
    public interface IObservable
    {
        event Action<object, object> OnChanged;
    }
}
