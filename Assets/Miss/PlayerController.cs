using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace matsuhima
{
    public class PlayerController : MonoBehaviour
    {

        #region [ Public変数 ]

        public GameObject _PlayerObj; // プレイヤーオブジェクト
        public GameObject _ArrowObj; // 矢印オブジェクト

        public float _SpinSpeed; // 矢印回転の速さ
        public float _Speed; // 移動の速さ
        public float _Acceleration; // 加速度
        public float _Decelerate; // 減速度
        public float _MaxSpeed; // 最大速度

        public KeyCode _KeyCode; // 操作するキー

        #endregion

        #region [ local変数 ]

        Transform playerTrs;

        Vector3 playerPos;
        Vector3 arrowVector;
        Vector3 arrowPos;

        float radius; // 矢印回転の半径
        float arrowTime; // 矢印回転の時間


        #endregion

        void Start()
        {
            playerTrs = _PlayerObj.transform;

            playerPos = playerTrs.transform.position;
            arrowPos = _ArrowObj.transform.localPosition;

            radius = 0.6f; // 半径を指定
            arrowTime = 0f; // 回転時間の初期化
        }

        void Update()
        {
            PlayerMove(); // 

        }
        /// <summary>
        /// 動きを管理する関数
        /// </summary>
        void PlayerMove()
        {
            //playerTrs.transform.localPosition=new Vector3(0, 0, 0);

            //　スペースキーが押されたとき
            if (Input.GetKeyDown(_KeyCode))
            {
                _Speed = 0f; // スピードを0に
                arrowVector = arrowPos.normalized;
            }

            //　スペースキーを押している間
            if (Input.GetKey(_KeyCode))
            {
                // 徐々に加速
                _Speed += _Acceleration * Time.deltaTime;
            }
            else //　スペースキーを離している間
            {
                ArrowSpin(_SpinSpeed); // 矢印回転
                _Speed -= _Decelerate * Time.deltaTime; // 徐々に減速
            }

            _Speed = Mathf.Clamp(_Speed, 0, _MaxSpeed); // スピードの下限値上限値をセット

            transform.Translate((arrowVector * _Speed) * Time.deltaTime); // スピードを加算
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
    }
}
