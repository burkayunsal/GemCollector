using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] Panels pnl;
    [Header("Images")]
    [SerializeField] Images img;
    [Header("Buttons")]
    [SerializeField] public Buttons btn;
    [Header("Texts")]
    [SerializeField] Texts txt;

    private CanvasGroup activePanel = null;

    public Panels GetPanel() => pnl;
    public Buttons GetButtons() => btn;

    public void Initialize()
    {
        btn.play.gameObject.SetActive(true);
        FadeInAndOutPanels(pnl.mainMenu);
    }

    public void StartGame()
    {
        GameManager.OnStartGame();
    }

    public void OnGameStarted()
    {
        FadeInAndOutPanels(pnl.gameIn);
        UpdateCoinTxt();
        UpdateGreenTxt();
        UpdatePinkTxt();
        UpdateYellowTxt();
    }
    
    public void OpenPopUp()
    {
        FadeInAndOutPanels(pnl.popUp);
    }

    public void ClosePopUp()
    {
        FadeInAndOutPanels(pnl.gameIn);

    }

    private Tween activeOutTween = null, activeInTween = null;

    void FadeInAndOutPanels(CanvasGroup _in)
    {
        CanvasGroup _out = activePanel;
        activePanel = _in;

        if(_out != null)
        {
            if(activeOutTween != null)
                activeOutTween.Kill(false);
            _out.interactable = false;
            _out.blocksRaycasts = false;

            activeOutTween = _out.DOFade(0f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                activeOutTween =_in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
                {
                    _in.interactable = true;
                    _in.blocksRaycasts = true;
                    activeOutTween = null;
                });
            });
        }
        else
        {
            if(activeInTween != null)
                activeInTween.Kill(false);
            
            activeInTween = _in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                activeInTween = null;
                _in.interactable = true;
                _in.blocksRaycasts = true;
            });
        }
    }

    public void ShowJoystickHighlights(int area)
    {
        for (int i = 0; i < img.joystickHighlights.Length; i++)
        {
            img.joystickHighlights[i].gameObject.SetActive(i == area);
        }
    }
    
    
    public void UpdateCoinTxt()
    {
        txt.coinCount.text = SaveLoadManager.GetCoin().ToString("0000");
    }
    
    public void UpdateGreenTxt()
    {
        txt.greenGemCount.text = "Count : " + SaveLoadManager.GetGreenGem().ToString("00");
    }
    
    public void UpdatePinkTxt()
    {
        txt.pinkGemCount.text = "Count : " + SaveLoadManager.GetPinkGem().ToString("00");
    }
    public void UpdateYellowTxt()
    {
        txt.yellowGemCount.text ="Count : " + SaveLoadManager.GetYellowGem().ToString("00");
    }
    
    
    [System.Serializable]
    public class Panels
    {
        public CanvasGroup mainMenu, gameIn,popUp;
    }

    [System.Serializable]
    public class Images
    {
        public Image[] joystickHighlights;
    }

    [System.Serializable]
    public class Buttons
    {
        public Button play;
    }

    [System.Serializable]
    public class Texts
    {
        public TextMeshProUGUI coinCount,greenGemCount,pinkGemCount,yellowGemCount;
    }
}
