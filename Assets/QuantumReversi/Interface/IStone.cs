using UnityEngine;

public interface IStone
{
    /// <summary>
    /// 自分の石になる確率
    /// setされてなかったら0
    /// </summary>
    public int Probability { get; }
    /// <summary>
    /// 観測する
    /// </summary>
    /// <returns>観測結果（1なら自分の駒、0なら無し、-1なら相手の駒）</returns>
    public int Watch();
    public void Set(int prob);

    public void Reverse();

    public WatchedStoneType watchedType { get; }
}
