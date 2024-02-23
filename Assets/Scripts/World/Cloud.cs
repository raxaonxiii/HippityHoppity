using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float _oldYOffset = 0;

    public void Move(float speed, float radius, float xOffset, float yOffset)
    {
        Vector3 newPos = this.transform.position;
        newPos.x = this.transform.parent.transform.position.x + (Mathf.Cos((Time.time + xOffset) * speed) * radius);
        newPos.z = this.transform.parent.transform.position.z + (Mathf.Sin((Time.time + xOffset) * speed) * radius);

        if(yOffset != _oldYOffset)
        {
            newPos.y = this.transform.parent.position.y + Random.Range(-yOffset, yOffset);
            _oldYOffset = yOffset;
        }

        this.transform.position = newPos;
        this.transform.LookAt(this.transform.parent);
    }
}
