#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Tools.Scripts.Inspector.Multidimensional_Arrays
{
    #if UNITY_EDITOR
    [CustomEditor(typeof(ExampleDropdownArray))]
    public class ExampleEnumArrayInspector:DropdownArrayInspector<ExampleDropdownArray.ExampleEnum>
    {
        protected override ExampleDropdownArray.ExampleEnum GetEnumPopup(int row, int column)
        {
            return (ExampleDropdownArray.ExampleEnum) EditorGUILayout.EnumPopup(_arrayData.Grid[row].ArrayColumns[column]);
        }
    }
    #endif
}