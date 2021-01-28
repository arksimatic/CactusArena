using System;
using System.Collections;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public GameObject objBullet;
    public GameObject objSpikesFolder;

    private Boolean _isBursting = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Spikes"), LayerMask.NameToLayer("Spikes"));
        StartCoroutine(Run());
    }

    private IEnumerator AutoaimBurst() 
    {
        _isBursting = true;
        for (Int32 i = 0; i < 36; i += 6)
        {
            yield return new WaitForSeconds(0.4f);
            GameObject bullet = Instantiate(objBullet, Vector3.zero, new Quaternion(0, 0, 0, 0), objSpikesFolder.transform);
            bullet.GetComponent<Spike>().SetGraphicalAngleAndFlyInAngleWithAutoaim(UnityEngine.Random.Range(0, 360));
        }
        _isBursting = false;
    }

    private IEnumerator RadialBurst()
    {
        _isBursting = true;
        for (Int32 i = 0; i < 360; i += 6)
        {
            yield return new WaitForSeconds(0.03f);
            GameObject bullet = Instantiate(objBullet, Vector3.zero, new Quaternion(0, 0, 0, 0), objSpikesFolder.transform);
            bullet.GetComponent<Spike>().SetGraphicalAngleAndFlyInAngle(i);
        }
        _isBursting = false;

    }

    private IEnumerator RandomBurst()
    {
        _isBursting = true;

        for (Int32 i = 0; i < 360; i += 6)
        {
            yield return new WaitForSeconds(0.04f);
            GameObject bullet = Instantiate(objBullet, Vector3.zero, new Quaternion(0, 0, 0, 0), objSpikesFolder.transform);
            bullet.GetComponent<Spike>().SetGraphicalAngleAndFlyInAngle(UnityEngine.Random.Range(0, 360));
        }
        _isBursting = false;

    }

    private IEnumerator ExplosionBurst()
    {
        _isBursting = true;

        GameObject bullet = Instantiate(objBullet, Vector3.zero, new Quaternion(0, 0, 0, 0), objSpikesFolder.transform);
        bullet.GetComponent<Spike>().SetGraphicalAngleAndFlyInAngleWithExplosion(UnityEngine.Random.Range(0, 360), 1);
        _isBursting = false;

        yield return null;

    }

    private IEnumerator Run()
    {
        if (!_isBursting)
        {
            Int32 burstId = UnityEngine.Random.Range(0, 3);
            //Int32 burstId = 2;
            switch (burstId)
            {
                case 0: StartCoroutine(RadialBurst()); break;
                case 1: StartCoroutine(RandomBurst()); break;
                case 2: StartCoroutine(ExplosionBurst()); yield return new WaitForSeconds(2f); break;
                //case 3: StartCoroutine(AutoaimBurst()); break;
            }
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(Run());
    }
}
