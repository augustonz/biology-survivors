using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class UpgradeScreen : MonoBehaviour
{

    public static UpgradeScreen instance;

    Animator _anim;
    bool _isVisible;

    private int _selected;
    private List<UpgradeOption> options = new();

    [SerializeField] private Player player;

    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescriptionText;



    [SerializeField] private Transform child;
    [SerializeField] private UpgradeList upgrade;

    void Awake()
    {

        if (instance != null && instance != this) Destroy(this);
        else instance = this;

        options = new()
        {
            Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>(),
            Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>(),
            Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>(),
            Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>()
        };
        upgrade.Init();
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        for (int i = 0; i < options.Count; i++)
        {
            options[i].SetId(i);
        }
    }

    void Update()
    {
    }

    void ToggleVisibility()
    {
        if (_isVisible) DisappearUpgradeScreen();
        else AppearUpgradeScreen();
    }

    public void AppearUpgradeScreen()
    {
        _anim.SetTrigger("appear");
        _isVisible = true;
        GameHandler.instance.ChangeState(GameState.MENU);

        ResetText();

        RerollChoices();
    }

    public void RerollChoices()
    {
        Upgrade[] pool = upgrade.GetFromPool();
        for (int i = 0; i < pool.Count(); i++)
        {
            options[i].SetUpgrade(pool[i]);
        }
        for (int i = pool.Count(); i < options.Count; i++)
        {
            options[i].gameObject.SetActive(false);
        }

        if (pool.Count() > _selected)
        {
            SetSelected(_selected);
        }
        else
        {
            SetSelected(0);
        }
    }

    public void ChooseUpgrade()
    {
        player.AddUpgrade(options[_selected].GetUpgrade());
        upgrade.ChooseUpgrade(options[_selected].GetUpgrade().Id);
        DisappearUpgradeScreen();
    }

    private void DisappearUpgradeScreen()
    {
        _anim.SetTrigger("disappear");
        _isVisible = false;
        GameHandler.instance.ChangeState(GameState.INGAME);
    }

    public void SetSelected(int id)
    {
        if (_selected != id)
        {
            options[_selected].OnUnfocus();
        }
        _selected = id;
        options[id].OnFocus();
        upgradeNameText.text = options[_selected].GetName();
        upgradeDescriptionText.text = options[_selected].GetDescription();
    }

    public void ResetText()
    {
        upgradeNameText.text = "";
        upgradeDescriptionText.text = "";
    }

}
