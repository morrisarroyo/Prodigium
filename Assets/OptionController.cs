using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour {
    public List<GameObject> buttons;

    private int buttonIndex = 0;

    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.PS4)
        {
            if (buttons[0].GetComponent<Button>() != null) {
                buttons[0].GetComponent<Button>().Select();
            } else
            {
                buttons[0].GetComponent<Toggle>().Select();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.PS4)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                //delayActive = true;
                if (buttonIndex < buttons.Count - 1)
                {
                    ++buttonIndex;
                }
                if (buttons[buttonIndex].GetComponent<Button>() != null)
                {
                    buttons[buttonIndex].GetComponent<Button>().Select();
                }
                else
                {
                    buttons[buttonIndex].GetComponent<Toggle>().Select();
                }

            }

            //Decrements option index and moves the image when keys are pressed
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                //delayActive = true;
                if (buttonIndex > 0)
                {
                    --buttonIndex;
                }
                if (buttons[buttonIndex].GetComponent<Button>() != null)
                {
                    buttons[buttonIndex].GetComponent<Button>().Select();
                }
                else
                {
                    buttons[buttonIndex].GetComponent<Toggle>().Select();
                }

            }
            //Increments option index and moves the image when keys are pressed

            // When key is pressed, run the method according to option index
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (buttons[buttonIndex].GetComponent<Button>() != null)
                {
                    buttons[buttonIndex].GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    buttons[buttonIndex].GetComponent<Toggle>().isOn = !buttons[buttonIndex].GetComponent<Toggle>().isOn;
                }
                
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
}
