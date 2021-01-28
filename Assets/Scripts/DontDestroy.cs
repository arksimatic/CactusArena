using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //this script is legit way to do not destroy objects and do not duplicate ones
    private static GameObject go;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (go == null)
            go = gameObject;
        else
            Destroy(gameObject);
    }

    //Besause of the license needs - music here is from www.bensound.com
}
