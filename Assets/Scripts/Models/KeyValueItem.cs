using System;

[Serializable]
public class KeyValueItem<TKey, TValue>
{
    public TKey key;
    public TValue value;
}