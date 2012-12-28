using UnityEngine;

public class GuardNPCController : NPCController
{
    protected override void Wandering()
    {
        return;
    }

    protected override System.Collections.IEnumerator CheckforPlayer()
    {
        while (checkingForPlayer)
        {
            yield return new WaitForSeconds(1f);
            //First looks through all gameobjects and finds the player by finding the object with characterstats
            foreach (GameObject gameObj in GameObject.FindObjectsOfType(typeof(GameObject)))
            {

                if (gameObj.GetComponent<ThirdPersonController>() != null)
                {

                    Firstplayer = gameObj;

                    //check to see if player is within a certain distance of player and within 180 degrees of sight
                    Vector3 rayDirection = gameObj.transform.localPosition - this.transform.localPosition;
                    Vector3 enemyDirection = transform.TransformDirection(Vector3.forward);
                    var angleDot = Vector3.Dot(rayDirection, enemyDirection);
                    var playerInFrontOfEnemy = angleDot > 0.0;
                    var playerCloseToEnemy = rayDirection.sqrMagnitude < maxDistanceSquared;

                    if (playerInFrontOfEnemy && playerCloseToEnemy && gameObj.GetCharacterStats().TeamNumber != this.gameObject.GetCharacterStats().TeamNumber)
                    {
                        playerseen = true;
                        currentspeed = followspeed;
                        FollowPlayer();
                        break;
                    }

                }
            }
        }
    }
}
