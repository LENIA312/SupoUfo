using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



    public class ScoreUIManager : MonoBehaviour
    {

        Vector3 SliderBase; //スライダーのベース座標

        GameObject SliderBase_Obj; // スライダーベースのオブジェクト
        public GameObject NumTextObj; // テキストオブジェクト

        RectTransform CanvasRect; // CanvasのRectTransform

        // この座標をMousePosに掛けることでスクリーン座標からキャンバス座標に変換できる
        float Magnification;

        float SliderBaseWidth;
        float SliderWidth;

        public static int Num;

        int StartPoint = 0;
        int MaxNum;

        float MemoryMax = 0;

        public int PlayerNum; // プレイヤー番号


        void Start()
        {
            SliderBase = GameObject.Find("ScoreSlider").transform.position; // スライダーのベースの座標を取得
            SliderBase_Obj = GameObject.Find("ScoreSlider").gameObject;

            MaxNum = 1;

            //スライダーの土台のX座標
            SliderBase.x = SliderBase.x * Magnification - CanvasRect.sizeDelta.x / 2;
            //Debug.Log("スライダー座標 ; " + SliderBase.x);

            //スライダーベースの幅
            SliderBaseWidth = SliderBase_Obj.GetComponent<RectTransform>().sizeDelta.x;
            //スライダーの幅
            SliderWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;

            MemoryMax = (((SliderBase.x + SliderBaseWidth / 2) - SliderWidth / 2) - ((SliderBase.x - SliderBaseWidth / 2) + SliderWidth / 2)) / MaxNum;

            //スライダー初期位置
            Vector3 TmpVec;
            TmpVec.x = ((SliderBase.x - SliderBaseWidth / 2) + SliderWidth / 2) + StartPoint * MemoryMax;

            TmpVec.y = transform.localPosition.y;
            TmpVec.z = transform.localPosition.z;

            transform.localPosition = TmpVec;

            Num = StartPoint;

        }

        void Awake()
        {
            CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>(); // RectTransformを取得
            Magnification = CanvasRect.sizeDelta.x / Screen.width;


        }

        void Update()
        {

            //最大得点を取得
            GameObject GM = GameObject.Find("GameManager");
            GameManager gm = GM.GetComponent<GameManager>();
            if (gm.GetMaxScore() != 0) MaxNum = gm.GetMaxScore();

             //プレイヤー番号に応じたスコアを取得
             Num = gm.GetScore(PlayerNum);

            //スコアテキストを更新
            Text NumText = NumTextObj.GetComponent<Text>();
            NumText.text = "" + Num;

             MemoryMax = (((SliderBase.x + SliderBaseWidth / 2) - SliderWidth / 2) - ((SliderBase.x - SliderBaseWidth / 2) + SliderWidth / 2)) / MaxNum;

             //スライダーの位置を更新
             Vector3 TmpVec;
             TmpVec.x = ((SliderBase.x - SliderBaseWidth / 2) + SliderWidth / 2) + Num * MemoryMax;

             TmpVec.y = transform.localPosition.y;
             TmpVec.z = transform.localPosition.z;

             transform.localPosition = TmpVec;

    }


    }

