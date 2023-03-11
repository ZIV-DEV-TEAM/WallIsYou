using System;

namespace Observer
{
    public class ObservableVariable<T> : IObservable
    {
        public event Action<object, object> OnChanged;
        private T _value;
        public T Value { 
            get { return _value; }
            set {
                T oldValue = _value;
                _value = value; 
                OnChanged?.Invoke(oldValue, value);
            }
        }
        public ObservableVariable()
        {
            Value = default;
        }

        public ObservableVariable(T value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
