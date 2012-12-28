using UnityEngine;
class TeleportCommand : ConsoleCommand
{

    public override void Execute(params string[] parameters)
    {
        if (parameters.Length == 0)
        {
            Debug.Log("invalid teleport target name.");
            return;
        }
        else if (parameters.Length == 1)
        {
            GameObject object1 = Util.MyLocalPlayerObject;
            GameObject object2 = GameObject.Find(parameters[0]);
            if (object2 == null)
            {
                Debug.Log("The gameObject \"" + parameters[0] + "\" could not be found.");
                return;
            }
            object1.transform.position = object2.transform.position;
            object1.transform.MoveToWorldGroundPosition(object1.transform.position);

        }
        else if (parameters.Length == 2)
        {
            GameObject object1 = GameObject.Find((string)parameters[0]);
            GameObject object2 = GameObject.Find((string)parameters[1]);
            if (object1 == null || object2 == null)
            {
                Debug.Log("One or both gameObjects were not found.");
                return;
            }
            object1.transform.position = object2.transform.position;
            object1.transform.MoveToWorldGroundPosition(object1.transform.position);
        }
        else if (parameters.Length == 3)
        {
            GameObject object1 = Util.MyLocalPlayerObject;
            object1.transform.position = new Vector3(float.Parse(parameters[0]), float.Parse(parameters[1]), float.Parse(parameters[2]));

        }
    }

    public override string[] Names
    {
        get { string[] names = { "teleport", "tp" }; return names; }
    }

    public override string HelpMessage
    {
        get { return "[Server Only] Teleports objects through the world.\nUSAGE:\ntp <Object1> <Object2> Teleports Object1 to Object2.\ntp <ObjectDestination> Teleports your gameObject to the destination.\ntp <x> <y> <z> Teleports you to the given coordinates."; }
    }
}