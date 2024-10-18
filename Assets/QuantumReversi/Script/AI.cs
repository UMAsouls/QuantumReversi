using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Zenject;
using Unity.Sentis;
using System.Linq;

public class AI : IAI
{
    [Inject]
    BoardGettableForAI board;

    StoneType type = StoneType.THIRTY;

    Worker worker;

    public void ModelLoad()
    {
        // load model
        ModelAsset modelAsset = Resources.Load("ViTPlayer_L") as ModelAsset;
        var runtimeModel = ModelLoader.Load(modelAsset);

        // create engine and execute
        worker = new Worker(runtimeModel, BackendType.GPUCompute);
        
    }

    public async UniTask SetStone()
    {
        //おける場所
        int[][] pos = board.JudgedPosForAI.ToArray();
        //確率盤面
        int[,] realBoard = board.RealBoard;

        //置く位置(x, y)
        int[] setPos = new int[2];
        //置く石の確率
        if(type == StoneType.TEN) type = StoneType.THIRTY;
        else type = StoneType.THIRTY;

        // 新しい4次元配列を定義 (1, 1, 6, 6)
        float[] reshapedBoard = new float[36];

        // 元の2次元配列を4次元配列にコピー
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                reshapedBoard[i*6+j] = realBoard[i, j] / 100;
            }
        }

        

        // create input tensor
        TensorShape shape = new TensorShape(1, 1, 6, 6);
        Tensor inputtensor = new Tensor<float>(shape, reshapedBoard);

        worker.Schedule(inputtensor);

        // get output
        Tensor<float> outputtensor = worker.PeekOutput() as Tensor<float>;

        // 座標に対応するoutputtensorの値を格納するリスト
        List<float> outputValues = new List<float>();

        var output = outputtensor.ReadbackAndClone();

        // pos配列に格納された座標に基づいて、outputtensorの値を取得
        for (int i = 0; i < pos.Length; i++)
        {
            int x = pos[i][0];  // x座標
            int y = pos[i][1];  // y座標

            // outputtensorから該当の座標の値を取得し、リストに追加
            
            float value = output[0,6*y+x];
            outputValues.Add(value);
        }
        
        // 最小値を取得
        float minValue = outputValues.Min();

        // 最小値のインデックスを取得
        int minIndex = outputValues.IndexOf(minValue);

        // 対応する座標を pos から取り出す
        setPos = pos[minIndex];

        inputtensor.Dispose();
        outputtensor.Dispose();
        output.Dispose();

        await board.SetStone(setPos[1], setPos[0], type);
    }
}