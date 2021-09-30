
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMiniGame : MonoBehaviour
{
	public GameObject wrenchPrefab;
	public GameObject flange;
	public GameObject WinScreen;
	
	// The canvas contains the "Use Wrench" button
	public GameObject canvas;

	public Vector3 attachedWrenchPosition;
	public float z_offset;

	public float flangeRotationUpperBound;
	public float flangeRotationlowerBound;
	public float targetRotation;
	public float targetThreshold;

	private GameObject wrench;

	private bool Turned = false;
	private bool started = false;
	
	private WrenchMiniGameMaster _wrenchMiniGameMaster;
	private LogToServer logger;

	public string ObjectiveName = "MissionGas";
	public bool _satisfied;
	public void Awake()
	{
		_wrenchMiniGameMaster = GameObject.FindObjectOfType<WrenchMiniGameMaster>();
		logger = GameObject.Find("Logger").GetComponent<LogToServer>();
	}


	private void Success()
	{
		
		//replace this with success action, i.e. turn off gas
		Debug.Log("Congratz, you have won!");
		if (Systems.Objectives.Register(ObjectiveName, () => _satisfied = true)) ;
		{
			//this is the offending statement
			Systems.Objectives.Satisfy(ObjectiveName, false);
		}
		Destroy(flange.GetComponent<RotateObjectWithMouse>());
		Turned = true;
		logger.sendToLog("Completed Level 1!","LEVEL");
		WinScreen.SetActive(true);
		canvas.SetActive(false);
		GameObject.Find("Gas").GetComponent<GasShutDown>().GasMiniGameWon();
		//		_wrenchMiniGameMaster.CheckCorrect(Turned);
	}

	private void AttachWrench()
	{
		logger.sendToLog("Wrench attached","MINIGAME");
		flange.GetComponent<CollisionCallback>().RemoveCallback(wrench.tag);
		Destroy(wrench);
		flange.transform.GetChild(0).gameObject.SetActive(true);
		var comp = flange.AddComponent<RotateObjectWithMouse>();
		comp.Initialize(flangeRotationlowerBound, flangeRotationUpperBound, targetRotation, targetThreshold, Success);
	}

	public void StartGame()
	{
		if (!started)
		{
			logger.sendToLog("Wrench created","MINIGAME");
			started = true;
			wrench = CreateWrench();
			flange.GetComponent<CollisionCallback>().AddCallback(wrench.tag, AttachWrench);
		}
	}

	private GameObject CreateWrench()
	{
		var newWrench =  Instantiate(wrenchPrefab, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + z_offset), Quaternion.identity);
		newWrench.AddComponent<ObjectFollowMouse>();
		return newWrench;
	}
}
