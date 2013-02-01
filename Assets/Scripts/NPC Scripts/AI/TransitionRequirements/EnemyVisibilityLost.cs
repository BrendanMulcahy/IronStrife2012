using UnityEngine;
using System.Collections;

public class EnemyVisibilityLost : TransitionRequirement {
	
	
	public GameObject gameObject;
	private EnemySearcher _enemySearcher;
	private EnemySearcher enemySearcher
	{
	get {
			if (!_enemySearcher)
			{
				_enemySearcher = gameObject.GetComponentInChildren<EnemySearcher>();
			}
			return _enemySearcher;	
		}
	}
	
	public EnemyVisibilityLost(GameObject gameObject)
	{
		this.gameObject = gameObject;
		_enemySearcher = this.gameObject.GetComponentInChildren<EnemySearcher>();
		
	}
	
	public override bool IsSatisfied()
    {
        if (enemySearcher && enemySearcher.enemiesNearby.Count == 0)
        {
            return true;
        }
        return false;
    }
}
