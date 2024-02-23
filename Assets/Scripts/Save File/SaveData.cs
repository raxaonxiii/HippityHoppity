using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    /*
     * only saving int value for theme array
     * ThemeObject[]
     *  Island Set
     *  Skybox
     *  Song
     */

    /*
     * next save option settings
     */

    public int highScore;
    public int coinCount;
    public bool[] purchasedSkins;
    public int currentSkinID;
    public bool[] purchasedThemes;
    public int currentThemeID;
    public bool BGMMute;
    public bool SFXMute;

    public SaveData( )
    {
        highScore = 0;
        coinCount = 0;
        purchasedSkins = null;
        currentSkinID = 0;
        purchasedThemes = null;
        currentThemeID = 0;
        BGMMute = false;
        SFXMute = false;
    }

    public SaveData( GameManager gMan )
    {
        highScore = gMan.platformCount;
        coinCount = gMan.coinCount;

        currentSkinID = gMan.currentSkinID;
        currentThemeID = gMan.currentThemeID;

        purchasedSkins = gMan.purchasedSkins;
        purchasedThemes = gMan.purchasedThemes;

        BGMMute = gMan.BGMMute;
        SFXMute = gMan.SFXMute;
    }
}