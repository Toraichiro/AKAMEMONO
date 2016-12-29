using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour {


	static public PuzzleController instance	= null;
			
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	static public PuzzleController GetInstance()
	{
		return instance;
	}

	private Dictionary<int, GameObject> chipData = new Dictionary<int, GameObject>();
	public GameObject[] chipPrefabs;
	public GameObject chipBase;





	public void SetChip( Vector2 pos )
	{
		int chipType = 0;

		GameObject obj = Instantiate(chipPrefabs[chipType]) as GameObject;
		obj.transform.SetParent(chipBase.transform,false);
		obj.transform.localPosition = new Vector3(pos.x * 82 - (82 * 4f), pos.y * 82 - (82 * 4f));

		chipData.Add((int)pos.y * 10 + (int)pos.x, obj);
	}









	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
