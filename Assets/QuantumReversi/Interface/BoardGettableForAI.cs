using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface BoardGettableForAI
{
    public int[,] RealBoard { get; }

    public int[,] JudgedPos { get; }

    public UniTask SetStone(int row, int col, StoneType type);

}