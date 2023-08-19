using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
	[Header("GameObject Holders")]
	public GameObject	go_Startpoint;
	public GameObject	go_BanananaHolder;
	public GameObject	go_ExitBlockerHolder;
	public GameObject	go_DoorsHolder;
	public GameObject	go_DarkPassagesHolder;
	public GameObject	go_BlockadesHolder;	//Blockade for Phase 1, will be removed in Phase 2

	[Header("GameObject Lists")]
	public List<GameObject>	go_Banananas;
	public List<GameObject>	go_ExitBlocker;
	public List<GameObject>	go_Doors;
	public List<GameObject>	go_DarkPassages;
	public List<GameObject>	go_Blockade;

	[Header("Other")]
	public CameraEffect camEffect;
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
			if (go_BlockadesHolder)
				GetGameObjects(go_BlockadesHolder, go_Blockade);
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

	private void SwitchLight()
	{
		camEffect.enabled = !camEffect.enabled;
	}
	public void TouchedDarkPassage()
	{
		SwitchLight();
		Invoke(nameof(SwitchLight), 1);
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
		if (BananAtExit <= 0)
		{
			print("Phase One didnt work.");
			return ;
		}
		int frontBanan = go_Banananas.Count - BananAtExit;
		for (int i = frontBanan; i < go_Banananas.Count; i++)
		{
			if (i == frontBanan)
				BANANAport(go_Banananas[i], TouchedDoors[^1]);	//^1 just replacing "TouchedDoors.Count - 1"
			else
				BANANAport(go_Banananas[i], go_ExitBlocker[i - frontBanan - 1]);
		}
		BananAtExit--;

		if (BananAtExit <= 0)
		{
			Phase = 2;
			for (int i = 0; i < go_Blockade.Count; i++)
				go_Blockade[i].SetActive(false);
		}
	}

	private void DoPhaseTwo()
	{
		if (TouchedDoors.Count < go_Banananas.Count)
		{
			DoReset();
			return ;
		}
		else
		{
			for (int i = 0; i < go_Banananas.Count; i++)
				BANANAport(go_Banananas[i], TouchedDoors[i]);
		}
	}

	private void DoReset()
	{
		//Reset Mannequin locations
		//Reset player position(?)
		BananAtExit = go_Banananas.Count;
		Phase = 1;
		for (int i = 0; i < go_Blockade.Count; i++)
		{
			go_Blockade[i].SetActive(true);
		}
		for (int i = 0; go_Banananas.Count > i; i++)
		{
			BANANAport(go_Banananas[i], go_ExitBlocker[i]);
		}
	}
}
