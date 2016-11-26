using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    //ﾃﾞﾊﾞｯｸ用ﾃｷｽﾄ
    public DebugText debugtext;

    Rigidbody2D rigidbody2d;
    //自分の位置(x,y)
    public float bullet_x;
    public float bullet_y;
    //狙う物の位置(x,y)
    public float target_x;
    public float target_y;
    //弾の速度
    public float speed;
    //自機狙い弾の制限角度
    public float theta;

    //playerｵﾌﾞｼﾞｪｸﾄの取得
    public GameObject obj;

    //弾の挙動設定
    public int type;
    //定数:弾の挙動
    enum Bullet_type {
        target_straight,
        target_homing_endless,
        target_homing_countend,
        random_straight
    };

    //時限処理用
    float frameCount = 0;
    public float limit_frame;

    //初期化ﾌﾗｸﾞ(true:初期 false:2回目以降)
    bool farst;

    Vector2 direction; //移動方向
    Vector2 direction_old; //1ﾌﾚｰﾑ前の移動方向

    // Use this for initialization
    void Start () {
        //コンポーネントの取得
        rigidbody2d = GetComponent<Rigidbody2D>();
        obj = GameObject.Find("player");

        //初期化ﾌﾗｸﾞの初期化
        farst = true;
    }

    // Update is called once per frame
    void Update () {

        switch (type)
        {
            case (int)Bullet_type.target_homing_endless:

                //2回以降の場合、上限角度内で移動をさせるために、過去の移動方向を参照して動かす
                if (farst == false)
                {
                    //移動方向を保存
                    direction_old = direction;

                    //playerの位置を代入
                    target_x = obj.transform.position.x;
                    target_y = obj.transform.position.y;
                    //弾の位置を代入
                    bullet_x = transform.position.x;
                    bullet_y = transform.position.y;
                    //移動する向きを求める
                    direction = new Vector2(target_x - bullet_x, target_y - bullet_y).normalized;

                    //右回り旋回角度上限のﾍﾞｸﾄﾙを求める
                    float rad = Mathf.PI / 180 * theta;
                    float x2 = Mathf.Cos(rad) * direction_old.x - Mathf.Sin(rad) * direction_old.y;
                    float y2 = Mathf.Sin(rad) * direction_old.x + Mathf.Cos(rad) * direction_old.y;

                    //自機方向と旋回角度上限のどちらに曲がるかを決める
                    if (direction_old.x * direction.x + direction_old.y * direction.y >= direction_old.x * x2 + direction_old.y * y2)
                    {
                        //自機方向が旋回可能範囲内の場合、自機方向に曲がるため、追加処理をしない?
                        //debug
                        debugtext.GetComponent<DebugText>().text = 1;
                    }
                    else
                    {
                        //debug
                        debugtext.GetComponent<DebugText>().text = 2;
                        //自機方向が旋回可能範囲外の場合
                        //左回り旋回角度上限の速度ﾍﾞｸﾄﾙを求める
                        float x3 = Mathf.Cos(rad) * direction_old.x + Mathf.Sin(rad) * direction_old.y;
                        float y3 = -Mathf.Sin(rad) * direction_old.x + Mathf.Cos(rad) * direction_old.y;

                        //弾から自機への相対位置ﾍﾞｸﾄﾙ(px,py)を求める
                        float px = target_x - bullet_x;
                        float py = target_y - bullet_y;

                        //右回りか左回りかを求める
                        if (px * x2 + py * y2 >= px * x3 + py * y3)
                        {
                            //右回りの場合
                            direction.Set(x2, y2);
                        }
                        else
                        {
                            //左回りの場合
                            direction.Set(x3, y3);
                        }
                    }
                    //移動する向きとスピードを代入する
                    rigidbody2d.velocity = direction.normalized * speed;
                }
                else
                {
                    //playerの位置を代入
                    target_x = obj.transform.position.x;
                    target_y = obj.transform.position.y;
                    //弾の位置を代入
                    bullet_x = transform.position.x;
                    bullet_y = transform.position.y;
                    //移動する向きを求める
                    direction = new Vector2(target_x - bullet_x, target_y - bullet_y).normalized;
                    //移動する向きとスピードを代入する
                    rigidbody2d.velocity = direction * speed;

                    //初期化ﾌﾗｸﾞを変更
                    if (farst)
                    {
                        farst = false;
                    }
                }
                break;
            case (int)Bullet_type.target_homing_countend:
                //制限時間以降はﾎｰﾐﾝｸﾞさせない
                if (frameCount <= limit_frame )
                {
                    ++frameCount;

                    //2回以降の場合、上限角度内で移動をさせるために、過去の移動方向を参照して動かす
                    if (farst == false)
                    {
                        //移動方向を保存
                        direction_old = direction;

                        //playerの位置を代入
                        target_x = obj.transform.position.x;
                        target_y = obj.transform.position.y;
                        //弾の位置を代入
                        bullet_x = transform.position.x;
                        bullet_y = transform.position.y;
                        //移動する向きを求める
                        direction = new Vector2(target_x - bullet_x, target_y - bullet_y).normalized;

                        //右回り旋回角度上限のﾍﾞｸﾄﾙを求める
                        float rad = Mathf.PI / 180 * theta;
                        float x2 = Mathf.Cos(rad) * direction_old.x - Mathf.Sin(rad) * direction_old.y;
                        float y2 = Mathf.Sin(rad) * direction_old.x + Mathf.Cos(rad) * direction_old.y;

                        //自機方向と旋回角度上限のどちらに曲がるかを決める
                        if (direction_old.x * direction.x + direction_old.y * direction.y >= direction_old.x * x2 + direction_old.y * y2)
                        {
                            //自機方向が旋回可能範囲内の場合、自機方向に曲がるため、追加処理をしない?
                            //debug
                            debugtext.GetComponent<DebugText>().text = 1;
                        }
                        else
                        {
                            //debug
                            debugtext.GetComponent<DebugText>().text = 2;
                            //自機方向が旋回可能範囲外の場合
                            //左回り旋回角度上限の速度ﾍﾞｸﾄﾙを求める
                            float x3 = Mathf.Cos(rad) * direction_old.x + Mathf.Sin(rad) * direction_old.y;
                            float y3 = -Mathf.Sin(rad) * direction_old.x + Mathf.Cos(rad) * direction_old.y;

                            //弾から自機への相対位置ﾍﾞｸﾄﾙ(px,py)を求める
                            float px = target_x - bullet_x;
                            float py = target_y - bullet_y;

                            //右回りか左回りかを求める
                            if (px * x2 + py * y2 >= px * x3 + py * y3)
                            {
                                //右回りの場合
                                direction.Set(x2, y2);
                            }
                            else
                            {
                                //左回りの場合
                                direction.Set(x3, y3);
                            }
                        }
                        //移動する向きとスピードを代入する
                        rigidbody2d.velocity = direction.normalized * speed;
                    }
                    else
                    {
                        //playerの位置を代入
                        target_x = obj.transform.position.x;
                        target_y = obj.transform.position.y;
                        //弾の位置を代入
                        bullet_x = transform.position.x;
                        bullet_y = transform.position.y;
                        //移動する向きを求める
                        direction = new Vector2(target_x - bullet_x, target_y - bullet_y).normalized;
                        //移動する向きとスピードを代入する
                        rigidbody2d.velocity = direction * speed;

                        //初期化ﾌﾗｸﾞを変更
                        if (farst)
                        {
                            farst = false;
                        }
                    }
                }
                else
                {
                    //移動する向きとスピードを代入する
                    rigidbody2d.velocity = direction * speed;
                }
                break;
            case (int)Bullet_type.target_straight:
                if (farst)
                {
                    //playerの位置を代入
                    target_x = obj.transform.position.x;
                    target_y = obj.transform.position.y;
                    //弾の位置を代入
                    bullet_x = transform.position.x;
                    bullet_y = transform.position.y;
                    //移動する向きを求める
                    //2回目以降実行しない
                    farst = false;
                }
                direction = new Vector2(target_x - bullet_x, target_y - bullet_y).normalized;
                //移動する向きとスピードを代入する
                rigidbody2d.velocity = direction * speed;
                break;
            case (int)Bullet_type.random_straight:
                if (farst)
                {
                    //targetにﾗﾝﾀﾞﾑ値を代入
                    direction = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                    //2回目以降実行しない
                    farst = false;
                }
                //移動する向きとスピードを代入する
                rigidbody2d.velocity = direction.normalized * speed;
                break;
        }

    }

}
