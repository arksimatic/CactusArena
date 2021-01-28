using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitMethod()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
