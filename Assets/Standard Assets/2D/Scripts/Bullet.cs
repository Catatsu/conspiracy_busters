using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Rigidbody2D rigidbody2d;
    //自分の位置(x,y)
    public float bullet_x;
    public float bullet_y;
    //狙う物の位置(x,y)
    public float target_x;
    public float target_y;
    //弾の速度
    public float speed;

    //playerｵﾌﾞｼﾞｪｸﾄの取得
    public GameObject obj;

    //弾の挙動設定
    public int type;
    //定数:弾の挙動
    enum Bullet_type {
        target_straight,
        target_homing
    };

    //初期化ﾌﾗｸﾞ(true:初期 false:2回目以降)
    bool farst;

    Vector2 direction;

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
            case (int)Bullet_type.target_homing:
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
        }

    }

}
