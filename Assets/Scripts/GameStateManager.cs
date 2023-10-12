using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public static int Mistakes;
    public static int Success;

    public AudioClip correct;
    public AudioClip tryAgain;

    private GameObject _spawnManger;

    [SerializeField]
    private TMP_Text _text;


    void Awake()
    {
        _spawnManger = GameObject.FindGameObjectWithTag("spawn");

        Mistakes = 0;
        Success = 0;
    }

    

    public void ResetChallenge()
    {
        //audio feedback
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(correct);
        }
        _text.SetText("Excellent! You are correct!");
        StartCoroutine(EraseMessage());

        if(Success < 9)
        {
            if (_spawnManger != null)
            {
                Success++;
                _spawnManger.GetComponent<SpawnManager>().DeactivateSelections();
                _spawnManger.GetComponent<SpawnManager>().Spawn = true;
            }
        }
        else
        {
            //wining condition
            SceneManager.LoadScene(2);
        }
    }



    public void DisplayMessage()
    {
        //audio feedback
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(tryAgain);
        }      
        _text.SetText("Let's try again");
        StartCoroutine(EraseMessage());

        Mistakes++;

        _spawnManger.GetComponent<SpawnManager>().TryAgain();
    }

    //reset messages after few seconds
    IEnumerator EraseMessage()
    {
        yield return new WaitForSeconds(3f);
        _text.SetText("");
    }


}
