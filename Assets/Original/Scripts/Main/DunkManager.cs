using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DunkManager : SingletonMonoBehaviour<DunkManager>
{

    #region //インスペクターに表示される変数//
    [SerializeField]
    private TextMeshProUGUI Score_Text;//スコア用テキスト

    [SerializeField]
    private TextMeshProUGUI Nine_Text;//スコア用テキスト

    [SerializeField]
    private TextMeshProUGUI GameOver_Text;//ゲームオーバーを出すためのテキスト

    [SerializeField]
    private GameObject Good_Object;//Goodの画像

    [SerializeField]
    private GameObject Nice_Object;//Nineの画像

    [SerializeField]
    private GameObject Excellent_Object;//Amazingの画像

    [SerializeField]
    private GameObject PlayerParticle_Black;//パーティクル用（黒）

    [SerializeField]
    private GameObject PlayerParticle_Orenge;//パーティクル用（薄橙）

    [SerializeField]
    private GameObject Pose_Image;//ポーズ用

    [SerializeField]
    private GameObject Feedoutpanel;//フェードアウトさせるパネル用

    [SerializeField]
    private List<AudioClip> audioClip = new List<AudioClip>();//複数の音声ファイルを格納

    [SerializeField]
    private AudioSource BGM;
    #endregion

    #region //pruvate変数//
    private int BestScore;//ベストスコアを格納

    private int NiceScore = 0;//Nineで追加されるScore

    private int P_good = 2;//パーティクル（黒）を出す値

    private int P_nice = 3;//パーティクル（薄橙）を出す値

    private int good = 1;//Good画像を出す値

    private int nine = 4;//Nine画像を出す値

    private int amazing = 8;//Amazing画像を出す値

    private AudioSource Ring_Sound;//オーディオソースを取得する用
    #endregion

    #region //public変数//
    [System.NonSerialized]
    public int Score;//スコア格納

    [System.NonSerialized]
    public bool F_Play = true;//プレイ続行フラグ

    [System.NonSerialized]
    public bool F_Under = false;//Playerがリング下に行っていないかのフラグ

    [System.NonSerialized]
    public bool F_Score = false;//スコアがもらえるかのフラグ

    [System.NonSerialized]
    public bool F_Pose = false;//ポース字ているかどうかのフラグ

    [System.NonSerialized]
    public bool F_Nice_Bad = true;//成功判定フラグ
    #endregion

    private void Start()
    {
        //テキストを取得
        Score_Text = Score_Text.GetComponent<TextMeshProUGUI>();
        Nine_Text = Nine_Text.GetComponent<TextMeshProUGUI>();
        GameOver_Text = GameOver_Text.GetComponent<TextMeshProUGUI>();

        //オーディオソースを取得
        Ring_Sound = GetComponent<AudioSource>();

        //テキスト、画像を非表示に
        Nice_Object.SetActive(false);
        Good_Object.SetActive(false);
        Excellent_Object.SetActive(false);
        Nine_Text.gameObject.SetActive(false);
        GameOver_Text.enabled = false;
        Pose_Image.SetActive(false);
        Feedoutpanel.SetActive(false);

        //パーティクルを非表示
        PlayerParticle_Black.SetActive(false);
        PlayerParticle_Orenge.SetActive(false);

        //ベストスコアを取得
        BestScore = PlayerPrefs.GetInt("BESTSCORE");

        //スコアを表示
        Score_Text.gameObject.SetActive(true);
        //テクストに反映
        Score_Text.text = Score.ToString();
    }


    void Update()
    {
        //プレイ可能状態かどうか
        if (F_Play == false)
        {
            Feedoutpanel.SetActive(true);


        }
    }

    public void GameOver()//ゲームオーバー
    {
        //スコア更新
        Update_Score();
        //ゲームオーバーテキストを表示
        GameOver_Text.enabled = true;
        //スコアテキストを非表示
        Score_Text.gameObject.SetActive(false);
        //操作を不可にする
        F_Play = false;
        //パーティクルを非表示
        PlayerParticle_Black.SetActive(false);
        PlayerParticle_Orenge.SetActive(false);

        //FacebookRanking.instance.OnClickSaveLeaderboardButton();

    }

    public void Point()//スコア判定
    {
        if (!F_Under)
        {
            //スコアを取れない状態に変更
            F_Score = true;

            return;
        }

        //Nineが取れる状態の場合
        if (F_Nice_Bad)
        {
            //BonasPointを追加
            NiceScore++;
            //テキストに反映
            Nine_Text.text = "+" + NiceScore.ToString();

            //NineScoreが10を超えている場合
            if (NiceScore >= amazing)
            {
                //Amazing画像を表示
                Excellent_Object.SetActive(true);
                //テキストを表示
                Nine_Text.gameObject.SetActive(true);
                CameraShake.Instance.Shake();
                //SEPlay
                Ring_Sound.PlayOneShot(audioClip[2]);
            }
            //NineScoreが5を超えている場合
            else if (NiceScore >= nine)
            {
                //Nine画像を表示
                Nice_Object.SetActive(true);
                //テキストを表示
                Nine_Text.gameObject.SetActive(true);
                //SEPlay
                Ring_Sound.PlayOneShot(audioClip[1]);
            }
            //NiceScoreが1以上の場合
            else if (NiceScore > good)
            {
                //good画像を表示
                Good_Object.SetActive(true);
                //テキストを表示
                Nine_Text.gameObject.SetActive(true);
                CameraShake.Instance.Shake();
                //SEPlay
                Ring_Sound.PlayOneShot(audioClip[0]);
            }
        }
        //スコアをNiceCoutぶん追加
        Score += NiceScore;

        //NineSoreが2以上の場合
        if (NiceScore >= P_good)
        {
            //パーティクル（黒）を表示
            PlayerParticle_Black.SetActive(true);
        }

        //NineScoreが3以上の場合
        if (NiceScore >= P_nice)
        {
            //パーティクル（薄橙）を表示
            PlayerParticle_Orenge.SetActive(true);
        }

        //テクストに反映
        Score_Text.text = Score.ToString();
        //F_Nice_Badを初期化
        F_Nice_Bad = true;
        //F_Underを初期化
        F_Under = false;
    }

    public void Flag_Under()//リングを通った判定
    {
        //リングが下から入った場合
        if (F_Score == true)
        {
            //ゲームオーバー関数を呼び出し
            GameOver();
        }
        //リングが通れる状態に
        F_Under = true;

    }

    public void Bad()//Nineスコアを変更する
    {
        //NineScoreを取れない状態に変更
        F_Nice_Bad = false;
        //NineScoreを初期化
        NiceScore = 1;
        //パーティクルを非表示
        PlayerParticle_Black.SetActive(false);
        PlayerParticle_Orenge.SetActive(false);
    }

    public void Pose()//一時停止開始
    {
        BGM.Pause();
        //ポーズ用画像を表示
        Pose_Image.SetActive(true);
        //ポースを開始
        F_Pose = true;
        //動きを停止する
        Time.timeScale = 0;
    }

    public void Start_Pose()
    {
        //ポース開始
        F_Pose = true;
        //動きを停止する
        Time.timeScale = 0;
    }

    public void RePose()//一時停止解除
    {
        //停止を解除する
        Time.timeScale = 1.0f;
        BGM.UnPause();
        //ポーズ解除
        F_Pose = false;
        //ポーズ画像を非表示
        Pose_Image.SetActive(false);
    }

    private void Update_Score()//ベストスコア、ラストスコアを保存する
    {
        //ベストスコアを取得
        BestScore = PlayerPrefs.GetInt("BESTSCORE");
        //ベストスコアを超えている場合
        if (BestScore < Score)
        {
            //ベストスコアを変更
            // コメントアウト:中永大輝
            // 理由:ハイスコアの保存先を「PlayerPrefs」からリーダーボードに変更するため
            //PlayerPrefs.SetInt("BESTSCORE", Score);
            //PlayerPrefs.Save();

            //最終スコアを変更
            PlayerPrefs.SetInt("LASTSCORE", Score);
            PlayerPrefs.Save();
        }
        else
        {
            //最終スコアを変更
            PlayerPrefs.SetInt("LASTSCORE", Score);
            PlayerPrefs.Save();
        }

    }
}
