using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonMenu : MonoBehaviour {
    public string gTriggerName;

    Button mButton;
    Animator mAnimator;
	// Use this for initialization
	void Start ()
    {
        mAnimator = gameObject.transform.parent.transform.parent.GetComponent<Animator>();
        mButton = gameObject.GetComponent<Button>();
        mButton.onClick.AddListener(LaunchAnimation);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void LaunchAnimation()
    {
            print(" change level ");
        mAnimator.SetTrigger(gTriggerName);
    }
}
