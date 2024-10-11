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
    /// AI���΂��Z�b�g����ۂɎg��2�����z��
    /// </summary>
    private StoneSettable[,] stones;

    /// <summary>
    /// �΂̊m���̔Ֆ�
    /// �����̐΂ɂȂ�m���������Ă���
    /// �����u����ĂȂ�������0
    /// </summary>
    private int[,] realBoard;
    /// <summary>
    /// �ϑ���̔Ֆ�
    /// 1�Ȃ玩���̐΁A0�Ȃ疳���A-1�Ȃ瑊��̐�
    /// </summary>
    private int[,] watchedBoard;

    public int[,] RealBoard => realBoard;

    public List<int[]> JudgedPosForAI => posJudge.Judge(watchedBoard, -1);

    private bool IsReal;

    public void BoardModeChange()
    {
        if(IsReal)
        {
            headMass.ChangeWatchedBoard();
            IsReal = false;
        }else
        {
            headMass.ChangeRealBoard();
            IsReal = true;
        }
    }

    private void PosJudge(int turn)
    {
        foreach (var row in stones)
        {
            foreach(var s in stones)
            {
                s.IsSettable = false;
            }
        }
        List<int[]> result = posJudge.Judge(watchedBoard, turn);
        foreach (var item in result)
        {
            stones[item[1], item[0]].IsSettable = true;
        }
    }

    public void PosJudgePlayer() =>PosJudge(1);
    public void PosJudgeCP() => PosJudge(-1);

    /// <summary>
    ///  AI���΂��Z�b�g����ۂ̊֐�
    ///  stones�̒��ɂ���row, col�ɂ���ʒu��StoneSettable���g���Đ΃Z�b�g
    ///  type�͂��̂܂ܓ����
    ///  StoneSet������Ƃ��� await ~.StoneSet(type)�Ə�������
    /// </summary>
    /// <param name="row">�s�ԍ��i�ォ�牽�ԁj</param>
    /// <param name="col">��ԍ��i�����牽�ԁj</param>
    /// <param name="type"></param>
    /// <returns>Unitask���ď����Ă��邯�ǉ����Ԃ��Ȃ�</returns>
    public async UniTask SetStone(int row, int col, StoneType type)
    {
        stones[row,col].StoneSet(type);
    }

    /// <summary>
    /// headMass��watch���s���B
    /// watchedBoard��watch�̕Ԃ�l������
    /// watchedBoard = await �ϐ���.watch()
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
        IsReal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
