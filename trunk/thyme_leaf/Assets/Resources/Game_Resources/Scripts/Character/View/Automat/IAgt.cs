using UnityEngine;
using System.Collections;

public interface IAgt : IAttackable
{
    void TakeDamage(int damage);
    void CheckDeath();
    void SetAttackable(bool active);
}
