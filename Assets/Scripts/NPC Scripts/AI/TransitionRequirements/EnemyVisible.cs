﻿using UnityEngine;
using System.Collections.Generic;

public class EnemyVisible : TransitionRequirement
{

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

    public EnemyVisible(GameObject gameObject)
    {
        this.gameObject = gameObject;
		_enemySearcher = gameObject.GetComponentInChildren<EnemySearcher>();
       
    }

    public override bool IsSatisfied()
    {
        if (enemySearcher && enemySearcher.charactersNearby.Count > 0)
        {
            return true;
        }
        return false;
    }
}