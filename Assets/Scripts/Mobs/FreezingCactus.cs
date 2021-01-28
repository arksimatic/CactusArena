using System;
using System.Collections;
using UnityEngine;

public class FreezingCactus : Cactus
{

    protected override void SetGraphicalAngleAndFlyInAngleWithAutoaim(GameObject bullet)
    {
        bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngleWithAutoaim(UnityEngine.Random.Range(0, 360));
    }

    protected override GameObject SpawnNewBullet()
    {
        GameObject bullet = Instantiate(objBullet, this.transform.position, new Quaternion(0, 0, 0, 0), objSpikesFolder.transform);
        bullet.GetComponent<FreezingSpike>().enabled = true;

        //Destroy(bullet.GetComponent<FreezingSpike>()); //it's probably done directly in freezingSpike, toxicSpike etc.
        return bullet;
    }

    protected override void SetGraphicalAngleAndFlyInAngle(GameObject bullet, int angle)
    {
        bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngle(angle);
    }

    protected override void SetGraphicalAngleAndFlyInAngleWithExplosion(GameObject bullet, int explosionTime)
    {
        bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngleWithExplosion(UnityEngine.Random.Range(0, 360), 1);
    }
}


//public class FreezingCactus// : Cactus
//{

//protected override void SetGraphicalAngleAndFlyInAngleWithAutoaim(GameObject bullet)
//{
//    bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngleWithAutoaim(UnityEngine.Random.Range(0, 360));
//}


//private GameObject SpawnNewBullet()
//{ 
//    GameObject bullet = Instantiate(objBullet, this.transform.position, new Quaternion(0, 0, 0, 0), objSpikesFolder.transform);
//    bullet.GetComponent<FreezingSpike>().enabled = true;
//    Destroy(bullet.GetComponent<NormalSpike>());

//    return bullet;
//}

//private IEnumerator AutoaimBurst()
//{
//    _isBursting = true;
//    for (Int32 i = 0; i < 36; i += 6)
//    {
//        yield return new WaitForSeconds(0.4f);
//        animator.SetTrigger("oneShot");
//        GameObject bullet = SpawnNewBullet();
//        bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngleWithAutoaim(UnityEngine.Random.Range(0, 360));
//    }
//    _isBursting = false;
//}

//private IEnumerator RadialBurst()
//{
//    _isBursting = true;
//    for (Int32 i = 0; i < 360; i += 6)
//    {
//        yield return new WaitForSeconds(0.03f);
//        GameObject bullet = SpawnNewBullet();
//        bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngle(i);
//    }
//    _isBursting = false;

//}

//private IEnumerator RandomBurst()
//{
//    _isBursting = true;

//    for (Int32 i = 0; i < 360; i += 6)
//    {
//        yield return new WaitForSeconds(0.04f);
//        GameObject bullet = SpawnNewBullet();
//        bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngle(UnityEngine.Random.Range(0, 360));
//    }
//    _isBursting = false;

//}

//private IEnumerator ExplosionBurst()
//{
//    _isBursting = true;

//    GameObject bullet = SpawnNewBullet();
//    bullet.GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngleWithExplosion(UnityEngine.Random.Range(0, 360), 1);
//    _isBursting = false;

//    yield return null;

//}

//private IEnumerator Run()
//{
//    if (!_isBursting)
//    {
//        Int32 burstId = UnityEngine.Random.Range(0, 4);
//        switch (burstId)
//        {
//            case 0:
//                {
//                    animator.SetBool("isShooting", true);
//                    StartCoroutine(RadialBurst()); break;
//                }
//            case 1:
//                {
//                    animator.SetBool("isShooting", true);
//                    StartCoroutine(RandomBurst()); break;
//                }
//            case 2:
//                {
//                    animator.SetBool("isShooting", false);
//                    animator.SetTrigger("oneShot");
//                    StartCoroutine(ExplosionBurst());
//                    yield return new WaitForSeconds(2f); break;
//                }
//            case 3:
//                {
//                    animator.SetBool("isShooting", false);
//                    StartCoroutine(AutoaimBurst()); break;
//                }
//        }
//    }

//    yield return new WaitForEndOfFrame();
//    StartCoroutine(Run());
//}
//}
