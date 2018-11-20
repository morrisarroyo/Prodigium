using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject mainMenuUI;
    public List<GameObject> buttons;

    private int buttonIndex = 0;

	// Use this for initialization
	void Start () {
        buttons[0].GetComponent<Button>().Select();
    }
	
	// Update is called once per frame
	void Update () {
           if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                //delayActive = true;
                if (buttonIndex < buttons.Count - 1)
                {
                    ++buttonIndex;
                }
                buttons[buttonIndex].GetComponent<Button>().Select();

            }

            //Decrements option index and moves the image when keys are pressed
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                //delayActive = true;
                if (buttonIndex > 0)
                {
                    --buttonIndex;
                }
                buttons[buttonIndex].GetComponent<Button>().Select();

            }
        //Increments option index and moves the image when keys are pressed

        // When key is pressed, run the method according to option index
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0) )
        {
            buttons[buttonIndex].GetComponent<Button>().onClick.Invoke();
            /*
            switch (buttonIndex)
            {
                case 0:
                    mainMenuUI.GetComponent<MainMenu>().PlayGame();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    mainMenuUI.GetComponent<MainMenu>().QuitGame();
                    break;

            }
            */
        }
    }
}
