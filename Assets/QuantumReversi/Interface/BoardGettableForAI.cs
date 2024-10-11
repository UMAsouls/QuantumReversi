using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BoardGettableForAI
{
    public int[,] RealBoard { get; }

    public List<int[]> JudgedPosForAI { get; }

    public UniTask SetStone(int row, int col, StoneType type);

}