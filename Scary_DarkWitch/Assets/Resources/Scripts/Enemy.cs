using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum EnemyType
{
    Angelica,
    Arachniodes,
    Lilium,
    Nuphar,
    Orchis,
    Phragmites
}

public class Enemy : MonoBehaviour
{
    
    // Start is called before the first frame update

    [SerializeField]
    private EnemyType type;

    [SerializeField]
    GameObject enemyBullet;

    GameObject player_GameObject;

    public float HP = 100;

    Vector3 movePattern = new Vector3(10,0);

    EnemyBulletSetting enemyBulletsetting;

    void Start()
    {

        player_GameObject = GameObject.Find("Player");
        StartCoroutine(EnemyCorotine());
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) Destroy(this.gameObject);
        transform.position += movePattern * Time.deltaTime;
    }

    public void EnemyTypeSet(EnemyType t)
    {
        type = t;
    }
    public void MovePatternSet(Vector2 movep)
    {
        movePattern = movep;
    }
    public void Damaged(int damage)
    {
        HP -= damage;
        StartCoroutine(ChangeColor());
    }
    public IEnumerator ChangeColor()
    {
        // �q�I�u�W�F�N�g���i�[����z��쐬
        var children = new Transform[transform.childCount];

        // 0�`��-1�܂ł̎q�����Ԃɔz��Ɋi�[
        for (var i = 0; i < children.Length; ++i)
        {
            children[i] = transform.GetChild(i);
        }
        foreach (Transform child in children) child.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //�����ɏ���������

        yield return new WaitForSeconds(0.1f);

        //�����ɍĊJ��̏���������
        foreach (Transform child in children) child.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
    public IEnumerator EnemyCorotine()
    {
        while (true)
        {
            switch (type)
            {
                case EnemyType.Angelica: // �ʏ�e�����G
                    for(int i = 0; i < 5; i++)
                    {
                        yield return new WaitForSeconds(0.2f);
                        enemyBulletsetting = new EnemyBulletSetting(
                            new Vector3(this.transform.position.x - 1.7f, this.transform.position.y, this.transform.position.z),
                            EnemyBullet.BulletType.Straight,
                            new Vector3(-10f, 0f)
                            );
                        Injection(enemyBulletsetting);

                    }
                    yield return new WaitForSeconds(2.5f);

                    break;

                case EnemyType.Arachniodes:

                    break;


                case EnemyType.Lilium: // �z�[�~���O�e�����G
                    enemyBulletsetting = new EnemyBulletSetting(
                        new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z),
                        EnemyBullet.BulletType.Tracking,
                        new Vector3(10f, 10f)
                        );
                    Injection(enemyBulletsetting);
                    yield return new WaitForSeconds(0.6f);

                    break;
                case EnemyType.Nuphar: // �e�������G
                    for(int i = 0; i < 20; i++)
                    {
                        var PI = Mathf.PI;
                        enemyBulletsetting = new EnemyBulletSetting(
                            new Vector3(this.transform.position.x - Mathf.Cos(PI * i / 10) * 2 - 1.7f, this.transform.position.y - Mathf.Sin(PI * i / 10) * 2, this.transform.position.z),
                            EnemyBullet.BulletType.Rotating,
                            new Vector3(-10f, 0)
                            );
                        enemyBulletsetting.EnemyBulletAngleSet(60 / 20 * i);
                        Injection(enemyBulletsetting);
                        yield return new WaitForSeconds(0.2f);
                    }

                    yield return new WaitForSeconds(10.0f);
                    break;
                case EnemyType.Orchis:
                    break;
                case EnemyType.Phragmites:
                    break;

            }

        }
    }

    private void Injection(EnemyBulletSetting enemy_bs)
    {
        // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
        var enemyBullet_GameObject = Instantiate(enemyBullet, enemy_bs.bulletSpawnPosition, Quaternion.identity);
        enemyBullet_GameObject.GetComponent<EnemyBullet>().Bullet_Set(enemy_bs);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
public class EnemyBulletSetting
{
    public Vector3 bulletSpawnPosition { get; private set; }
    public EnemyBullet.BulletType bulletType { get; private set; }
    public Vector3 bulletSpeed { get; private set; }
    public float Angle { get; private set; }

    /// <summary>
    /// �G�̒e�̐ݒ�
    /// </summary>
    /// <param name="bulletSpawnPosition">�e�̏o���ʒu</param>
    /// <param name="bulletType">�e�̃^�C�v</param>
    /// <param name="bulletSpeed">�e�̑���</param>
    public EnemyBulletSetting(Vector3 bulletSpawnPosition, EnemyBullet.BulletType bulletType, Vector3 bulletSpeed)
    {
        this.bulletSpawnPosition = bulletSpawnPosition;
        this.bulletType = bulletType;
        this.bulletSpeed = bulletSpeed;

        this.Angle = 0;
    }
    public void EnemyBulletAngleSet(float Angle)
    {
        this.Angle = Angle;
    }
}