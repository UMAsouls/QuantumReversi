using Cysharp.Threading.Tasks;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using Zenject.Internal;

[RequireComponent(typeof(Animator))]
public class Mass : MonoBehaviour, HeadMass, StoneSettable
{
    [Inject]
    private IStone stone;

    [SerializeField]
    GameObject PlayerStone;
    [SerializeField]
    GameObject CPStone;
    [SerializeField]
    private GameObject SettableEffect;

    [SerializeField]
    public Mass right;
    [SerializeField]
    public Mass bottom;

    [SerializeField]
    private bool IsFirstPlayer;
    [SerializeField]
    private bool IsFirstCP;

    public Mass topleft, top, topright, bottomleft, left, bottomright;

    private Mass[,] masses = new Mass[3, 3];

    private bool isSettable;

    private Animator animator;
    public StoneSettable[,] Stones => GetStones();

    private StoneSettable[,] GetStones()
    {
        StoneSettable[,] stones = new StoneSettable[6, 6];

        Mass m1 = this;
        for(int i = 0; i < 6; i++)
        {
            Mass m2 = m1;
            for(int j = 0; j < 6; j++)
            {
                stones[i, j] = m2;
                m2 = m2.right;
            }
            m1 = m1.bottom;
        }
        Debug.Log(stones.Length);
        return stones;
    }

    private void SettableSet(bool value)
    {
        isSettable = value;
        SettableEffect.SetActive(value);
    }
    public bool IsSettable { get => isSettable; set => SettableSet(value); }

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
            switch (reverseType)
            {
                case WatchedStoneType.PlayerSTONE:
                    PlayerStone.SetActive(false);
                    CPStone.SetActive(true);
                    break;
                case WatchedStoneType.CPSTONE:
                    PlayerStone.SetActive(true);
                    CPStone.SetActive(false);
                    break;
            }

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
        
        switch (reverseType)
        {
            case WatchedStoneType.PlayerSTONE:
                PlayerStone.SetActive(true);
                break;
            case WatchedStoneType.CPSTONE:
                CPStone.SetActive(true);
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
        if(right != null) right.left = this;
        if (bottom != null) bottom.top = this;

        if (right != null && bottom != null)
        {
            bottomright = right.bottom;
            right.bottomleft = bottom;
            bottom.topright = right;
            bottomright.topleft = this;
        }

        if (right != null) right.SetMass();
        if (bottom != null && left == null) bottom.SetMass();
    }
    public void Focus()
    {
        animator.SetBool("Focus", true);
    }

    public void UnFocus()
    {
        animator.SetBool("Focus", false);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (IsFirstPlayer)
        {
            stone.Set(90);
            PlayerStone.SetActive(true);
        }
        else if (IsFirstCP)
        {
            stone.Set(10);
            CPStone.SetActive(true);
        }
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