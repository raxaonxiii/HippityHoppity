using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance = null;
    [SerializeField] private ShopItem[] allSkins, allThemes;
    [SerializeField] private bool[] purchasedSkins, purchasedThemes;
    public CoinCounter coinCounter;
    public GameObject confirmPurchase, purchasedPopUp, equipPopUp, skinYesButton, themeYesButton, insufficientCoinsPopUp;
    public ShopButton tempButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        allSkins = Resources.LoadAll<ShopItem>("PlayerSkins/ScriptableObjects");
        purchasedSkins = GameManager.Instance.purchasedSkins;
        
        allThemes = Resources.LoadAll<ShopItem>("Themes/ScriptableObjects");
        purchasedThemes = GameManager.Instance.purchasedThemes;
    }

    private void Start()
    {
        ShopUI.Instance.LoadSkins(allSkins, purchasedSkins, 0);
        ShopUI.Instance.LoadThemes(allThemes, purchasedThemes, 1);
    }

    public void SetTempButton(ShopButton button)
    {
        tempButton = button;
    }

    public void ConfirmPurchase()
    {
        for (int i = 0; i < ShopUI.Instance.GetCurrentFolder().shopButtons.Count; ++i)
            ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().interactable = false;
        confirmPurchase.GetComponent<PopUp>().SetUpText(tempButton.item.itemName + "?");
        if (ShopUI.Instance.GetCurrentFolder().IsLeftFolder())
        {
            skinYesButton.SetActive(true);
            themeYesButton.SetActive(false);
        }
        else
        {
            skinYesButton.SetActive(false);
            themeYesButton.SetActive(true);
        }

        confirmPurchase.SetActive(true);
    }
    
    public void PurchaseSkin()
    {
        int tempCoins = GameManager.Instance.coinCount - tempButton.GetItem().cost;
        if(tempCoins >= 0)
        {
            coinCounter.CountDown(GameManager.Instance.coinCount, tempButton.GetItem().cost);
            GameManager.Instance.coinCount = tempCoins;
            tempButton.SetPurchased(true);
            purchasedSkins[tempButton.ID] = true;
            SFXManager.Instance.PlaySound("ka-ching");
            ConfirmEquip();

            SaveSystem.Save(GameManager.Instance);
        }
        else
        {
            StartCoroutine(InsufficientFunds());
        }
    }

    public void PurchaseTheme()
    {
        int tempCoins = GameManager.Instance.coinCount - tempButton.GetItem().cost;
        if (tempCoins >= 0)
        {
            coinCounter.CountDown(GameManager.Instance.coinCount, tempButton.GetItem().cost);
            GameManager.Instance.coinCount = tempCoins;
            tempButton.SetPurchased(true);
            purchasedThemes[tempButton.ID] = true;
            SFXManager.Instance.PlaySound("ka-ching");
            ConfirmEquip();

            SaveSystem.Save(GameManager.Instance);
        }
        else
        {
            StartCoroutine(InsufficientFunds());
        }
    }

    private IEnumerator InsufficientFunds()
    {
        insufficientCoinsPopUp.SetActive(true);
        yield return new WaitForSeconds(1f);
        insufficientCoinsPopUp.GetComponent<Animator>().SetTrigger("Shrink");
        yield return new WaitForSeconds(insufficientCoinsPopUp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        insufficientCoinsPopUp.SetActive(false);
        for (int i = 0; i < ShopUI.Instance.GetCurrentFolder().shopButtons.Count; ++i)
            ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().interactable = true;
    }


    public void ConfirmEquip()
    {
        for (int i = 0; i < ShopUI.Instance.GetCurrentFolder().shopButtons.Count; ++i)
            ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().interactable = false;
        equipPopUp.GetComponent<PopUp>().SetUpText(tempButton.item.itemName + "?");
        equipPopUp.SetActive(true);
    }

    public void Equip()
    {
        if (tempButton.folderNum == 0)
        {
            GameManager.Instance.currentSkinID = tempButton.ID;
            GameManager.Instance.player.SetPlayerPrefab(GameManager.Instance.skins[GameManager.Instance.currentSkinID]);
            GameManager.Instance.player.LoadPlayerPrefab();

            for (int i = 0; i < ShopUI.Instance.GetCurrentFolder().shopButtons.Count; ++i)
            {
                if (ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().ID == GameManager.Instance.currentSkinID)
                    ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().SetEquip(true);
                else
                    ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().SetEquip(false);
            }
        }

        if (tempButton.folderNum == 1)
        {
            GameManager.Instance.currentThemeID = tempButton.ID;
            GameManager.Instance.pMan.SetPlatformPrefabs(GameManager.Instance.themes[GameManager.Instance.currentThemeID]);
            GameManager.Instance.pMan.LoadPlatformPrefab();

            for (int i = 0; i < ShopUI.Instance.GetCurrentFolder().shopButtons.Count; ++i)
            {
                if (ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().ID == GameManager.Instance.currentThemeID)
                    ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().SetEquip(true);
                else
                    ShopUI.Instance.GetCurrentFolder().shopButtons[i].GetComponent<ShopButton>().SetEquip(false);
            }
        }

        SaveSystem.Save(GameManager.Instance);
    }
}
