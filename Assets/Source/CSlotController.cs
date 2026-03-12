using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CSlotController : MonoBehaviour
{

    List<GameObject> mSlotparent;//スロットの親オブジェクトlist
    List<GameObject> mSlotlaneleft;//左スロットレーン
    List<GameObject> mSlotlanefront;//中央スロットレーン
    List<GameObject> mSlotlaneright;//右スロットレーン

    List<GameObject> mStopslotobjt;//真ん中に止まったスロット目

    int mPhase;//フェーズ管理用
    int mSelectslotnumber;//選択されているスロット
    int[] mCreatecount;

    Vector3[] mUppos;//開始位置
    Vector3[] mFrontpos;//次点位置
    Vector3[] mDownpos;//終点位置
    Vector3[] mMoveendpos;//スロットが止まるときの位置

    bool[] misNextphase;//次のフェーズ移動へのフラグ

    [SerializeField]
    List<Sprite> mSlotimageleft;//左レーンに使う画像リスト
    [SerializeField]
    List<Sprite> mSlotimagefront;//中央レーンに使う画像リスト
    [SerializeField]
    List<Sprite> mSlotimageright;//右レーンに使う画像リスト



    // Start is called before the first frame update
    void Start()
    {
        /*
         複数化
         */
        //各レーンのオブジェクト生成数
        mCreatecount = new int[3];

        misNextphase = new bool[3];

        //開始位置
        mUppos = new Vector3[3];
        mFrontpos = new Vector3[3];
        mDownpos = new Vector3[3];
        mMoveendpos = new Vector3[3];


        //スロット選択値初期化
        mSelectslotnumber = 0;

        //スロットレーンの親オブジェクト
        mSlotparent = new List<GameObject>();

        //スロットレーン変数生成
        mSlotlaneleft = new List<GameObject>();
        mSlotlanefront = new List<GameObject>();
        mSlotlaneright = new List<GameObject>();
        mStopslotobjt = new List<GameObject>();

        //複製するPrefab取得
        GameObject Originalitemobj = (GameObject)Resources.Load("SlotObject");
        
        List<GameObject> Slotlane;
        Slotlane = new List<GameObject>();

        List<Sprite> sloimages;
        sloimages = new List<Sprite>();

        mSlotparent.Add(GameObject.Find("SlotlaneLeft"));
        mSlotparent.Add(GameObject.Find("SlotlaneFront"));
        mSlotparent.Add(GameObject.Find("SlotlaneRight"));


        for (int i = 0; i < 3; i++)
        {
            

            //各リストアドレスを取得
            switch (i)
            {
                case 0:
                    Slotlane = mSlotlaneleft;
                    sloimages = mSlotimageleft;
                    break;
                case 1:
                    Slotlane = mSlotlanefront;
                    sloimages = mSlotimagefront;
                    break;
                case 2:
                    Slotlane = mSlotlaneright;
                    sloimages = mSlotimageright;
                    break;
            }



            //一段目位置取得
            mUppos[i] = mSlotparent[i].transform.Find("UpTargetObj").GetComponent<RectTransform>().position;

            //二段目位置取得
            mFrontpos[i] = mSlotparent[i].transform.Find("FrontTargetObj").GetComponent<RectTransform>().position;

            //三段目位置取得
            mDownpos[i] = mSlotparent[i].transform.Find("DownTargetObj").GetComponent<RectTransform>().position;

            //一段目に戻る位置位置
            mMoveendpos[i] = mSlotparent[i].transform.Find("MoveEndTargetObj").GetComponent<RectTransform>().position;
           
            //生成から生成しきるまで
            //オブジェクト生成
            GameObject instntobj = Instantiate(Originalitemobj, new Vector3(mUppos[i].x, mUppos[i].y, 0.0f), Quaternion.identity);
            //生成したオブジェクトを生成先オブジェクトの子として設定
            instntobj.transform.SetParent(mSlotparent[i].transform, false);

            instntobj.GetComponent<CSlotObject>().SetStartpos(mUppos[i]);
            instntobj.GetComponent<CSlotObject>().SetEndpos(mMoveendpos[i]);


            instntobj.GetComponent<CSlotObject>().SetSlotTexture(mSlotimageleft[0]);


            //スロット目の画像を設定
            instntobj.GetComponent<CSlotObject>().SetSlotTexture(sloimages[0]);



            //オブジェクトのリストに追加
            Slotlane.Add(instntobj);

            //生成数カウント初期化（一つ目は作成したので1で初期化）
            mCreatecount[i] = 0;

        }



    }

    // Update is called once per frame
    void Update()
    {

        List<GameObject> Slotlane;
        Slotlane = new List<GameObject>();

        List<Sprite> sloimages;
        sloimages = new List<Sprite>();

        Debug.Log("フェーズ　" + mPhase);
        switch (mPhase)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {


                    //各リストアドレスを取得
                    switch (i)
                    {
                        case 0:
                            Slotlane = mSlotlaneleft;
                            sloimages = mSlotimageleft;
                            break;
                        case 1:
                            Slotlane = mSlotlanefront;
                            sloimages = mSlotimagefront;
                            break;
                        case 2:
                            Slotlane = mSlotlaneright;
                            sloimages = mSlotimageright;
                            break;
                    }

                    //Debug.Log("IN " + i + "　" + mFrontpos[i].y);

                    //次点を超えたかつ３以上生成していなければ
                    if (Slotlane[mCreatecount[i]].GetComponent<RectTransform>().position.y < mFrontpos[i].y && mCreatecount[i] < 2)
                    {
                        //複製するPrefab取得
                        GameObject Originalitemobj = (GameObject)Resources.Load("SlotObject");

                        //生成から生成しきるまで
                        //オブジェクト生成
                        GameObject instntobj = Instantiate(Originalitemobj, new Vector3(mUppos[i].x, mUppos[i].y, 0.0f), Quaternion.identity);
                        //生成したオブジェクトを生成先オブジェクトの子として設定
                        instntobj.transform.SetParent(mSlotparent[i].transform, false);

                        //Start位置とエンド位置取得
                        instntobj.GetComponent<CSlotObject>().SetStartpos(mUppos[i]);
                        instntobj.GetComponent<CSlotObject>().SetEndpos(mMoveendpos[i]);

                        //スロット目の画像を設定
                        instntobj.GetComponent<CSlotObject>().SetSlotTexture(sloimages[mCreatecount[i]+1]);

                        //オブジェクトのリストに追加
                        Slotlane.Add(instntobj);

                        //生成カウント増加
                        mCreatecount[i]++;
                    }



                }

                //生成数が全レーンで３つ以上になれば
                if (mCreatecount[0] > 1 && mCreatecount[1] > 1 && mCreatecount[1] > 1)
                {
                    mPhase = 1;
                }
                
                break;
            case 1:
                //入力まち
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    mPhase = 2;
                    
                }
                break;
            case 2:
                //停止時更新

                /*
                 中央店と一致するまで動く
                一致したら全てにStopする
                そして次のフェーズへ
                 
                 */

                

                //各リストアドレスを取得
                switch (mSelectslotnumber)
                {
                    case 0:
                        Slotlane = mSlotlaneleft;
                        break;
                    case 1:
                        Slotlane = mSlotlanefront;
                        break;
                    case 2:
                        Slotlane = mSlotlaneright;
                        break;
                }

                //停止処理
                for (int i = 0; i < Slotlane.Count; i++)
                {
                    Slotlane[i].GetComponent<CSlotObject>().StopIsMove();

                }


                int targetobjnumber;//中央に選ばれた要素番号を格納
                float distance;//中心からの距離（中央に最も近い物を探す判定用）

                //初期化
                distance = 0;
                targetobjnumber = 0;

                for (int i = 0; i < Slotlane.Count; i++)
                {
                    if (i == 0)
                    {
                        targetobjnumber = i;
                        distance = Mathf.Abs(mMoveendpos[mSelectslotnumber].y - Slotlane[i].transform.position.y);
                    }
                    else
                    {
                        if (distance > Mathf.Abs(mMoveendpos[mSelectslotnumber].y - Slotlane[i].transform.position.y))
                        {
                            targetobjnumber = i;
                            distance = Mathf.Abs(mMoveendpos[mSelectslotnumber].y - Slotlane[i].transform.position.y);
                        }
                    }


                }

                //中央に止まったものを取得
                mStopslotobjt.Add(Slotlane[targetobjnumber]);



                //スロット目の位置を強制移動で整える
                switch (targetobjnumber)
                {
                    case 0:

                        Slotlane[1].GetComponent<RectTransform>().position = mUppos[mSelectslotnumber];
                        Slotlane[0].GetComponent<RectTransform>().position = mFrontpos[mSelectslotnumber];
                        Slotlane[2].GetComponent<RectTransform>().position = mDownpos[mSelectslotnumber];
                        break;
                    case 1:
                        Slotlane[2].GetComponent<RectTransform>().position = mUppos[mSelectslotnumber];
                        Slotlane[1].GetComponent<RectTransform>().position = mFrontpos[mSelectslotnumber];
                        Slotlane[0].GetComponent<RectTransform>().position = mDownpos[mSelectslotnumber];

                        break;
                    case 2:
                        Slotlane[0].GetComponent<RectTransform>().position = mUppos[mSelectslotnumber];
                        Slotlane[2].GetComponent<RectTransform>().position = mFrontpos[mSelectslotnumber];
                        Slotlane[1].GetComponent<RectTransform>().position = mDownpos[mSelectslotnumber];
                        break;
                }

                mPhase = 1;

                mSelectslotnumber++;

                if (mSelectslotnumber > 2)
                {
                    mPhase = 3;
                }


                break;
            case 3:


                //役判定＋得点加算
                if(mStopslotobjt[0].GetComponent<Image>().sprite.name == mStopslotobjt[1].GetComponent<Image>().sprite.name &&
                    mStopslotobjt[1].GetComponent<Image>().sprite.name == mStopslotobjt[2].GetComponent<Image>().sprite.name)
                {
                    Debug.Log("得点ゲット！ " + mStopslotobjt[0].GetComponent<Image>().sprite.name);
                }

                mPhase = 4;


                break;

            case 4:

                //入力まち
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    //再稼働処理
                    for (int i = 0; i < mSlotlaneleft.Count; i++)
                    {
                        mSlotlaneleft[i].GetComponent<CSlotObject>().StartIsMove();
                        mSlotlanefront[i].GetComponent<CSlotObject>().StartIsMove();
                        mSlotlaneright[i].GetComponent<CSlotObject>().StartIsMove();
                    }

                    //初期化
                    targetobjnumber = 0;
                    mSelectslotnumber = 0;
                    mStopslotobjt = new List<GameObject>();

                    mPhase = 1;


                }

                break;
        }
    }
}
