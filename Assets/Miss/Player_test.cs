using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{

    public GameObject Target;
    GameObject Player;

    Vector3 TargetPos;
    Vector3 PlayerPos;

    Transform Target_Trs;
    Transform Player_Trs;

    float Speed;

    float theta;

    float Vx, Vy;





    // Start is called before the first frame update
    void Start()
    {
        // Transform
        Player_Trs = this.transform;
        Target_Trs = Target.transform;

        // Vector3
        PlayerPos = Player_Trs.position;
        TargetPos = Target_Trs.position;

        Speed = 0.01f;

        theta = Mathf.Atan2(TargetPos.z - PlayerPos.z, TargetPos.x - PlayerPos.x);
        Vx = Mathf.Cos(theta);
        Vy = Mathf.Sin(theta);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos.x = PlayerPos.x + Vx * Speed;
        PlayerPos.z = PlayerPos.z + Vy * Speed;

        Player_Trs.position = PlayerPos;
    }
}
