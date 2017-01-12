using System;
using UnityEditor;

namespace EcsRx.Unity.Helpers.EditorInputs
{
    public class EnumEditorInput : SimpleEditorInput<Enum>
    {
        protected override Enum CreateTypeUI(string label, Enum value)
        { return EditorGUILayout.EnumPopup(label, value); }
    }
}