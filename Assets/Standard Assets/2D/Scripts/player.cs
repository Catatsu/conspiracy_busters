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
	public float speed = 3;

	void Start ()
	{
		//コンポーネントの取得
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update () 
	{
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		//移動する向きを求める
		Vector2 direction = new Vector2 (x, y).normalized;
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
