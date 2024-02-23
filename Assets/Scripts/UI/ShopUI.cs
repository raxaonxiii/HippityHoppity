using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MenuUI
{
    public static ShopUI Instance = null;
    public ShopFolder leftFolder, rightFolder, currentFolder;
    public PopUp purchasePopUp, equipPopUp;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        leftFolder.SetToggleState(true);
        rightFolder.SetToggleState(false);
    }

    public void LoadSkins(ShopItem[] allSkins, bool[] purchasedSkins, int folderNum)
    {
        StartCoroutine(leftFolder.FillList(allSkins, purchasedSkins, folderNum));
    }
    
    public void LoadThemes(ShopItem[] allThemes, bool[] purchasedThemes, int folderNum)
    {
        StartCoroutine(rightFolder.FillList(allThemes, purchasedThemes, folderNum));
    }

    public ShopFolder GetCurrentFolder()
    {
        return currentFolder;
    }

    public void SetCurrentFolder(ShopFolder newFolder)
    {
        currentFolder = newFolder;
    }


    public void ActivateShopButtons()
    {
        for (int i = 0; i < ShopUI.Instance.GetCurrentFolder().shopButtons.Count; ++i)
        {
            ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().interactable = true;
        }
    }

    public void ClosePopUps()
    {
        ClosePopUp();
        if (purchasePopUp.gameObject.activeInHierarchy)
            purchasePopUp.ClosePopUp();
        if (equipPopUp.gameObject.activeInHierarchy)
            equipPopUp.ClosePopUp();
        foreach (GameObject button in currentFolder.shopButtons)
            button.GetComponent<ShopButton>().interactable = true;
    }
}
