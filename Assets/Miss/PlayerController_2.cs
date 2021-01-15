using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_2 : MonoBehaviour
{
    GameObject Player_Obj; // プレイヤーオブジェクト
    public GameObject Arrow_Obj; // 矢印オブジェクト

    float Deg; // 角度(度)
    float Rad; // ラジアン値

    float SpinX, SpinY; // 円軌道の中心座標



    // Start is called before the first frame update
    void Start()
    {
        Player_Obj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Transform PlayerTrs = Player_Obj.transform;
        Transform ArrowTrs = Arrow_Obj.transform;

        Vector3 PlayerPos = PlayerTrs.position;
        Vector3 ArrowPos = ArrowTrs.position;

        for(Deg = 0; Deg < 360; Deg += 20)
        {
            Rad = (float)(Deg * 3.14 / 180);

            SpinX = PlayerPos.x + 0.1f * Mathf.Cos(Rad);
            SpinY = PlayerPos.z + 0.1f * Mathf.Sin(Rad);

            ArrowTrs.transform.Translate(SpinX, 0.6f, SpinY);

        }
    }

        /*
        size(400,400);
        float x;    //円軌道の中心x
        float y;    //円軌道の中心y
        float deg;  //角度°(度)単位
        float rad;  //角度ラジアン単位
        fill(255,120,120);
        for(deg=0;deg<360;deg+=20){ //度単位で20ずつ変化
	    rad = deg*3.14/180;	//ラジアン単位へ変換
	    x=200+50*cos(rad);	//中心200,200,半径50の円軌道
	    y=200+50*sin(rad);	//中心200,200,半径50の円軌道
	    ellipse(x,y,10,10);	//円軌道上に半径10の円を表示
        */

}
