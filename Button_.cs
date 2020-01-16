using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button_ : MonoBehaviour
{
    /*ボタンの関数一覧*/

    private void NotActive_Image()
    {
        //画像を非表示にする
        this.gameObject.SetActive(false);
    }

    private void NotActive_Text()
    {
        //テキストを非表示にする
        this.gameObject.SetActive(false);

    }

    //ポーズを開始
    public void Button_Pose()
    {
        DunkManager.Instance.Pose();
    }

    //スキン画面へ遷移
    public void Secsen_Skin()
    {
        SceneManager.LoadScene("Skin");
    }

    //メイン画面へ遷移
    public void Secsen_Main()
    {
        
        SceneManager.LoadScene("Main");
    }

    //タイトル画面へ遷移
    public void Secsen_Title()
    {
        SceneManager.LoadScene("Title");
    }

    //フェードアウトを開始
    public void Feed()
    {
        AudioFeed.Instance.Soundfeedout();
        TitleManager.Instance.FeedPanel.SetActive(true);
    }
}
