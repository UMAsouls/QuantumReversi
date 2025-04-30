using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

//小田原作成
//AIを格納したCPU Behavior
//AIの処理をこのBehaviorで呼び出す
public class CP : MonoBehaviour, ICP, CPStoneCountable
{
    //AI(DIコンテナにより自動で代入される)
    [Inject]
    IAI ai;

    private int stone90;
    private int stone70;

    //セットする石の種類
    StoneType mode;

    public int Stone90 => stone90;

    public int Stone70 => stone70;

    //CPUのターン処理
    //AIの処理を呼び出す
    public async UniTask CPTurn()
    {
        await ai.SetStone();
        if(mode == StoneType.TEN)
        {
            stone90--; mode = StoneType.THIRTY;
        }else
        {
            stone70--; mode = StoneType.TEN;
        }
    }


    // Use this for initialization
    void Start()
    {
        stone90 = 9; stone70 = 9;
        ai.ModelLoad();
    }

    private void OnDestroy()
    {
        ai.ModelDispose();
    }

    // Update is called once per frame
    void Update()
    {

    }
}