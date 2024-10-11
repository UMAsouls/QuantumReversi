using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPosJudge
{
    List<int[]> Judge(int[,] watchBoard, int stone);
}
