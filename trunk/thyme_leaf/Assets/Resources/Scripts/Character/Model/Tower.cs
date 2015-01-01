using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tower : Unit, IAttackable
{
    [SerializeField]
    private Weapon weapon;
    private List<GameEntity> enemies;
    private GameEntity currentTarget;

    private float reloadingTime = 0.5f; // 재장전시간 // 재장전은 무기의 주인이 하는 것이니 여기에 정의

    public Tower(GameEntity owner)
    {
        this.enemies = new List<GameEntity>();
        this.weapon = new Weapon(owner);
    }

    /*
    * followings are member functions
    */
    private GameEntity FindBestTarget()
    {
        // 초기에 이애가 죽었는지 살았는지 판단해야함.
        // 다른 놈에 의해 제거될 가능성도 있으므로

        enemies.ForEach(e => { if (!e.gameObject.activeInHierarchy) enemies.Remove(e); });

        if (Enemies.Count > 0)
        {
            CurrentTarget = Enemies[0];
        }
        else
            CurrentTarget = null;

        return CurrentTarget;
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
    public IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(reloadingTime);
            //yield return StartCoroutine(weapon.Fire(this));

            weapon.Fire(FindBestTarget());
        }
    }

    /*
     * 
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
    public new const string TAG = "[Tower]";



   
}
