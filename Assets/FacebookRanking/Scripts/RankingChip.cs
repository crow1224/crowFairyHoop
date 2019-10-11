using UnityEngine;
using UnityEngine.UI;

public class RankingChip : MonoBehaviour
{
    [SerializeField] Text m_rankText;
    [SerializeField] Text m_nickNameText;
    [SerializeField] Text m_scoreText;

    public void SetValue(int rank, string name, int score)
    {
        m_rankText.text = rank.ToString();
        m_nickNameText.text = name.ToString();
        m_scoreText.text = score.ToString();
    }
}
