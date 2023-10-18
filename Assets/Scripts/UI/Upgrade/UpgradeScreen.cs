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

    private int _selectedUpgrade;
    private int _selectedSkillTree;

    private List<UpgradeOption> options;
    private List<SkillTreeOption> skillTreeOptions = new();

    [SerializeField] private Player player;

    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescriptionText;



    [SerializeField] private Transform skillTreeParent;
    [SerializeField] private Transform upgradeOptionsParent;
    [SerializeField] private UpgradeList upgrade;

    void Awake()
    {

        if (instance != null && instance != this) Destroy(this);
        else instance = this;

        options = new()
        {
            Instantiate(PrefabLoader.instance.getUpgradeOption(), upgradeOptionsParent).GetComponent<UpgradeOption>(),
            Instantiate(PrefabLoader.instance.getUpgradeOption(), upgradeOptionsParent).GetComponent<UpgradeOption>(),
            Instantiate(PrefabLoader.instance.getUpgradeOption(), upgradeOptionsParent).GetComponent<UpgradeOption>(),
            Instantiate(PrefabLoader.instance.getUpgradeOption(), upgradeOptionsParent).GetComponent<UpgradeOption>()
        };
        GetComponentsInChildren(skillTreeOptions);
        upgrade.Init();

        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        for (int i = 0; i < options.Count; i++)
        {
            options[i].SetId(i);
        }

        for (int i = 0; i < skillTreeOptions.Count; i++)
        {
            skillTreeOptions[i].SetId(i);
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

        if (pool.Count() > _selectedUpgrade)
        {
            SetSelected(_selectedUpgrade);
        }
        else if (pool.Count() > 0)
        {
            SetSelected(0);
        }
        SetSkillSelected(-1);
    }

    public void ChooseUpgrade()
    {
        player.AddUpgrade(options[_selectedUpgrade].GetUpgrade());
        upgrade.ChooseUpgrade(options[_selectedUpgrade].GetUpgrade().Id);
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
        if (_selectedUpgrade != id)
        {
            options[_selectedUpgrade].OnUnfocus();
            skillTreeOptions[_selectedSkillTree].OnUnfocus();
        }
        _selectedUpgrade = id;
        options[id].OnFocus();
        SetSkillTree(options[id].GetUpgrade().skillTree);
        SetText(options[id].GetName(), options[id].GetDescription());
    }

    public void SetSkillSelected(int id)
    {
        if (id == -1)
        {
            skillTreeOptions[_selectedSkillTree].OnUnfocus();
            _selectedSkillTree = id;
            return;
        }
        if (_selectedSkillTree == id)
        {
            skillTreeOptions[_selectedSkillTree].OnUnfocus();
            SetText(options[_selectedUpgrade].GetName(), options[_selectedUpgrade].GetDescription());
            _selectedSkillTree = -1;
            return;
        }
        if ((_selectedSkillTree != id || id > skillTreeOptions.Count) && _selectedSkillTree != -1)
        {
            skillTreeOptions[_selectedSkillTree].OnUnfocus();
        }
        _selectedSkillTree = id;
        skillTreeOptions[_selectedSkillTree].OnFocus();
        SetText(skillTreeOptions[id].GetName(), skillTreeOptions[id].GetDescription());
    }

    private void SetSkillTree(SkillTree skillTree)
    {
        List<Upgrade> temp = skillTree.GetSkillTree();
        for (int i = 0; i < skillTreeOptions.Count; i++)
        {
            if (i > temp.Count - 1)
            {
                skillTreeOptions[i].Reset();
            }
            else
            {
                skillTreeOptions[i].SetUpgrade(temp[i]);
            }
        };
    }

    public void ResetText()
    {
        upgradeNameText.text = "";
        upgradeDescriptionText.text = "";
    }

    public void SetText(string name, string description)
    {
        upgradeNameText.text = name;
        upgradeDescriptionText.text = description;
    }

}
