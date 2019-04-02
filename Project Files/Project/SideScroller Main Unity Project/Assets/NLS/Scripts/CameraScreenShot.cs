using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraScreenShot : MonoBehaviour {

    public void ScreenShot()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.jpg");
    }
}

//---------------------------------------------------------------------------
//Custom inspector
#if UNITY_EDITOR
[CustomEditor(typeof(CameraScreenShot))]
public class CameraScreenShot_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        CameraScreenShot script = target as CameraScreenShot;

        if (GUILayout.Button("Take ScreenShot"))
        {
            script.ScreenShot();
        }
    }
}
#endif
