using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrayData<T> : ScriptableObject
{
    [Min(1f)] public int RowsNum;
    [Min(1f)] public int ColumnsNum;
    [HideInInspector] public ArrayRow<T>[] Grid;
}

[Serializable]
public class ArrayRow<T>
{
    public T[] ArrayColumns;
}