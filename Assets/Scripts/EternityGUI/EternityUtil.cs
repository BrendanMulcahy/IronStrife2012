namespace EternityGUI
{
    using System.Collections.Generic;
    using UnityEngine;
    using System.Text.RegularExpressions;

    public static class EternityUtil
    {
        public static Vector3 ScreenToViewport(this Vector3 v)
        {
            return new Vector3(v.x / Screen.width, v.y / Screen.height, v.z);
        }

        public static int GetElementLayer(GameObject go)
        {
            var t = go.transform;
            var root = go.transform.root;
            int toReturn = -1;
            if (go.GetComponent<RenderTextureElement>())
                toReturn++;

            while (t != root)
            {
                t = t.parent;
                toReturn--;
            }
            return toReturn;
        }

        private static Camera _inventoryCamera;
        public static Camera InventoryCamera
        {
            get
            {
                if (!_inventoryCamera)
                {
                    _inventoryCamera = Util.MyLocalPlayerObject.transform.FindChild("InventoryPreviewCamera").GetComponent<Camera>();
                }
                return _inventoryCamera;
            }
        }

        /// <summary>
        /// Returns all GUIElements under the mouse, in order from front to back.
        /// </summary>
        /// <param name="guiLayer">The layer to test the hit on</param>
        /// <param name="position">The position in screen space (Input.mouseposition works)</param>
        /// <returns>All GUIElements under the given position, in order from front to back</returns>
        public static GUIElement[] HitTestAll(this GUILayer guiLayer, Vector3 position)
        {
            var toReturn = new List<GUIElement>();
            var previousPositions = new List<Vector3>();
            GUIElement element;
            while ((element = guiLayer.HitTest(position)) != null)
            {
                toReturn.Add(element);
                previousPositions.Add(element.gameObject.transform.position);
                element.transform.position = new Vector3(-100, -100, -100);
            }

            for (int g = 0; g < previousPositions.Count; g++)
            {
                toReturn[g].transform.position = previousPositions[g];
            }
            return toReturn.ToArray();
        }

        private static GUILayer _mainGuiLayer;
        public static GUILayer MainGUILayer
        {
            get
            {
                if (!_mainGuiLayer)
                {
                    _mainGuiLayer = Camera.main.GetComponent<GUILayer>();
                }
                return _mainGuiLayer;
            }

        }

        /// <summary>
        /// Returns all GUIElements under the mouse, in order from front to back, using the default guiLayer.
        /// </summary>
        /// <param name="guiLayer">The layer to test the hit on</param>
        /// <param name="position">The position in screen space (Input.mouseposition works)</param>
        /// <returns>All GUIElements under the given position, in order from front to back</returns>
        public static GUIElement[] HitTestAll(Vector3 position)
        {
            var guiLayer = MainGUILayer;
            var toReturn = new List<GUIElement>();
            var previousPositions = new List<Vector3>();
            GUIElement element;
            while ((element = guiLayer.HitTest(position)) != null)
            {
                toReturn.Add(element);
                previousPositions.Add(element.gameObject.transform.position);
                element.transform.position = new Vector3(-100, -100, -100);
            }

            for (int g = 0; g < previousPositions.Count; g++)
            {
                toReturn[g].transform.position = previousPositions[g];
            }
            return toReturn.ToArray();
        }

        const string HTML_TAG_PATTERN = "<.*?>";
        public static string StripHTML(string inputString)
        {
            return Regex.Replace
              (inputString, HTML_TAG_PATTERN, string.Empty);
        }

        public static Rect FormatGuiTextArea(GUIText guiText, float maxAreaWidth)
        {
            string[] words = guiText.text.Split(' ');
            string result = "";
            LinkedList<int> lineBreaks = new LinkedList<int>();

            Rect textArea = new Rect();

            for (int i = 0; i < words.Length; i++)
            {
                if (Regex.IsMatch(words[i], HTML_TAG_PATTERN))
                {
                    guiText.text = (result + words[i] + " ");
                    result = guiText.text;
                    continue;
                }
                // set the gui text to the current string including new word
                guiText.text = (result + words[i] + " ");
                // measure it
                textArea = guiText.GetScreenRect();
                // if it didn't fit, put word onto next line, otherwise keep it
                if (textArea.width > maxAreaWidth)
                {
                    result += ("\n" + words[i] + " ");
                    lineBreaks.AddLast(i);
                }
                else
                {
                    result = guiText.text;
                }
            }
            guiText.text = guiText.text.Replace('_', ' ');
            return textArea;
        }

        public static string ColorToHex(this Color color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        public static Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }

        private static string GetHex(int dec)
        {
            var alpha = "0123456789ABCDEF";
            var toReturn = "" + alpha[dec];
            return toReturn;
        }

        public static string RGBToHex(Color color)
        {
            float red = color.r * 255f;
            float green = color.g * 255f;
            float blue = color.b * 255f;
            //float alpha = color.a * 255f;


            var a = GetHex((int)Mathf.Floor(red / 16f));
            var b = GetHex((int)Mathf.Round(red % 16f));
            var c = GetHex((int)Mathf.Floor(green / 16f));
            var d = GetHex((int)Mathf.Round(green % 16f));
            var e = GetHex((int)Mathf.Floor(blue / 16f));
            var f = GetHex((int)Mathf.Round(blue % 16f));
            //var g = GetHex((int)Mathf.Floor(alpha / 16f));
            // var h = GetHex((int)Mathf.Round(alpha % 16f));

            var z = a + b + c + d + e + f;

            return z;
        }

        internal static Tooltip CurrentTooltip;
    }
}