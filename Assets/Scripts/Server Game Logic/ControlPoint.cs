using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public float NeutralSpawnRadius = 100f;
    public int numNPCsPerWave = 25;
    public string npcType = "AssaultSkeleton";

    void OnMasterGameLogicAdded()
    {
        NPCManager.Main.AddControlPoint(this);
    }

    internal void SpawnNeutralWave()
    {
        for (int g = 0; g < numNPCsPerWave; g++)
        {
            var direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            direction.Normalize();
            var locationToSpawn = this.transform.position + direction * NeutralSpawnRadius;
            Debug.Log("Will spawn a neutral at " + locationToSpawn);

            var newNPC = NPCManager.Main.ServerSpawnNPC(npcType, locationToSpawn);
            newNPC.GetComponent<NeutralWaveBehaviour>().Target = this.gameObject;
        }
    }
}