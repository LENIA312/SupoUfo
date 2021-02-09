using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



    public class Test_002 : MonoBehaviour, IPointerDownHandler
    {

        Vector3 SliderBase; //スライダーのベース座標

        GameObject SliderBase_Obj; // スライダーベースのオブジェクト
        GameObject NumTextObj;

        private AudioSource kachi; // カチ


        RectTransform CanvasRect; // CanvasのRectTransform

        // この座標をMousePosに掛けることでスクリーン座標からキャンバス座標に変換できる
        float Magnification;

        bool ButtonClick;

        public bool SoundOn;

        float SliderBaseWidth;
        float SliderWidth;

        public static int Num;

        public int StartPoint;
        public int MaxNum;

        float MemoryMax = 0;
        int tmpNum;


        void Start()
        {
            SliderBase = GameObject.Find("Slider_Base").transform.position; // スライダーのベースの座標を取得
            SliderBase_Obj = GameObject.Find("Slider_Base").gameObject;
            NumTextObj = GameObject.Find("NumText").gameObject;

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

            tmpNum = Num;

        // オーディオソースを取得
            kachi = GetComponent<AudioSource>();

        }

        void Awake()
        {
            CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>(); // RectTransformを取得
            Magnification = CanvasRect.sizeDelta.x / Screen.width;


        }

        void Update()
        {
     

            if (Input.GetMouseButtonUp(0)) ButtonClick = false; // ボタンを離したときにフラグを解除

            if (ButtonClick) Num = FollowMouse(true, false); // フラグが経っている間マウスに追従,現在の値を取得
                                                             //Debug.Log(tmp);

        if (Num != tmpNum && SoundOn)
        {
            kachi.PlayOneShot(kachi.clip);
            tmpNum = Num;
        }


            Text NumText = NumTextObj.GetComponent<Text>();
            NumText.text = "" + Num;

        }

        //UIが押されたときに行われる処理
        public void OnPointerDown(PointerEventData pointEventData)
        {
            //Debug.Log(gameObject.name + "からの応答 : 問題ない");

            //クリックされたときにフラグを立てる
            ButtonClick = true;

        }



        #region [ マウス追従 ]
        /// <summary>
        /// マウスに追従させる
        /// 概要 : 第1引数 X方向,第2引数 Y方向に移動させるか固定させるか
        ///        TRUEは移動、FALSEは固定
        /// </summary>
        public int FollowMouse(bool x, bool y)
        {
            int MemoryNum;
            //float MemoryMax;

            var MousePos = Input.mousePosition; // マウス座標の取得

            //Z座標は固定
            MousePos.z = transform.localPosition.z;

            #region[ X座標の処理 ]
            if (x)
            {
                MousePos.x = MousePos.x * Magnification - CanvasRect.sizeDelta.x / 2;
            }
            else
            {
                MousePos.x = transform.localPosition.x;
            }
            #endregion

            #region[ Y座標の処理 ]
            if (y)
            {
                MousePos.y = MousePos.y * Magnification - CanvasRect.sizeDelta.y / 2;
            }
            else
            {
                MousePos.y = transform.localPosition.y;
            }
            #endregion

            //移動範囲の制限
            MousePos.x = Mathf.Clamp(MousePos.x, (SliderBase.x - SliderBaseWidth / 2) + (SliderWidth / 2), (SliderBase.x + SliderBaseWidth / 2) - (SliderWidth / 2));
            transform.localPosition = MousePos;
            //Debug.Log("スライダー 　; " + tmp);

            //MemoryNum = Mathf.FloorToInt( ((SliderBase.x + SliderBaseWidth / 2) - SliderWidth / 2) - ((SliderBase.x - SliderBaseWidth / 2) + SliderWidth / 2));
            //MemoryMax = (((SliderBase.x + SliderBaseWidth / 2) - SliderWidth / 2) - ((SliderBase.x - SliderBaseWidth / 2) + SliderWidth / 2))/MaxNum;
            MemoryNum = Mathf.FloorToInt((MousePos.x - ((SliderBase.x - SliderBaseWidth / 2) + SliderWidth / 2)) / MemoryMax);

            //Debug.Log("test 　; " + MemoryNum);

            return MemoryNum;


        }
        #endregion


        public static int GetNum()
        {
            return Num;
        }



    }

