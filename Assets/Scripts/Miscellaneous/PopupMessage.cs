using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PopupMessage : MonoBehaviour
{
    private static GUIText _guiText;
    private static GUIText mainGUIText
    {
        get
        {
            if (_guiText == null)
            {
                var go = GameObject.Find("PopupMessages");
                if (!go)
                {
                    go = new GameObject("PopupMessages");
                    go.AddComponent<PopupMessage>();
                    _guiText = go.AddComponent<GUIText>();
                }
                else
                    _guiText = go.GetComponent<GUIText>();
                
            }
            return _guiText;
        }
    }

    private static PopupMessage Main { get { return mainGUIText.gameObject.GetComponent<PopupMessage>(); } }

    [RPC]
    public static void Display(string message, float fadeTime = 2.5f, float r = 1, float g = 1, float b = 1)
    {
        // Set the color / transparency for the text.
        Color c = mainGUIText.material.color;
        c.a = 1f; c.r = r; c.g = g; c.b = b;
        mainGUIText.material.color = c;
        mainGUIText.text = message;
        Main.StartCoroutine(FadeInSeconds(fadeTime));
    }

    private static IEnumerator FadeInSeconds(float seconds)
    {
        yield return new WaitForSeconds(3.0f);
        float remaining = seconds;
        Material material = mainGUIText.material;
        while (remaining > 0)
        {
            Color c = material.color;
            c.a = 1 * (remaining / seconds);
            material.color = c;
            yield return null;
            remaining -= Time.deltaTime;
        }
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