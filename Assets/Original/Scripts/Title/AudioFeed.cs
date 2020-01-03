using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeed : SingletonMonoBehaviour<AudioFeed>
{
    private float bgmvolume;//BGMを入れる変数

    private float MaxVolume = 30;//音量の最大値
    private int Volume_Moth = 100;//ゲームボリュームに合わせる変数

    //フェードアウトさせるコルーチンを呼び出す関数
    public void Soundfeedout()
    {
        //コルーチン開始（フェードアウト）
        StartCoroutine(Fadeout());
    }

    //フェードインをさせるコルーチンを呼び出す関数
    public void Soundfeedin()
    {
        //コルーチン開始（フェードイン）
        StartCoroutine(Fadein());
    }

    //フェードアウトさせるコルーチン
    IEnumerator Fadeout()
    {
        //MaxVolumeから0までを-1ずつ減らす
        for (bgmvolume =MaxVolume; bgmvolume > 0; bgmvolume--)
        {
            //オーディオソースのボリュームを取得し÷100
            this.GetComponent<AudioSource>().volume = bgmvolume / Volume_Moth;
            yield return null;
        }
        //強制的にボリュームを0に変更
        this.GetComponent<AudioSource>().volume = 0;
        
    }

    //フェードインさせるコルーチン
    IEnumerator Fadein()
    {
        //0からMaxVolumeまでを+1ずつ増やす
        for (bgmvolume = 0; bgmvolume < MaxVolume; bgmvolume++)
        {
            //オーディオソースのボリュームを取得し÷100
            this.GetComponent<AudioSource>().volume = bgmvolume / Volume_Moth;
            yield return null;
        }
        //強制的にMaxVolumeに変更
        this.GetComponent<AudioSource>().volume = MaxVolume / Volume_Moth;

    }
}
