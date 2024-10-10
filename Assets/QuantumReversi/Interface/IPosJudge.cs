using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPosJudge
{
    int[,] Judge(int[,] watchBoard);
}
