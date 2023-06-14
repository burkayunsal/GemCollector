using UnityEngine;

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

    #region GREEN_GEM

    const string KEY_GREEN_GEM = "grenGemCount";

    public static void AddGreenGem()
    {
        PlayerPrefs.SetInt(KEY_GREEN_GEM, GetGreenGem() + 1);
        UIManager.I.UpdateGreenTxt();
    }

    public static int GetGreenGem() => PlayerPrefs.GetInt(KEY_GREEN_GEM, 0);

    #endregion
    
    
    #region PINK_GEM

    const string KEY_PINK_GEM = "pinkGemCount";

    public static void AddPinkGem()
    {
        PlayerPrefs.SetInt(KEY_PINK_GEM, GetPinkGem() + 1);
        UIManager.I.UpdatePinkTxt();
    }

    public static int GetPinkGem() => PlayerPrefs.GetInt(KEY_PINK_GEM, 0);

    #endregion
    
    
    #region YELLOW_GEM

    const string KEY_YELLOW_GEM = "YELLOWGemCount";

    public static void AddYellowGem()
    {
        PlayerPrefs.SetInt(KEY_YELLOW_GEM, GetYellowGem() + 1);
        UIManager.I.UpdateYellowTxt();
    }

    public static int GetYellowGem() => PlayerPrefs.GetInt(KEY_YELLOW_GEM, 0);

    #endregion
}
