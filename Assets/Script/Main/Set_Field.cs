using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Field : MonoBehaviour
{
    #region     //背景に関係する変数群//

    [SerializeField]
    private float Change_Position = 19;//背景を移動する位置（19毎に移動）

    [SerializeField]
    private GameObject[] Field = null;//背景を入れる配列

    [SerializeField]
    private SpriteRenderer[] BG_Random;

    [SerializeField]
    private Sprite[] BG_Skin;//背景画像格納用


    private SpriteRenderer BG_Sprite;//背景画像用


    private int Field_number = 0;//次に移動する背景

    private int Field_number_Max = 3;//背景の最大数

    private int BG_number;

    private int BG_AddPos = 57;//背景の移動幅

    private float BG_CameraPosition = 0;//背景用のカメラのｘ座標

    private int BG_Count;
    #endregion

    #region     //リング生成に関係する変数群//

    [SerializeField]
    private float Instans_Position = 9;//リングの生成幅

    [SerializeField]
    private GameObject[] Ring = null;//リングを生成するオブジェクト格納配列

    private int Ring_number = 0;//生成するリングの数字

    private int Ring_AddPos = 9;//次回生成するリングに追加するｘ座標値

    private int Ring_Move = 3;//上下するリングがある配列の数字

    private int Set_Ring_FirstScore = 3;//リングのランダム生成制限のスコア（動かないリング）

    private int Set_Ring_SecondScore = 30;//リングランダム生成制限のスコア（縦と斜めに動くリング）

    private int Set_Ring_ThreeScore = 80;//リングランダム生成制限のスコア（全てのリング解放）

    private int First_Set = 3;//ランダム生成の最初の上限

    private int Second_Set = 7;//ランダム生成の2番目の上限

    private float Ring_cameraPosition = 0;//リング用のカメラのｘ座標

    private float Ring_pos_y = 0;//リングのｙ座標

    private float Ring_Random_Max = 2.5f;//ランダムのMAX

    private float Ring_Random_Min = -2.5f;//ランダムのMIN
    #endregion





    // Start is called before the first frame update
    void Start()
    {
        //カメラのｘ座標を取得
        BG_CameraPosition = this.transform.position.x;
        Ring_cameraPosition = this.transform.position.x;

        //ランダムで背景を選択
        BG_Random_Set();

    }

    // Update is called once per frame
    void Update()
    {
        //現在のカメラのX座標が背景を変更する座標よりも大きい場合
        if(this.transform.position.x > BG_CameraPosition + Change_Position)
        {
            //背景を移動する
            Change_Field();
            //次回の座標設定
            BG_CameraPosition += Change_Position;
        }

        //現在のカメラのX座標がリング生成する座標よりも大きい場合
        if (this.transform.position.x > Ring_cameraPosition + Instans_Position)
        {
            //リングを生成
            Instans_Ring();
            //次回の生成座標を設定
            Ring_cameraPosition += Instans_Position;
        }
    }


    private void Change_Field()//背景座標を変更する関数
    {
        //配列が最後までいった場合
        if (Field_number == Field_number_Max)
        {
            //初期状態に戻す
            Field_number = 0;
        }
        //配置予定の配列のオブジェクトの座標を取得
        Vector3 pos = Field[Field_number].transform.position;
        //オブジェクトを配置
        Field[Field_number].transform.position = new Vector3(pos.x + BG_AddPos, pos.y, pos.z);

        //次回配置する背景を設定
        Field_number++;

    }

    private void Instans_Ring()//リングを生成する関数
    {
        //スコアが80以上の場合はすべてのリングを解放
        if(DunkManager.Instance.Score >= Set_Ring_ThreeScore)
        {
            //配置するオブジェクトをランダムで決定
            Ring_number = Random.Range(0, Ring.Length);

        }
        //スコアが30以上の場合は横に動くもの以外を解放
        else if(DunkManager.Instance.Score >= Set_Ring_SecondScore)
        {
            //配置するオブジェクトをランダムで決定
            Ring_number = Random.Range(0, Second_Set);

        }
        //スコアが３以上なら斜めの動かないリングを解放
        else if(DunkManager.Instance.Score >= Set_Ring_FirstScore)
        {
            //配置するオブジェクトをランダムで決定
            Ring_number = Random.Range(0, First_Set);
        }

       
        //配置するオブジェクトが上下するリングだった場合
        if(Ring_number >= Ring_Move)
        {
            
            //生成する最大座標位置を下げる
            Ring_pos_y = Random.Range(Ring_Random_Min, 0);
            
        }
        else
        {
            
            //ランダムでｙ座標を決定
            Ring_pos_y = Random.Range(Ring_Random_Min, Ring_Random_Max);
        }




        //リングを生成
        Instantiate(Ring[Ring_number], new Vector3(Ring_cameraPosition + Ring_AddPos, Ring_pos_y, 0), Ring[Ring_number].transform.rotation);

    }

    private void BG_Random_Set()//背景をランダムに変更する関数
    {
        BG_number = Random.Range(0, BG_Skin.Length);

        for (BG_Count =0;BG_Count < BG_Random.Length; BG_Count++)
        {
            //BG_Sprite = BG_Random[BG_Count].GetComponent<SpriteRenderer>();

            //背景画像を変更
            //BG_Random[BG_Count].sprite = BG_Skin[BG_number];
        }

    }
}
