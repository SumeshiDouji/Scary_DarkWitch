using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum BulletType
    {
        Straight=0,
        Tracking=1,
        Rotating=2
    }
    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.Find("Player");

        power = 10;
        switch (bulletType)
        {
            case BulletType.Straight:

                break;
            case BulletType.Tracking:
                StartCoroutine(TrackingBullet(0.5f));
                if (playerGameObject == null || playerGameObject.transform.position.x >= this.transform.position.x)
                {
                }

                // transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                break;
            case BulletType.Rotating:
                var angle = this.transform.eulerAngles;
                angle.z = 2 * Mathf.PI * bulletAngle;
                this.transform.eulerAngles = angle;
                break;

        }
    }
    Vector3 bulletSpeed;
    BulletType bulletType;
    float bulletAngle;
    float angle;
    public int power { get; set; }

    GameObject playerGameObject;

    Vector3 bulletPosition;

    // Update is called once per frame
    void Update()
    {
        switch (bulletType)
        {
            case BulletType.Straight:


                transform.position += transform.rotation * (bulletSpeed * Time.deltaTime);
                break;
            case BulletType.Tracking:
                transform.position += bulletSpeed * Time.deltaTime;
                break;
            case BulletType.Rotating:

                transform.position += transform.rotation * (bulletSpeed * Time.deltaTime);
                break;


        }
        //new Vector3 * Time.deltaTime new Vector3(transform.position.x + xspeed * Time.deltaTime, transform.position.y + xspeed * Time.deltaTime);
        if (transform.position.x < -19f || transform.position.x > 19f || transform.position.y < -4.5f || transform.position.y > 25.5f)
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// <param Frequency="弾のトラッキングの待機時間">description</param>
    /// </summary>
    /// <returns></returns>
    private IEnumerator TrackingBullet(float waitingTime)
    {
        while (true)
        {
            if (playerGameObject != null)
            {
                if(playerGameObject.transform.position.x < this.transform.position.x)
                {
                    //Vector3 diff = (this.playerGameObject.transform.position - this.transform.position).normalized;
                    //this.transform.rotation = Quaternion.FromToRotation(Vector3.left, diff);
                }

                // transform.position += transform.forward * bulletSpeed.x * Time.deltaTime;

            }
            else
            {
                // transform.position += bulletSpeed * Time.deltaTime;
            }
            yield return new WaitForSeconds(waitingTime);
        }
    }

    public void Bullet_Set(EnemyBulletSetting eb)
    {
        bulletType = eb.bulletType;
        bulletSpeed = eb.bulletSpeed;
        bulletAngle = eb.Angle;
    }

    private void BulletDestroy()
    {
        // 弾が爆発するエフェクトを出す
        Destroy(this);
    }
}
