using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IBoard
{
    public UniTask WatchBoard();
    public int PosJudgePlayer();
    public int PosJudgeCP();

    public void BoardModeChange2Real();
    public void BoardModeChange2Watch();

    public void SettableReset();
    public void ThunderAnime();
}
