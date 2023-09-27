using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class TesterSkillSystem : MonoBehaviour
{
    private List<ISkill> _skills;
    private BoxCollider _hitBoxCollider;
    private TesterMoveBehavior _moveBehavior;
    private TesterLiveBehavior _liveBehavior;
    private CompositeDisposable _disposables;
    private void Awake()
    {
        _skills = new List<ISkill>();
        _disposables = new CompositeDisposable();
        _hitBoxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        InitSkill();
    }
    private void InitSkill()
    {
        //Resource resource = GameManager.Instance.Resource;
        GameSceneUI gameSceneUI = Gameplay.Instance.GameSceneUI;

        AddMoveBehavior(int.Parse(gameSceneUI.DataTesterPanel.RunInputField.text));
        AddLiveBehavior(int.Parse(gameSceneUI.DataTesterPanel.HpInputField.text));

        InitEvent();
    }
    private void AddMoveBehavior(int runData)
    {
        _moveBehavior = gameObject.GetOrAddComponent<TesterMoveBehavior>();
        _moveBehavior.InitData(runData);
        _skills.Add(_moveBehavior);
    }
    private void AddLiveBehavior(int hpData)
    {
        _liveBehavior = gameObject.GetOrAddComponent<TesterLiveBehavior>();
        _liveBehavior.InitData(hpData);
        _skills.Add(_liveBehavior);
    }
    private void InitEvent()
    {
        _liveBehavior.IsDead.Where(IsDead => IsDead)
            .Subscribe(IsDead =>
        {
            Gameplay.Instance.Fire(GamePlayTrigger.Lose);
            RemoveAllSkill();
        }).AddTo(_disposables);
    }

    private void RemoveAllSkill()
    {
        Destroy(_hitBoxCollider);
        foreach (ISkill skill in _skills)
            Destroy((MonoBehaviour)skill);

        _skills.Clear();
    }

    private void OnDestroy()
    {
        _disposables.Clear();
    }
}
