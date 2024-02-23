using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CoinCounter : MonoBehaviour
{
    private int coinCount, scoreCount;
    private float lerpTime;
    public GameObject mathSign;
    public TextMeshProUGUI addCoins;
    public ParticleSystem particles;

    public void SetCoinCount(int coinValue, int scoreValue)
    {
        coinCount = coinValue;
        GetComponent<TextMeshProUGUI>().text = coinCount.ToString();
        addCoins.text = scoreValue.ToString();
        scoreCount = scoreValue;
    }

    public void TestButton(float newAmount)
    {
        CountUp(newAmount);
        coinCount = (int)newAmount;
    }
    private IEnumerator WaitForGameOverLoad(float newAmount)
    {
        yield return new WaitForSeconds(0.25f);
        SetCoinCount(coinCount, (int)newAmount);
        CountUp(newAmount);
    }

    public void CountUp(float newAmount)
    {
        if (Mathf.Abs(coinCount - newAmount) <= 30 && coinCount - newAmount != 0)
        {
            lerpTime = 0.5f;
            SFXManager.Instance.PlaySound("CoinEnd");
        }
        else if (coinCount - newAmount == 0)
            lerpTime = 0;
        else
        {
            lerpTime = 1f;
            StartCoroutine(PlayFullCoinSound());
        }
        var emission = particles.emission;
        emission.rateOverTime = ((Mathf.Abs(coinCount - newAmount)) + 1);
        emission.enabled = true;
        particles.Play();
        DOVirtual.Float(coinCount, newAmount, lerpTime, (v) => GetComponent<TextMeshProUGUI>().text = Mathf.Floor(v).ToString());
        
        DOVirtual.Float(scoreCount, 0, lerpTime, (v) => addCoins.text = Mathf.Floor(v).ToString());

        StartCoroutine(TurnOffExtras());
    }

    private IEnumerator PlayFullCoinSound()
    {
        SFXManager.Instance.PlaySound("CoinStart");
        yield return new WaitForSeconds(SFXManager.Instance.GetAudioSource().clip.length);
        SFXManager.Instance.PlaySound("CoinStart");
        yield return new WaitForSeconds(SFXManager.Instance.GetAudioSource().clip.length);
        SFXManager.Instance.PlaySound("CoinEnd");
    }

    public void CountDown(float currentCoins, float newAmount)
    {
        lerpTime = 1;
        mathSign.SetActive(true);
        addCoins.text = newAmount.ToString();
        addCoins.gameObject.SetActive(true);

        float difference = currentCoins - newAmount;
        DOVirtual.Float(currentCoins, difference, 1f, (v) => GetComponent<TextMeshProUGUI>().text = Mathf.Floor(v).ToString());
        DOVirtual.Float(newAmount, 0, 1f, (v) => addCoins.text = Mathf.Floor(v).ToString());
        StartCoroutine(TurnOffExtras());
    }

    private IEnumerator TurnOffExtras()
    {
        yield return new WaitForSeconds(lerpTime);
        mathSign.SetActive(false);
        addCoins.gameObject.SetActive(false);
    }
}
