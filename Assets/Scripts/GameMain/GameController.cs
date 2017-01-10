using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text debugText;
	public Text turnText;

	// Use this for initialization
	void Start () {
		// player
		PuzzleController.GetInstance().SetChip(new Vector2(PuzzleController.PuzzleMeshWidth/2, PuzzleController.PuzzleMeshHeight/2), GameChip.CHIP_TYPE.player);
		// tree
		for(int i = 0; i < 4; i ++)
		{
			PuzzleController.GetInstance().SpawnChip(GameChip.CHIP_TYPE.tree);
		}
		// enemy
		for (int i = 0; i < 2; i++)
		{
			PuzzleController.GetInstance().SpawnChip(GameChip.CHIP_TYPE.enemy);
		}
		// friend
		for (int i = 0; i < 1; i++)
		{
			PuzzleController.GetInstance().SpawnChip(GameChip.CHIP_TYPE.friend);
		}
		PuzzleController.GetInstance().SpawnChip(GameChip.CHIP_TYPE.weapon);
	}

	// Update is called once per frame
	int turnCount = 0;
	int spawnCount = 0;
	bool swipableFlag = true;
	void Update () {
		bool movedFlag = false;

		if (swipableFlag == true)
		{
			if (TouchControl.singleFingerUpSwipe)
			{
				debugText.text = "SWIPE : UP";
				movedFlag = PuzzleController.GetInstance().MoveChips(PuzzleController.Move.UP);
				GetComponent<TouchControl>().Reset();
			}
			else if (TouchControl.singleFingerDownSwipe)
			{
				debugText.text = "SWIPE : DOWN";
				movedFlag = PuzzleController.GetInstance().MoveChips(PuzzleController.Move.DOWN);
				GetComponent<TouchControl>().Reset();
			}
			else if (TouchControl.singleFingerRightSwipe)
			{
				debugText.text = "SWIPE : RIGHT";
				movedFlag = PuzzleController.GetInstance().MoveChips(PuzzleController.Move.RIGHT);
				GetComponent<TouchControl>().Reset();
			}
			else if (TouchControl.singleFingerLeftSwipe)
			{
				debugText.text = "SWIPE : LEFT";
				movedFlag = PuzzleController.GetInstance().MoveChips(PuzzleController.Move.LEFT);
				GetComponent<TouchControl>().Reset();
			}
			else if (TouchControl.singleFingerTap)
			{
				debugText.text = "TAP : SINGLE";
			}

			// step game turn
			if (movedFlag)
			{
				swipableFlag = false;
				turnCount++;
				turnText.text = "TURN " + turnCount;

				StartCoroutine(TurnEndProc());


			}
		}
	}

	
	IEnumerator TurnEndProc()
	{
		yield return new WaitForSeconds(0.4f);

		GameChip.CHIP_TYPE chiptype = GameChip.CHIP_TYPE.enemy;
		if (PuzzleController.GetInstance().ChipCount(GameChip.CHIP_TYPE.friend) <= 0)
		{
			chiptype = GameChip.CHIP_TYPE.friend;
		}
		else
		{
			switch (Random.Range(0, 9))
			{
				case 0:
				case 1:
				case 2:
				case 4:
					chiptype = GameChip.CHIP_TYPE.enemy;
					break;
				case 5:
					chiptype = GameChip.CHIP_TYPE.weapon;
					break;
				case 6:
					chiptype = GameChip.CHIP_TYPE.tree;
					break;
				case 7:
				case 8:
				default:
					chiptype = GameChip.CHIP_TYPE.friend;
					break;
			}
		}
		PuzzleController.GetInstance().SpawnChip(chiptype, 5);

		yield return new WaitForSeconds(0.2f);
		swipableFlag = true;
	}
}
