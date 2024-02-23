using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public PlatformSpawner pMan;

    void OnTriggerEnter( Collider other )
    {
        if( other.CompareTag( "Platform" ) )
        {
            //gMan.HOP( );
            GameManager.Instance.tempCount++;
            pMan.Spawn( );
            other.GetComponent<Platform>().Fall( );  
            Destroy( other.gameObject, 2f ); 
        }
    }
}
