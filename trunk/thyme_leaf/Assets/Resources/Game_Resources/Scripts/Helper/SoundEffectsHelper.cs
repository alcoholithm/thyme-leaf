using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffectsHelper : MonoBehaviour {
	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;
	
	public AudioClip automatTestAttack;
	public AudioClip automatTestDamaged;
	public AudioClip automatTestCritical;
	public AudioClip automatTestDied;
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}
	
	public void MakeAutomatTestAttackSound(Transform userTransform)
	{
		MakeSound(automatTestAttack, userTransform);
	}
	
	public void MakeAutomatTestDamagedSound(Transform userTransform)
	{
		MakeSound(automatTestDamaged, userTransform);
	}
	
	public void MakeAutomatTestCriticalSound(Transform userTransform)
	{
		MakeSound(automatTestCritical, userTransform);
	}

	public void MakeAutomatTestDiedSound(Transform userTransform)
	{
		MakeSound(automatTestDied, userTransform);
	}

	/// <summary>
	/// Play a given sound
	/// </summary>
	/// <param name="originalClip"></param>
	private void MakeSound(AudioClip originalClip, Transform originalTransform)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, originalTransform.position);
	}
}
