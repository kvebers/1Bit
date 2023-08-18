using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
	[Header("Gameobjects")]
	public GameObject	go_Startpoint;
	public GameObject	go_BanananaHolder;
	public GameObject	go_ExitBlockerHolder;
	public GameObject	go_DoorsHolder;
	public GameObject	go_DarkPassagesHolder;
	public List<GameObject>	go_Banananas;
	public List<GameObject>	go_ExitBlocker;
	public List<GameObject>	go_Doors;
	public List<GameObject>	go_DarkPassages;

	[Header("Other")]
	public List<GameObject> TouchedDoors;
	public int BananAtExit;
	public int Phase = 1;

	void Start()
	{
		if (go_BanananaHolder && 
			go_ExitBlockerHolder && 
			go_DoorsHolder && 
			go_DarkPassagesHolder)
		{
			GetGameObjects(go_BanananaHolder, go_Banananas);
			GetGameObjects(go_ExitBlockerHolder, go_ExitBlocker);
			GetGameObjects(go_DoorsHolder, go_Doors);
			GetGameObjects(go_DarkPassagesHolder, go_DarkPassages);
		}



		if (!go_Startpoint
			|| go_Doors.Count < 1
			|| go_DarkPassages.Count < 1
			|| go_ExitBlocker.Count < 1
			|| go_Banananas.Count < 1
			|| go_Banananas.Count != go_ExitBlocker.Count)
		{
			print($"Some GameObjects are missing!! Disable level script <{gameObject.name}>.");
			enabled = false;
		}
		DoReset();

		CollisionReporter[] scr = GetComponentsInChildren<CollisionReporter>();
		for (int i = 0; i < scr.Length; i++)
			scr[i].lvlScr = this;
	}

	void GetGameObjects(GameObject parent, List<GameObject> into)
	{
		into.Clear();
		int count = parent.transform.childCount;
		for (int i = 0; i < count; i++)
		{
			//print($"Detected: {parent.transform.GetChild(i).gameObject.name}");
			into.Add(parent.transform.GetChild(i).gameObject);
		}
	}

	public void TouchedDoor(GameObject go)
	{
		if (TouchedDoors.Contains(go))
			TouchedDoors.Remove(go);
		TouchedDoors.Add(go);
	}

	public void TouchedDarkPassage()
	{
		if (TouchedDoors.Count > 0)
		{
			if (Phase == 1)
				DoPhaseOne();
			else if (Phase == 2)
				DoPhaseTwo();
			else
			{
				print($"Phase is not 1 or 2, but {Phase}. Disable script.");
				enabled = false;
			}
			TouchedDoors.Clear();
		}
		else
			DoReset();
	}

	private void BANANAport(GameObject BANANA, GameObject Door)
		=> BANANA.transform.SetPositionAndRotation(Door.transform.position, Door.transform.rotation);

	private void DoPhaseOne()
	{
		//The first mannequin at the exit will move to the last door touched
		if (BananAtExit <= 0)
		{
			print("Phase One didnt work.");
			return ;
		}
		int frontBanan = go_Banananas.Count - BananAtExit;
		for (int i = frontBanan; i < go_Banananas.Count; i++)
		{
			print($"i:{i} | front:{frontBanan} | AtExit:{BananAtExit}");
			if (i == frontBanan)
				BANANAport(go_Banananas[i], TouchedDoors[^1]);	//^1 just replacing "TouchedDoors.Count -1"
			else
				BANANAport(go_Banananas[i], go_ExitBlocker[i - frontBanan - 1]);
		}
		BananAtExit--;
		//if BananAtExit gets to 0, set Phase to 2
	}

	private void DoPhaseTwo()
	{
		print("Phase two yee");
	}

	private void DoReset()
	{
		//Reset Mannequin locations
		//Reset player position(?)
		BananAtExit = go_Banananas.Count;
		for (int i = 0; go_Banananas.Count > i; i++)
		{
			BANANAport(go_Banananas[i], go_ExitBlocker[i]);
		}
	}
}
