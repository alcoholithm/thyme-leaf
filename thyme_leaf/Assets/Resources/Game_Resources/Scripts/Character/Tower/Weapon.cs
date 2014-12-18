using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon //: MonoBehaviour
{
    [SerializeField]
    private float delay = 0.2f; // 무기를 발사하는 애니메이션이 완료되는 시간
    private GameEntity owner;

    public Weapon(GameEntity owner)
    {
        this.owner = owner;
    }

    public IEnumerator Fire(GameEntity target)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("play attack motions");

        Projectile projectile = ProjectileSpawner.Instance.Allocate(owner.transform);
        projectile.FireProcess(owner, target);

        //Message msg = target.ObtainMessage(MessageTypes.MSG_DAMAGE, 10);
        //target.DispatchMessage(msg);
    }

    /*
     * attributes
     */
    public const string TAG = "[Weapon]";
}
