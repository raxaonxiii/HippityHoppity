using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public PlayerController player;
    public PlatformSpawner pMan;
    public CoinCounter mainMenuCoinCounter, gManCounter;

    public GameObject TitleUI, HUD, HUDButtons, CountDownUI, GameOverUI, PausedPopUp, SettingsPopUp, Fader, controlsTut;
    public Toggle BGMMuteToggle, SFXMuteToggle;
    public TextMeshProUGUI cointCountText, Score, CountDown, gameOverScore;
    public int platformCount;
    public int tempCount = 0;
    public int maxPlats;

    public int highScore, coinCount, currentSkinID, currentThemeID;
    public float conversionRate = 1f;
    public ShopItem[] skins, themes;
    public bool[] purchasedSkins, purchasedThemes;

    public bool paused, BGMMute, SFXMute;
    bool isLoading = false;
    bool isPlaying = false;
    bool gameOver = false;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadItems();
        player.SetUp();
    }

    void OnEnable( )
    {
        TitleUI.SetActive( true );
        CountDownUI.SetActive( false );
        HUD.SetActive( false );
        GameOverUI.SetActive( false );
        platformCount = 0;

        DisplayCoins(coinCount);
    }

    void Update( )
    {
        if( player.isFalling )
        {
            if (!gameOver)
                GameOver();
        }
    }

    private void LoadItems()
    {
        SaveData data = SaveSystem.LoadSave();
        
        highScore = data.highScore;
        coinCount = data.coinCount;
        DisplayCoins(coinCount);
        
        currentSkinID = data.currentSkinID;
        skins = Resources.LoadAll<ShopItem>("PlayerSkins/ScriptableObjects");
        player.SetPlayerPrefab(skins[currentSkinID]);

        purchasedSkins = new bool[skins.Length];
        if (data.purchasedSkins != null)
        {
            for (int i = 0; i < data.purchasedSkins.Length; ++i)
                purchasedSkins[i] = data.purchasedSkins[i];
        }
        purchasedSkins[0] = true;

        currentThemeID = data.currentThemeID;
        themes = Resources.LoadAll<ShopItem>("Themes/ScriptableObjects");
        pMan.SetPlatformPrefabs(themes[currentThemeID]);
        pMan.LoadPlatformPrefab();

        purchasedThemes = new bool[themes.Length];
        if (data.purchasedThemes != null)
        {
            for (int i = 0; i < data.purchasedThemes.Length; ++i)
                purchasedThemes[i] = data.purchasedThemes[i];
        }
        purchasedThemes[0] = true;

        BGMManager.Instance.SetMuteToggle(BGMMuteToggle);
        BGMManager.Instance.SetMute(data.BGMMute);
        SFXManager.Instance.SetMuteToggle(SFXMuteToggle);
        SFXManager.Instance.SetMute(data.SFXMute);
    }

    public void BeginGame()
    {
        if (!isPlaying && !isLoading)
        {
            isLoading = true;
            StartCoroutine(Spawn());
            StartCoroutine(StartGame());
        }
    }

    IEnumerator Spawn( )
    {
        int plats = 0;
        while( plats < maxPlats )
        {
            pMan.Spawn( );
            yield return new WaitForSeconds( 0.25f );
            plats++;
        }
    }

    IEnumerator StartGame( )
    {
        TitleUI.SetActive( false );
        CountDownUI.SetActive( true );
        HUDButtons.SetActive(false);
        HUD.SetActive( true );
        SetControlsOn(true);

        Score.text = "Score: " + platformCount.ToString( );

        CountDown.text = "3";
        yield return new WaitForSeconds( 1f );
        CountDown.text = "2";
        yield return new WaitForSeconds( 1f );
        CountDown.text = "1";
        yield return new WaitForSeconds( 1f );
        CountDown.text = "HOP";

        player.isWaitingToStartGame = false;
        isPlaying = true;
        HUDButtons.SetActive(true);
        yield return new WaitForSeconds( 1f );
        CountDownUI.SetActive( false );
    }

    public void SetControlsOn(bool value)
    {
        controlsTut.GetComponent<Animator>().SetBool("On", value);
    }

    public void HOP( )
    {
        platformCount += tempCount;
        tempCount = 0;
        Score.text = "Score: " + platformCount.ToString( );
    }

    public void PauseGame(bool value)
    {
        if(value)
        {
            paused = true;
            Time.timeScale = 0;
            PausedPopUp.SetActive(true);
        }
        else
        {
            PausedPopUp.SetActive(false);
            StartCoroutine(Unpause());
        }
    }

    private IEnumerator Unpause()
    {
        yield return new WaitForEndOfFrame();
        paused = false;
        Time.timeScale = 1;
    }

    public void OpenSettings(bool value)
    {
        if (value)
        {
            paused = true;
            Time.timeScale = 0;
            SettingsPopUp.SetActive(true);
        }
        else
        {
            SettingsPopUp.SetActive(false);
            StartCoroutine(Unpause());
        }
    }

    private void GameOver()
    {
        gameOver = true;
        SFXManager.Instance.PlaySound("DM-CGS-07");
        gameOverScore.text = platformCount.ToString();
        HUD.SetActive(false);
        GameOverUI.SetActive(true);

        if (platformCount > highScore)
            highScore = platformCount;

        StartCoroutine(WaitForGameOverLoad());
        
    }

    private IEnumerator WaitForGameOverLoad()
    {
        yield return new WaitForSeconds(0.25f);
        gManCounter.SetCoinCount(coinCount, platformCount);
        coinCount += Mathf.FloorToInt(platformCount * conversionRate);
        gManCounter.CountUp(coinCount);
        DisplayCoins(coinCount);
        SaveSystem.Save(this);
    }

    public void DisplayCoins(int count)
    {
        cointCountText.text = count.ToString();
    }

    public void ResetGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    

    private void OnApplicationQuit()
    {
        SaveSystem.Save(this);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("game paused");
            SaveSystem.Save(this);
        }
    }
}
