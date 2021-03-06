﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutomaticVerticalSize))]
public class AutomaticVerticalSizeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawDefaultInspector();

        if(GUILayout.Button("Recalc Size"))
        {
            AutomaticVerticalSize myScript = (AutomaticVerticalSize)target;
            myScript.AdjustSize();
        }
    }
}
