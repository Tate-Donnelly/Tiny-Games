using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Tools.Scripts.Inspector.Multidimensional_Arrays
{
    [CreateAssetMenu(fileName = "New Test Grid", menuName = "Assets/Tools/Grid")]
    public class SampleArray: ArrayData<State>
    {
        
    }

    [Serializable]
    public enum State
    {
        Disabled,
        Enabled,
        Unavailable
    }
}