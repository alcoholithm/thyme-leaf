using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tower : Unit
{
    private List<GameEntity> enemies;

    [SerializeField]
    private GameEntity currentTarget;

    private float reloadingTime = 2f; // 재장전시간


    public Tower(GameEntity owner)
    {
        Initialize();
    }

    /*
    * followings are public member functions
    */
    private void Initialize()
    {
        this.enemies = new List<GameEntity>();
    }

    /*
    * followings are public member functions
    */
    public void FindBestTarget()
    {
        // 초기에 이애가 죽었는지 살았는지 판단해야함.
        // 다른 놈에 의해 제거될 가능성도 있으므로
        
        enemies.ForEach(e => { if (e == null || !e.gameObject.activeInHierarchy) enemies.Remove(e); });

        TrimEnemies();

        if (Enemies.Count > 0)
        {
            CurrentTarget = Enemies[0];
        }
        else
            CurrentTarget = null;

        NotifyObservers(ObserverTypes.Enemy);
    }

    public void TrimEnemies()
    {
        enemies.ForEach(e => { if (!e.gameObject.activeInHierarchy) enemies.Remove(e); });
    }

    public bool HasMoreEnemies()
    {
        return enemies.Count > 0;
    }

    /*
    * followings are implemented methods of "ITower"
    */
    public void AddEnemy(GameEntity enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameEntity enemy)
    {
        enemies.Remove(enemy);
    }

    /*
     * followings are implemented methods of "IAttackable"
     */
    //public void Attack()
    //{
    //    weapon.Fire(FindBestTarget());
    //}

    /*
     * Followings are attributes
     */ 
    public List<GameEntity> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }
    public GameEntity CurrentTarget
    {
        get { return currentTarget; }
        set { currentTarget = value; }
    }
    public float ReloadingTime
    {
        get { return reloadingTime; }
        set { reloadingTime = value; }
    }

    public new const string TAG = "[Tower]";
}
