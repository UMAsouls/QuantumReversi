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
    ///  AI���΂��Z�b�g����ۂ̊֐�
    ///  stones�̒��ɂ���row, col�ɂ���ʒu��StoneSettable���g���Đ΃Z�b�g
    ///  type�͂��̂܂ܓ����
    /// </summary>
    /// <param name="row">�s�ԍ��i�ォ�牽�ԁj</param>
    /// <param name="col">��ԍ��i�����牽�ԁj</param>
    /// <param name="type"></param>
    /// <returns>Unitask���ď����Ă��邯�ǉ����Ԃ��Ȃ�</returns>
    public async UniTask SetStone(int row, int col, StoneType type)
    {

    }

    /// <summary>
    /// headMass��watch���s���B
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
