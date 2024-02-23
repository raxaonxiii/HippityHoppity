using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public List<GameObject> cloudList;

    [Header("Spawn Settings")]
    [Tooltip("Need to respawn clouds to change the max clouds")]
    public int maxClouds = 6;
    public float radius = 1;
    [Range(0, 180)]
    public float viewAngle = 90;
    public float speed = 1;
    public float distBetweenClouds = 0;
    public float yOffset = 0;

    private List<Cloud> _activeClouds = new List<Cloud>();

    private void Start()
    {
        if (maxClouds == 0)
        {
            Debug.LogWarning("Max Clouds is 0..");
            return;
        }
        if (cloudList.Count == 0)
        {
            Debug.LogWarning("cloud list is 0...");
            return;
        }

        SpawnClouds();
    }

    private void Update()
    {
        for (int i = 0; i < _activeClouds.Count; i++)
        {
            _activeClouds[i].Move(speed, radius, distBetweenClouds * i, yOffset);
        }
    }

    public void SpawnClouds()
    {
        if(_activeClouds.Count !=0)
        {
            foreach(Cloud cloud in _activeClouds)
            {
                Destroy(cloud.gameObject);
            }

            _activeClouds.Clear();
        }
        

        for (int i = 0; i < maxClouds; i++)
        {
            int randIndex = Random.Range(0, cloudList.Count - 1);
            GameObject cloud = GameObject.Instantiate(cloudList[randIndex], this.transform.position, Quaternion.identity);
            _activeClouds.Add(cloud.GetComponent<Cloud>());
        }

        foreach (Cloud cloud in _activeClouds)
        {
            cloud.transform.SetParent(this.transform);
            cloud.gameObject.SetActive(true);
        }
    }
}