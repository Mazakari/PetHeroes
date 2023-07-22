using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ScreenCapture.CaptureScreenshot("SH.png");
        }
    }
}
