using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//小田原作成
//タイトル画面の処理
public class TitleManager : MonoBehaviour
{

    //ゲーム開始
    public void GamePlay()
    {
        SceneManager.LoadScene("TestScene");
    }

    //ゲーム終了
    public void GameEnd()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
    #else
        Application.Quit();//ゲームプレイ終了
    #endif
    }
}
