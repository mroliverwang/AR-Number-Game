using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
       
    [SerializeField]
    private GameObject _apple;

    private int[] _numberList;

    private GameObject[] _buttonList;
    private GameObject _buttonCollection;

    private GameObject[] _appleList;

    private int _insertIndex;

    private int randomNumber;

    public bool Spawn
    { get; set; }


    void Awake()
    {
        Spawn = true;

        randomNumber = -1;

        _insertIndex = 0;

        _appleList = new GameObject[10];

        _numberList = new int[10];

        //initialize the _buttonList 
        _buttonCollection =  GameObject.FindGameObjectWithTag("UIButton");

        _buttonList = new GameObject[_buttonCollection.transform.childCount];

        for(int index = 0; index < _buttonCollection.transform.childCount; index++)
        {
            _buttonList[index] = _buttonCollection.transform.GetChild(index).gameObject;
        }

        _buttonCollection.SetActive(false);
    }


    
    void FixedUpdate()
    {
        if (Spawn)
        {
            //reduce repetition
            randomNumber = UnityEngine.Random.Range(1,11);
            while (System.Array.Exists(_numberList, element => element == randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(0, 11);
            }

            _numberList[_insertIndex] = randomNumber;
            _insertIndex++;

            //start spawning apples
            StartCoroutine(SpawnApples(randomNumber));
        }
    }



    IEnumerator SpawnApples(int n)
    {
        Spawn = false;

        yield return new WaitForSeconds(2f);

        //trim clip manually
        GetComponent<AudioSource>().time = 1.7f;
        
        for (int index = 0; index < n; index++)
        {           
            _appleList[index] = Instantiate(_apple);
            
            //manually match the audio source with spawn speed
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1.17f);
            GetComponent<AudioSource>().Pause();
            yield return new WaitForSeconds(0.2f);                      
        }
        GetComponent<AudioSource>().Stop();
        ActivateSelections(n);
    }



    //spawn apples with lower speed
    IEnumerator SpawnApplesAgain(int n)
    {
        Spawn = false;

        yield return new WaitForSeconds(2f);

        GetComponent<AudioSource>().time = 1.7f;

        for (int index = 0; index < n; index++)
        {
            _appleList[index] = Instantiate(_apple);

            //manually match the audio source with spawn speed, slow down the speed when the player made mistakes
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1.17f);
            GetComponent<AudioSource>().Pause();
            yield return new WaitForSeconds(0.6f);
        }
        GetComponent<AudioSource>().Stop();
        ActivateSelections(n);
    }



    void ActivateSelections(int n)
    {
        _buttonCollection.SetActive(true);

        //create the right answer
        int tempIndex = Random.Range(0, 4);
        _buttonList[tempIndex].GetComponent<Button>().Value = true;
        _buttonList[tempIndex].GetComponent<Button>().Text = n.ToString();

        for (int index = 0; index < 4; index++)
        {
            //define values of other buttons
            if (index != tempIndex)
            {
                _buttonList[index].GetComponent<Button>().Value = false;

                //create wrong answers with consecutive numbers
                //avoid negative answers
                if((n + (index - tempIndex)) < 0)
                {
                    _buttonList[index].GetComponent<Button>().Text = (n + (index - tempIndex)+5).ToString();
                }
                else
                {
                    _buttonList[index].GetComponent<Button>().Text = (n + (index - tempIndex)).ToString();
                }               
            }
        }
    }




    public void DeactivateSelections()
    {
        StartCoroutine(ButtonFadeOut());
        DestroyApples();
    }



    public void TryAgain()
    {
        DeactivateSelections();
        StartCoroutine(SpawnApplesAgain(randomNumber));
    }



    private void DestroyApples()
    {
        for (int index = 0; index < _appleList.Length; index++)
        {
            Destroy(_appleList[index]);
        }
    }


    //fade out animation for button transitions
    IEnumerator ButtonFadeOut()
    {
        _buttonCollection.GetComponent<Animator>().SetBool("end", true);
        yield return new WaitForSeconds(1.5f);
        _buttonCollection.SetActive(false);
    }

}
