using UnityEngine;

[CreateAssetMenu( fileName = "theme", menuName = "Theme", order = 0)]
public class ThemeData : ScriptableObject
{
    public string Name = "";
    public GameObject[] Islands = new GameObject[0];
    public Material SkyBox = null;
    public AudioClip Song = null;
}
