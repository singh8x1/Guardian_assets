using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entitysummoner : MonoBehaviour
{
   
    public static List<Enemy> EnemiesInGame;
    public static Dictionary<int, GameObject> EnemyPrefabs;
    public static Dictionary<int, Queue<Enemy>> EnemyObjectPools;

    private static bool IsIntialized;
    public static void Init()
    {
        if (IsIntialized)
        {


            EnemyPrefabs = new Dictionary<int, GameObject>();
            EnemyObjectPools = new Dictionary<int, Queue<Enemy>>();
            EnemiesInGame = new List<Enemy>();


            Enemiessummondata[] Enemies = Resources.LoadAll<Enemiessummondata>("enemies");

            foreach (Enemiessummondata enemy in Enemies)
            {
                EnemyPrefabs.Add(enemy.EnemyID, enemy.EnemyPrefab);
                EnemyObjectPools.Add(enemy.EnemyID, new Queue<Enemy>());
            }
            IsIntialized = true;
        }
    
        else
        {
            Debug.Log("Already intialized");
        }
    }

    public static Enemy SummonEnemy(int EnemyID)
    {
        Enemy SummonedEnemy = null;
        if (EnemyPrefabs.ContainsKey(EnemyID))
        {

            Queue<Enemy> RefrencedQueue = EnemyObjectPools[EnemyID];

            if (RefrencedQueue.Count > 0)
            {
                //deque
                SummonedEnemy = RefrencedQueue.Dequeue();
                SummonedEnemy.Init();

                SummonedEnemy.gameObject.SetActive(true);
            }
            else
            {
                //intiating
                GameObject NewEnemy = Instantiate(EnemyPrefabs[EnemyID], Vector3.zero, Quaternion.identity);
                SummonedEnemy = NewEnemy.GetComponent<Enemy>();
                SummonedEnemy.Init();
            }
        }
        else
        {
            Debug.Log("Does not exis {EnemyID}");
            return null;
        }
        EnemiesInGame.Add(SummonedEnemy);
        SummonedEnemy.ID = EnemyID;
        return SummonedEnemy;
    }

    // Update is called once per frame
   public static void RemoveEnemy(Enemy EnemyToRemove)
    {
        EnemyObjectPools[EnemyToRemove.ID].Enqueue(EnemyToRemove);
        EnemyToRemove.gameObject.SetActive(false);
        EnemiesInGame.Remove(EnemyToRemove);

    }
}
