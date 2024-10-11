using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosJudge : IPosJudge
{

    private int[,] directions = new int[,]
    {
        {-1,0 }, {1, 0 }, {0, -1}, {0, 1}, {-1, -1},  {-1, 1},  {1, -1 }, {1,  1 }
    };

    bool canPlace(int[,] board, int row, int col, int stone)
    {
        if (board[row, col] != 0) return false;
        if(stone*stone != 1) return false;

        int size = board.GetLength(0);
        

        for(int d = 0; d < 8; d++)
        {
            int dx = directions[d, 0];
            int dy = directions[d, 1];
            int x = row + dx;
            int y = col + dy;

            bool hasOpponentBetween = false;

            while(x >= 0 && x < size && y >= 0 && y < size)
            {
                if (board[x, y] != -1 * stone) break;
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
        int size = watchBoard.GetLength(0);
        List<int[]> board = new List<int[]>();

        string s = "";
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                s += watchBoard[i, j] + " ,";
                if (canPlace(watchBoard, i, j, stone)) board.Add(new int[] { j, i });
            }
            s += "\n";
        }
        Debug.Log(s); 

        return board;
    }
}