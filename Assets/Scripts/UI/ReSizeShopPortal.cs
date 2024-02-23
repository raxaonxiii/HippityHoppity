using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReSizeShopPortal : MonoBehaviour
{
    public GameObject portal;
    public Vector2 baseResolution;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResizePortal();       
    }

    private void ResizePortal()
    {
        float resizeWidth = Screen.width / baseResolution.x;
        float resizeHeight = Screen.height / baseResolution.y;

        if (resizeWidth > 1)
            resizeWidth *= .7f;
        else if (resizeWidth < 1)
            resizeWidth *= 1.7f;
        if (resizeHeight > 1)
            resizeHeight *= .7f;
        else if (resizeHeight < 1)
            resizeHeight *= 1.7f;


        portal.transform.localScale = new Vector3(resizeWidth, resizeHeight, 1f);
    }
}
