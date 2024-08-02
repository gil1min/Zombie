using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Disable,
    Cooling,
    WaitingSun,
    Ready
}

public enum PlantType
{
    Sunflower,
    PeaShooter
}


public class Card : MonoBehaviour
{
    private CardState cardState = CardState.Disable;
    public PlantType plantType = PlantType.Sunflower;

    public GameObject cardLight;
    public GameObject cardGray;
    public Image cardMask;

    public float cdTime = 2;
    public float cdTimer = 0;

    [SerializeField]
    private int needSunAmount = 50;

    private void Update()
    {
        switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }
    }

    void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;
        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;
        if (cdTimer >= cdTime)
        {
            TransitionToWaitingSun();
        }
    }

    void WaitingSunUpdate()
    {
        if (needSunAmount <= SunManager.Instance.SunAmount)
        {
            TransitionToReady();
        }
    }

    void ReadyUpdate()
    {

    }

    void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;

        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }

    void TransitionToReady()
    {
        cardState = CardState.Ready;

        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }

    void TransitionToCooling()
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (cardState == CardState.Disable) return;
        if (needSunAmount > SunManager.Instance.SunAmount) return;

        bool isSuccess = HandManager.Instance.AddPlant(plantType);
        if (isSuccess)
        {
            SunManager.Instance.SubSun(needSunAmount);
            TransitionToCooling();
        }
    }

    public void DisableCard()
    {
        cardState = CardState.Disable;
    }

    public void EnableCard()
    {
        TransitionToCooling();
    }
}
