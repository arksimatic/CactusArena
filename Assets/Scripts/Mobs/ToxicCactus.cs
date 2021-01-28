using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicCactus : Cactus
{

    protected override void SetGraphicalAngleAndFlyInAngleWithAutoaim(GameObject bullet)
    {
        bullet.GetComponent<ToxicSpike>().SetGraphicalAngleAndFlyInAngleWithAutoaim(UnityEngine.Random.Range(0, 360));
    }

    protected override GameObject SpawnNewBullet()
    {
        GameObject bullet = Instantiate(objBullet, this.transform.position, new Quaternion(0, 0, 0, 0), objSpikesFolder.transform);
        bullet.GetComponent<ToxicSpike>().enabled = true;

        //Destroy(bullet.GetComponent<FreezingSpike>()); //it's probably done directly in freezingSpike, toxicSpike etc.
        return bullet;
    }

    protected override void SetGraphicalAngleAndFlyInAngle(GameObject bullet, int angle)
    {
        bullet.GetComponent<ToxicSpike>().SetGraphicalAngleAndFlyInAngle(angle);
    }

    protected override void SetGraphicalAngleAndFlyInAngleWithExplosion(GameObject bullet, int explosionTime)
    {
        bullet.GetComponent<ToxicSpike>().SetGraphicalAngleAndFlyInAngleWithExplosion(UnityEngine.Random.Range(0, 360), 1);
    }
}
