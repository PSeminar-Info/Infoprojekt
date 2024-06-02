using System.IO;
using UnityEngine;

namespace Terrain.Hauptdorf.Demo.Low_poly_package.Scripts
{
    public class ScreenshotHandler : MonoBehaviour
    {
        private static ScreenshotHandler _instance;

        private Camera _myCamera;
        private bool _takeScreenshotOnNextFrame;

        private void Awake()
        {
            _instance = this;
            _myCamera = gameObject.GetComponent<Camera>();
        }

        private void OnPostRender()
        {
            if (_takeScreenshotOnNextFrame)
            {
                _takeScreenshotOnNextFrame = false;
                var renderTexture = _myCamera.targetTexture;

                var renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
                var rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
                renderResult.ReadPixels(rect, 0, 0);

                var byteArray = renderResult.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
                Debug.Log("Saved CameraScreenshot.png");

                RenderTexture.ReleaseTemporary(renderTexture);
                _myCamera.targetTexture = null;
            }
        }


        private void TakeScreenshot(int width, int height)
        {
            _myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
            _takeScreenshotOnNextFrame = true;
        }

        public static void TakeScreenshot_Static(int width, int height)
        {
            _instance.TakeScreenshot(width, height);
        }
    }
}