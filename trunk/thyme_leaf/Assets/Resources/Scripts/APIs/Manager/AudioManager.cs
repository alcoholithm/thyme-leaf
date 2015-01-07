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
        base.Awake();
        //DontDestroyOnLoad(gameObject.transform.parent.gameObject);

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

    void OnDestroy()
    {
        Debug.Log("DESTROYED AUDIO MANAGER");
    }

    void Start()
    {
        Settings.Awake();
        UpdateValues();
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
                //Debug.Log("Sound Not Found for World Map");
                audioPlayer.clip = music[(int)MusicType.LOBBY];
                break;
            case 2: // battle
                audioPlayer.clip = music[(int)MusicType.BATTLE_1];
                break;
            default:
                return;
        }
        StartAudio();
    }

    public void UpdateValues()
    {
        audioPlayer.volume = Settings.CurrentSettingData.MusicVolume;
        audioPlayer.mute = !Settings.CurrentSettingData.MusicEnabled;
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

    private SoundType BuildSoundType(AudioUnitType unitType, StateType stateType)
    {
        SoundType resultSoundType;
        
        // Automats
            // Falstaff
                // Type1
        if(unitType == AudioUnitType.FALSTAFF_TYPE1
            && stateType == StateType.ATTACKING)
        {
            resultSoundType = SoundType.AUTOMAT_FALSTAFF_TYPE1_ATTACKING;
        }
        else if (unitType == AudioUnitType.FALSTAFF_TYPE1
            && stateType == StateType.DYING)
        {
            resultSoundType = SoundType.AUTOMAT_FALSTAFF_TYPE1_DYING;
        }
            // Franscis
                // Type1
        else if (unitType == AudioUnitType.FRANSCIS_TYPE1
            && stateType == StateType.ATTACKING)
        {
            resultSoundType = SoundType.AUTOMAT_FRANSIS_TYPE1_ATTACKING;
        }
        else if (unitType == AudioUnitType.FRANSCIS_TYPE1
            && stateType == StateType.DYING)
        {
            resultSoundType = SoundType.AUTOMAT_FRANSIS_TYPE1_DYING;
        }
        // Towers
            // APT
                // Type1
        else if (unitType == AudioUnitType.APT_TYPE1
            && stateType == StateType.ATTACKING)
        {
            resultSoundType = SoundType.AUTOMAT_APT_TYPE1_ATTACKING;
        }
        else if (unitType == AudioUnitType.PROJECTILE_POISON
            && stateType == StateType.ATTACKING)
        {
            resultSoundType = SoundType.AUTOMAT_APT_TYPE1_ATTACKING;
        }

        else if (unitType == AudioUnitType.ASPT_TYPE1
            && stateType == StateType.ATTACKING)
        {
            resultSoundType = SoundType.AUTOMAT_FRANSIS_TYPE1_ATTACKING;
        }

        // Trovants
            // Comma
        else if (unitType == AudioUnitType.COMMA
        && stateType == StateType.ATTACKING)
        {
            resultSoundType = SoundType.TROVANT_COMMA_ATTACKING;
        }
        else if (unitType == AudioUnitType.COMMA
            && stateType == StateType.DYING)
        {
            resultSoundType = SoundType.TROVANT_COMMA_DYING;
        }
            // Python
        else if (unitType == AudioUnitType.PYTHON
            && stateType == StateType.ATTACKING)
        {
            resultSoundType = SoundType.TROVANT_COMMA_ATTACKING;
        }
        else if (unitType == AudioUnitType.PYTHON
            && stateType == StateType.DYING)
        {
            resultSoundType = SoundType.TROVANT_COMMA_DYING;
        }
        // WChat
            // Type1
        //else if (unitType == AudioUnitType.WCHAT_TYPE1
        //&& stateType == StateType.ATTACKING)
        //{
        //}
        //else if (unitType == AudioUnitType.WCHAT_TYPE1
        //    && stateType == StateType.DYING)
        //{
        //}

        else
        {
            resultSoundType = SoundType.NOTHING;
            Debug.Log("NOT FOUND SOUND TYPE : " + unitType + " , " + stateType);
        }

        return resultSoundType;
    }

    public void PlayClipWithState(GameObject go, StateType stateType)
    {
        AudioUnitType aut = go.GetComponent<AudioType>().audioUnitType;
        SoundType soundType  = BuildSoundType(aut, stateType);
        Debug.Log("SOUND NAME : " + soundType);
        PlayClipAtPoint(soundType, go.gameObject.transform.position);
    }


    private void PlayClipAtPoint(SoundType type, Vector3 pos)
    {
        if (sound[(int)type] == null) Debug.Log("SOUND ERROR : NOT FOUND " + type);
        if (Settings.CurrentSettingData.SoundEffectsEnabled && sound[(int)type] != null)
            AudioSource.PlayClipAtPoint(sound[(int)type], pos, Settings.CurrentSettingData.SoundEffectsVolume);
    }
}
