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

    private StoneSettable[][] stones;

    private int[][] realBoard;
    private int[][] watchedBoard;

    public int[][] RealBoard => realBoard;

    public int[][] JudgedPos => posJudge.Judge(watchedBoard);

    /// <summary>
    ///  AIが石をセットする際の関数
    ///  stonesの中にあるrow, colにある位置のStoneSettableを使って石セット
    ///  typeはそのまま入れる
    /// </summary>
    /// <param name="row">行番号（上から何番）</param>
    /// <param name="col">列番号（左から何番）</param>
    /// <param name="type"></param>
    /// <returns>Unitaskって書いてあるけど何も返さない</returns>
    public async UniTask SetStone(int row, int col, StoneType type)
    {

    }

    /// <summary>
    /// headMassのwatchを行う。
    /// </summary>
    /// <returns></returns>
    public async UniTask WatchBoard()
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        stones = headMass.Stones;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
