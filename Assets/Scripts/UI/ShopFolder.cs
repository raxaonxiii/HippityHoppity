using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFolder : MonoBehaviour
{
    public bool leftFolder;
    public Toggle toggle;
    public ScrollRect scroller;
    public GameObject itemPrefab;
    public List<GameObject> shopButtons;
    
    public void SetActiveFolder(bool value)
    {
        scroller.gameObject.SetActive(value);
        if (value)
        {
            transform.SetSiblingIndex(2);
            ShopUI.Instance.SetCurrentFolder(this);
        }

        for (int i = 0; i < shopButtons.Count; ++i)
            shopButtons[i].gameObject.SetActive(value);
    }

    public bool GetToggleState()
    {
        return toggle.isOn;
    }

    public void SetToggleState(bool value)
    {
        toggle.isOn = value;
        if (value)
            ShopUI.Instance.SetCurrentFolder(this);
        SetActiveFolder(value);
    }

    public IEnumerator FillList(ShopItem[] allItems, bool[] purchasedItems, int folderNum)
    {
        for (int i = 0; i < allItems.Length; ++i)
        {
            GameObject newItem = Instantiate(itemPrefab, scroller.content.transform);
            newItem.GetComponent<ShopButton>().SetUpShopItem(allItems[i], purchasedItems[i], folderNum);
            shopButtons.Add(newItem);
        }

        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
    }

    public bool IsLeftFolder()
    {
        return leftFolder;
    }
}
