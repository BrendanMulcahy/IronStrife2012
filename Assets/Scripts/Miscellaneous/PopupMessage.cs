using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[DefaultSceneObject("PopupMessage", "PopupMessage")]
public class PopupMessage : MonoBehaviour
{
    private static GUIText _guiText;
    private static GUIText mainGUIText
    {
        get
        {
            if (_guiText == null)
            {
                var go = GameObject.Find("PopupMessage");
                if (!go)
                {
                    go = new GameObject("PopupMessage");
                    go.AddComponent<PopupMessage>();
                    _guiText = go.AddComponent<GUIText>();
                }
                else
                    _guiText = go.GetComponent<GUIText>();
                
            }
            return _guiText;
        }
    }
    private static GUIText _shadowGUIText;
    private static GUIText shadowGUIText
    {
        get
        {
            if (_shadowGUIText == null)
            {
                var go = GameObject.Find("PopupMessage");
                _shadowGUIText = go.transform.FindChild("ShadowText").GetComponent<GUIText>();
            }
            return _shadowGUIText;
        }
    }

    private static PopupMessage Main { get { return mainGUIText.gameObject.GetComponent<PopupMessage>(); } }

    void Start()
    {
        shadowGUIText.material.color = Color.black;
    }

    [RPC]
    public static void Display(string message, float fadeTime = 2.5f, float r = 1, float g = 1, float b = 1)
    {
        Main.StopAllCoroutines();
        // Set the color / transparency for the text.
        Color c = mainGUIText.material.color;
        c.a = 1f; c.r = r; c.g = g; c.b = b;
        mainGUIText.material.color = c;
        mainGUIText.text = message;

        c = shadowGUIText.material.color;
        c.a = 1f;
        shadowGUIText.material.color = c;
        shadowGUIText.text = message;
        Main.StartCoroutine(FadeInSeconds(fadeTime));
    }

    private static IEnumerator FadeInSeconds(float seconds)
    {
        yield return new WaitForSeconds(3.0f);
        float remaining = seconds;
        Material material = mainGUIText.material;
        Material shadowMat = shadowGUIText.material;
        while (remaining > 0)
        {
            Color c = material.color;
            Color c2 = shadowMat.color;
            c.a = 1 * (remaining / seconds);
            c2.a = 1 * (remaining / seconds);
            material.color = c;
            shadowMat.color = c2;
            yield return null;
            remaining -= Time.deltaTime;
        }
        Color col = material.color;
        col.a = 0f;
        Color col2 = shadowMat.color;
        col2.a = 0f;
        material.color = col;
        shadowMat.color = col2;
    }

    internal static void NetworkDisplay(string p, float fadeTime = 2.5f)
    {
        if (Network.isClient) return;


        Main.networkView.RPC("ClientDisplay", RPCMode.All, p, fadeTime);
    }

    [RPC]
    void ClientDisplay(string p, float fadeTime = 2.5f)
    {
        PopupMessage.Display(p, fadeTime);
    }

    public static void LocalDisplay(string message, float fadeTime = 2.5f, float r = 1, float g = 1, float b = 1)
    {
        Display(message, fadeTime, r, g, b);
    }
}