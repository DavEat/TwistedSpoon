using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonMenu : MonoBehaviour {

    public enum ESTATE_ACTION
    {
        PAUSED,
        INSTRUCTION,
        GAME,
        VALIDATED
    };

    public ESTATE_ACTION gStateAction;

    string mNameTrigger;

    Scene mScene;
    Button mButton;
    Animator mAnimator;

	// Use this for initialization
	void Start ()
    {
        mScene = GameObject.FindObjectOfType<Scene>();

        mAnimator = gameObject.transform.parent.transform.parent.GetComponent<Animator>();
        mButton = gameObject.GetComponent<Button>();

        switch (gStateAction)
        {
            case ESTATE_ACTION.VALIDATED:
                {
                    mButton.onClick.AddListener(StartRotationBoard);
                }
                break;
            default:
                {
                    mNameTrigger = gStateAction.ToString();
                    mButton.onClick.AddListener(LaunchAnimation);
                }
                break;

        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void LaunchAnimation()
    {
        mAnimator.SetTrigger(mNameTrigger);
    }

    void StartRotationBoard()
    {
        mScene.StartRotateBoard();
    }
}
