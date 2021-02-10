using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    #region[ Public ]
    //Mesh
    public MeshRenderer[] UFOColor; // UFOのプレイヤー識別用の色を変えるところ

    // GameObject
    public GameObject _Player_Obj; // プレイヤーオブジェクト
    public GameObject _Arrow_Obj; // 矢印オブジェクト

    // Vector3
    public Vector3 _PlayerScale; // プレイヤーの拡大率
    
    // float
    public float _Speed; // 移動および矢印の回転の速さ

    // int
    public int _PlayerNum; // プレイヤー識別番号
    public static int[] _MyScore = { 0, 0, 0, 0 }; // スコア格納庫

    #endregion

    #region[ Private ]

    // Vector3
    Vector3 arrowPos;
    Vector3 arrowVec;
    Vector3 shotVector;

    // float
    float angle; // 矢印と自機の角度
    float arrowTime; // 矢印回転の時間  
    float resetTimer; // リセットタイマー
    float resetTime; // リセットまでの時間
    float defauleSpeed; // スピードの初期値

    // KeyCode
    KeyCode keyCode; // 操作するキー

    // bool
    bool aliveFlg; // 生存フラグ

    #endregion

    // Start is called before the first frame update
    void Start()
    {    
        init(); // 初期化
        SetColor(UFOColor[0], UFOColor[1]); // 色を変える
        keyCode = GetKeycode(_PlayerNum); // プレイヤー別に別々の操作キーを指定
    }


    // Update is called once per frame
    void Update()
    {
        // 動かす
        PlayerMove();
        // 拡大率
        this.transform.localScale = _PlayerScale;
        // リセットタイマーを稼働
        resetTimer += Time.deltaTime;
        if (resetTimer > resetTime) parametaInit();

        //Debug.Log("Player_" + _PlayerNum + " : " + _MyScore[_PlayerNum]);
    }

    void init()
    {

        arrowTime = 0f; // 矢印回転時間の初期化
        aliveFlg = false; // 生存フラグの初期化
        _PlayerScale = new Vector3(1, 1, 1); // 拡大率の初期化

        defauleSpeed = _Speed; // スピードの初期値を決定
        resetTimer = 0; // タイマーの初期化
    }

    void parametaInit()
    {
        _Speed = defauleSpeed; // スピードの初期化
        _PlayerScale = new Vector3(1, 1, 1); // 拡大率の初期化
        resetTimer = 0; // タイマーの初期化
    }

    void PlayerMove(){

        /**[ Regidbodyの取得・設定]**/
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        //回転しないように
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;


        if (Input.GetKeyDown(keyCode)) // キーを入力したときに動きを止める
        {
            //停止
            rigidbody = transform.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;

            //arrowVec = arrowPos.normalized;
        }

        //操作するキーが押されたとき
        if (Input.GetKey(keyCode)){
            //ラジアン角から移動方向ベクトルを作成
            double addForceX = Mathf.Sin(angle * Mathf.Deg2Rad);
            double addForceZ = Mathf.Cos(angle * Mathf.Deg2Rad);
            shotVector = new Vector3((float)addForceX * (_Speed * 0.17f), 0, (float)addForceZ * (_Speed * 0.17f));

            //移動方向へ進む
            rigidbody.AddForce(shotVector);

        }
        else{
            //矢印を回転させる
            ArrowSpin(_Speed);
            //矢印とプレイヤーの角度を取得し、ラジアン角に変換
            float rad = Mathf.Atan2(_Arrow_Obj.transform.localPosition.x - _Player_Obj.transform.localPosition.x, _Arrow_Obj.transform.localPosition.z - _Player_Obj.transform.localPosition.z);
            angle = (float)(rad * 180 / 3.14);
        }
    }

    void ArrowSpin(float Speed){
        float radius = 0.8f; // 矢印回転の半径

        arrowTime += Time.deltaTime; // 回転時間の加算

        //矢印回転の処理
        arrowPos.x = radius * Mathf.Sin(arrowTime * Speed);
        arrowPos.z = radius * Mathf.Cos(arrowTime * Speed);
        arrowPos.y = 0f;

        //矢印の座標を更新
        _Arrow_Obj.transform.localPosition = new Vector3(arrowPos.x, arrowPos.y, arrowPos.z);

        //矢印が常に外側を向くようにする
        var aim = _Player_Obj.transform.position - _Arrow_Obj.transform.position;
        var look = Quaternion.LookRotation(aim, Vector3.up);
        _Arrow_Obj.transform.rotation = Quaternion.Lerp(_Arrow_Obj.transform.rotation, look, 1f);

    }

    public void AddScore(int Score,int num){
        _MyScore[num] += Score; // スコアを加算
    }

    public void Reset_Timer(float time){
        resetTimer = 0; // リセットタイマーを初期化
        resetTime = time; // 引数を基にリセットまでの時間をセット
    }

    public int GetScore()
    {
        return _MyScore[_PlayerNum]; // 自身のスコアを返す
    }

    public void SetPlayerNum(int num)
    {
        _PlayerNum = num; // プレイヤー識別番号をセット
    }

    void SetColor(MeshRenderer mesh, MeshRenderer mesh2)
    {

        mesh.material.color = GetPlayerColor(_PlayerNum);
        mesh2.material.color = GetPlayerColor(_PlayerNum);
        //switch

    }

    Color GetPlayerColor(int playerNum)
    {
        Color retuenColor = Color.clear;

        switch (playerNum)
        {
            case 0:
                retuenColor = Color.red;
                break;
            case 1:
                retuenColor = Color.green;
                break;
            case 2:
                retuenColor = Color.blue;
                break;
            case 3:
                retuenColor = Color.yellow;
                break;
            default:
                break;
        }
        return retuenColor; // 色を返却
    }

    KeyCode GetKeycode(int playerNum){
        KeyCode returnCode = KeyCode.None;

        switch (playerNum)
        {
            case 0:
                returnCode = KeyCode.A;
                break;
            case 1:
                returnCode = KeyCode.W;
                break;
            case 2:
                returnCode = KeyCode.D;
                break;
            case 3:
                returnCode = KeyCode.S;
                break;
            default:
                break;
        }

        return returnCode;
    }

    void OnTriggerEnter(Collider Object)
    {

        // 当たったオブジェクトのタグがPlayerなら
        if (Object.gameObject.tag == "Item")
        {
            Object.gameObject.GetComponent<ItemData>().Action(_PlayerNum); // アクションを実行させる

        }
    }
}
