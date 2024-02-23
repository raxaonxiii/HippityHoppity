using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetTrans;

    private Vector3 myTrans;
    private float currentPosX;

    void Start( )
    {
        myTrans = transform.position;
        currentPosX = myTrans.x;
    }

    // Update is called once per frame
    void Update( )
    {
        transform.position = new Vector3( currentPosX + targetTrans.position.x, myTrans.y, myTrans.z );
    }
}
