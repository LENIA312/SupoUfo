using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_3 : MonoBehaviour
{
    //GameObject
    public GameObject Player_Obj;
    public GameObject Arrow_Obj;

    //Transform
    Transform Player_Trans; // プレイヤー


    // Vector3
    Vector3 Player_Pos; // プレイヤー
    Vector3 Arrow_Vector;
    Vector3 Arrow_Pos; // 矢印
    Vector3 MyPos;

    // float
    public float SpinSpeed; // 回転の速度
    private float Radius; // 回転の半径

    float Theta; // シータ
    float Vx, Vy;
    public float Speed;
    float ArrowTime;

    //加速率・減速率
    public float SpeedUp;
    public float SpeedDown;

    void Start()
    {
        // Transform
        Player_Trans = Player_Obj.transform;


        // Vector3
        Player_Pos = Player_Trans.transform.position;
        Arrow_Pos = Arrow_Obj.transform.localPosition;
        MyPos = this.transform.position;

        Radius = 0.6f; // 半径を指定
        ArrowTime = 0f;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Speed = 0;
            Arrow_Vector = Arrow_Pos.normalized;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Speed += SpeedUp * Time.deltaTime;
        }
        else
        {
            ArrowSpin(SpinSpeed); // 矢印の回転
            Speed -= SpeedUp * Time.deltaTime;
        }

        Speed = Mathf.Clamp(Speed,0, 500);


        transform.Translate((Arrow_Vector * Speed) * Time.deltaTime);








    }

    /// <summary>
    /// 矢印の回転、及び向きの処理
    /// 引数 : float型 : 回転の速度 
    /// </summary>
    /// <param name="Speed"></param>
    void ArrowSpin(float Speed)
    {
        ArrowTime += Time.deltaTime;

        //回転
        Arrow_Pos.x = Radius * Mathf.Sin(ArrowTime * Speed);
        Arrow_Pos.z = Radius * Mathf.Cos(ArrowTime * Speed);
        Arrow_Pos.y = 0f;

        //矢印の座標を更新
        Arrow_Obj.transform.localPosition = new Vector3(Arrow_Pos.x, Arrow_Pos.y, Arrow_Pos.z);

        //矢印が常に外側を向くように
        var aim = Player_Obj.transform.position - Arrow_Obj.transform.position;
        var look = Quaternion.LookRotation(aim, Vector3.up);
        Arrow_Obj.transform.rotation = Quaternion.Lerp(Arrow_Obj.transform.rotation, look, 1f);

    }
}
