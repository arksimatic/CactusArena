using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//add this bibliothek for the working with scenes
using UnityEngine.SceneManagement;

public class Kapar : MonoBehaviour
{


    public void LoadSceneAfterTime()
    {
        SceneManager.LoadScene(15);
    }


}