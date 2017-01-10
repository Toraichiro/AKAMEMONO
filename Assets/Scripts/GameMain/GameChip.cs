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
	public Text HitPointText = null;
	public Text AttackPointText = null;

	private int _hitPoint = 10;
	private int HitPoint
	{
		get
		{
			return _hitPoint;
		}
		set
		{
			_hitPoint = value;
			if (HitPointText != null)
			{
				HitPointText.text = _hitPoint.ToString();
			}
		}

	}


	private int _attackPoint = 10;
	private int AttackPoint
	{
		get
		{
			return _attackPoint;
		}
		set
		{
			_attackPoint = value;
			if (AttackPointText != null)
			{
				AttackPointText.text = _attackPoint.ToString();
			}
		}
	}

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


	public int GetHitPoint()
	{
		return HitPoint;
	}

	public void SetHitPoint(int point)
	{
		HitPoint = point;
	}

	public bool SubHitPoint(int point)
	{
		HitPoint -= point;
		if (HitPoint <= 0)
		{
			HitPoint = 0;
			return true;	// 死んだ
		}
		return false;
	}

	public int GetAttackPoint()
	{
		return AttackPoint;
	}

	public void SetAttackPoint(int point)
	{
		AttackPoint = point;
	}

	public void AddAttackPoint(int point)
	{
		AttackPoint += point;
	}

	public void SubAttackPoint(int point)
	{
		AttackPoint -= point;
		if (AttackPoint < 0)
		{
			AttackPoint = 0;
		}
	}


	public bool Damaged(int dmg=1000)
	{
		if (ChipBase != null)
		{
			switch (chipType)
			{
				case CHIP_TYPE.tree:
					iTween.ScaleFrom(ChipBase, iTween.Hash(
						"y", 2.6f,
						"x", 2.6f,
						"easeType", iTween.EaseType.easeInOutBounce,
						"time", 0.3f)
					);
					Destroy(this.gameObject, 0.6f);
					return true;
					break;

				case CHIP_TYPE.enemy:
					iTween.ShakePosition(ChipBase, iTween.Hash(
						"y", 8,
						"x", 8,
						"delay", 0.3f,
						"time", 0.3f)
					);
					if (SubHitPoint(dmg))
					{	// 死んだ
						Destroy(this.gameObject, 0.6f);
						return true;
					}
					break;

				default:
					break;
			}
		}
		return false;
	}

	public int GetWeapon()
	{
		if (chipType == CHIP_TYPE.weapon)
		{
			Destroy(this.gameObject, 0.4f);
			return AttackPoint;
		}
		return 0;
	}

	// Use this for initialization
	void Start () {
		if( HitPointText!=null)
		{
			HitPointText.text = HitPoint.ToString();
		}
		if (AttackPointText != null)
		{
			AttackPointText.text = AttackPoint.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
