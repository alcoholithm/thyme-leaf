using UnityEngine;
using System.Collections;

public class Weapon //: MonoBehaviour
{
    private float delay = 0.2f; //
    private int power = 10;

    public IEnumerator Fire(GameEntity target)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("play attack motions");

        // 이건 이제 발사체 안에 들어가야 한다.
        // 발사체와 충돌시 메시지를 보내는 방식으로 가야한다.
        // 지금은 테스트
        Message msg = target.ObtainMessage(MessageTypes.MSG_DAMAGE, power);
        target.DispatchMessage(msg);
    }

    /*
     * attributes
     */
    public const string TAG = "[Weapon]";
}
