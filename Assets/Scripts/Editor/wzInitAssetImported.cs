using UnityEngine;


using UnityEditor;


using System.Xml;


using System.Collections.Generic;


/// <summary>


/// Import ScaleFactor and Animations From XML File


/// Shortkey : Shift + Alt + a


/// </summary>


public class wzInitAssetImported : ScriptableWizard
{


    public GameObject AssetObject = null;





    [MenuItem("Ipak Games/Wizard/Init Asset Imported #&a", false, 1)]


    static void CreateWindow()
    {


        ScriptableWizard.DisplayWizard("Init Asset Imported",


            typeof(wzInitAssetImported), "Close", "Get XML File");


    }


    void OnWizardUpdate()
    {


        if (AssetObject == null)
        {


            isValid = false;


            errorString = "Set FBX File.";


        }


        else
        {


            isValid = true;


            errorString = "";


        }


    }


    // Called when you press the "Info" button.


    void OnWizardOtherButton()
    {


        XmlDocument doc = new XmlDocument();


        string path = EditorUtility.OpenFilePanel("Settings", "", "xml");


        doc.Load(path);





        ModelImporter importer = (ModelImporter)ModelImporter.GetAtPath(AssetDatabase.GetAssetPath(AssetObject.GetInstanceID()));


        importer.globalScale = float.Parse(doc.SelectSingleNode("Asset").Attributes["ScaleFactor"].Value);





        List<ModelImporterClipAnimation> Animations = new List<ModelImporterClipAnimation>();





        foreach (XmlNode node in doc.SelectNodes("Asset/Animations/Clip"))
        {


            ModelImporterClipAnimation ClipAnimation = new ModelImporterClipAnimation();


            ClipAnimation.name = node.Attributes["name"].Value.ToString();


            ClipAnimation.firstFrame = (int.Parse(node.Attributes["Start"].Value));


            ClipAnimation.lastFrame = (int.Parse(node.Attributes["End"].Value));


            ClipAnimation.wrapMode = (WrapMode)System.Enum.Parse(typeof(WrapMode), node.Attributes["WarpMode"].Value, true);
            
            
            ClipAnimation.loopPose = bool.Parse(node.Attributes["Loop"].Value);

            Animations.Add(ClipAnimation);


        }





        importer.clipAnimations = Animations.ToArray();


        AssetDatabase.WriteImportSettingsIfDirty((AssetDatabase.GetAssetPath(AssetObject.GetInstanceID())));


        AssetDatabase.Refresh();


        helpString = "Imported " + Animations.Count.ToString() + "Animations";


    }


    void OnWizardCreate()
    {


        return;


    }


}