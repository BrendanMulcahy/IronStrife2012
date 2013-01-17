using UnityEngine;

public class EnemySearcher : MonoBehaviour
{
    public EnemyVisible enemyVisible;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!enemyVisible.charactersNearby.Contains(other.transform.root.gameObject) && other.transform.root != this.transform.root)
        {
            if (other.GetComponent<CharacterStats>())
                enemyVisible.charactersNearby.Add(other.transform.root.gameObject);

        }
    }

    void OnTriggerLeave(Collider other)
    {
        if (enemyVisible.charactersNearby.Contains(other.transform.root.gameObject))
            enemyVisible.charactersNearby.Remove(other.transform.root.gameObject);
    }
}