using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed;
    [SerializeField] private GameObject playerBullet;

    [SerializeField] private int BulletMode;

    /// <summary>
    /// 残機
    /// </summary>
    int remain = 3;

    float bulletIntervalTime1 = 0f;
    float bulletIntervalTime2 = 0f;

    [SerializeField] int defaultHP = 100;
    int HP;

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI remainText;
    SpriteRenderer spriteRenderer;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        HP = defaultHP;
    }

    IEnumerator SpriteDisplayDelay()
    {
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = true;
        HP = defaultHP;
    }

    void HPRunsOut()
    {
        PlayerExplosion();
        spriteRenderer.enabled = false;
        if(remain <= 0)
        {
            GameOver();
        }
        else
        {
            remain--;
            StartCoroutine(SpriteDisplayDelay());
        }
    }
    void GameOver() { Destroy(this.gameObject);  }

    /// <summary>
    /// 破壊されたときのエフェクトを表示
    /// </summary>
    void PlayerExplosion()
    {
        // 爆発エフェクトを表示させる
        // ピチューンという音を発生させる
        audioSource.PlayOneShot(audioSource.clip);
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP:" + HP.ToString();
        remainText.text = "Remain:" + remain.ToString();

        if (HP <= 0)
        {
            // HP0のときは移動や攻撃はできない
            return;
        }

        float moveX=0;
        float moveY=0;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime / 2;
            moveY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime / 2;
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            moveY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        }
        var pos = new Vector2(
        //エリア指定して移動する
        Mathf.Clamp(transform.position.x + moveX, -20.0f, 20.0f),
        Mathf.Clamp(transform.position.y + moveY, -1.5f, 21.5f)
        );
        transform.position = pos;

        if (Input.GetKey(KeyCode.Z))
        {
            // CubeプレハブをGameObject型で取得
            // GameObject obj = (GameObject)Resources.Load("Prefabs/PlayerBullet.prefab");

            Injection();
        }
        if(bulletIntervalTime1 > 0) bulletIntervalTime1 -= Time.deltaTime;
        if (bulletIntervalTime2 > 0) bulletIntervalTime2 -= Time.deltaTime;
    }
    private void Injection()
    {
        switch (BulletMode)
        {
            case 0:
                if (bulletIntervalTime1 <= 0f)
                {
                    Bullet1(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y));
                }
                break;
            case 1:
                if (bulletIntervalTime1 <= 0f)
                {
                    Bullet1(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y + 1.0f));
                    Bullet1(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y - 1.0f));

                }
                break;
            case 2:
                if (bulletIntervalTime2 <= 0f)  Bullet2(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y));
                if (bulletIntervalTime1 <= 0f)
                {
                    Bullet1(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y + 1.25f), 4f);
                    Bullet1(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y - 1.25f), -4f);
                }
                break;
            default:
                Bullet1(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y));
                break;
        }
    }
    private void Bullet1(Vector2 vec2)
    {
        // Cubeプレハブを元に、インスタンスを生成、
        Instantiate(playerBullet, vec2, Quaternion.identity);
        bulletIntervalTime1 = 0.08f;
    }
    private void Bullet1(Vector2 vec2,float ys)
    {
        // Cubeプレハブを元に、インスタンスを生成、
        var bullet = Instantiate(playerBullet, vec2, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().yspeed_set(ys);
        bulletIntervalTime1 = 0.08f;
    }
    private void Bullet2(Vector2 vec2)
    {
        // Cubeプレハブを元に、インスタンスを生成、
        Instantiate(playerBullet, vec2, Quaternion.identity);
        bulletIntervalTime2 = 0.3f;
    }
    private void Damaged(int damage)
    {
        HP -= damage;
        if (HP <= 0) HPRunsOut(); // HP0なら残機を補填するかゲームオーバー
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HP <= 0)
        {
            // HP0のときは当たり判定の処理は通さない
            return;
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            var enemyBullet = collision.gameObject.GetComponent<EnemyBullet>();
            Damaged(enemyBullet.power);

        }
    }
}
