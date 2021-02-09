using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    #region [ プレイヤーに変更を与える変数、関数 ]
    //_Speed : float : 矢印の回転の速さ、及び移動の速さ
    //_PlayerScale : Vector3 : プレイヤーの拡大率

    //Reset_Timer(int) : 引数に入力した時間が経過すると変更された値がすべてリセットされる
    //Add_Score(int) : 引数に入力した値のスコアが加算される
    #endregion

    public int ItemNum; // アイテムナンバー



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// アクションを起こさせる
    /// 引数 : int : プレイヤーの番号
    /// </summary>
    /// <param name="PlayerNum"></param>
    public void Action(int PlayerNum)
    {
        // 呼び出されたプレイヤーの番号を基にアクションを実行させる対象を検索
        GameObject Object = GameObject.Find("Player_" + PlayerNum);

        Destroy(this.gameObject); // 自分を消す

        //アイテムナンバーによって変わるアクション
        switch (ItemNum)
        {
            case 0: // テスト
                Object.gameObject.GetComponent<PlayerCon_now>().AddScore(1);

                break;

            case 1:

                Object.gameObject.GetComponent<PlayerCon_now>()._PlayerScale = new Vector3(2, 2, 2);
                Object.gameObject.GetComponent<PlayerCon_now>().Reset_Timer(3);
                break;

            case 2:
                Object.gameObject.GetComponent<PlayerCon_now>()._Speed = 10;
                Object.gameObject.GetComponent<PlayerCon_now>().Reset_Timer(10);
                break;

            default:
                break;
        }
    }


}
