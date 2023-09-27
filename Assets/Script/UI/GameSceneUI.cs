using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class GameSceneUI : MonoBehaviour
{
    private const string QUANTITY_COMP_DEFAULT = "1";

    public TMP_InputField InputField => _quantityCompInputField;
    public DataTesterPanel DataTesterPanel => _dataTesterPanel;
    public DataCompPanel DataCompPanel => _compTesterPanel;

    private CompositeDisposable _disposable;
    [SerializeField, TabGroup("Component")] private TMP_Text _quantityText;
    [SerializeField, TabGroup("Component")] private TMP_Text _testerHealthPointText;
    [SerializeField, TabGroup("Component")] private TMP_InputField _quantityCompInputField;
    [SerializeField, TabGroup("Component")] private Button _playButton;
    [SerializeField, TabGroup("Component")] private Button _retryButton;
    [SerializeField, TabGroup("Script")] private DataTesterPanel _dataTesterPanel;
    [SerializeField, TabGroup("Script")] private DataCompPanel _compTesterPanel;
    private void Awake()
    {
        _disposable = new CompositeDisposable();
    }
    private void Start()
    {
        OnStart();
        Subcribe();
        SetEvent();
    }
    private void OnStart()
    {
        _quantityCompInputField.text = QUANTITY_COMP_DEFAULT;
    }
    public void UpdateHealthPointTester(int hp)
    {
        _testerHealthPointText.SetText("Tester Hp: "+hp);
    }
    private void Subcribe()
    {
        Gameplay.Instance.CurrentState
            .Subscribe(state =>
            {
                bool isActive = state == GamePlayState.Init;
                _quantityCompInputField.gameObject.SetActive(isActive);
                _quantityText.gameObject.SetActive(isActive);
                _playButton.gameObject.SetActive(isActive);
                _dataTesterPanel.gameObject.SetActive(isActive);
                _compTesterPanel.gameObject .SetActive(isActive);
            }).AddTo(_disposable);

        Gameplay.Instance.CurrentState.Where(state => state == GamePlayState.Running)
            .Subscribe(state =>
            {
                _retryButton.gameObject.SetActive(true);
                _testerHealthPointText.gameObject.SetActive(true);
            }).AddTo(_disposable);
    }
    private void SetEvent()
    {
        _playButton.onClick.AddListener(() => Gameplay.Instance.Fire(GamePlayTrigger.Play));
        _retryButton.onClick.AddListener(() => 
        {
            Gameplay.Instance.Fire(GamePlayTrigger.Retry);
            _retryButton.gameObject.SetActive(false);
            _testerHealthPointText.gameObject.SetActive(false);
        });
    }
}
