using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class CP : MonoBehaviour, ICP
{
    [Inject]
    IAI ai;

    private int stone90;
    private int stone70;

    StoneType mode;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
}