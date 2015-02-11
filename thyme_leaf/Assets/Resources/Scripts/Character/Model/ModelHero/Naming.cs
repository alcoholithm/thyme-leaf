using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Naming : Manager<Naming> //MonoBehaviour
{
    public static int maxId;

    private Dictionary<string, int> nameDictionary;

    public const string AGT = "AGT";
	public const string APT = "APT";
	public const string ASPT = "ASPT";
	public const string AST = "AST";
    public const string ATT = "ATT";

    public const string TYPE = "Type";
    public const int TYPE_NUM = 5;

    public const string FALSTAFF = "Falstaff";
    public const string FRANSCIS = "Franscis";
    public const string FORTINBRAS = "Fortinbras";
    public const string VICTOR = "Victor";
    public const string MARIEN = "Marien";

    public const string COMMA = "Comma";
    public const string PYTHON = "Python";
	public const string HADOOP = "Hadoop";

    public const string ATTACKING = "Attacking";
    public const string CHASING = "Chasing";
    public const string DYING = "Dying";
    public const string HITTING = "Hitting";
    public const string IDLING = "Idling";
    public const string MOVING = "Moving";
    public const string BUILDING = "Building";
    public const string SELLING = "Selling";

    public List<string> towerNames;
    public List<string> automatNames;
    public List<string> trovantNames;
    public List<string> stateNames;
    public List<List<string>> names;

    //private static object syncRoot = new System.Object();
    private static Naming instance;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public string BuildAnimationName(GameObject go, string state)
    {
        AudioUnitType type = go.GetComponent<AudioType>().audioUnitType;
        string result = null;
        switch(type)
        {
            case AudioUnitType.FALSTAFF_TYPE1:
                result = BuildAutomatNameWithState(Naming.FALSTAFF, 1, state);
                break;
            case AudioUnitType.FOLTINBRAS_TYPE1:
                result = BuildAutomatNameWithState(Naming.FORTINBRAS, 1, state);
                break;
            case AudioUnitType.FRANSCIS_TYPE1:
                result = BuildAutomatNameWithState(Naming.FRANSCIS, 1, state);
                break;
            case AudioUnitType.VICTOR_TYPE1:
                result = BuildAutomatNameWithState(Naming.VICTOR, 1, state);
                break;
            case AudioUnitType.MARIEN_TYPE1:
                result = BuildAutomatNameWithState(Naming.MARIEN, 1, state);
                break;

            case AudioUnitType.AGT_TYPE1:
                result = BuildAutomatNameWithState(Naming.AGT, 1, state);
                break;
            case AudioUnitType.APT_TYPE1:
                result = BuildAutomatNameWithState(Naming.APT, 1, state);
                break;
			case AudioUnitType.ASPT_TYPE1:
				result = BuildAutomatNameWithState(Naming.ASPT, 1, state);
				break;
			case AudioUnitType.AST_TYPE1:
				result = BuildAutomatNameWithState(Naming.AST, 1, state);
				break;
			case AudioUnitType.ATT_TYPE1:
				result = BuildAutomatNameWithState(Naming.ATT, 1, state);
				break;

            case AudioUnitType.COMMA:
                result = BuildTrovantNameWithState(Naming.COMMA, state);
                break;
            case AudioUnitType.PYTHON:
                result = BuildTrovantNameWithState(Naming.PYTHON, state);
                break;
			case AudioUnitType.HADOOP:
				result = BuildTrovantNameWithState(Naming.HADOOP, state);
				break;
            default:
                Debug.Log("Not Registered Animation Name");
                break;
        }
        return result;
    }

    private string BuildAutomatName(string name, int typeNum)
    {
        return name + "_" + TYPE + (typeNum + "");
    }

    private string BuildTrovantName(string name)
    {
        return name;
    }

    private string BuildAutomatNameWithState(string name, int typeNum, string state)
    {
        return BuildAutomatName(name,typeNum) + "_" + state;
    }

    private string BuildTrovantNameWithState(string name, string state)
    {
        return name + "_" + state;
    }

    private void Init()
    {
        maxId = 1;
        nameDictionary = new Dictionary<string, int>();
        towerNames = new List<string>(); 
        automatNames = new List<string>();
        trovantNames = new List<string>();
        stateNames = new List<string>(); 
        names = new List<List<string>>();

        towerNames.Add(AGT); 
		towerNames.Add(APT);
		towerNames.Add(ASPT); 
		towerNames.Add(AST); 
        towerNames.Add(ATT);

        automatNames.Add(FALSTAFF); 
        automatNames.Add(FRANSCIS); 
        automatNames.Add(FORTINBRAS);
        automatNames.Add(VICTOR); 
        automatNames.Add(MARIEN);

        trovantNames.Add(COMMA);
        trovantNames.Add(PYTHON);

        stateNames.Add(ATTACKING); 
        stateNames.Add(CHASING); 
        stateNames.Add(DYING);
        stateNames.Add(HITTING); 
        stateNames.Add(IDLING); 
        stateNames.Add(MOVING);

        names.Add(towerNames); 
        names.Add(automatNames);
        names.Add(trovantNames);


        for (int i = 0; i < names.Count; i++)
        {
            for (int j = 0; j < names[i].Count; j++)
            {
                for (int k = 1; k <= TYPE_NUM; k++)
                {
                    for (int l = 0; l < stateNames.Count; l++)
                    {
                        if (nameDictionary.ContainsKey(BuildTrovantNameWithState(names[i][j], stateNames[l]))
                            || nameDictionary.ContainsKey(BuildAutomatNameWithState(names[i][j], k, stateNames[l])))
                            continue;

                        if (trovantNames.Contains(BuildTrovantNameWithState(names[i][j],stateNames[l]))) {
                            nameDictionary.Add(BuildTrovantNameWithState(names[i][j], stateNames[l]),
                                BuildTrovantNameWithState(names[i][j], stateNames[l]).GetHashCode());
                        }
                        else
                        {
                            
                            nameDictionary.Add(BuildAutomatNameWithState(names[i][j], k, stateNames[l]),
                                BuildAutomatNameWithState(names[i][j], k, stateNames[l]).GetHashCode());
                        }
                    }
                }
            }
        }
    }

    
}
