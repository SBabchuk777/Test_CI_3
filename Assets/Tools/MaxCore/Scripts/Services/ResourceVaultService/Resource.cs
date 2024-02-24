using System;

namespace Tools.MaxCore.Scripts.Services.ResourceVaultService
{
    public class Resource<T>
    {
        public ResourceType Type { get; }

        public T Amount
        {
            get => amount;
            set
            {
                var oldValue = amount;
                amount = value;

                if (!Equals(oldValue, amount))
                {
                    OnChanged?.Invoke(value);
                   
                }
            }
        }
        
        public event Action<T> OnChanged;

        private T amount;

        public Resource(ResourceType type, T amountByDefault = default)
        {
            Type = type;
            Amount = amountByDefault;
        }
    }
}