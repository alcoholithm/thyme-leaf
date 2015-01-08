using UnityEngine;
using System.Collections;

public interface IProjectile : IAttackable, IMovable
{
    void Explode();
}