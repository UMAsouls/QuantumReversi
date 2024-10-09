using System.Collections;
using UnityEngine;

public interface HeadMass 
{
    int[][] Watch();
    int[][] GetRealBoard();

    StoneSettable[][] Stones { get; }
}