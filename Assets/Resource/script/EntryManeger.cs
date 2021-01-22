using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryManeger : MonoBehaviour
{
    bool[] Entry= { false, false, false, false }; // エントリー状態の保存
    [SerializeField] GameObject[] EntryBack = new GameObject[4];
    [SerializeField] GameObject[] EntryText = new GameObject[4];


    public void EntryButtonDown(int ButtonNum)
    {
        Entry[ButtonNum] = true;
        //Debug.Log(Entry[0] + "" + Entry[1] + "" + Entry[2] + "" + Entry[3]);



        EntryText[ButtonNum].SetActive(true);
        ButtonNum += 1;
        GameObject.Find("entryButton" + ButtonNum).SetActive(false);
        //Debug.Log("entryButton" + ButtonNum);

    }

    private void Update()
    {

    }

    private void Start()
    {
        
    }
}
