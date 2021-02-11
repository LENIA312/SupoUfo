using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region[ Public ]

    // AudioClip
    public AudioClip _GameMusic; // ゲームBGM
    public AudioClip[] _SE; // SE ( 0:カウントダウンSE 1:ゲーム開始GOのSE )

    // int
    public int _PlayerCount; // プレイヤーの人数

    // GameObject
    public GameObject[] _SpawnPoint_Obj = new GameObject[4]; // 初期スポーン地点のオブジェクト
    public GameObject[] _ScoreMark_Obj = new GameObject[4]; // 各スコアを表示するマーク
    public GameObject[] _Dusts_Obj; // 発生させるゴミオブジェクト
    public GameObject _Player_Obj; // プレイヤーオブジェクト
    public GameObject _CountDownTimer_Obj; // カウントダウンテキスト
    public GameObject _GameTimer_Obj; // ゲームタイマー

    // float
    public float _GameTimer; // ゲームタイマー

    // bool
    public bool _GameStartFlg = false; // ゲーム開始フラグ

    #endregion

    #region[ Private ]

    // AudioSource
    AudioSource audioSource; // オーディオソースコンポーネント

    // int
    int[] playerScore = new int[4]; // 各プレイヤーのスコア

    // GameObject
    GameObject playerBox; // プレイヤーを格納するからオブジェクト

    // float
    float countDownTimer; // ゲーム開始前のカウントダウンタイマー
    float genelateTimer; // ゴミを生成するタイマー
    float tmpGameTimer; // ゲーム制限時間を格納
    float gameFinishTimer; // ゲーム終了後のタイマー
    [SerializeField] float genelateTime; // 生成までの時間

    // bool
    bool gameFinishFlg = false; // ゲーム終了フラグ

    #endregion

    // Start is called before the first frame update
    void Start(){

        // プレイヤーを生成
        GenelatePlayer();
        //スコアUIを表示
        GenelateScoreUI();
        //初期化
        init();
        //オーディオソースコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update(){

        //ゲーム開始前
        if (!_GameStartFlg)
        {
            //カウントダウンのテキスト
            string CountDownChara = null;
            Text CountDown = _CountDownTimer_Obj.GetComponent<Text>();
            //スタート前のカウントダウン
            countDownTimer -= Time.deltaTime; // カウントダウン
            if (countDownTimer > 0) CountDownChara = "" + Mathf.Floor(countDownTimer); // 時間を切り捨てて表示
                                                                                       //カウントダウンが0になったら
            if (countDownTimer < 1)CountDownChara = "GO"; // GOと表示

            // GOのあとカウントダウンを消す
            if (countDownTimer < 0)
            {
                _CountDownTimer_Obj.SetActive(false);
                _GameStartFlg = true; // ゲームを開始
            }

            //カウントダウンテキストを更新
            CountDown.text = CountDownChara;
        }

        // ゲームプレイ中
       if(_GameStartFlg && !gameFinishFlg)
        {
            // ゲーム音楽を再生する
            if (_GameTimer == tmpGameTimer) audioSource.PlayOneShot(_GameMusic);
            // ゴミ生成タイマーのカウントアップ
            genelateTimer += Time.deltaTime;
            // ゲームタイマーのカウントダウン
            _GameTimer -= Time.deltaTime;

            // ゴミ生成時間が経ったら
            if (genelateTimer > genelateTime)
            {
                // 生成タイマーを初期化
                genelateTimer = 0;
                // ゴミオブジェクトを生成
                GenelateDust();
            }

            // ゲームタイマーが0になったらゲーム終了フラグを折る
            if (_GameTimer < 1) gameFinishFlg = true;
        
        }

       // ゲームが終了したら
        if (gameFinishFlg)
        {
            // 終了のテキスト
            Text Finish = _CountDownTimer_Obj.GetComponent<Text>();
            // テキストをFINISHに変更
            Finish.text = "FINISH";
            // テキストを表示
            _CountDownTimer_Obj.SetActive(true);
            // 音楽を停止
            audioSource.Stop();

            //ゲーム終了後タイマーをカウントアップ
            gameFinishTimer += Time.deltaTime;
            if(gameFinishTimer > 3)
            {
                // リザルトシーンへ移動
                SceneManager.LoadScene("ResultScene");
            }
        }

        // UIの表示
        PrintUI();

        // スクリーンショットを許可
        ScreenShot(KeyCode.Space);

        // 各プレイヤーのスコアを取得する
        for(int i = 0; i < _PlayerCount; i++)
        {
            // プレイヤーのオブジェクトを順番に検索
            GameObject Player = GameObject.Find("Player_" + i);
            // コンポーネントを取得する
            Player_Controller pc = Player.GetComponent<Player_Controller>();
            // スコアを取得する
            playerScore[i] = pc.GetScore();
        }
     
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void init()
    {
        genelateTimer = 0; // 生成タイマーの初期化
        countDownTimer = 4; // カウントダウンタイマーの初期化
        tmpGameTimer = _GameTimer; // ゲーム制限時間を格納
        gameFinishTimer = 0;

    }

    /// <summary>
    /// 人数に応じてプレイヤーを生成
    /// </summary>
    void GenelatePlayer()
    {
        for (int i = 0; i < _PlayerCount; i++)
        {
            //プレイヤーオブジェクトをスポーンポイントに生成
            GameObject Player = Instantiate(_Player_Obj, new Vector3(_SpawnPoint_Obj[i].transform.position.x, 2f, _SpawnPoint_Obj[i].transform.position.z), Quaternion.identity) as GameObject;
            //管理のしやすいように、名前を付けて生成
            Player.name = "Player_" + i;
            //プレイヤーコントローラーを取得し、プレイヤー識別番号をセット
            Player_Controller pc = Player.GetComponent<Player_Controller>();
            pc.SetPlayerNum(i);
            // プレイヤーをまとめるからオブジェクトを取得し、子オブジェクトとしてセット
            playerBox = GameObject.Find("PlayerBox");
            Player.transform.parent = playerBox.transform;
            // スコアを初期化
            playerScore[i] = 0;
        }
    }

    /// <summary>
    /// 人数に応じてスコアを表示するUIを表示
    /// </summary>
    void GenelateScoreUI()
    {
        for (int i = 0; i < _PlayerCount; i++)
        {
            _ScoreMark_Obj[i].SetActive(true);
        }
    }

    /// <summary>
    /// ゴミの生成
    /// </summary>
    void GenelateDust(){
        GameObject Dust = Instantiate(_Dusts_Obj[0], new Vector3(Random.Range(0.0f, 15.5f) - 6.8f, 1.2f, Random.Range(0.0f, 8.7f) - 3.8f), Quaternion.identity) as GameObject;   
        GameObject DustBox = GameObject.Find("Dusts");
        // ゴミ格納からオブジェクトの子オブジェクトとして生成
        Dust.transform.parent = DustBox.transform;
    }

    /// <summary>
    /// UIを表示する
    /// </summary>
    void PrintUI()
    {
        //テキストコンポーネントを取得
        Text TimerText = _GameTimer_Obj.GetComponent<Text>();
        //タイマーを表示
        TimerText.text = "のこり時間 : " + Mathf.Floor(_GameTimer);
    }

    /// <summary>
    /// スクリーンショット
    /// </summary>
    /// <param name="code"></param>
    void ScreenShot(KeyCode code){
        // 指定されたキーが押されたら
        if (Input.GetKeyDown(code)){
            // 現在時刻を名前としてスクリーンショットを保存
            string Name = System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "\'" + System.DateTime.Now.Minute.ToString() + ".png";
            ScreenCapture.CaptureScreenshot("D:/2年/UFOproject/Assets/ScreenShot/" + Name);
        }
    }

    /// <summary>
    /// 最大スコアを取得する
    /// </summary>
    /// <returns></returns>
    public int GetMaxScore(){
        int maxScore = 0;
        //最大スコアを検索する
        for(int i = 0; i < _PlayerCount; i++){
            if (maxScore < playerScore[i]) maxScore = playerScore[i];
        }
        //最大スコアを返却
        return maxScore;
    }

    /// <summary>
    /// スコアを取得
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int GetScore(int num)
    {
        return playerScore[num];
    }
}
