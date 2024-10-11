using Cysharp.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
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

    private bool cpPass;
    private bool playerPass;

    private async UniTask PlayerTurn()
    {
        board.BoardModeChange2Real();
        playerStart.SetActive(true);
        await UniTask.Delay(1000);
        playerStart.SetActive(false);
        board.BoardModeChange2Watch();

        await UniTask.Delay(1000);
        
        board.ThunderAnime();
        await board.WatchBoard();
        await UniTask.Delay(800);
        if (board.PosJudgePlayer() == 0) return;
        await player.PlayerTurn();
        board.SettableReset();
        board.BoardModeChange2Watch();
        await UniTask.Delay(500);
    }

    private async UniTask CPTurn()
    {
        board.BoardModeChange2Real();
        cpStart.SetActive(true);
        await UniTask.Delay(1000);
        cpStart.SetActive(false);
        board.BoardModeChange2Watch();

        await UniTask.Delay(500);

        board.ThunderAnime();
        await board.WatchBoard();
        await UniTask.Delay(800);
        if (board.PosJudgeCP() == 0) return;
        await UniTask.Delay(400);
        await CP.CPTurn();
        board.SettableReset();
        board.BoardModeChange2Watch();
        await UniTask.Delay(500);
    }

    private async UniTask FirstPlayerGame()
    {

        await PlayerTurn();
        
        await CPTurn();
        



    }

    private async UniTask FirstCPGame()
    {
        await CPTurn();

        await PlayerTurn();
    }


    // Use this for initialization
    async void Start()
    {
        while (true)
        {
            cpPass = false; playerPass = false;
            if (IsFirstPlayer) await FirstPlayerGame();
            else await FirstCPGame();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}