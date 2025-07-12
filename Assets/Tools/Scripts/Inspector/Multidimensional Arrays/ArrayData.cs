using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrayData<T> : ScriptableObject
{
    [Min(1f)] public int RowsNum=1;
    [Min(1f)] public int ColumnsNum=1;
    [HideInInspector] public ArrayRow<T>[] Grid;
}

[Serializable]
public class ArrayRow<T>
{
    public T[] ArrayColumns;
}