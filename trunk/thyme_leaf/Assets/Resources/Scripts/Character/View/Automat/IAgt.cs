using UnityEngine;
using System.Collections;

public interface IAgt : IAttackable
{
    void TakeDamage(int damage);
    void SetAttackable(bool active);
}
