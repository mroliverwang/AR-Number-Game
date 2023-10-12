using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    //indicate whether the option on the button is right or wrong
    public bool Value
    { get; set; }

    public string Text
    { get; set; }

    private GameObject _gameStateManager;



    private void Awake()
    {
        Value = false;
        _gameStateManager = GameObject.FindGameObjectWithTag("gameStateManager");
    }



    private void Update()
    {
        //update game message
        if(Text != null)
        {
            GetComponentInChildren<TMP_Text>().SetText(Text);
        }
    }



    public void CheckAnswer()
    {
        if (Value == true)
        {
            //next challenge
            if(_gameStateManager != null)
            {
                _gameStateManager.GetComponent<GameStateManager>().ResetChallenge();
            }
        }
        else
        {
            //try again
            _gameStateManager.GetComponent<GameStateManager>().DisplayMessage();
        }
    }



    //press button to start game
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
