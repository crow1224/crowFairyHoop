using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QMG;

public class FacebookRanking : MonoBehaviour
{
    static private FacebookRanking Instance;
    static public FacebookRanking instance
    {
        get { return Instance; }
    }

    const string KeyName = "RankingLeaderboard";
    const int Front = 1;
    const int Back = 0;

    Color32 normalColor = new Color32(255, 255, 255, 255);
    Color32 hideColor = new Color32(128, 128, 128, 255);

    [Header("スコア表示UI")]
    [SerializeField] Text hiScore_text;

    [Header("ランキングUIのroot")]
    [SerializeField] GameObject rankingWindow;

    [Header("フレンドランキングタブとワールドランキングタブ")]
    [SerializeField] Image friendRankingTab;
    [SerializeField] Image worldRankingTab;

    [Header("ランキング表示に使う画像")]
    [SerializeField] Sprite[] topRankingImage;
    [SerializeField] Sprite otherRankingImage;

    [Header("ランキングバー")]
    [SerializeField] RankingChip[] myRankingchip;
    [SerializeField] RankingChip[] friendRankingchip;
    [SerializeField] RankingChip[] worldRankingchip;

    enum RankType
    {
        FRIEND,
        WORLD
    }

    RankType nowRankType = RankType.FRIEND;

    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        worldRankingTab.color = hideColor;
        friendRankingTab.color = normalColor;

        // ハイスコア表示更新
        FBInstant.leaderboard.getPlayerEntryAsync(KeyName, OnGetPlayerLeaderboardScore);
    }

    public void OnClickRankingEnd()
    {
        rankingWindow.SetActive(false);
    }

    // フレンドランキングを前面に表示
    public void OnClickChangeFriendRank()
    {
        nowRankType = RankType.FRIEND;

        worldRankingTab.color = hideColor;
        friendRankingTab.color = normalColor;

        // 場所入れ替え
        worldRankingTab.transform.SetSiblingIndex(Back);
        friendRankingTab.transform.SetSiblingIndex(Front);
    }

    // ワールドランクを前面に表示
    public void OnClickChangeWorldRank()
    {
        nowRankType = RankType.WORLD;

        worldRankingTab.color = normalColor;
        friendRankingTab.color = hideColor;

        // 場所入れ替え
        friendRankingTab.transform.SetSiblingIndex(Back);
        worldRankingTab.transform.SetSiblingIndex(Front);
    }

    // リーダーボードを保存する関数
    public void OnClickSaveLeaderboardButton()
    {
        int score = DunkManager.Instance.Score;

        Dictionary<string, object> extraData = new Dictionary<string, object>();
        FBInstant.leaderboard.setScoreAsync(KeyName, score, extraData, OnSetLeaderboardScore);
    }

    // リーダーボードが保存された時に呼ばれるコールバック関数
    private void OnSetLeaderboardScore()
    {

    }

    // リーダーボードを読み込む関数
    public void OnClickLoadLeaderboardButton()
    {
        rankingWindow.SetActive(true);

        FBInstant.leaderboard.getPlayerEntryAsync(KeyName, OnGetPlayerLeaderboardScore);
        FBInstant.leaderboard.getEntriesAsync(KeyName, 10, OnGetWorldLeaderboardList);
        FBInstant.leaderboard.getConnectedPlayerEntriesAsync(KeyName, 10, OnGetFriendLeaderboardList);
    }

    // 自分のスコア、ランキングを読み込んで表示する関数
    private void OnGetPlayerLeaderboardScore(FBLeaderboardEntry entry)
    {
        for (int i = 0; i < myRankingchip.Length; i++)
        {
            if (topRankingImage.Length > entry.rank)
            {
                myRankingchip[i].GetComponent<Image>().sprite = topRankingImage[entry.rank - 1];
            }
            else
            {
                myRankingchip[i].GetComponent<Image>().sprite = otherRankingImage;
            }

            myRankingchip[i].SetValue(entry.rank, FBInstant.player.getName().ToString(), entry.score);
        }

        hiScore_text.text = "Best:" + entry.score.ToString();
    }

    // 全てのプレイヤーのリーダーボードを読み込んで表示する関数
    private void OnGetWorldLeaderboardList(FBLeaderboardEntry[] entries)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            if (topRankingImage.Length > i)
            {
                worldRankingchip[i].GetComponent<Image>().sprite = topRankingImage[i];
            }
            else
            {
                worldRankingchip[i].GetComponent<Image>().sprite = otherRankingImage;
            }

            worldRankingchip[i].SetValue(i + 1, entries[i].nickName, entries[i].score);
        }
    }

    // フレンドのリーダーボードを読み込んで表示する関数
    private void OnGetFriendLeaderboardList(FBLeaderboardEntry[] entries)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            if (topRankingImage.Length > i)
            {
                friendRankingchip[i].GetComponent<Image>().sprite = topRankingImage[i];
            }
            else
            {
                friendRankingchip[i].GetComponent<Image>().sprite = otherRankingImage;
            }

            friendRankingchip[i].SetValue(i + 1, entries[i].nickName, entries[i].score);
        }
    }
}
