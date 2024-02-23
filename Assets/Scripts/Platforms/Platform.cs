using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void Fall( )
    {
        myAnim.SetBool( "Fall", true );
    }
}
