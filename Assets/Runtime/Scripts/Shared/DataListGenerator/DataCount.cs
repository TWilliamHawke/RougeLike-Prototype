using UnityEngine;

[System.Serializable]
public abstract class DataCount<T>
{
    [SerializeField] T _item;
    [SerializeField] int _count = 1;

    protected DataCount(T item)
    {
        _item = item;
    }

    protected DataCount(T item, int count)
    {
        _item = item;
        _count = count;
    }

    public T item => _item;
    public int count => _count;

    public void AddToStack()
    {
        _count++;
    }

    public void RemoveFromStack()
    {
        if (count == 0) return;
        _count--;
    }

    public void IncreaseCountBy(int num)
    {
        _count += num;
    }

    public void DecreaseCountBy(int num)
    {
        _count = Mathf.Max(0, _count - num);
    }

    protected void SetCount(int newCount)
    {
        _count = newCount;
    }

}

