using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum TicTacToeState{none, cross, circle}

[System.Serializable]
public class WinnerEvent : UnityEvent<int>
{
}

public class TicTacToeAI : MonoBehaviour
{

	int _aiLevel;

	TicTacToeState[,] boardState;

	[SerializeField]
	private bool _isPlayerTurn;

	[SerializeField]
	private int _gridSize = 3;
	
	[SerializeField]
	private TicTacToeState playerState = TicTacToeState.circle;
	TicTacToeState aiState = TicTacToeState.cross;

	[SerializeField]
	private GameObject _xPrefab;

	[SerializeField]
	private GameObject _oPrefab;

	public UnityEvent onGameStarted;

	public UnityEvent playerClicked;

	public UnityEvent aiClicked;
	//Call This event with the player number to denote the winner
	public WinnerEvent onPlayerWin;

	private ClickTrigger[,] _triggers;

	private bool _isGaming;

	[SerializeField] private TextMeshProUGUI miniPanel;
	
	private void Awake()
	{
		if(onPlayerWin == null){
			onPlayerWin = new WinnerEvent();
		}
	}

	private void Start()
	{
		_isGaming = true;
		onPlayerWin.AddListener(Winner);
		playerClicked.AddListener(AfterPlayer);
	}

	public void StartAI(int AILevel){
		_aiLevel = AILevel;
		StartGame();
	}

	public void RegisterTransform(int myCoordX, int myCoordY, ClickTrigger clickTrigger)
	{
		_triggers[myCoordX, myCoordY] = clickTrigger;
	}

	private void StartGame()
	{
		_triggers = new ClickTrigger[_gridSize,_gridSize];
		boardState = new TicTacToeState[_gridSize, _gridSize];
		onGameStarted.Invoke();
	}

	public void PlayerSelects(int coordX, int coordY)
	{
		if (_isPlayerTurn)
		{
			SetVisual(coordX, coordY, playerState);
			boardState[coordX, coordY] = playerState;
			playerClicked.Invoke();
			_triggers[coordX,coordY].transform.gameObject.GetComponent<ClickTrigger>().DisbleClick();
			ShowPosition();
			CheckWin();
		}
		
	}

	public void AiSelects(int coordX, int coordY)
	{
		if (_isGaming)
		{
			SetVisual(coordX, coordY, aiState);
			boardState[coordX, coordY] = aiState;
			aiClicked.Invoke();
			_triggers[coordX,coordY].transform.gameObject.GetComponent<ClickTrigger>().DisbleClick();
			ShowPosition();
			CheckWin();
		}
		
	}

	private void SetVisual(int coordX, int coordY, TicTacToeState targetState)
	{
		Instantiate(
			targetState == TicTacToeState.circle ? _oPrefab : _xPrefab,
			_triggers[coordX, coordY].transform.position,
			Quaternion.identity
		);
	}
	
	private void Winner(int i)
	{
		_isGaming = false;
	}

	private void AfterPlayer()
	{
		
		_isPlayerTurn = false;
		int posX = Random.Range(0, 3);
		int posY = Random.Range(0, 3);
		
		if (CheckPosition(posX, posY))
		{
			StartCoroutine(TurnAi(posX, posY));
		}
		else
		{
			AfterPlayer();
		}
	}

	private IEnumerator TurnAi(int posX, int posY)
	{
		yield return new WaitForSeconds(1);
		AiSelects(posX, posY);
		_isPlayerTurn = true;
	}

	private bool CheckPosition(int x, int y)
	{
		return boardState[x, y] == TicTacToeState.none;
	}

	private void ShowPosition()
	{
		string msj = "";
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				msj += $"[{ChangeValueEmun(boardState[i, j].ToString())}]";
			}
			msj += "\n";
		}
		miniPanel.SetText(msj);
	}

	private string ChangeValueEmun(string str)
	{
		string strFinal = "";
		switch (str)
		{
			case "none":
				strFinal = "_";
				break;
			case "cross":
				strFinal = "X";
				break;
			case "circle":
				strFinal = "0";
				break;
		}

		return strFinal;
	}

	private void CheckWin()
	{
		if (boardState[0,0] == TicTacToeState.circle && boardState[0,1] == TicTacToeState.circle && boardState[0,2] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		else if (boardState[1,0] == TicTacToeState.circle && boardState[1,1] == TicTacToeState.circle && boardState[1,2] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		else if (boardState[2,0] == TicTacToeState.circle && boardState[2,1] == TicTacToeState.circle && boardState[2,2] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		
		else if (boardState[0,0] == TicTacToeState.circle && boardState[1,0] == TicTacToeState.circle && boardState[2,0] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		else if (boardState[0,1] == TicTacToeState.circle && boardState[1,1] == TicTacToeState.circle && boardState[2,1] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		else if (boardState[0,2] == TicTacToeState.circle && boardState[1,2] == TicTacToeState.circle && boardState[2,2] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		
		else if (boardState[0,0] == TicTacToeState.circle && boardState[1,1] == TicTacToeState.circle && boardState[2,2] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		else if (boardState[2,0] == TicTacToeState.circle && boardState[1,1] == TicTacToeState.circle && boardState[0,2] == TicTacToeState.circle)
		{
			onPlayerWin.Invoke(2);
		}
		//
		else if (boardState[0,0] == TicTacToeState.cross && boardState[0,1] == TicTacToeState.cross && boardState[0,2] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
		else if (boardState[1,0] == TicTacToeState.cross && boardState[1,1] == TicTacToeState.cross && boardState[1,2] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
		else if (boardState[2,0] == TicTacToeState.cross && boardState[2,1] == TicTacToeState.cross && boardState[2,2] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
		
		else if (boardState[0,0] == TicTacToeState.cross && boardState[1,0] == TicTacToeState.cross && boardState[2,0] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
		else if (boardState[0,1] == TicTacToeState.cross && boardState[1,1] == TicTacToeState.cross && boardState[2,1] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
		else if (boardState[0,2] == TicTacToeState.cross && boardState[1,2] == TicTacToeState.cross && boardState[2,2] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
		
		else if (boardState[0,0] == TicTacToeState.cross && boardState[1,1] == TicTacToeState.cross && boardState[2,2] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
		else if (boardState[2,0] == TicTacToeState.cross && boardState[1,1] == TicTacToeState.cross && boardState[0,2] == TicTacToeState.cross)
		{
			onPlayerWin.Invoke(1);
		}
	}
}
