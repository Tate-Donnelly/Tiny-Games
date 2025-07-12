using System;
using UnityEngine;

namespace Tools.Scripts.Inspector.Multidimensional_Arrays
{
    [CreateAssetMenu(fileName = "New Test Grid", menuName = "Assets/Tools/Grid")]
    public class SampleArray: ArrayData<Active>
    {
        
    }

    [Serializable]
    public enum Active
    {
        Enabled,
        Disabled,
        Unavailable
    }
}