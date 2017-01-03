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
		/*
		for (int y = 0; y < PuzzleMeshHeight; y++)
		{
			for (int x = 0; x < PuzzleMeshWidth; x++)
			{
				chipData[y, x] = null;
			}
		}
		*/
	}

	static public PuzzleController GetInstance()
	{
		return instance;
	}

	public const int PuzzleMeshWidth    = 7;
	public const int PuzzleMeshHeight	= 7;
	public const int ChipSize = 100;

	private GameObject[,] chipData = new GameObject[PuzzleMeshWidth, PuzzleMeshHeight];
	public GameObject[] chipPrefabs;
	public GameObject chipBase;

	private int[] chipTypeCount = new int[(int)GameChip.CHIP_TYPE.max];

	// Use this for initialization
	void Start()
	{
	}

	float ChipPos_x(int x)
	{
		return (float)(x * ChipSize - (ChipSize * (PuzzleMeshWidth / 2)));
	}

	float ChipPos_y(int y)
	{
		return (float)(y * -ChipSize + (ChipSize * (PuzzleMeshHeight / 2)));
	}

	Vector2 ChipPos(int x, int y)
	{
		return new Vector2(ChipPos_x(x), ChipPos_y(y));
	}

	Vector2 ChipPos( Vector2 pos)
	{
		return new Vector2(ChipPos_x((int)pos.x), ChipPos_y((int)pos.y));
	}

	public void SetChip( Vector2 pos, GameChip.CHIP_TYPE type )
	{
		if (chipData[(int)pos.x, (int)pos.y] == null)
		{
			GameObject obj = Instantiate(chipPrefabs[(int)type]) as GameObject;
			obj.transform.SetParent(chipBase.transform, false);
			obj.transform.localPosition = ChipPos(pos);

			chipData[(int)pos.x,(int)pos.y] = obj;

			chipTypeCount[(int)type]++;
		}
	}

	void DestroyChip(int x, int y, GameChip.CHIP_TYPE type)
	{
		if(chipData[x, y])
		{
			chipData[x, y].GetComponent<GameChip>().Damaged();
			chipData[x, y] = null;
			chipTypeCount[(int)type]--;
		}
		else
		{
			print("ERROR:Not found data : "+type);
		}
	}

	public int ChipCount(GameChip.CHIP_TYPE type)
	{
		return chipTypeCount[(int)type];
	}

	// 空きを探してチップを配置する
	public bool SpawnChip(GameChip.CHIP_TYPE type)
	{
		List<Vector2> chiptable = new List<Vector2>();
		for (int x = 0; x < PuzzleMeshWidth; x++)
		{
			for (int y = 0; y < PuzzleMeshHeight; y++)
			{
				if(chipData[x, y] == null)
				{
					chiptable.Add( new Vector2(x,y));
				}
			}
		}

		// 置く場所がない
		if(chiptable.Count == 0)
		{
			print("NOT SPAWN PLACE");
			return false;
		}

		SetChip(chiptable[Random.Range(0, chiptable.Count)], type);
		return true;
	}


	public enum Move
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

	// GameControllerからスワイプしたら呼ばれる
	public bool MoveChips(Move move)
	{
		bool moved = false;
		MoveCheck(move);

		switch (move)
		{
			case Move.UP:
				for (int y = 1; y < PuzzleMeshHeight; y++)
				{
					for (int x = 0; x < PuzzleMeshWidth; x++)
					{
						if( ChipProcess(x, y, x, y - 1, move))
						{
							moved = true;
						}
					}
				}
				break;

			case Move.DOWN:
				for (int y = PuzzleMeshHeight - 1 - 1; y >= 0; y--)
				{
					for (int x = 0; x < PuzzleMeshWidth; x++)
					{
						if( ChipProcess(x, y, x, y+1, move))
						{
							moved = true;
						}
					}
				}
				break;

			case Move.RIGHT:
				for (int x = PuzzleMeshWidth-1-1; x >= 0; x--)
				{
					for (int y = 0; y < PuzzleMeshHeight; y++)
					{
						if( ChipProcess(x, y, x + 1, y, move))
						{
							moved = true;
						}
					}
				}
				break;

			case Move.LEFT:
				for (int x = 1; x < PuzzleMeshWidth; x++)
				{
					for (int y = 0; y < PuzzleMeshHeight; y++)
					{
						if( ChipProcess(x, y, x - 1, y, move))
						{
							moved = true;
						}
					}
				}
				break;
		}
		return moved;

	}

	bool ChipProcess(int chip_x, int chip_y, int target_x, int target_y, Move moveang)
	{
		int[] plus_x = { 0, 0, 1, -1 };
		int[] plus_y = { -1, 1, 0, 0 };
		bool movedFlag = false;
		if (chipData[chip_x, chip_y] != null)
		{
			GameChip chip = chipData[chip_x, chip_y].GetComponent<GameChip>();
			if (chip.MovableFlag() == false)
			{	// 攻撃され中
				// 仲間はゾンビに攻撃されると動けない
				return true;
			}

			switch (chip.GetChipType())
			{
				case GameChip.CHIP_TYPE.player:
					{
						if( MoveStraightChipProc(chip_x, chip_y, moveang, chip.GetChipType()) != false)
						{ // 目的地に何かいる

						}
						else
						{
							movedFlag = true;
						}
					}
					break;

				case GameChip.CHIP_TYPE.enemy:
					{
						if (MoveStraightChipProc(chip_x, chip_y, moveang, chip.GetChipType()) != false)
						{ // 目的地に何かいる
						}
						else
						{
							movedFlag = true;
						}
					}
					break;

				case GameChip.CHIP_TYPE.friend:
				case GameChip.CHIP_TYPE.weapon:
					if (MoveChipProc(chip_x, chip_y, target_x, target_y) == false)
					{ // 目的地に何かいる

					}
					else
					{
						movedFlag = true;
					}
					break;

				case GameChip.CHIP_TYPE.tree:
					break;

				case GameChip.CHIP_TYPE.safehouse:
					break;
			}
		}
		return movedFlag;
	}


	// 攻撃目標があるかのチェック
	// あった場合対象を１ターン動けなくする
	void MoveCheck(Move moveang)
	{
		int[] target_x = { 0, 0, 1, -1 };
		int[] target_y = { -1, 1, 0, 0 };

		// reset all chips
		for (int y = 0; y < PuzzleMeshHeight; y++)
		{
			for (int x = 0; x < PuzzleMeshWidth; x++)
			{
				if (chipData[x, y] != null)
				{
					chipData[x, y].GetComponent<GameChip>().ResetMovableFlag();
				}
			}
		}

		for (int y = 0; y < PuzzleMeshHeight; y++)
		{
			for (int x = 0; x < PuzzleMeshWidth; x++)
			{
				if (chipData[x, y] != null)
				{
					GameChip chip = chipData[x, y].GetComponent<GameChip>();
					switch (chip.GetChipType())
					{
						case GameChip.CHIP_TYPE.player:
							break;
						case GameChip.CHIP_TYPE.enemy:
							MoveCheck_Enemy(x,y,moveang);
							break;
						default:
							break;
					}
				}
			}
		}
	}

	// 直線の先をチェック
	void MoveCheck_Enemy(int x, int y, Move moveang)
	{
		int[] target_x = { 0, 0, 1, -1 };
		int[] target_y = { -1, 1, 0, 0 };

		for (int v = 0; v < PuzzleMeshWidth; v++)
		{
			x += target_x[(int)moveang];
			y += target_y[(int)moveang];
			if ((x >= 0) && (x < PuzzleMeshWidth) && (y >= 0) && (y < PuzzleMeshHeight))
			{
				if (chipData[x, y] != null)
				{
					GameChip chip = chipData[x, y].GetComponent<GameChip>();
					switch (chip.GetChipType())
					{
						case GameChip.CHIP_TYPE.friend:
							chip.SetMovableFlag(false);
							break;
					}
					break;
				}
			}
		}
	}

	// 一歩ずつ動くキャラ(friend)
	bool MoveChipProc(int chip_x, int chip_y, int target_x, int target_y)
	{
		if (chipData[target_x, target_y] == null)
		{
			chipData[target_x, target_y] = chipData[chip_x, chip_y];
			iTween.MoveTo(chipData[chip_x, chip_y], iTween.Hash(
				"x", (target_x) * ChipSize - (ChipSize * (PuzzleMeshWidth / 2)),
				"y", (target_y) * -ChipSize + (ChipSize * (PuzzleMeshHeight / 2)),
				"easeType", iTween.EaseType.easeOutQuint,
				"time", 0.4f,
				"isLocal", true));
			chipData[chip_x, chip_y] = null;
			return true;
		}
		return false;	// 目的地に何かある
	}

	// 直線的に動くキャラ(player/enemy)
	bool MoveStraightChipProc(int chip_x, int chip_y, Move moveang, GameChip.CHIP_TYPE type)
	{
		bool actionFlag = false;

		int[] step_x = { 0, 0, 1, -1 };
		int[] step_y = { -1, 1, 0, 0 };

		int target_x = chip_x;
		int target_y = chip_y;

		for ( int v = 0; v < PuzzleMeshWidth; v++)
		{
			if( ((target_x+step_x[(int)moveang])<0)
				|| ((target_x + step_x[(int)moveang]) >= PuzzleMeshWidth)
				|| ((target_y + step_y[(int)moveang]) < 0)
				|| ((target_y + step_y[(int)moveang]) >= PuzzleMeshHeight))
			{
				break;
			}

			if (chipData[target_x + step_x[(int)moveang], target_y + step_y[(int)moveang]] != null)
			{
				actionFlag = true;  // 先に何かある
				break;
			}
			else
			{
				target_x += step_x[(int)moveang];
				target_y += step_y[(int)moveang];
			}
		}

		if ((chip_x != target_x) || (chip_y != target_y)) {
			chipData[target_x, target_y] = chipData[chip_x, chip_y];
			iTween.MoveTo(chipData[chip_x, chip_y], iTween.Hash(
				"x", ChipPos_x(target_x),
				"y", ChipPos_y(target_y),
				"easeType", iTween.EaseType.easeOutQuint,
				"time", 0.4f,
				"isLocal", true));
			chipData[chip_x, chip_y] = null;
		}

		if (actionFlag)
		{
			GameObject obj = chipData[target_x + step_x[(int)moveang], target_y + step_y[(int)moveang]];
			GameChip targetChip = obj.GetComponent<GameChip>();
			switch (type){
				case GameChip.CHIP_TYPE.player:
					switch (targetChip.GetChipType())
					{
						case GameChip.CHIP_TYPE.enemy:
							DestroyChip(target_x + step_x[(int)moveang], target_y + step_y[(int)moveang], GameChip.CHIP_TYPE.enemy);
							break;

						case GameChip.CHIP_TYPE.tree:
							DestroyChip(target_x + step_x[(int)moveang], target_y + step_y[(int)moveang], GameChip.CHIP_TYPE.tree);
							break;
						case GameChip.CHIP_TYPE.weapon:
							DestroyChip(target_x + step_x[(int)moveang], target_y + step_y[(int)moveang], GameChip.CHIP_TYPE.weapon);
							break;
					}
					break;

				case GameChip.CHIP_TYPE.enemy:
					switch (targetChip.GetChipType())
					{
						case GameChip.CHIP_TYPE.friend:
							DestroyChip(target_x + step_x[(int)moveang], target_y + step_y[(int)moveang], GameChip.CHIP_TYPE.friend);
							break;
					}
					break;	
			}
		}
		return actionFlag;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
