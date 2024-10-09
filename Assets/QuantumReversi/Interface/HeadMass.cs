﻿using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;


public interface HeadMass 
{
    public UniTask<int[][]> Watch();
    int[][] GetRealBoard();

    StoneSettable[][] Stones { get; }
}