using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopButton : Button
{
    public ShopItem item;
    public Transform modelParent;
    public Image icon;
    public GameObject[] prefabs;
    public int ID, folderNum;
    public TextMeshProUGUI itemName, cost;
    public Image purchaseMarker, equippedMarker;
    private bool purchased, equipped;
    public float jiggleMult;

    public void SetUpShopItem(ShopItem newItem, bool purchased, int folderNum)
    {
        item = newItem;
        ID = newItem.ID;
        this.folderNum = folderNum;
        prefabs = newItem.prefabs;
        itemName.text = newItem.itemName;
        cost.text = newItem.cost.ToString();

        SetPurchased(purchased);
        if (folderNum == 0 && ID == GameManager.Instance.currentSkinID)
            SetEquip(true);
        else if (folderNum == 1 && ID == GameManager.Instance.currentThemeID)
            SetEquip(true);
        else
            SetEquip(false);
        icon.sprite = newItem.image;

        //GameObject model = Instantiate(prefabs[0], modelParent.transform.position, Quaternion.identity);
        //SetLayer(7, model.transform);
        //model.transform.parent = modelParent.transform;
        //if (model.tag == "Platform")
        //    model.transform.localPosition = new Vector3(0, 20, 0);
        //else
        //    model.transform.localPosition = new Vector3(0, 5, 0);

        //if (model.GetComponent<JellyCube.RubberEffect>())
        //{
        //    model.transform.localScale = new Vector3(75f, 75f, 75f);
        //    model.GetComponent<JellyCube.RubberEffect>().m_EffectIntensity = jiggleMult;
        //    model.GetComponentInChildren<PlayerCollider>().enabled = false;
        //}

        //if (model.GetComponent<Animator>())
        //{
        //    model.transform.localScale = new Vector3(25f, 25f, 25f);
        //    model.GetComponent<Animator>().enabled = false;
        //}
    }

    void SetLayer(int newLayer, Transform trans)
    {
        trans.gameObject.layer = newLayer;
        foreach (Transform child in trans)
        {
            child.gameObject.layer = newLayer;
            if (child.childCount > 0)
            {
                SetLayer(newLayer, child.transform);
            }
        }
    }

    public void SetPurchased(bool value)
    {
        purchased = value;
        purchaseMarker.gameObject.SetActive(value);
    }

    public bool GetPurchased()
    {
        return purchased;
    }

    public void SetEquip(bool value)
    {
        equipped = value;
        equippedMarker.gameObject.SetActive(value);
    }

    public ShopItem GetItem()
    {
        return item;
    }

    public void ButtonClick()
    {
        ShopSystem.Instance.SetTempButton(this);
        if (!purchased)
            ShopSystem.Instance.ConfirmPurchase();
        else if (purchased && !equipped)
            ShopSystem.Instance.ConfirmEquip();
    }
}
