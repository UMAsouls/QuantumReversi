using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Board : MonoBehaviour, IBoard, BoardGettableForAI
{
    [Inject]
    private IPosJudge posJudge;

    [Inject]
    private CountableMass topMass;

    private int[][] realBoard;
    private int[][] watchedBoard;

    public int[][] RealBoard => realBoard;

    public int[][] JudgedPos => posJudge.Judge(watchedBoard);

    public async UniTask SetStone(int row, int col, StoneType type)
    {

    }

    public async UniTask WatchBoard()
    {

    }



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
