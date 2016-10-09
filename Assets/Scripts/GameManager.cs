using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set;}

	public enum GameState { GameState_PlayerTurn, GameState_IATurn, GameState_Paused, GameState_Menu}

	public GameState gameState = GameState.GameState_Menu;

	private bool GameIsLaunched = false;

	void Start () 
	{
		Instance = this;
	}
	
	void Update () 
	{
		
	}

	void UpdateState( GameState state)
	{
		switch (state) 
		{
			case GameState.GameState_Menu:
				break;
			case GameState.GameState_PlayerTurn:
				break;
			case GameState.GameState_IATurn:
				break;
			case GameState.GameState_Paused:
				break;
		}
	}

	public void LaunchGame()
	{
		if(!GameIsLaunched)
		{
			GameIsLaunched = true;
			Amb.Instance.PlayAmbianteSound ();
			SwitchState(GameState.GameState_IATurn);
		}
	}

	public void SwitchState ( GameState newState)
	{
		gameState = newState;
		switch (gameState) 
		{
			case GameState.GameState_Menu:
				break;
			case GameState.GameState_PlayerTurn:
				break;
			case GameState.GameState_IATurn:
				IA.Instance.Play ();	
				break;
			case GameState.GameState_Paused:
				break;
		}
	}
}
