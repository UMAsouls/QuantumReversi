using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosJudge : MonoBehaviour, IPosJudge
{

    private int[,] directions = new int[,]
    {
        {-1,0 }, {1, 0 }, {0, -1}, {0, 1}, {-1, -1},  {-1, 1},  {1, -1 }, {1,  1 }
    };

    bool canPlace(int[,] board, int row, int col, int stone)
    {
        if (board[row, col] != 0) return false;
        if(stone*stone != 1) return false;

        int size = board.Length;

        for(int d = 0; d < 8; d++)
        {
            int dx = directions[d, 0];
            int dy = directions[d, 1];
            int x = row + dx;
            int y = col + dy;

            bool hasOpponentBetween = false;

            while(x >= 0 && x < size && y >= 0 && y < size && board[x, y] == -1*stone)
            {
                hasOpponentBetween = true; x += dx; y += dy;
            }

            if(!hasOpponentBetween) continue;
            if(x < 0 || x >= size || y < 0 || y >= size) continue;
            if (board[x, y] != stone) continue;

            return true;
        }


        return false;
    }

    public List<int[]> Judge(int[,] watchBoard, int stone)
    {
        int size = watchBoard.Length;
        List<int[]> board = new List<int[]>();

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if (canPlace(watchBoard, i, j, stone)) board.Add(new int[] { j, i });
            }
        }

        return board;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}