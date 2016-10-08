using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

    // At the first level no lever arm
    // This 4 levels 
    // and the same Scene ever 

    public int mCurrentLevel = 0;

    Board 
        mBoard; 
    CircleBoard 
        mCircleBoard;

    GameObject
        mBoardParent,
        mCircleBoardParent;

	// Use this for initialization
	void Start ()
    {
        mBoard = GameObject.FindObjectOfType<Board>();
        mCircleBoard = GameObject.FindObjectOfType<CircleBoard>();

        mBoardParent = FindParentWithTag(mBoard.transform, "Board").gameObject;
        mCircleBoardParent = FindParentWithTag(mCircleBoard.transform, "Board").gameObject;

        Init();
    }

    Transform FindParentWithTag( Transform t_transform , string s_name )
    {
        Transform tReturn;

        tReturn = t_transform;

        while ( tReturn != null )
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
                    mBoardParent.gameObject.SetActive( false );
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
	void Update ()
    {
        if (Input.GetKeyDown("e"))
        {
            ChangeLevel();
        }
	}

    void ChangeLevel()
    {
        mCurrentLevel++;
        Init();
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
}
