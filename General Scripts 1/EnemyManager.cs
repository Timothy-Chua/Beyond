using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public AIEnemy[] floor1;
    public AIEnemy[] floor2;
    public AIEnemy[] floor3;

    private AIEnemy[] currentEnemyFloor;
    private AIEnemy closestEnemy;
    private float distToEnemy;
    private float distToClosestEnemy;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AIEnemy GetClosestEnemy(Transform originTransform, EnemyFloor enemyFloor)
    {
        switch (enemyFloor)
        {
            case EnemyFloor.Floor1:
                currentEnemyFloor = floor1;
                break;
            case EnemyFloor.Floor2:
                currentEnemyFloor = floor2;
                break;
            case EnemyFloor.Floor3:
                currentEnemyFloor = floor3;
                break;
        }

        foreach (AIEnemy enemy in currentEnemyFloor)
        {
            distToEnemy = Vector3.Distance(enemy.transform.position, originTransform.position);

            if (distToEnemy <= distToClosestEnemy)
            {
                distToClosestEnemy = distToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public void Distract(Transform originTransform, EnemyFloor enemyFloor)
    {
        AIEnemy enemy = GetClosestEnemy(originTransform, enemyFloor);
        enemy.playerLastPosition = originTransform.position;
        enemy.state = AIState.Detecting;
    }
}

public enum EnemyFloor
{
    Floor1, Floor2, Floor3
}
