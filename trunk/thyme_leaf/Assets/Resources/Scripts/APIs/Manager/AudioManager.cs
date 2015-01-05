using UnityEngine;
using System.Collections;

public class AudioManager : Manager<AudioManager>
{
    // list of music
    public AudioClip LOBBY;
    public AudioClip BATTLE_1;

    // list of sound
    public AudioClip AT_FRANSIS_1_ATTACKING;
    public AudioClip AT_FRANSIS_1_DYING;
    public AudioClip AT_FALSTAFF_1_ATTACKING;
    public AudioClip AT_FALSTAFF_1_DYING;
    public AudioClip AT_APT_1_ATTACKING;
    public AudioClip AT_APT_1_DYING;
    public AudioClip TV_COMMA_ATTACKING;
    public AudioClip TV_COMMA_DYING;
    public AudioClip TV_PYTHON_ATTACKING;
    public AudioClip TV_PYTHON_DYING;

    // objects for audoPlayer
    private AudioClip[] sound;
    private AudioClip[] music;
    private AudioSource audioPlayer;

    void Awake()
    {
        sound = new AudioClip[100];
        music = new AudioClip[100];
        audioPlayer = GetComponent<AudioSource>();

        // insert music into manager
        music[(int)MusicType.LOBBY] = LOBBY;
        music[(int)MusicType.BATTLE_1] = BATTLE_1;

        // insert sounds into manager
        sound[(int)SoundType.AUTOMAT_APT_TYPE1_ATTACKING] = AT_APT_1_ATTACKING;
        sound[(int)SoundType.AUTOMAT_APT_TYPE1_DYING] = AT_APT_1_DYING;
        sound[(int)SoundType.AUTOMAT_FALSTAFF_TYPE1_ATTACKING] = AT_FALSTAFF_1_ATTACKING;
        sound[(int)SoundType.AUTOMAT_FALSTAFF_TYPE1_DYING] = AT_FALSTAFF_1_DYING;
        sound[(int)SoundType.AUTOMAT_FRANSIS_TYPE1_ATTACKING] = AT_FRANSIS_1_ATTACKING;
        sound[(int)SoundType.AUTOMAT_FRANSIS_TYPE1_DYING] = AT_FALSTAFF_1_DYING;
        sound[(int)SoundType.TROVANT_COMMA_ATTACKING] = TV_COMMA_ATTACKING;
        sound[(int)SoundType.TROVANT_COMMA_DYING] = TV_COMMA_DYING;
        sound[(int)SoundType.TROVANT_PYTHON_ATTACKING] = TV_PYTHON_ATTACKING;
        sound[(int)SoundType.TROVANT_PYTHON_DYING] = TV_PYTHON_DYING;
    }

    void Start()
    {
        InitValues();
        string levelName = Application.loadedLevelName;
        if (levelName.Equals("1_Lobby")) audioPlayer.clip = music[(int)MusicType.LOBBY];
        else if (levelName.Equals("3_Battle")) audioPlayer.clip = music[(int)MusicType.BATTLE_1];
        
        StartAudio();
    }

    void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0: // lobby
                audioPlayer.clip = music[(int)MusicType.LOBBY];
                break;
            case 1: // world map
                Debug.Log("Sound Not Found for World Map");
                break;
            case 2: // battle
                audioPlayer.clip = music[(int)MusicType.BATTLE_1];
                break;
            default:
                return;
        }
        StartAudio();
    }

    void InitValues()
    {
        audioPlayer.volume = 0.5f;
        
        //audioPlayer.volume = Settings.MusicVolume;
        //audioPlayer.mute = !Settings.MusicEnabled;        
    }

    public void StartAudio()
    {
        if (!audioPlayer.isPlaying)
        {
            audioPlayer.Play();
        }
    }

    public void PauseAudio()
    {
        if (audioPlayer.isPlaying) audioPlayer.Pause();
    }

    public void StopAudio()
    {
        if (audioPlayer.isPlaying) audioPlayer.Stop();
    }

    //public void PlayClip(GameObject go)
    //{
    //    //PlayClip(Naming.Instance.GetName(go), go.transform.position);
    //}

    public void PlayClipWithState(GameObject go, string state)
    {
        string name = Naming.Instance.GetName(go, state);
        Debug.Log("NAME : "+name);
        PlayClip(name, go.transform.position);
        //PlayClipAtPoint(SoundType.AUTOMAT_APT_TYPE1_ATTACKING, go.gameObject.transform.position);
    }

    private void PlayClip(string name, Vector3 pos)
    {
        

        //PlayClipAtPoint(soundType, pos);
    }

    private void PlayClipAtPoint(SoundType type, Vector3 pos)
    {
        if (sound[(int)type] == null) Debug.Log("SOUND ERROR : NOT FOUND " + type);

        //if (Settings.SoundEffectsEnabled && sound[(int)type] != null)
            AudioSource.PlayClipAtPoint(sound[(int)type], pos, Settings.SoundEffectsVolume);
    }
}
