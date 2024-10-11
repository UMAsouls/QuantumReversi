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

    private async UniTask FirstPlayerGame()
    {

        playerStart.SetActive(true);
        await UniTask.Delay(1000);
        playerStart.SetActive(false);

        await board.WatchBoard();
        board.PosJudgePlayer();
        await player.PlayerTurn();

        cpStart.SetActive(true);
        await UniTask.Delay(1000);
        cpStart.SetActive(false);

        await board.WatchBoard();
        board.PosJudgeCP();
        await UniTask.Delay(100);
        await CP.CPTurn();
        await UniTask.Delay(100);



    }

    private async UniTask FirstCPGame()
    {
        cpStart.SetActive(true);
        await UniTask.Delay(1000);
        cpStart.SetActive(false);

        await board.WatchBoard();
        board.PosJudgeCP();
        await UniTask.Delay(100);
        await CP.CPTurn();
        await UniTask.Delay(100);

        playerStart.SetActive(true);
        await UniTask.Delay(1000);
        playerStart.SetActive(false);

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