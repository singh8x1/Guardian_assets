using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameloopManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static Queue<int> EnemyIDsToSummon;
    public bool Loopshouldend;
    private void Start()
    {
        EnemyIDsToSummon = new Queue<int>();
        Entitysummoner.Init();

        StartCoroutine(Gameloop());
        InvokeRepeating("SummonTest", 0f, 1f);
        //InvokeRepeating("RemoveTest", 0f, 0.5f);

    }
    //void RemoveTest()
    //{
     //   if (Entitysummoner.EnemiesInGame.Count > 0)
     //   {
     //       Entitysummoner.RemoveEnemy(Entitysummoner.EnemiesInGame[Random.Range(0, Entitysummoner.EnemiesInGame.Count)]);
     //   }
   // }
    void SummonTest()
    {
        EnqueueEnemyIDToSummon(1);
    }
   IEnumerator Gameloop()
    {
        while (Loopshouldend==false)
        {


            //spawn enemies

            if (EnemyIDsToSummon.Count > 0)
            {
                for(int i = 0; i < EnemyIDsToSummon.Count; i++)
                {
                    Entitysummoner.SummonEnemy(EnemyIDsToSummon.Dequeue());
                }
            }

            //spawn towers

            //move enemies
            //tick towers

            //Apply effects


            //Damage enemies
            //remove enemies
            //remove towers
            yield return null;

        }
    }

    public static void EnqueueEnemyIDToSummon(int ID)
    {
        EnemyIDsToSummon.Enqueue(ID);
    }
}
