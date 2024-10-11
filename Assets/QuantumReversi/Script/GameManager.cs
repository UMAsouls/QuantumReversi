using Cysharp.Threading.Tasks;
using System.Collections;
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

    private async UniTask FirstPlayerGame()
    {
        await board.WatchBoard();
        board.PosJudgePlayer();
        await player.PlayerTurn();

        await board.WatchBoard();
        board.PosJudgeCP();
        await CP.CPTurn();

    }

    private async UniTask FirstCPGame()
    {
        await board.WatchBoard();
        board.PosJudgeCP();
        await CP.CPTurn();

        await board.WatchBoard();
        board.PosJudgePlayer();
        await player.PlayerTurn();
    }


    // Use this for initialization
    async void Start()
    {
        while (true)
        {
            if (IsFirstPlayer) await FirstPlayerGame();
            else await FirstCPGame();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}