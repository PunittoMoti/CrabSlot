using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSlotController : MonoBehaviour
{

    List<GameObject> mSlotobjs;//スロット目のObject
    int mPhase;//フェーズ管理用
    int mCreatecount;

    Vector3 mStartpos;//開始位置
    Vector3 mNexpos;//次点位置
    Vector3 mEndpos;//終点位置


    // Start is called before the first frame update
    void Start()
    {
        mSlotobjs = new List<GameObject>();
        //初期位置取得
        mStartpos = GameObject.Find("StartTargetObj").GetComponent<RectTransform>().position;

        //次点位置取得
        mNexpos = GameObject.Find("NextTargetObj").GetComponent<RectTransform>().position;

        //エンド位置取得
        mEndpos = GameObject.Find("EndTargetObj").GetComponent<RectTransform>().position;

        //複製するPrefab取得
        GameObject Originalitemobj = (GameObject)Resources.Load("SlotObject");
        //生成先のオブジェクト取得
        GameObject Plantobj = GameObject.Find("SlotObjectList");

        //生成から生成しきるまで
        //オブジェクト生成
        GameObject instntobj = Instantiate(Originalitemobj, new Vector3(mStartpos.x, mStartpos.y, 0.0f), Quaternion.identity);
        //生成したオブジェクトを生成先オブジェクトの子として設定
        instntobj.transform.SetParent(Plantobj.transform, false);

        instntobj.GetComponent<CSlotObject>().SetStartpos(mStartpos);
        instntobj.GetComponent<CSlotObject>().SetEndpos(mEndpos);

        //オブジェクトのリストに追加
        mSlotobjs.Add(instntobj);
    }

    // Update is called once per frame
    void Update()
    {
        switch (mPhase)
        {
            case 0:
                //複製するPrefab取得
                GameObject Originalitemobj = (GameObject)Resources.Load("SlotObject");
                //生成先のオブジェクト取得
                GameObject Plantobj = GameObject.Find("SlotObjectList");

                if (mSlotobjs[mCreatecount].GetComponent<RectTransform>().position.y < mNexpos.y)
                {
                    //生成から生成しきるまで
                    //オブジェクト生成
                    GameObject instntobj = Instantiate(Originalitemobj, new Vector3(mStartpos.x, mStartpos.y, 0.0f), Quaternion.identity);
                    //生成したオブジェクトを生成先オブジェクトの子として設定
                    instntobj.transform.SetParent(Plantobj.transform, false);

                    instntobj.GetComponent<CSlotObject>().SetStartpos(mStartpos);
                    instntobj.GetComponent<CSlotObject>().SetEndpos(mEndpos);

                    //オブジェクトのリストに追加
                    mSlotobjs.Add(instntobj);

                    mCreatecount++;
                }

                if (mCreatecount > 2)
                {
                    mPhase = 1;
                }
                
                break;
            case 1:
                //通常更新
                break;
    
        }
    }
}
