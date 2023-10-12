using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateProgress : MonoBehaviour
{  
    void Start()
    {
        GetComponent<TMP_Text>().SetText($"You finished 10 challenges, \n with {GameStateManager.Mistakes} mistakes made.");
    }
}
