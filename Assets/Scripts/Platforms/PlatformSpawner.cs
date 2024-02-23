using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public float offsetX = 5;
    //public GameObject spawnFX;

    private GameObject PlatformPrefabv1;
    private GameObject PlatformPrefabv2;
    private float currentPosX;
    
    void OnEnable( )
    {
        currentPosX = 0;
    }

    public void SetPlatformPrefabs(ShopItem item)
    {
        PlatformPrefabv1 = item.prefabs[0];
        PlatformPrefabv2 = item.prefabs[1];
    }

    public void LoadPlatformPrefab()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
        GameObject model = Instantiate(PlatformPrefabv1, transform.position, Quaternion.identity);
        model.GetComponent<Animator>().enabled = false;
        model.transform.parent = transform;
        model.tag = "Platform";
    }

    public void Spawn( )
    {
        int rand = Random.Range( 0, 10 );
        currentPosX += (rand < 5) ? offsetX : (offsetX*2);

        rand = Random.Range( 0, 10 );        
        //Instantiate(spawnFX, new Vector3(currentPosX, this.transform.position.y - 20, this.transform.position.z), spawnFX.transform.rotation);
        GameObject temp = Instantiate( (rand < 5) ? PlatformPrefabv1 : PlatformPrefabv2 );
        temp.transform.parent = transform;
        temp.transform.position = new Vector3( currentPosX, 0, 0 );
    }

    // public void Reset( )
    // {
    //     foreach( GameObject child in gameObject )
    //         Destroy( child );
    // }
}
