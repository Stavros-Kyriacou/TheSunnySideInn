using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ScreenViewSnapshot : MonoBehaviour
{
    [MenuItem("Tools/Capture Scene View")]
    static void CaptureSceneView()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;

        int width = sceneView.camera.pixelWidth;
        int height = sceneView.camera.pixelHeight;

        Texture2D capture = new Texture2D(width, height);

        sceneView.camera.Render();

        RenderTexture.active = sceneView.camera.targetTexture;

        capture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        capture.Apply();

        byte[] bytes = capture.EncodeToPNG();
        string filename = "sceneViewCapture.png";
        File.WriteAllBytes(Application.dataPath + "/Textures/Screenshots/" + filename, bytes);

        AssetDatabase.Refresh();
    }


}
