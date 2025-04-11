using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveSystem
{
    void Save();
    void Load();
    SaveState SaveState {get; set;}
    void ResetData();
    void SetData();
}
