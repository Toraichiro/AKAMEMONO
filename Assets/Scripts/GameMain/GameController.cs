using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject chipPrefab;
    public GameObject chipBase;

	public Text debugText;

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
		if( Input.touchCount > 0)
		{
			print("TOUCH COUNT : " + Input.touchCount);
		}


		if (TouchControl.singleFingerUpSwipe)
		{
			debugText.text = "SWIPE : UP";
		}
		else if (TouchControl.singleFingerDownSwipe)
		{
			debugText.text = "SWIPE : DOWN";
		}
		else if (TouchControl.singleFingerRightSwipe)
		{
			debugText.text = "SWIPE : RIGHT";
		}
		else if (TouchControl.singleFingerLeftSwipe)
		{
			debugText.text = "SWIPE : LEFT";
		}
		else if (TouchControl.singleFingerTap)
		{
			debugText.text = "TAP : SINGLE";
		}

	}
}
