using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision Object)
    {

        // 当たったオブジェクトのタグがPlayerなら
        if (Object.gameObject.name == "Player")
        {
            Destroy(this.gameObject); // 自分を消す

            Object.gameObject.GetComponent<PlayerCon_now>().AddScore(1);
                
          
        }

    }
}
