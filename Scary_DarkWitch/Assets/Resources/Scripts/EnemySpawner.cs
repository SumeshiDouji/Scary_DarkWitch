using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public enum ManagerPattern
    {
        Stage1,
        Stage2,
        Oni
    }
    public enum SpawnPattern
    {
        zakogundan1,
        zakogundan2,
        zakogundan3,
        zakogundan4,
        zakogundan5,
        zakogundan6,
        zakogundan7,
        zakogundan8,
        zakogundan9,
        zakogundan10,
        zakogundan11,
        zakogundan12,
        zakogundan13,
        seiei,
        totugekitai,
        bumeran,
        tairetu
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StageSpawnPattern());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    ManagerPattern m = ManagerPattern.Stage1;
    /// <summary>
    /// ステージ全体でエネミースポーンマネージャーを呼ぶタイミングを設定するコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator StageSpawnPattern()
    {

        switch (m)
        {
            case ManagerPattern.Stage1:
                StartCoroutine(SpawnManager(SpawnPattern.zakogundan1));
                yield return new WaitForSeconds(8f);
                StartCoroutine(SpawnManager(SpawnPattern.zakogundan1));
                yield return new WaitForSeconds(8f);
                break;
                
            case ManagerPattern.Stage2:
                break;
            case ManagerPattern.Oni:
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(10f);
    }
    /// <summary>
    /// エネミーを呼ぶタイミングを設定するコルーチン
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public IEnumerator SpawnManager(SpawnPattern sp)
    {
        switch (sp)
        {
            case SpawnPattern.zakogundan1:
                var enemyGameObject1= Instantiate(Enemy, new Vector2(transform.position.x + 1, transform.position.y), Quaternion.identity);
                enemyGameObject1.GetComponent<Enemy>().EnemyTypeSet(EnemyType.Nuphar);
                enemyGameObject1.GetComponent<Enemy>().MovePatternSet(new Vector2(1, 2));
                yield return new WaitForSeconds(6f);

                var enemyGameObject2 = Instantiate(Enemy, new Vector2(transform.position.x + 2, transform.position.y + 2), Quaternion.identity);
                enemyGameObject2.GetComponent<Enemy>().EnemyTypeSet(EnemyType.Angelica);
                enemyGameObject2.GetComponent<Enemy>().MovePatternSet(new Vector2(-5, 0));
                var enemyGameObject3 = Instantiate(Enemy, new Vector2(transform.position.x + 2, transform.position.y - 2), Quaternion.identity);
                enemyGameObject3.GetComponent<Enemy>().EnemyTypeSet(EnemyType.Angelica);
                enemyGameObject3.GetComponent<Enemy>().MovePatternSet(new Vector2(-2, 0));
                yield return new WaitForSeconds(6f);
                break;
            case SpawnPattern.zakogundan2:
                break;
            case SpawnPattern.seiei:
                break;
            case SpawnPattern.totugekitai:
                break;
            case SpawnPattern.bumeran:
                break;
            case SpawnPattern.tairetu:
                break;
        }


    }
}
