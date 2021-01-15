using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    public GameObject dust; // ゴミ1
    public GameObject TimerObject; // タイマーオブジェクト

    float GenelateTimer; // タイマー
    float GameTimer; // ゲームタイマー

    // Start is called before the first frame update
    void Start()
    {
        GenelateTimer = 0; // タイマーの初期化
    }

    // Update is called once per frame
    void Update()
    {
        //生成タイマーの加算
        GenelateTimer += Time.deltaTime;

        //10秒経ったら
        if(GenelateTimer > 10)
        {
            //生成タイマーを初期化
            GenelateTimer = 0;
            //オブジェクトを生成
            DustGenelate();
        }
        //Debug.Log(time);


        //ゲームタイマーをカウント
        GameTimer += Time.deltaTime;
        
        //テキストコンポーネントを取得
        Text TimerText = TimerObject.GetComponent<Text>();

        //タイマーを表示
        TimerText.text = "Time : " + Mathf.Floor(GameTimer);

        if (Input.GetKey(KeyCode.A))
        {
            GameObject player = GameObject.Find("Player");
            player.gameObject.GetComponent<PlayerCon_now>().SetSpeed(20);
        }


    }

    /// <summary>
    /// ゴミを生成
    /// </summary>
    void DustGenelate()
    {
        //Instantiate (obj, new Vector3(0.0f,2.0f,0.0f), Quaternion.identity);
        Instantiate(dust, new Vector3(-2.68f, 1.2f, 0.0f), Quaternion.identity);
    }
}
