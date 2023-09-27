using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class CompSkillSystem : MonoBehaviour
{
    private List<ISkill> _skills;
    private CompMoveBehavior _moveBehavior;
    private CompLiveBehavior _liveBehavior;
    private CompAttackBehavior _attackBehavior;
    private CompositeDisposable _disposables;
    private void Awake()
    {
        _skills = new List<ISkill>();
        _disposables = new CompositeDisposable();
    }
    private void Start() => InitSkill();
    private void InitSkill()
    {
        Resource resource = GameManager.Instance.Resource;
        CompSetting dataComp = resource.CompSetting;
        GameSceneUI gameSceneUI = Gameplay.Instance.GameSceneUI;

        Transform target = Gameplay.Instance.Tester.transform;
        int runDataInputField = int.Parse(gameSceneUI.DataCompPanel.RunInputField.text);
        int hpDataInputField = int.Parse(gameSceneUI.DataCompPanel.HpInputField.text);

        AddMoveBehavior(target, runDataInputField);
        AddLiveBehavior(hpDataInputField);
        AddAttackBehavior(dataComp.AttackBehaviorSetting.Damage,
            dataComp.AttackBehaviorSetting.CountDownTimeAttack,
            dataComp.AttackBehaviorSetting.AttackEffect);
    }
    private void AddMoveBehavior(Transform target, int runData)
    {
        _moveBehavior = gameObject.GetOrAddComponent<CompMoveBehavior>();
        _moveBehavior.InitData(target, runData);
        _skills.Add(_moveBehavior);
    }
    private void AddLiveBehavior(int hpData)
    {
        _liveBehavior = gameObject.GetOrAddComponent<CompLiveBehavior>();
        _liveBehavior.InitData(hpData);
        _skills.Add(_liveBehavior);
    }
    private void AddAttackBehavior(int damageData, float countDownTimeAttack, GameObject attackEffect)
    {
        _attackBehavior = gameObject.GetOrAddComponent<CompAttackBehavior>();
        _attackBehavior.InitData(damageData,
            countDownTimeAttack,
            attackEffect);
        _skills.Add(_attackBehavior);
    }
    private void RemoveAllSkill()
    {
        foreach (ISkill skill in _skills)
            Destroy((MonoBehaviour)skill);

        _skills.Clear();
    }

    private void OnDestroy()
    {
        _disposables.Clear();
    }
}
