using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PrepareUI prepareUI;
    public CardListUI cardListUI;
    public FailUI failUI;
    public WinUI winUI;

    private bool isGameEnd = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameStart();
    }

    void GameStart()
    {
        Vector3 currentPosition = Camera.main.transform.position;
        Camera.main.transform.DOPath(
                new Vector3[] {currentPosition, new Vector3(5, 0, -10), currentPosition },
                4,
                PathType.Linear
            ).OnComplete(ShowPrepareUI);
    }

    public void GameEndSuccess()
    {
        if (isGameEnd) return;
        print("Game end success.");
        isGameEnd = true;
        winUI.Show();
        cardListUI.DisableCardList();
        SunManager.Instance.StopProduce();
        
    }

    public void GameEndFail()
    {
        if (isGameEnd) return;
        isGameEnd = true;
        failUI.Show();
        ZombieManager.Instance.Pause();
        cardListUI.DisableCardList();
        SunManager.Instance.StopProduce(); 
    }

    void ShowPrepareUI()
    {
        prepareUI.Show(OnPrepareUIComplete);
    }


    void OnPrepareUIComplete()
    {
        SunManager.Instance.StartProduce();
        ZombieManager.Instance.StartSpawn();
        cardListUI.ShowCardList();
    }
}
