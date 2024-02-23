using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerController parent;

    public void SetUp(PlayerController pController)
    {
        parent = pController;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            parent.isGrounded = true;
            GameManager.Instance.HOP();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            parent.isGrounded = false;
        }
    }
}
