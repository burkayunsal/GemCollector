﻿using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    #region LEVEL

    const string KEY_LEVEL = "levels";

    public static void IncreaseLevel() => PlayerPrefs.SetInt(KEY_LEVEL, GetLevel() + 1);
    public static int GetLevel() => PlayerPrefs.GetInt(KEY_LEVEL, 0);

    #endregion

    #region COIN

    const string KEY_COIN = "coins";

    public static void AddCoin(int add)
    {
        PlayerPrefs.SetInt(KEY_COIN, GetCoin() + add);
        UIManager.I.UpdateCoinTxt();
    }

    public static int GetCoin() => PlayerPrefs.GetInt(KEY_COIN, 0);

    #endregion

    #region GEM

    const string KEY_GEM = "gemCount";

    public static void AddGem(GemType gt)
    {
        PlayerPrefs.SetInt(KEY_GEM + gt, GetGem(gt) + 1);
        UIManager.I.UpdateTxt();
    }

    public static int GetGem(GemType gt) => PlayerPrefs.GetInt(KEY_GEM +  gt, 0);

    #endregion
    
    
  
}
