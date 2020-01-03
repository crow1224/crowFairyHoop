using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleManager : SingletonMonoBehaviour<TitleManager>
{
    private int Title_BestScore;//ベストスコア用
    private int Title_LastScore;//ラストスコア用

    [SerializeField]
    private Text BEST;//ベストスコアテキスト用

    [SerializeField]
    private Text LAST;//ラストスコアテキスト用

    public GameObject FeedPanel;//フェードアウトパネル用


    // Start is called before the first frame update
    void Start()
    {
        //画像を非表示
        FeedPanel.SetActive(false);

        //スコアのロード
        // コメントアウト:中永大輝
        // 理由:ハイスコアの保存先を「PlayerPrefs」からリーダーボードに変更するため
        //Title_BestScore = PlayerPrefs.GetInt("BESTSCORE");

        Title_LastScore = PlayerPrefs.GetInt("LASTSCORE");

        //テキストに反映
        // コメントアウト:中永大輝
        // 理由:ハイスコアの保存先を「PlayerPrefs」からリーダーボードに変更するため
        //BEST.text = "BEST:" + Title_BestScore;

        LAST.text = "LAST:" + Title_LastScore;









    }
}
