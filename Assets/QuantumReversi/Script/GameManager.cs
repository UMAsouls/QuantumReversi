﻿using Cysharp.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    IPlayer player;

    [Inject]
    ICP CP;

    [Inject]
    IBoard board;

    [SerializeField]
    private bool IsFirstPlayer;

    [SerializeField]
    private GameObject playerStart;
    [SerializeField]
    private GameObject cpStart;

    [SerializeField]
    private GameObject countUI;

    [SerializeField]
    GameObject resultUI;

    [SerializeField]
    GameObject WinUI;

    [SerializeField]
    GameObject LoseUI;

    [SerializeField]
    private bool twoPlayer;

    private bool cpPass;
    private bool playerPass;

    private CancellationToken cts;

    private async UniTask PlayerTurn()
    {
        board.BoardModeChange2Real();
        playerStart.SetActive(true);
        await UniTask.Delay(1000, cancellationToken: cts);
        playerStart.SetActive(false);

        await UniTask.Delay(1000, cancellationToken: cts);
        
        board.ThunderAnime();
        board.BoardModeChange2Watch();
        await board.WatchBoard();
        await UniTask.Delay(800, cancellationToken: cts);
        if (board.PosJudgePlayer() == 0) return;
        await player.PlayerTurn();
        board.SettableReset();
        board.BoardModeChange2Watch();
        await UniTask.Delay(500, cancellationToken: cts);
    }

    private async UniTask CPTurn()
    {
        board.BoardModeChange2Real();
        cpStart.SetActive(true);
        await UniTask.Delay(1000, cancellationToken: cts);
        cpStart.SetActive(false);
        
        await UniTask.Delay(500, cancellationToken: cts);

        board.ThunderAnime();
        board.BoardModeChange2Watch();
        await board.WatchBoard();
        await UniTask.Delay(800, cancellationToken: cts);
        if (board.PosJudgeCP() == 0) return;
        await UniTask.Delay(400, cancellationToken: cts);
        await CP.CPTurn();
        board.SettableReset();
        board.BoardModeChange2Watch();
        await UniTask.Delay(500, cancellationToken: cts);
    }

    private async UniTask FirstPlayerGame()
    {

        await PlayerTurn();

        if (board.EndJudge()) return;

        await CPTurn();
    }

    private async UniTask FirstCPGame()
    {
        await CPTurn();

        if (board.EndJudge()) return;

        await PlayerTurn();
    }

    private async UniTask TwoPlayerGame()
    {
        await PlayerTurn();

        if(board.EndJudge()) return;

        await PlayerTurn();
    }


    // Use this for initialization
    async void Start()
    {
        cts = this.GetCancellationTokenOnDestroy();
        while (true)
        {
            cpPass = false; playerPass = false;
            if (IsFirstPlayer) await FirstPlayerGame();
            else await FirstCPGame();


            if(board.EndJudge()) break;
        }

        board.BoardModeChange2Real();
        countUI.SetActive(true);
        await UniTask.Delay(1000, cancellationToken: cts);
        countUI.SetActive(false);
        board.BoardModeChange2Watch();

        await UniTask.Delay(1000, cancellationToken: cts);

        board.ThunderAnime();
        await board.WatchBoard();

        await UniTask.Delay(600, cancellationToken: cts);
        int count = board.CountStone();

        resultUI.SetActive(true);

        if (count < 18)
        {
            WinUI.SetActive(false);
            LoseUI.SetActive(true);
        }else
        {
            WinUI.SetActive(true);
            LoseUI.SetActive(false) ;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GamePlay()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}