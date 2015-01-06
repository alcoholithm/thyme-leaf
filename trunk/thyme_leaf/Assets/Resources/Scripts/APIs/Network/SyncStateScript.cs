using UnityEngine;
using System.Collections;

public class SyncStateScript : MonoBehaviour
{
    private Vector3 lastPosition;
    private float minimumMovement;
    private Hero hero;

    // Use this for initialization
    void Start()
    {
        // Prevent game object from synchronizing when connected to network
        if (Network.peerType == NetworkPeerType.Disconnected)
            Destroy(this);

        minimumMovement = 1.5f;
        if (hero == null)
        {
            hero = gameObject.GetComponent<Hero>();
            
        }
    }

    void Update()
    {
        if (networkView.isMine && Vector3.Distance(transform.localPosition, lastPosition) >= minimumMovement)
        {
            lastPosition = transform.localPosition;
            networkView.RPC("SyncPosition", RPCMode.Others, transform.localPosition);
        }
    }

    [RPC]
    void SyncPosition(Vector3 newPos)
    {
        transform.localPosition = newPos;
    }

    [RPC]
    void OnArriveBranch(Vector3 position)
    {
        transform.position = position;
    }

    /****************************************************************************************************/
    // Network methods for changing state

    [RPC]
    void NetworkChangeState(NetworkViewID ownerID)
    {
        Hero owner = NetworkView.Find(ownerID).GetComponent<Hero>();
        owner.StateMachine.ChangeState(HeroState_Moving.Instance);

        //if (owner.helper.attack_target == null || (!isCharacter && owner.helper.attack_target.model.HP <= 0))
        //{
        //    owner.target = null;
        //    owner.StateMachine.ChangeState(HeroState_Moving.Instance);
        //    Debug.Log("Enemy is died or disappeared");
        //}

        //if (owner.helper.attack_target != null)
        //{
        //    //attack...
        //    owner.helper.attack_delay_counter += Time.deltaTime;
        //    if (owner.helper.attack_delay_counter >= owner.model.AttackDelay)
        //    {
        //        Message msg = owner.ObtainMessage(MessageTypes.MSG_DAMAGE,
        //            new HeroDamageCommand(owner.helper.attack_target, (int)owner.model.AttackDamage));
        //        //msg.arg1 = (int) owner.model.AttackDamage;

        //        owner.DispatchMessage(msg);
        //        owner.helper.attack_delay_counter = 0;
        //    }
        //}
    }


    /****************************************************************************************************/
    // Network methods for tower

    public void NetworkInitTower(BattleModel model, GameObject view)
    {
        if (gameObject.networkView.isMine)
        {
            networkView.RPC("OnNetworkInitTower", RPCMode.All, model.SelectedObject.networkView.viewID);
        }
    }

    [RPC]
    void OnNetworkInitTower(NetworkViewID parentViewID)
    {
        Transform parentTransform = NetworkView.Find(parentViewID).transform;
        gameObject.transform.parent = parentTransform;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.position = parentTransform.position;
        gameObject.GetComponent<AutomatTower>().StateMachine.ChangeState(TowerState_Building.Instance);

        if(!networkView.isMine)
            gameObject.transform.parent.GetComponent<UIButton>().enabled = false;
    }

    /****************************************************************************************************/
    // Network methods for projectile

    public void NetworkInitProjectile(GameEntity owner, GameEntity target)
    {
        if (gameObject.networkView.isMine)
            networkView.RPC("OnNetworkInitProjectile", RPCMode.All, owner.networkView.viewID, target.networkView.viewID);
    }

    [RPC]
    void OnNetworkInitProjectile(NetworkViewID ownerViewID, NetworkViewID targetViewID)
    {
        GameObject owner = NetworkView.Find(ownerViewID).gameObject;
        GameObject target = NetworkView.Find(targetViewID).gameObject;

        gameObject.transform.position = owner.transform.position;
        gameObject.transform.localScale = Vector3.one;
        gameObject.GetComponent<Projectile>().FireProcess(owner.GetComponent<GameEntity>(), target.GetComponent<GameEntity>());
    }


    /****************************************************************************************************/
    // Test methods for real time synchronization

    //void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    //{
    //    Debug.Log("OnSerializeNetworkView");
    //    Vector3 syncPos = Vector3.zero;
    //    if (stream.isWriting)
    //    {
    //        syncPos = currPos;
    //        stream.Serialize(ref syncPos);
    //    }
    //    else
    //    {
    //        stream.Serialize(ref syncPos);
    //        currPos = syncPos;
    //    }
    //}
    
}
