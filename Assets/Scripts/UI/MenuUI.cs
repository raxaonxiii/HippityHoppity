using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private Animator anim;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ClosePopUp()
    {
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        anim.SetTrigger("Shrink");
        SFXManager.Instance.PlaySound("close_button");
        yield return new WaitForSeconds(.3f);
        gameObject.SetActive(false);
    }

    public void MuteBGM(bool value)
    {
        BGMManager.Instance.Mute(value);
    }
}
