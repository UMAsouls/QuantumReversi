using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Zenject;

public class AI : IAI
{
    [Inject]
    BoardGettableForAI board;

    public async UniTask SetStone()
    {
        //おける場所
        int[][] pos = board.JudgedPos;
        //確率盤面
        int[][] realBoard = board.RealBoard;

        //置く位置(x, y)
        int[] setPos = new int[2];
        //置く石の確率
        StoneType type = StoneType.TEN;


        //こっからAIのコード

        //

       await board.SetStone(setPos[1], setPos[0], type);
    }
}