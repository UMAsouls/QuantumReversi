using System.Collections;
using UnityEngine;

//石を表すクラス
//小田原作成
public class Stone : IStone
{
    //石が白色である確率
    private int probability = 0;
    public int Probability => probability;

    //今の石の種類
    private WatchedStoneType type = WatchedStoneType.NONE;
    public WatchedStoneType watchedType => type;

    /// <summary>
    /// 石の確率をセットする
    /// </summary>
    /// <param name="prob">確率</param>
    public void Set(int prob)
    {
        probability = prob;

        if (probability <= 50) type = WatchedStoneType.CPSTONE;
        else type = WatchedStoneType.PlayerSTONE;
    }

    /// <summary>
    /// 反転
    ///  確率とtypeを反転する
    /// </summary>
    public void Reverse()
    {
        if(type == WatchedStoneType.PlayerSTONE)
        {
            type = WatchedStoneType.CPSTONE;
        }
        else if(type == WatchedStoneType.CPSTONE)
        {
            type = WatchedStoneType.PlayerSTONE;
        }
        Debug.Log("bef " + probability);
        probability = 100 - probability;
        Debug.Log("aft " + probability);
    }

    /// <summary>
    /// 自分のTypeを確定させる
    ///  Probabilityの確率でtypeがPlayerStoneになる
    ///  それ以外ならCPStone
    ///   probability = 0ならtype = WatchedStoneType.NONE
    /// </summary>
    /// <returns></returns>

    public int Watch()
    {
        if(probability == 0)
        {
            type = WatchedStoneType.NONE;
            return 0;
        }

        float t = UnityEngine.Random.value * 100.0f;
        if (t < probability)
        {
            type = WatchedStoneType.PlayerSTONE;
        }
        else
        {
            type = WatchedStoneType.CPSTONE;
        }

        switch (type)
        {
            case WatchedStoneType.CPSTONE:
                return -1;
                break;
            case WatchedStoneType.PlayerSTONE:
                return 1;
                break;
            default:
                return 0;
                break;
        }
    }
}