using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> Enemies { get; private set; }

    public int TotalEnemies { get; set; }
    public int CurrentNumberOfEnemies { get; set; }

    private void Awake()
    {
        Enemies = new List<Enemy>();
    }

    public void RegisterEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        TotalEnemies++;
        CurrentNumberOfEnemies = Enemies.Count;
    }

    public void DeregisterEnemy(Enemy enemy)
    {
        // Handle some sort of enemy killed event here.

        Enemies.Remove(enemy);
        CurrentNumberOfEnemies = Enemies.Count;
    }
}
