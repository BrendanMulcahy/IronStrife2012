using UnityEngine;
using System.Collections;

public class CameraFade : MonoBehaviour {

    Texture2D solidTexture;
    Rect screenRect;
    public Color color;

	// Use this for initialization
	void Start () {
        solidTexture = Resources.Load("BlackSquare") as Texture2D;
        color = Color.black;
        color.a = 0;
	}

    public void FadeToSolid(float duration)
    {
        this.color.a = 0;
        StopCoroutine("FadeToTransparentRoutine");
        StartCoroutine(FadeToSolidRoutine(duration));
    }

    private IEnumerator FadeToSolidRoutine(float duration)
    {
        float totalTime = 0;
        while (totalTime < duration)
        {
            totalTime += Time.deltaTime;
            color.a = (totalTime / duration) * 1f;
            yield return null;
        }
        yield break;
    }

    public void FadeToTransparent(float duration)
    {
        StopCoroutine("FadeToSolidRoutine");
        StartCoroutine(FadeToTransparentRoutine(duration));
    }

    private IEnumerator FadeToTransparentRoutine(float duration)
    {
        float totalTime = 0;
        var startAlpha = color.a;
        while (totalTime < duration)
        {
            totalTime += Time.deltaTime;
            color.a = startAlpha - ((totalTime / duration) * (1-startAlpha));
            yield return null;
        }
        yield break;
    }
	
	// Update is called once per frame
	void Update () {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
	}

    void OnGUI()
    {
        GUI.color = color;
        GUI.DrawTexture(screenRect, solidTexture);
    }
}
