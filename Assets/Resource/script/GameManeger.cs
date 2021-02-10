using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    public int Player_Count; // プレイヤーの人数
    int[] Player_Score = new int[4]; // 各プレイヤーのスコア

    public GameObject[] SpawnPoint = new GameObject[4]; // スポーンする初期値
    public GameObject Player_Obj; // プレイヤーオブジェクト
    public GameObject CountDownTimerText; // カウントダウンタイマー
    GameObject PlayerBox; // プレイヤーを格納する箱

    public GameObject[] dust; // ゴミ1
    public GameObject TimerObject; // タイマーオブジェクト

    float CountDownTimer; // カウントダウンタイマー
    float GenelateTimer; // タイマー
    float GameTimer; // ゲームタイマー
    [SerializeField]float GenelateTime; // 生成までの時間

    bool GameStartFlg = false; // ゲームが始まっているかどうか

    // Start is called before the first frame update
    void Start(){

        GenelateTimer = 0; // タイマーの初期化
        GameTimer = 0; // ゲームタイマーの初期化
        CountDownTimer = 4; // カウントダウンタイマーの初期化


        //人数に応じてプレイヤーを生成
        for(int i = 0; i < Player_Count; i++){
            GameObject Player = Instantiate(Player_Obj, new Vector3(SpawnPoint[i].transform.position.x,2f, SpawnPoint[i].transform.position.z),Quaternion.identity)as GameObject;
            Player.name = "Player_" + i; 
            Player_Controller pc = Player.GetComponent<Player_Controller>();
            pc.SetPlayerNum(i);
            PlayerBox = GameObject.Find("PlayerBox");
            Player.transform.parent = PlayerBox.transform; // 子オブジェクトとしてセット
            Player_Score[0] = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {

            // スタート前のカウントダウン
            string CoundDownChara;
            CountDownTimer -= Time.deltaTime;
            Text CountDown = CountDownTimerText.GetComponent<Text>();

            CoundDownChara = "" + Mathf.Floor(CountDownTimer);

            if (CountDownTimer < 1) CoundDownChara = "GO"; // カウントが0になったらGOと表示
            CountDown.text = CoundDownChara;

            if (CountDownTimer < 0)
            {
                CountDownTimerText.SetActive(false);
                GameStartFlg = true; //ゲーム開始
            }

        //if (CountDownTimer < 2) CountDownAnim.Play();

        if (GameStartFlg == true)
        {

            //生成タイマーの加算
            GenelateTimer += Time.deltaTime;
            //ゲームタイマーをカウント
            GameTimer += Time.deltaTime;

            //10秒経ったら
            if (GenelateTimer > GenelateTime)
            {
                //生成タイマーを初期化
                GenelateTimer = 0;
                //オブジェクトを生成
                DustGenelate();
            }
        }

        PrintUI(); // UIの表示
        ScreenShot(KeyCode.S); // スクショ

        for (int i = 0; i < Player_Count; i++){
            GameObject Player = GameObject.Find("Player_" + i);
            Player_Controller pc = Player.GetComponent<Player_Controller>();
            //Debug.Log("Player" + j + " : " + pc.GetScore());
            Player_Score[i] = pc.GetScore();
        }
        Debug.Log(Player_Score[0] + " : " + Player_Score[1] + " : " + Player_Score[2] + " : " + Player_Score[3]);


    }

    /// <summary>
    /// ゴミを生成
    /// </summary>
    void DustGenelate()
    {
        //Instantiate (obj, new Vector3(0.0f,2.0f,0.0f), Quaternion.identity);
        GameObject Dust = Instantiate(dust[0], new Vector3(Random.Range(0.0f, 15.5f) - 6.8f, 1.2f, Random.Range(0.0f,8.7f)-3.8f), Quaternion.identity)as GameObject;
        GameObject DustBox = GameObject.Find("Dusts");
        Dust.transform.parent = DustBox.transform; // 子オブジェクトとして生成
    }

    void PrintUI()
    {
        //テキストコンポーネントを取得
        Text TimerText = TimerObject.GetComponent<Text>();

        //タイマーを表示
        TimerText.text = "Time : " + Mathf.Floor(GameTimer);
    }

    void ScreenShot(KeyCode code)
    {
        // スペースキーが押されたら
        if (Input.GetKeyDown(code))
        {
            // スクリーンショットを保存
            //CaptureScreenShot("ScreenShot.png");
            ScreenCapture.CaptureScreenshot("D:/2年/UFOproject/Assets/ScreenShot/S.png");
        }
    }


}
