using System;
public class PriorityList<T>
{
    #region private variables
    private T[] data;
    private int[] dataPriority;

    private int size = 0;
    private int capacity;
    #endregion

    #region public variables
    public int Size { get { return size; } }
    public bool IsEmpty { get { return size == 0; } }
    /// <summary>
    /// Returns the item with min priority without removing it from the list.
    /// </summary>
    public T Min
    {
        get
        {
            if (IsEmpty) ThrowIndexOutOfRange(-1);
            return data[0];
        }
    }
    /// <summary>
    /// Returns the item with max priority without removing it from the list.
    /// </summary>
    public T Max
    {
        get
        {
            if (IsEmpty) ThrowIndexOutOfRange(-1);
            return data[size - 1];
        }
    }

    #endregion
    public PriorityList(int initialCapacity = 8)
    {
        if (initialCapacity < 1) initialCapacity = 1;
        this.capacity = initialCapacity;

        data = new T[initialCapacity];
        dataPriority = new int[initialCapacity];
    }
    #region public methods
    /// <summary>
    /// Add an item and their priority to the list.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="priority"></param>
    public void Add(T item, int priority)
    {
        if (size == capacity) Resize();
        data[size] = item;
        dataPriority[size] = priority;
        size++;

        int childIndex = size - 1;
        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1);
            if (dataPriority[childIndex].CompareTo(dataPriority[parentIndex]) >= 0) break;

            var tempPriority = dataPriority[childIndex];
            var tempData = data[childIndex];

            dataPriority[childIndex] = dataPriority[parentIndex];
            dataPriority[parentIndex] = tempPriority;

            data[childIndex] = data[parentIndex];
            data[parentIndex] = tempData;

            childIndex = parentIndex;
        }
    }

    /// <summary>
    /// Remove an item and their priority from the list.
    /// </summary>
    /// <param name="item">Item to remove.</param>
    public void Remove(T item)
    {
        //if (!Contains(item)) return;
        int index = -1;

        for (int i = 0; i < size; i++)
        {
            if (data[i].Equals(item))
            {
                index = i; break;
            }
        }
        ThrowIndexOutOfRange(index);
        for (int i = index; i < size - 1; i++)
        {
            data[i] = data[i + 1];
            dataPriority[i] = dataPriority[i + 1];
        }
        data[size - 1] = default(T);
        dataPriority[size - 1] = default(int);
        size--;
    }

    /// <summary>
    /// Returns true or false whether the list contains the item.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Contains(T value)
    {
        for (int i = 0; i < size; i++)
        {
            if (data[i].Equals(value)) return true;
        }
        return false;
    }

    /// <summary>
    /// Clears the list.
    /// </summary>
    public void Clear()
    {
        data = new T[capacity];
        dataPriority = new int[capacity];
        size = 0;
    }

    /// <summary>
    /// Returns and removes the item from the list with the minimum priority.
    /// </summary>
    /// <returns></returns>
    public T GetMin()
    {
        var item = data[0];
        Remove(item);
        return item;
    }

    /// <summary>
    /// Returns and removes the item from the list with the maximum priority.
    /// </summary>
    /// <returns></returns>
    public T GetMax()
    {
        var item = data[size - 1];
        Remove(item);
        return item;
    }

    /// <summary>
    /// Change the priority of an item in the list;
    /// </summary>
    /// <param name="item">Item in list</param>
    /// <param name="priority">New priority</param>
    public void ChangePriority(T item, int priority)
    {
        int index = -1;
        for (int i = 0; i < size; i++)
        {
            if (data[i].Equals(item))
            {
                index = i;
                break;
            }
        }
        ThrowIndexOutOfRange(index);
        Remove(item);
        Add(item, priority);
    }

    /// <summary>
    /// Returns the priority of the item.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetPriority(T item)
    {
        int index = -1;
        for (int i = 0; i < size; i++)
        {
            if (data[i].Equals(item))
            {
                index = i;
                break;
            }
        }
        ThrowIndexOutOfRange(index);
        return dataPriority[index];
    }

    public override string ToString()
    {
        string s = "[";
        for (int i = 0; i < size; i++)
        {
            s += "{" + data[i].ToString() + " , " + dataPriority[i].ToString() + "}" + ", ";
        }
        s = s.Substring(0, s.Length - 2);
        s += "]";
        return s;
    }
    #endregion

    #region private methods
    private void Resize()
    {
        T[] resizedData = new T[capacity * 2];
        int[] resizedPriority = new int[capacity * 2];
        for (int i = 0; i < capacity; i++)
        {
            resizedData[i] = data[i];
            resizedPriority[i] = dataPriority[i];
        }
        data = resizedData;
        dataPriority = resizedPriority;
        capacity = capacity * 2;
    }
    private void ThrowIndexOutOfRange(int index)
    {
        if (index > size - 1 || index < 0)
        {
            throw new ArgumentOutOfRangeException(string.Format("Index out of range!", size));
        }
    }
    #endregion
}
