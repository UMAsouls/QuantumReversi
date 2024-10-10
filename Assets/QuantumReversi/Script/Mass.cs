using Cysharp.Threading.Tasks;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Zenject.Internal;

[RequireComponent(typeof(Animator))]
public class Mass : MonoBehaviour, HeadMass, StoneSettable
{
    [Inject]
    private IStone stone;

    [SerializeField]
    public Mass right;
    [SerializeField]
    public Mass bottom;

    public Mass topleft, top, topright, bottomleft, left, bottomright;

    private Mass[, ] masses = new Mass[3,3];

    private bool isSettable;

    private Animator animator;
    public StoneSettable[,] Stones => throw new System.NotImplementedException();
    private Animator animator;
>>>>>>> b8d69c08daa23c7c17ce4efb7a9043f07ef095e0
    public StoneSettable[][] Stones => throw new System.NotImplementedException();

    public bool IsSettable { get => isSettable; set => isSettable = value; }

    public void getRealBoard(int[,] board, int row, int col)
    {
        board[row, col] = stone.Probability;
        if (right != null)
        {
            right.getRealBoard(board, row, col + 1);
        }
        if (bottom != null && left == null)
        {
            bottom.getRealBoard(board, row + 1, col);
        }
    }


    /// <summary>
    /// rightとbottomを上手く使って、実際に置いてある石を配列に表現
    /// stoneから確率(Probability)を取得してリストに書いていく
    /// 再帰用の関数を新しく作った方がいいと思う
    /// 配列だけ作って再帰の関数に引数として渡す
    /// </summary>
    /// <returns>nullは消してね</returns>
    public int[,] GetRealBoard()
    {
        int[,] board = new int[6, 6];
        getRealBoard(board, 0, 0);
        return board;
    }

    /// <summary>
    /// 自分自身の石がreverseTypeと同じならひっくり返す
    ///  ひっくり返したならdirの方向にある石もReverseする
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="reverseType"></param>
    public void Reverse(int[] dir, WatchedStoneType reverseType)
    {
        if(stone.watchedType == reverseType)
        {
            stone.Reverse();
            if (masses[dir[0], dir[1]] == null) return;
            masses[dir[0], dir[1]].Reverse(dir, reverseType);
        }
    }

    /// <summary>
    /// 石をセットする
    /// セットした際に石もひっくり返す処理を行う
    /// typeによって石の確率を変える
    /// </summary>
    /// <param name="type"></param>
    public void StoneSet(StoneType type)
    {
        WatchedStoneType reverseType = WatchedStoneType.NONE;
        switch (type)
        {
            case StoneType.TEN:
                stone.Set(10);
                reverseType = WatchedStoneType.PlayerSTONE;
                break;
            case StoneType.THIRTY:
                stone.Set(30);
                reverseType = WatchedStoneType.PlayerSTONE;
                break;
            case StoneType.SEVENTY:
                stone.Set(70);
                reverseType = WatchedStoneType.CPSTONE;
                break;
            case StoneType.NINETY:
                stone.Set(90);
                reverseType = WatchedStoneType.CPSTONE;
                break;
        }
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1) continue;
                if(masses[i, j] == null) continue;
                masses[i, j].Reverse(new int[] { i, j }, reverseType);
            }
        }
    }

    public void watch(int[,] board, int row , int col)
    {
        board[row, col] = stone.Watch();
        if(right != null)
        {
            right.watch(board, row, col + 1);
        }
        if(bottom != null && left == null)
        {
            bottom.watch(board, row + 1, col);
        }
    }


    /// <summary>
    /// Board観測
    /// こっちもrightとbottomを上手く使う
    /// 再帰用の関数を新しく作った方がいいと思う
    /// 配列だけ作って再帰の関数に引数として渡す
    /// </summary>
    /// <returns>観測後のBoardを返す(nullは消してね)</returns>
    public async UniTask<int[,]> Watch()
    {
        int[,] board = new int[6,6];
        watch(board, 0, 0);
        return board;
    }

    /// <summary>
    /// 八方向のMassをunderとtopのマスから構成
    /// Start時にHeadMassが行う
    /// 最後にmassesにtopleft ~ bottomrightまでのMassを入れる（方向に対応するように）
    /// </summary>
    public void SetMass()
    {
        bottomright = right.bottom;
        right.left = this;
        bottom.top = this;
        right.bottomleft = bottom;
        bottom.topright = right;
        bottomright.topleft = this;

        if (right != null)
        {
            right.SetMass();
        }
        if(bottom != null && left == null)
        {
            bottom.SetMass();
        }
    }

    public void Focus()
    {
        throw new System.NotImplementedException();
    }

    public void UnFocus()
    {
        throw new System.NotImplementedException();
    }
    public void Focus()
    {
<<<<<<< HEAD
        animator.SetBool("Focus", true);
    }

    public void UnFocus()
    {
        animator.SetBool("Focus", false);
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}