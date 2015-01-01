using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon
{
    //[SerializeField]
    //private float delay = 0.2f; // 무기를 발사하는 애니메이션이 완료되는 시간

    [SerializeField]
    private int criticalDamage;
    [SerializeField]
    private int criticalProbability;

    //없애야됨
    private GameEntity owner;

    public Weapon(GameEntity owner)
    {
        this.owner = owner;
    }

    public void Fire(GameEntity target)
    {
        if (target == null)
            return;

        //yield return new WaitForSeconds(delay); // 애니메이션 시간동안 딜레이, 나중에 콜백으로 바꿔야 한다.
        //Debug.Log("play attack motions");

        // owner 없애야 한다. 이 부분은 옵저버를 둬서 애니메이션이 다 끝나면 옵저버로 컨트롤러에게 연락을 취해서
        // 컨트롤러에서 밑의 코드를 실행해야한다.
        Projectile projectile = Spawner.Instance.GetProjectile(ProjectileType.POISON);

        if (Network.peerType == NetworkPeerType.Disconnected) InitBuildedProjectile(ref projectile, ref target); // Single mode
        else projectile.gameObject.GetComponent<SyncStateScript>().NetworkInitProjectile(owner, target); // Multi mode
        //Message msg = target.ObtainMessage(MessageTypes.MSG_DAMAGE, 10);
        //target.DispatchMessage(msg);
    }

    public void InitBuildedProjectile(ref Projectile projectile, ref GameEntity target)
    {
        projectile.transform.position = owner.transform.position;
        projectile.transform.localScale = Vector3.one;
        projectile.FireProcess(owner, target);
    }

    /*
     * attributes
     */
    public const string TAG = "[Weapon]";
}
