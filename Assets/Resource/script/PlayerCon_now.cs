using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class PlayerCon_now : MonoBehaviour
    {

        #region [ Public変数 ]

        public GameObject _PlayerObj; // プレイヤーオブジェクト
        public GameObject _ArrowObj; // 矢印オブジェクト
        //public float _SpinSpeed; // 矢印回転の速さ
        public float _Speed; // 移動の速さ
       //public float _Decelerate; // 減速度
        public Vector3 _PlayerScale; // 拡大率
        public KeyCode _KeyCode; // 操作するキー
        public static int _MyScore; // 自身のスコア
        public int _PlayerNum; // プレイヤー番号

        #endregion

        #region [ local変数 ]

        Transform playerTrs;

        Vector3 playerPos;
        Vector3 arrowVector;
        Vector3 arrowPos;
        Vector3 shotVector; // 移動する角度

        float radius; // 矢印回転の半径
        float arrowTime; // 矢印回転の時間
        float angle; // 矢印と自機の角度
        float ResetTimer; // リセットタイマー
        float ResetTime; // リセットまでの時間
        float defaultSpeed; // スピードの初期値

        bool deathFlg; // 生存フラグ

    #endregion



    void Start()
        {
            playerTrs = _PlayerObj.transform;

            playerPos = playerTrs.transform.position;
            arrowPos = _ArrowObj.transform.localPosition;

            radius = 0.8f; // 半径を指定
            arrowTime = 0f; // 回転時間の初期化
            _MyScore = 0; // 得点のリセット
            deathFlg = false; // 生存フラグの初期化
            _PlayerScale = new Vector3(1, 1, 1); // 拡大率を初期化

            defaultSpeed = _Speed; // スピードの初期値セット
            ResetTimer = 0; // タイマーの初期化
    }

        void Update()
        {
            PlayerMove(); // 動け

            this.transform.localScale = _PlayerScale; // 拡大率

            ResetTimer += Time.deltaTime; // リセットタイマーを稼働
            if (ResetTimer > ResetTime) default_State();

    }


        /// <summary>
        /// 動きを管理する関数
        /// </summary>
        void PlayerMove()
        {

            //Rigidbodyの取得
            Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
            // 回転しない設定
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            //Y軸の移動を固定
            //rigidbody.constraints = RigidbodyConstraints.FreezePositionY;

        //playerTrs.transform.localPosition=new Vector3(0, 0, 0);

        //　スペースキーが押されたとき
        if (Input.GetKeyDown(_KeyCode))
            {
                //停止
                rigidbody = transform.GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;

                arrowVector = arrowPos.normalized;
            }

            //　スペースキーを押している間
            if (Input.GetKey(_KeyCode))
            {

                //ラジアン角から発射用ベクトルを作成
                double addforceX = Mathf.Sin(angle * Mathf.Deg2Rad);
                double addforceZ = Mathf.Cos(angle * Mathf.Deg2Rad);
                shotVector = new Vector3((float)addforceX * (_Speed * 0.17f), 0, (float)addforceZ * (_Speed * 0.17f));

                //Rigidbodyを取得
                rigidbody = transform.GetComponent<Rigidbody>();
                //加速
                rigidbody.AddForce(shotVector);

            }
            else //　スペースキーを離している間
            {
                ArrowSpin(_Speed);
                float rad = Mathf.Atan2(_ArrowObj.transform.localPosition.x - _PlayerObj.transform.localPosition.x, _ArrowObj.transform.localPosition.z - _PlayerObj.transform.localPosition.z);
                angle = (float)(rad * 180 / 3.14);

            }

        //_Speed = Mathf.Clamp(_Speed, 0, _MaxSpeed); // スピードの下限値上限値をセット

        //transform.Translate((arrowVector * _Speed) * Time.deltaTime); // スピードを加算
        //transform.Translate((arrowVector * -_Speed) * Time.deltaTime); // 矢印の反対方向へ
    }

        /// <summary>
        /// 矢印回転
        /// 引数 : float型 : 回転速度
        /// </summary>
        /// <param name="Speed"></param>1
        void ArrowSpin(float Speed)
        {
            arrowTime += Time.deltaTime;

            //矢印回転の処理
            arrowPos.x = radius * Mathf.Sin(arrowTime * Speed);
            arrowPos.z = radius * Mathf.Cos(arrowTime * Speed);
            arrowPos.y = 0f;

            //矢印の座標を更新
            _ArrowObj.transform.localPosition = new Vector3(arrowPos.x, arrowPos.y, arrowPos.z);

            //矢印が常に外側を向くように
            var aim = _PlayerObj.transform.position - _ArrowObj.transform.position;
            var look = Quaternion.LookRotation(aim, Vector3.up);
            _ArrowObj.transform.rotation = Quaternion.Lerp(_ArrowObj.transform.rotation, look, 1f);

        }

        /// <summary>
        /// スコアの加算
        /// </summary>
        /// <param name="Score"></param>
        public void AddScore(int Score)
        {
            _MyScore += Score;
        }

        /// <summary>
    /// 生存フラグをセット
    /// </summary>
    /// <param name="DeathFlg"></param>
        public void SetSurvival(bool DeathFlg)
    {
        deathFlg = DeathFlg;
    }

        /// <summary>
        /// スピードをセット
        /// </summary>
        /// <param name="Speed"></param>
        public void SetSpeed(int Speed)
        {
        _Speed = Speed; // スピードをセット
        }


    /// <summary>
    /// ステータスを初期化
    /// 引数 : time 
    /// 型   : float
    /// 概要 : 初期化させるまでの時間
    /// </summary>
    public void default_State()
    {

        _PlayerScale = new Vector3(1, 1, 1); // 拡大率を初期化
        _Speed = defaultSpeed; // スピードを初期化

        ResetTime = 0;

    }

    /// <summary>
    /// リセットまでのカウンター
    /// 引数 : time 
    /// 型   : float
    /// 概要 : 初期化させるまでの時間
    /// </summary>
    /// <param name="time"></param>
    public void Reset_Timer(float time)
    {
        ResetTimer = 0; // リセットタイマーを初期化
        ResetTime = time; // 引数を基にリセットまでの時間をセット
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

