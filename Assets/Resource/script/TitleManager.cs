using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject[] _SelectButton = new GameObject[2]; // 選択ボタン

    int selectNum; // 選択番号
    int noneSelectNum; // 選択されてない番号

    public static class Define
    {
        public const int LocalPlay = 0;
        public const int InternetPlay = 1;
    };

    // Start is called before the first frame update
    void Start()
    {
        selectNum = Define.LocalPlay; // 選択番号の初期値
    }

    // Update is called once per frame
    void Update()
    {
        //選択を受け付ける
        SelectController();
        // スペースキーが押されたら選択に応じたシーンへ移動
        if (Input.GetKeyDown(KeyCode.Space)) MoveScene();

    }

    /// <summary>
    /// 選択コントロール
    /// </summary>
    void SelectController()
    {
        // 矢印キー上下で選択番号を加減
        if (Input.GetKeyDown(KeyCode.DownArrow)) selectNum++;
        if (Input.GetKeyDown(KeyCode.UpArrow)) selectNum--;
        // 選択番号の制限
        if (selectNum == -1) selectNum = Define.InternetPlay;
        if (selectNum == 2) selectNum = Define.LocalPlay;
        // 選択されていない番号の取得
        if (selectNum == Define.LocalPlay) noneSelectNum = Define.InternetPlay;
        if (selectNum == Define.InternetPlay) noneSelectNum = Define.LocalPlay;

        // 選択されている方を大きく
        _SelectButton[selectNum].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        // 選択されていないほうを通常サイズに
        _SelectButton[noneSelectNum].transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// シーンの移動
    /// </summary>
    void MoveScene()
    {
        
        switch (selectNum)
        {
            case Define.LocalPlay:
                break;

            case Define.InternetPlay:
                break;

            default:
                break;
        }
    }

}
