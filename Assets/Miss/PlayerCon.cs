using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{

    public GameObject Text1, Text2, Text3;
    GameObject Player;
    GameObject Arrow;


    Vector3 Arrow_Vec; // 矢印の座標
    Vector3 MyPos; // 自分の座標


    public float Speed; // 回転の速さ
    private float Radius; // 回転の半径

    private float YPos; // 高さ

    float TmpX, TmpY, TmpZ; // 座標



    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        Arrow = GameObject.Find("Arrow").gameObject;

        Arrow_Vec = GameObject.Find("Arrow").transform.position;
        MyPos = this.transform.position;

        Radius = 0.6f;
        YPos = 0.5f;




    }

    // Update is called once per frame
    void Update()
    {

        TmpX = Radius * Mathf.Sin(Time.time * Speed);
        TmpY = YPos;
        TmpZ = Radius * Mathf.Cos(Time.time * Speed);

        //transform.position = new Vector3(0 + TmpX, 0 + TmpY, 0 + TmpZ);
        //Arrow = new Vector3(TmpX, TmpY, TmpZ);
        GameObject.Find("Arrow").transform.position = new Vector3(MyPos.x + TmpX, TmpY, MyPos.z + TmpZ);

        Debug();



        var aim = transform.position - Arrow.transform.position;
        aim.y = 0;
        var look = Quaternion.LookRotation(aim,Vector3.up);
        Arrow.transform.rotation = Quaternion.Lerp(Arrow.transform.rotation, look, 0.1f);
 



    }

    private void Debug()
    {
        Text TextX = Text1.GetComponent<Text>();
        Text TextY = Text2.GetComponent<Text>();
        Text TextZ = Text3.GetComponent<Text>();

        TextX.text = "X : " + TmpX;
        TextY.text = "Y : " + TmpY;
        TextZ.text = "Z : " + TmpZ;
    }

}
