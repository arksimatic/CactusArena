using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ACollectable
{
    public GameObject BarrierPrefab;
    protected override void OnCollisionWithThisObject()
    {
        Instantiate(BarrierPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }

}
