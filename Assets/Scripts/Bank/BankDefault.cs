using System;
using Observer;
using UnityEngine;

namespace Bank
{
    public abstract class BankDefault : MonoBehaviour
    {
        protected ObservableVariable<int> _bankValue;
        public int Value => _bankValue.Value;
        // Start is called before the first frame update
        void Awake()
        {
            Initialize();
        }
        protected virtual void Initialize()
        {
            _bankValue = new ObservableVariable<int>(0);
        }

        public virtual void Add(int value)
        {
            _bankValue.Value += value;
        }

        public virtual bool TryRemove(int value)
        {
            if (_bankValue.Value >= value)
            {
                _bankValue.Value -= value;
                return true;
            }
            return false;
        }


        public virtual void AddObserver(Action<object, object> OnChanged)
        {
            _bankValue.OnChanged += OnChanged;
        }
        public virtual void RemoveObserver(Action<object, object> OnChanged)
        {
            _bankValue.OnChanged -= OnChanged;
        }
    }
}
