using System.Collections;
using UnityEngine;

public class Stone : IStone
{
    public int probability;
    public int Probability => probability;

    private WatchedStoneType type;

    public WatchedStoneType watchedType => type;

    /// <summary>
    /// 石の確率をセットする
    /// </summary>
    /// <param name="prob"></param>
    public void Set(int prob)
    {

    }

    /// <summary>
    /// 反転
    ///  確率とtypeを反転する
    /// </summary>
    public void Reverse()
    {

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
        return probability;
    }
}