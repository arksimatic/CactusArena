using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateCollectables : MonoBehaviour
{
    public List<GameObject> collectables;
    protected GameObject sand;
    public Single MinTimeToNextItem;
    public Single MaxTimeToNextItem;
    private Vector2 MinSpawnPosition;
    private Vector2 MaxSpawnPosition;
    public GameObject CollectablesFolder;

    void Start()
    {
        sand = GameObject.Find("tlmSand");
        UnityEngine.Random.InitState(12343);
        StartCoroutine("SpawnCollectable", 0.1f);

        SetMinAndMaxPosition();
    }

    void SetMinAndMaxPosition()
    {
        //the spawn is actually the object this script is attached to
        Single width = ((RectTransform)this.gameObject.transform).rect.width;
        Single height = ((RectTransform)this.gameObject.transform).rect.height;
        Single x = this.gameObject.transform.position.x;
        Single y = this.gameObject.transform.position.y;

        MinSpawnPosition = new Vector2(x - width / 2, y - height / 2);
        MaxSpawnPosition = new Vector2(x + width / 2, y + height / 2);
    }

    IEnumerator SpawnCollectable(Single secondsToWait)
    {
        //randomize position and collectable
        Int32 randomIndexOfItem = UnityEngine.Random.Range(0, collectables.Count);
        Single posX = UnityEngine.Random.Range(MinSpawnPosition.x, MaxSpawnPosition.x);
        Single posY = UnityEngine.Random.Range(MinSpawnPosition.y, MaxSpawnPosition.y);

        //create collectable
        GameObject createdCollectable = Instantiate(collectables[randomIndexOfItem], new Vector3(posX, posY, 0), Quaternion.identity);
        createdCollectable.name = "collectable";
        createdCollectable.transform.SetParent(CollectablesFolder.transform);

        //despawn collectable
        yield return new WaitForSeconds(secondsToWait);
        StartCoroutine("DespawnCollectable", createdCollectable);

        //spawn another collectable
        StartCoroutine("SpawnCollectable", UnityEngine.Random.Range(MinTimeToNextItem, MaxTimeToNextItem)); //0.2f for testing
    }

    IEnumerator DespawnCollectable(GameObject expiringCollectalbe)
    {
        //blink 6 times before despawn
        for(int i=0; i<6; i++)
        {
            StartCoroutine("BlinkBeforeDisappearing", expiringCollectalbe);
            yield return new WaitForSeconds(0.4f);
        }
        if (expiringCollectalbe != null) Destroy(expiringCollectalbe);
    }

    IEnumerator BlinkBeforeDisappearing(GameObject expiringCollectalbe)
    {
        Color color = new Color();
        if (expiringCollectalbe != null) color = expiringCollectalbe.GetComponent<SpriteRenderer>().color;
        if (expiringCollectalbe != null) expiringCollectalbe.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.3f);
        yield return new WaitForSeconds(0.2f);
        if (expiringCollectalbe != null) expiringCollectalbe.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1f);
    }
}
