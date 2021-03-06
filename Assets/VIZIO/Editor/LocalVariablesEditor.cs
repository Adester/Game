//Created by Neodrop
//mailto : neodrop@unity3d.ru
using UnityEngine;
using UnityEditor;
using System.Collections;
using Editor;
//#pragma warning disable

[CustomEditor(typeof(Antares.Vizio.Blocks.LocalVariableInUnityInspector))]
public class LocalVariablesEditor : EditorBase<Antares.Vizio.Blocks.LocalVariableInUnityInspector> {

    public override void OnInspectorGUI()
    {
        Antares.Vizio.Blocks.LocalVariableInUnityInspector inspector =
            target as Antares.Vizio.Blocks.LocalVariableInUnityInspector;

        if (inspector)
        {
            foreach (var t in inspector.guiDelegats)
                t();
        }
    }
}
