using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon_Test : MonoBehaviour
{
    public float angle = 0; //角度
    Vector3 shotVector;

    public GameObject Obj2;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetForce();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            PlayerMove();
        }

        //PlayerMove();

    }

    void GetForce()
    {
        float rad = angle * Mathf.Deg2Rad; //角度をラジアン角に変換
        //float rad = GetAngle(this.transform.position, Obj2.transform.position);
        //rad = rad * Mathf.Deg2Rad;

        //ラジアン角から発射用ベクトルを作成
        double addforceX = Mathf.Sin(rad);
        double addforceY = Mathf.Cos(rad);
        shotVector = new Vector3((float)addforceX,0, (float)addforceY);


    }

    void PlayerMove()
    {
        //Rigidbody2Dを取得してから
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        //発射
        rigidbody.AddForce(shotVector);
    }



    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        Debug.Log(degree);
        return degree;

    }



}
