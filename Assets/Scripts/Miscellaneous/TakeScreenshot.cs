using UnityEngine;
using System.Collections;

[DefaultSceneObject("ScreenshotTaker")]
public class TakeScreenshot : MonoBehaviour
{
    private int screenshotCount = 0;

    // Check for screenshot key each frame
    void Update()
    {
        // take screenshot on up->down transition of F9 key
        if (Input.GetKeyDown(KeyCode.F9))
        {
            string screenshotFilename;
            do
            {
                screenshotCount++;
                screenshotFilename = "screenshot" + screenshotCount + ".png";

            } while (System.IO.File.Exists(screenshotFilename));

            Application.CaptureScreenshot(screenshotFilename);
        }
    }
}