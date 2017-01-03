using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameChip : MonoBehaviour {
	public enum CHIP_TYPE{
		player,
		enemy,
		friend,
		tree,
		safehouse,
		weapon,
		max
	}

	public CHIP_TYPE chipType;
	public GameObject ChipBase = null;
	public Text StatusText = null;

	bool movableFlag = true;

	public CHIP_TYPE GetChipType()
	{
		return chipType;
	}

	public bool MovableFlag()
	{
		return movableFlag;
	}

	public void SetMovableFlag(bool flag)
	{
		movableFlag = flag;
	}

	public void ResetMovableFlag()
	{
		SetMovableFlag(true);
	}



	public void Damaged()
	{
		if (ChipBase != null)
		{
			switch (chipType)
			{
				case CHIP_TYPE.tree:
				case CHIP_TYPE.weapon:
					iTween.ScaleFrom(ChipBase, iTween.Hash(
						"y", 2.6f,
						"x", 2.6f,
						"easeType", iTween.EaseType.easeInOutBounce,
						"time", 0.3f)
					);
					Destroy(this.gameObject, 0.6f);
					break;

				default:
					iTween.ShakePosition(ChipBase, iTween.Hash(
						"y", 8,
						"x", 8,
						"delay", 0.3f,
						"time", 0.3f)
					);
					Destroy(this.gameObject, 0.6f);
					break;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
