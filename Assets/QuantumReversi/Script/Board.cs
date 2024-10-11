using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
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

    public int[,] RealBoard => headMass.GetRealBoard();

    public List<int[]> JudgedPosForAI => posJudge.Judge(watchedBoard, -1);

    private bool IsReal;

    [SerializeField]
    GameObject thunder;
    [SerializeField]
    AudioClip thunderSound;

    private AudioSource audioSource;

    public void ThunderAnime()
    {
        audioSource.PlayOneShot(thunderSound);
        Instantiate(thunder, Vector3.zero, Quaternion.identity);
    }

    public void BoardModeChange2Real()
    {
        headMass.ChangeRealBoard();
        IsReal = true;
    }

    public void BoardModeChange2Watch()
    {
        headMass.ChangeWatchedBoard();
        IsReal = false;
    }

    public void BoardModeChange()
    {
        if (IsReal) BoardModeChange2Watch();
        else BoardModeChange2Real();
    }

    public void SettableReset()
    {
        foreach (var row in stones)
        {
            foreach (var s in stones)
            {
                s.IsSettable = false;
            }
        }
    }

    private int PosJudge(int turn)
    {

        SettableReset();
        
        List<int[]> result = posJudge.Judge(watchedBoard, turn);
        foreach (var item in result)
        {
            stones[item[1], item[0]].IsSettable = true;
        }

        return result.Count;
    }

    public int PosJudgePlayer() =>PosJudge(1);
    public int PosJudgeCP() => PosJudge(-1);

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
        audioSource = GetComponent<AudioSource>();

        stones = headMass.Stones;
        headMass.SetMass();
        IsReal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
