using UnityEngine;

[CreateAssetMenu(fileName = "DropdownArray", menuName = "Tools/2D Arrays/Dropdown Array")]
public class ExampleDropdownArray : ArrayData<ExampleDropdownArray.ExampleEnum>
{
    public enum ExampleEnum
    {
        A,
        B,
        C,
        D,
        E,
    }
}
