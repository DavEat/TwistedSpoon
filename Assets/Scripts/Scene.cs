using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

    // At the first level no lever arm
    // This 4 levels 
    // and the same Scene ever 

    public int
        mCurrentLevel = 0,
        mChanceMaxPlay = 2,
        mMaxTurn = 5;
    public bool
        mPlayerHasPlayed = true ;
    public float mTweakValueAngle = 7.0f;
    public float mTweakAngleMax = 12.0f;
    CreateWeight
        mCreateWeight;
    Board
        mBoard;

    CircleBoard
        mCircleBoard;

    GameObject
        mBoardParent,
        mCircleBoardParent;

    int
        mChanceToPlay = 0,
        mCurrentTurn = 0;

    // Use this for initialization
    void Start()
    {
        mBoard = GameObject.FindObjectOfType<Board>();
        mCircleBoard = GameObject.FindObjectOfType<CircleBoard>();
        mCreateWeight = GameObject.FindObjectOfType<CreateWeight>();

        mBoardParent = FindParentWithTag(mBoard.transform, "Board").gameObject;
        mCircleBoardParent = FindParentWithTag(mCircleBoard.transform, "Board").gameObject;

        Init();
    }

    Transform FindParentWithTag(Transform t_transform, string s_name)
    {
        Transform tReturn;

        tReturn = t_transform;

        while (tReturn != null)
        {
            if (tReturn.tag == s_name)
            {
                break;
            }
            else
            {
                tReturn = tReturn.parent;
            }
        }

        return tReturn;

    }

    void Init()
    {
        mChanceToPlay = mChanceMaxPlay;
        switch (mCurrentLevel)
        {
            case 0:
                {
                    mBoardParent.gameObject.SetActive(true);
                    mCircleBoardParent.SetActive(false);

                    mBoard.LeverArm = false;
                }
                break;
            case 1:
                {
                    mBoard.LeverArm = true;
                }
                break;
            case 2:
                {
                    mBoardParent.gameObject.SetActive(false);
                    mCircleBoardParent.gameObject.SetActive(true);

                    mCircleBoard.LeverArm = false;
                }
                break;
            case 3:
                {
                    mCircleBoard.LeverArm = true;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            ChangeLevel();
        }

  
    }

    public void ChangeLevel()
    {
        mCurrentLevel++;
        ReStartLevel();
        Init();
    }

    public void CheckRotationPlayLeft(float f_Angle)
    {
        if (f_Angle > 180)
        {
            f_Angle = Mathf.Abs ( f_Angle - 360 );
        }
        if (f_Angle > mTweakAngleMax)
        {
            if (mChanceToPlay > 0 )
            {
                // :: You can Play AGAIN
                mChanceToPlay--; 
            }
            else
            {
            // :: LOOSE
            GameManager.Instance.SwitchState(GameManager.GameState.GameState_Loose);
            }
        }
        else 
        {
            // :: Win Next Turn
            if (mCurrentTurn >= mMaxTurn)
            {
                GameManager.Instance.SwitchState(GameManager.GameState.GameState_Win);
            }
            else
            {
                if (mPlayerHasPlayed)
                {
                mCurrentTurn++;
                GameManager.Instance.SwitchState(GameManager.GameState.GameState_IATurn);
                    mPlayerHasPlayed = false;
                    StartRotateBoard();
                }
            }
        }   
    }
    public void StartRotateBoard()
    {
        if (mCurrentLevel >= 0)
        {
            mBoard.BoardState = Board.State.State_Rotate;

        }
        else if (mCurrentLevel > 1)
        {
            mCircleBoard.BoardState = CircleBoard.State.State_Rotate;
        }
    }

    public void ReStartLevel()
    {
        mChanceToPlay = 0;
        mCurrentTurn = 0;
        if (mCurrentLevel >= 0)
        {
            mBoard.ReStartLevel();
            mCreateWeight.Start();

        }
        else if (mCurrentLevel > 1)
        {
           // mCircleBoard.ReStartLevel();
        }

    }
}
