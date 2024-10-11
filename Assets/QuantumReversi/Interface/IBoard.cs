using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IBoard
{
    public UniTask WatchBoard();
    public void PosJudgePlayer();
    public void PosJudgeCP();
}
