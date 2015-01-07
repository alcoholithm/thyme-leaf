using UnityEngine;
using System.Collections.Generic;

public interface IEntityManager
{
    void RegisterEntity(GameEntity entity);
    void RemoveEntity(GameEntity entity);
}

[System.Serializable]
public class EntityManager : Manager<EntityManager>, IEntityManager
{
    [SerializeField]
    private Dictionary<UnitType, List<GameEntity>> _entityMap;

    /*
     * Followings are unity callback methods
     */ 
    protected override void Awake()
    {
        base.Awake();

        _entityMap = new Dictionary<UnitType, List<GameEntity>>();
        _entityMap.Add(UnitType.AUTOMAT_WCHAT, new List<GameEntity>());
        _entityMap.Add(UnitType.AUTOMAT_CHARACTER, new List<GameEntity>());
        _entityMap.Add(UnitType.AUTOMAT_TOWER, new List<GameEntity>());
        _entityMap.Add(UnitType.TROVANT_THOUSE, new List<GameEntity>());
        _entityMap.Add(UnitType.TROVANT_CHARACTER, new List<GameEntity>());
    }

    /*
     * Followings are implemented mehtods of "IEntityManager"
     */
    public void RegisterEntity(GameEntity entity)
    {
        if (entity is WChat)
        {
            _entityMap[UnitType.AUTOMAT_WCHAT].Add(entity);
        }
        else if (entity is THouse)
        {
            _entityMap[UnitType.TROVANT_THOUSE].Add(entity);
        }
        else if (entity is AutomatTower)
        {
            _entityMap[UnitType.AUTOMAT_TOWER].Add(entity);
        }
    }

    public void RemoveEntity(GameEntity entity)
    {
        if (entity is WChat)
        {
            _entityMap[UnitType.AUTOMAT_WCHAT].Remove(entity);

            if (_entityMap[UnitType.AUTOMAT_WCHAT].Count <= 0)
                GameRuler.Instance.Judge(false);
        }
        else if (entity is THouse)
        {
            _entityMap[UnitType.TROVANT_THOUSE].Remove(entity);

            if (_entityMap[UnitType.TROVANT_THOUSE].Count <= 0)
                GameRuler.Instance.Judge(true);
        }
        else if (entity is AutomatTower)
        {
            _entityMap[UnitType.AUTOMAT_TOWER].Remove(entity);
        }
    }
}