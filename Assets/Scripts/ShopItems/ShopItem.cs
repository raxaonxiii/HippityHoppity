using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItem : ScriptableObject
{
    public int ID;
    public string itemName;
    public int cost;
    public GameObject[] prefabs;
    public Sprite image;
}
