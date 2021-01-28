using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : ACollectable
{
    protected override void OnCollisionWithThisObject()
    {
        player.GetComponent<Player>().OnMilkCollected();
        Destroy(this.gameObject);
    }


    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (!collision.gameObject.name.Contains("Sand"))
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log(collision.gameObject.name);
    //    }
    //}


}
