using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSlotObject : MonoBehaviour
{
    /*
     想定
    画像生成一括生成
    →順番に下に移動
    →下の意っていい一に行ったら上に移動
    →上に移動した際に判定（ボタンが押されたかどうか）
    →止まるFlagをオンにしたあと徐々にスピード落とした３週くらいで止まる

    //必要なもの
    ■変数
    画像を一括生成する配列
    開始位置
    次点移動開始位置
    終点位置

    ■処理
    開始位置への初期化

    生成したオブジェクトの下への移動
    →下に降りる処理を生成するオブジェクトに設定
    　→常に下に降りる・Flag制御で更新処理停止・移動速度調整・止まる時の演出移動
    →順々に送るために一定位置になるとつきの者を動かす

    終点についたら上に戻す

    確定したら3週後止める（演出アリ）

    ■構成
    ・スロット目のObject
    ・↑を制御するManager
    ・演出面の管理者


     */
    bool mIsMove; //移動フラグ
    Vector3 mStartpos;//開始位置
    Vector3 mEndpos;//終点位置

    // Start is called before the first frame update
    void Start()
    {
        mIsMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //移動フラグがONなら
        if (mIsMove)
        {
            Vector3 nowpos = gameObject.GetComponent<RectTransform>().position;

            nowpos.y = nowpos.y - 0.001f; 

            //終点位置より下なら開始位置へ更新
            if(nowpos.y< mEndpos.y)
            {
                gameObject.GetComponent<RectTransform>().position = new Vector3(mStartpos.x, mStartpos.y, 0.0f);
            }
            //終点位置より上なら通常更新
            else
            {
                gameObject.GetComponent<RectTransform>().position = new Vector3(nowpos.x, nowpos.y, nowpos.z);
            }

            
        }
    }

    //移動フラグON
    public void StartIsMove()
    {
        mIsMove = true;
    }

    //移動フラグOFF
    public void StopIsMove()
    {
        mIsMove = false;
    }

    //開始位置取得
    public void SetStartpos(Vector3 pos)
    {
        mStartpos = pos;

        gameObject.GetComponent<RectTransform>().position = new Vector3(mStartpos.x, mStartpos.y, 0.0f);

    }

    //終点位置取得
    public void SetEndpos(Vector3 pos)
    {
        mEndpos = pos;
    }


}
