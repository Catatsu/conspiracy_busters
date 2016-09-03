using UnityEngine;
using System.Collections;

public class player : MonoBehaviour 
{
	//animator読み込み
	Animator animator;
	Rigidbody2D rigidbody2d;

    //爆発のﾌﾟﾚﾊﾌﾞを読み込む
    public GameObject bomb;
    //スピード設定
    public float fast = 3; //高速
    public float late = 1.5f; //低速
    public float speed; //playerの速度

	void Start ()
	{
		//コンポーネントの取得
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();

        //speed初期値設定(初期高速
        speed = fast;
    }

	// Update is called once per frame
	void Update () 
	{
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		//移動する向きを求める
		Vector2 direction = new Vector2 (x, y).normalized;

        //現在の速度を設定する
        if(Input.GetKey(KeyCode.LeftShift) && speed == fast)
        {
            //高速移動中に左ｼﾌﾄが押された場合、低速にする
            speed = late;
        }else if(!Input.GetKey(KeyCode.LeftShift) && speed == late)
        {
            //低速移動中に左ｼﾌﾄが離された場合、高速にする
            speed = fast;
        }
		//移動する向きとスピードを代入する
		rigidbody2d.velocity = direction * speed;

		//アニメーションの切り替えようにステータスを入れる
		animator.SetFloat("animation_state",x);
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        //爆発させる
        Instantiate(bomb, transform.position, transform.rotation);

        //ﾌﾟﾚｲﾔｰを削除
        Destroy(gameObject);

    }
}
