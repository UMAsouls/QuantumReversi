using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Board : MonoBehaviour, IBoard, BoardGettableForAI
{
    [Inject]
    private IPosJudge posJudge;

    [Inject]
    private HeadMass headMass;

    /// <summary>
    /// AIが石をセットする際に使う2次元配列
    /// </summary>
    private StoneSettable[,] stones;

    /// <summary>
    /// 石の確率の盤面
    /// 自分の石になる確率が書いてある
    /// 何も置かれてなかったら0
    /// </summary>
    private int[,] realBoard;
    /// <summary>
    /// 観測後の盤面
    /// 1なら自分の石、0なら無し、-1なら相手の石
    /// </summary>
    private int[,] watchedBoard;

    public int[,] RealBoard => realBoard;

    public int[,] JudgedPos => posJudge.Judge(watchedBoard);

    /// <summary>
    ///  AIが石をセットする際の関数
    ///  stonesの中にあるrow, colにある位置のStoneSettableを使って石セット
    ///  typeはそのまま入れる
    ///  StoneSetをするときは await ~.StoneSet(type)と書くこと
    /// </summary>
    /// <param name="row">行番号（上から何番）</param>
    /// <param name="col">列番号（左から何番）</param>
    /// <param name="type"></param>
    /// <returns>Unitaskって書いてあるけど何も返さない</returns>
    public async UniTask SetStone(int row, int col, StoneType type)
    {
        stones[row,col].StoneSet(type);
    }

    /// <summary>
    /// headMassのwatchを行う。
    /// watchedBoardにwatchの返り値を入れる
    /// watchedBoard = await 変数名.watch()
    /// </summary>
    /// <returns></returns>
    public async UniTask WatchBoard()
    {
        watchedBoard = await headMass.Watch();
    }



    // Start is called before the first frame update
    void Start()
    {
        stones = headMass.Stones;
        headMass.SetMass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
