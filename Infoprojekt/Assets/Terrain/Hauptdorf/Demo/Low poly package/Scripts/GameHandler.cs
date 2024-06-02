using UnityEngine;

namespace Terrain.Hauptdorf.Demo.Low_poly_package.Scripts
{
    public class GameHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) ScreenshotHandler.TakeScreenshot_Static(1024, 768);
        }
    }
}