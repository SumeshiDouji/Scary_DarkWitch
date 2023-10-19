using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OkuBeam());
    }
    int movePattern;
    // Update is called once per frame
    void Update()
    {
        switch (movePattern)
        {
            case 0:
                break;
            case 1:
                transform.position += new Vector3(0, 0, 100) * Time.deltaTime;
                break;
            case 2:
                transform.position += new Vector3(0, 0, -100) * Time.deltaTime;
                break;
        }

    }
    public IEnumerator OkuBeam()
    {
        while (true)
        {
            // 移動
            movePattern = 1;
            /*
            Injection(new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z),
                EnemyBullet.BulletType.Tracking,
                new Vector3(10f, 10f)
                );
            */
            yield return new WaitForSeconds(1f);

            // 待機＆攻撃
            movePattern = 0;

            GenerateBeam(new Vector3(this.transform.position.x, this.transform.position.y + 10.0f, this.transform.position.z),
                Beam.BeamType.UeBeam,
                new Vector3(100f, 100f)
                );

            // transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            yield return new WaitForSeconds(0.6f);

            // 移動
            movePattern = 2;
            yield return new WaitForSeconds(1f);

            // 待機
            movePattern = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            yield return new WaitForSeconds(2f);
        }
    }
    /// <summary>
    /// ビーム
    /// </summary>
    [SerializeField]
    private GameObject beamObject;
    private void GenerateBeam(Vector3 bulletSpawnPosition, Beam.BeamType beamType, Vector3 beamSpeed)
    {
        // Cubeプレハブを元に、インスタンスを生成
        var bossBeam_GameObject = Instantiate(beamObject, bulletSpawnPosition, Quaternion.identity);
        bossBeam_GameObject.GetComponent<Beam>().Beam_Set(beamType, beamSpeed);
    }
}
