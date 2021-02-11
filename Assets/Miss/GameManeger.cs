using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    public AudioClip GameMusic; // ゲーム音
    public AudioClip[] CountDownSE = new AudioClip[2]; // カウントダウンのSE
    AudioSource audioSource; // オーディオソース

    public int Player_Count; // プレイヤーの人数
    int[] Player_Score = new int[4]; // 各プレイヤーのスコア
   
    public GameObject[] SpawnPoint = new GameObject[4]; // スポーンする初期値
    public GameObject Player_Obj; // プレイヤーオブジェクト
    public GameObject CountDownTimerText; // カウントダウンタイマー
    public GameObject[] ScoreUI = new GameObject[4]; // スコアのUI
    public GameObject[] dust; // ゴミ1
    public GameObject TimerObject; // タイマーオブジェクト

    GameObject PlayerBox; // プレイヤーを格納する箱

    float CountDownTimer; // カウントダウンタイマー
    float GenelateTimer; // タイマー
    public float GameTimer; // ゲームタイマー
    float tmpGameTimer; // ゲームタイマーを避ける
    float tt; // 万能タイマー
    [SerializeField]float GenelateTime; // 生成までの時間

    public bool GameStartFlg = false; // ゲームが始まっているかどうか
    bool GameFinishFlg = false; // ゲームが終了したかどうか
    bool CountDownPlayFlg = false; // カウントダウンのSEの再生フラグ

    // Start is called before the first frame update
    void Start(){

        GenelateTimer = 0; // タイマーの初期化
        //GameTimer = 0; // ゲームタイマーの初期化
        CountDownTimer = 4; // カウントダウンタイマーの初期化
        tmpGameTimer = GameTimer;

        //コンポーネントを取得
        audioSource = GetComponent<AudioSource>();

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

        //人数に応じてスコアUIを表示
        for (int i = 0; i < Player_Count; i++){

            ScoreUI[i].SetActive(true); // 表示

        }

        //audioSource.PlayOneShot(GameMusic); //BGMを再生
        //audioSource.PlayOneShot(CountDownSE[0]);
    }

    // Update is called once per frame
    void Update()
    {

            //カウントダウンのテキスト
            string CoundDownChara = null;
            Text CountDown = CountDownTimerText.GetComponent<Text>();

            // スタート前のカウントダウン
            CountDownTimer -= Time.deltaTime;       
            if(CountDownTimer > 0)CoundDownChara = "" + Mathf.Floor(CountDownTimer); // 切捨てて表示

            //カウントダウンが0になったら
            if (CountDownTimer < 1 && CountDownTimer > 0)
            {
                CoundDownChara = "GO"; // GOと表示
                GameStartFlg = true; //ゲーム開始
            }

            //カウントダウンを非表示
            if (CountDownTimer < 0 && CountDownTimer > -1) CountDownTimerText.SetActive(false);

            //カウントダウンテキストを更新
            CountDown.text = CoundDownChara;

            if(GameTimer == 0) audioSource.PlayOneShot(GameMusic); //BGMを再生

            //if (CountDownTimer < 2) CountDownAnim.Play();

            //ゲームが始まったら
            if (GameStartFlg && !GameFinishFlg)
            {
                //ゲームBGMを再生
                if (GameTimer == tmpGameTimer) audioSource.PlayOneShot(GameMusic);

                //生成タイマーの加算
                GenelateTimer += Time.deltaTime;
                //ゲームタイマーをカウント
                GameTimer -= Time.deltaTime;

                //10秒経ったら
                if (GenelateTimer > GenelateTime)
                {
                    //生成タイマーを初期化
                    GenelateTimer = 0;
                    //オブジェクトを生成
                    DustGenelate();
                }

            if (GameTimer < 1) GameFinishFlg = true;

            }

            //ゲームが終了したら
            if(GameFinishFlg){

                CoundDownChara = "FINISH"; // カウントが0になったらFINISHと表示
                CountDownTimerText.SetActive(true);
                
            }

            PrintUI(); // UIの表示
            ScreenShot(KeyCode.Space); // スクショ

            for (int i = 0; i < Player_Count; i++){
                GameObject Player = GameObject.Find("Player_" + i);
                Player_Controller pc = Player.GetComponent<Player_Controller>();
                //Debug.Log("Player" + j + " : " + pc.GetScore());
                Player_Score[i] = pc.GetScore();
            }
            //Debug.Log(Player_Score[0] + " : " + Player_Score[1] + " : " + Player_Score[2] + " : " + Player_Score[3]);

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
            //ScreenCapture.CaptureScreenshot("D:/2年/UFOproject/Assets/ScreenShot/S.png");
            string Name = System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "\'" + System.DateTime.Now.Minute.ToString() + ".png";
            ScreenCapture.CaptureScreenshot("D:/2年/UFOproject/Assets/ScreenShot/" + Name);
        }
    }

    public int GetMaxScore()
    {
        int maxScore = 0;
        for(int i = 0; i < Player_Count; i++)
        {
            if (maxScore < Player_Score[i]) maxScore = Player_Score[i];
        }

        return maxScore;

    }

    public int GetScore(int num)
    {
        return Player_Score[num];
    }


}
