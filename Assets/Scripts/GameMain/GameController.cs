using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject chipPrefab;
    public GameObject chipBase;

	// Use this for initialization
	void Start () {
		for(int x = 0; x < 9; x++)
        {
            for(int y = 0; y < 9; y++)
            {
                GameObject obj = Instantiate(chipPrefab) as GameObject;
                obj.transform.SetParent(chipBase.transform);
                obj.transform.localPosition = new Vector3(x * 82 -(82*4f), y * 82 - (82 * 4f));
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
