using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set;}

	public enum GameState { GameState_PlayerTurn, GameState_IATurn, GameState_Paused, GameState_Menu, GameState_Loose, GameState_Win }

	public GameState gameState = GameState.GameState_Menu;

    public Animator
        mCanvasAnimator;

    Scene
        mScene;

	private bool GameIsLaunched = false;

	void Start () 
	{
		Instance = this;
        mScene = GameObject.FindObjectOfType<Scene>();
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
            case GameState.GameState_Loose:
                mCanvasAnimator.SetTrigger("LOSE");
                break;
            case GameState.GameState_Win:
                mCanvasAnimator.SetTrigger("WIN");
                mScene.ChangeLevel();
                break;
        }
	}
}
