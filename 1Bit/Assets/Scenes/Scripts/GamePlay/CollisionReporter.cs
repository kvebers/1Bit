using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionReporter : MonoBehaviour
{
	private bool		thisHolder = true;
	public bool			isDoor;	//ObjectType. Else DarkPassage.
	public LevelScript	lvlScr;

	void Start()
	{
		//if (lvlScr == null)
		//{
		//	print($"You have forgotten to add the LevelScript to an CollisionReporter Script.\nParent: {transform.parent.name} | This: {gameObject.name}");
		//	enabled = false;
		//}
		//else if (thisHolder)
		if (thisHolder)
		{
			Transform[] tra_child = GetComponentsInChildren<Transform>();
			for (int i = 0; i < tra_child.Length; i++)
			{
				if (tra_child[i] == transform)
					continue ;
				CollisionReporter scr = tra_child[i].gameObject.AddComponent<CollisionReporter>();
				scr.thisHolder = false;
				scr.isDoor = isDoor;
				scr.lvlScr = lvlScr;
			}
			Destroy(this);
		}
		else
		{
			//check if this go has a collider with "isTrigger" on
			BoxCollider coll = GetComponent<BoxCollider>();
			if (coll == null)
			{
				print($"No Collider Detected. Deactivate CollisionReporter Script. isDoor: {isDoor}");
				enabled = false;
			}
			else if (!coll.isTrigger)
			{
				print($"Colliders 'isTrigger' is off. Deactivate CollisionReporter Script. isDoor: {isDoor}");
				enabled = false;
			}
		}
	}

	private void OnTriggerEnter(Collider coll)
	{
		if (coll.CompareTag("Player"))
		{
			if (isDoor)
				lvlScr.TouchedDoor(gameObject);
			else
				lvlScr.TouchedDarkPassage();
		}
	}
}
