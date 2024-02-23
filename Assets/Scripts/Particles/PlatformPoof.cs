using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPoof : MonoBehaviour
{
    private bool activated = false;
    [SerializeField]
    private ParticleSystem _pSystem;
    [SerializeField]
    private ParticleSystem _pSystem2;

    // Start is called before the first frame update
    void Start()
    {
        activated = true;
        _pSystem.Play();
        _pSystem2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated == false)
            return;

        if (_pSystem.IsAlive() == false && _pSystem2.IsAlive() == false)
        {
            Destroy(this.gameObject);
        }
    }
}
