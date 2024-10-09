using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class Mass : MonoBehaviour, HeadMass, StoneSettable
{
    [Inject]
    private IStone stone;

    [SerializeField]
    public Mass right;
    [SerializeField]
    public Mass bottom;

    public Mass topleft, top, topright, bottomleft, bottomright;

    private Mass[][] masses = new Mass[3][];

    private bool isSettable;

    public StoneSettable[][] Stones => throw new System.NotImplementedException();

    public bool IsSettable { get => isSettable; set => isSettable = value; }

    /// <summary>
    /// rightとbottomを上手く使って、実際に置いてある石を配列に表現
    /// stoneから確率(Probability)を取得してリストに書いていく
    /// 再帰用の関数を新しく作った方がいいと思う
    /// 配列だけ作って再帰の関数に引数として渡す
    /// </summary>
    /// <returns>nullは消してね</returns>
    public int[][] GetRealBoard()
    {
        return null;
    }

    /// <summary>
    /// 自分自身の石がreverseTypeと同じならひっくり返す
    ///  ひっくり返したならdirの方向にある石もReverseする
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="reverseType"></param>
    public void Reverse(int[] dir, WatchedStoneType reverseType)
    {

    }

    /// <summary>
    /// 石をセットする
    /// セットした際に石もひっくり返す処理を行う
    /// typeによって石の確率を変える
    /// </summary>
    /// <param name="type"></param>
    public void StoneSet(StoneType type)
    {
    }

    /// <summary>
    /// Board観測
    /// こっちもrightとbottomを上手く使う
    /// 再帰用の関数を新しく作った方がいいと思う
    /// 配列だけ作って再帰の関数に引数として渡す
    /// </summary>
    /// <returns>観測後のBoardを返す(nullは消してね)</returns>
    public async UniTask<int[][]> Watch()
    {
        return null;
    }

    /// <summary>
    /// 八方向のMassをunderとtopのマスから構成
    /// Start時にHeadMassが行う
    /// 最後にmassesにtopleft ~ bottomrightまでのMassを入れる（方向に対応するように）
    /// </summary>
    public void SetMass()
    {
       
    }

    public void Focus()
    {
        throw new System.NotImplementedException();
    }

    public void UnFocus()
    {
        throw new System.NotImplementedException();
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