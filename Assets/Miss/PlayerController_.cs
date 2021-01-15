using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace matsushima
{

    public class PlayerController : MonoBehaviour
    {
        #region[ 変数宣言 ]

        // GameObject
        GameObject Player_Obj; // プレイヤーのオブジェクト
        public GameObject Arrow_Obj; // 矢印のオブジェクト

        // Vector3
        Vector3 Player_Pos; // プレイヤー
        Vector3 Arrow_Pos; // 矢印

        //Transform
        Transform Player_Trans; // プレイヤー

        // float
        public float SpinSpeed; // 回転の速度
        private float Radius; // 回転の半径
        float Arrow_x, Arrow_y, Arrow_z; // 矢印の座標
        float Theta; // シータ

        // bool
        bool SpinOn; // 矢印が回転しているか

        #endregion

        void Start()
        {
            #region[ オブジェクト取得 ]

            // Transform
            Player_Trans = this.transform;

            // GameObject
            Player_Obj = this.gameObject;

            // Vector3
            Player_Pos = Player_Trans.position;
            Arrow_Pos = Arrow_Obj.transform.position;


            #endregion

            #region[ 初期化 ]

            Radius = 0.6f; // 半径を指定
            Arrow_y = 0.8f; // 矢印の高さを指定
            SpinOn = true;

            #endregion



        }

        void Update()
        {

            if (SpinOn)
            {
                // 回転フラグがオンの時
                ArrowSpin(SpinSpeed); // 矢印の回転
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SpinOn = false; // スペースが押されたとき、回転フラグをオフに
                                    //Theta = Mathf.Atan2(Arrow_Pos.x - Player_Pos.x, Arrow_Pos.z - Player_Pos.z);
                }
                
            }else
            {
                // 回転フラグがオフの時
                if (Input.GetKeyDown(KeyCode.Space)) SpinOn = true; // スペースが押されたとき、回転フラグをオンに



            }





        }

        /// <summary>
        /// 矢印の回転、及び向きの処理
        /// 引数 : float型 : 回転の速度 
        /// </summary>
        /// <param name="Speed"></param>
        void ArrowSpin(float Speed)
        {
            //回転
            Arrow_x = Radius * Mathf.Sin(Time.time * Speed);
            Arrow_z = Radius * Mathf.Cos(Time.time * Speed);

            //矢印の座標を更新
            Arrow_Obj.transform.position = new Vector3(Player_Pos.x + Arrow_x, Arrow_y, Player_Pos.z + Arrow_z);

            //矢印が常に外側を向くように
            var aim = transform.position - Arrow_Obj.transform.position;
            var look = Quaternion.LookRotation(aim, Vector3.up);
            Arrow_Obj.transform.rotation = Quaternion.Lerp(Arrow_Obj.transform.rotation, look, 1f);

        }

    }
}
