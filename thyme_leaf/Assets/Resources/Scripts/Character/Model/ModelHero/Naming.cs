using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Naming : Singleton<Naming> {

    Dictionary<string, int> nameDictionary;

    public const string AGT = "APT";
    public const string AST = "AST";
    public const string APT = "APT";
    public const string ASPT = "ASPT";
    public const string ATT = "ATT";

    public const string TYPE = "TYPE";

    public const string FALSTAFF = "FALSTAFF";
    public const string FRANSIS = "FRANSIS";
    public const string FORTINBRAS = "FORTINBRAS";
    public const string VICTOR = "VICTOR";
    public const string MARIEN = "MARIEN";

    public const string ATTACKING = "ATTACKING";
    public const string CHASING = "CHASING";
    public const string DYING = "DYING";
    public const string HITTING = "HITTING";
    public const string IDLING = "IDLING";
    public const string MOVING = "MOVING";

    public List<string> towerNames;
    public List<string> automatNames;
    public List<string> stateNames;
    public List<List<string>> names;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        towerNames = new List<string>(); automatNames = new List<string>();
        stateNames = new List<string>(); names = new List<List<string>>();

        towerNames.Add(AGT); towerNames.Add(AST); towerNames.Add(APT); 
        towerNames.Add(ASPT); towerNames.Add(ATT);
        automatNames.Add(FALSTAFF); automatNames.Add(FRANSIS); automatNames.Add(FORTINBRAS);
        automatNames.Add(VICTOR); automatNames.Add(MARIEN);
        stateNames.Add(ATTACKING); stateNames.Add(CHASING); stateNames.Add(DYING);
        stateNames.Add(HITTING); stateNames.Add(IDLING); stateNames.Add(MOVING);
        names.Add(towerNames); names.Add(automatNames);

        for (int i = 0; i < names.Count; i++) for (int j = 0; j < names[i].Count; j++) 
            for (int k = 1; k <= 5; k++) for (int l = 1; l <= 5; l++)
                nameDictionary.Add(BuildName(names[i][j], k, stateNames[l]), 
                    BuildName(names[i][j], k, stateNames[l]).GetHashCode());
    }

    public string BuildName(string name, int typeNum, string state)
    {
        return name+"_"+TYPE+(typeNum+"")+"_"+state;
    }

}
