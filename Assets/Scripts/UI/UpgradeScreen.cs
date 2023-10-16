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

        options.Add(Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>());
        options.Add(Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>());
        options.Add(Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>());
        options.Add(Instantiate(PrefabLoader.instance.getUpgradeOption(), child).GetComponent<UpgradeOption>());

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

        foreach (var op in options)
        {
            op.SetUpgrade(upgrade.upgrades[Random.Range(0, 4)]);
        }
        SetSelected(0);
    }

    public void ChooseUpgrade()
    {
        player.AddUpgrade(options[_selected].GetUpgrade());
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
        _selected = id;
        upgradeNameText.text = options[_selected].GetName();
        upgradeDescriptionText.text = options[_selected].GetDescription();
    }

}
