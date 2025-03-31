using UnityEngine;

public class Signal : MonoBehaviour
{
    public enum SignalState { Red, Blue }
    public SignalState _currentSignal;
    [SerializeField] private Sprite _redSignalSprite; // 赤信号のスプライト
    [SerializeField] private Sprite _blueSignalSprite; // 青信号のスプライト
    private SpriteRenderer _sr;

    [SerializeField] private List<Road> roads; // 管理する道路のリスト

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        // **初期状態をランダムに設定**
        _currentSignal = (Random.value > 0.5f) ? SignalState.Red : SignalState.Blue;
        UpdateSignalColor();
    }

    // **信号を攻撃すると色が切り替わる**
    public void ChangeSignal()
    {
        _currentSignal = (_currentSignal == SignalState.Red) ? SignalState.Blue : SignalState.Red;
        UpdateSignalColor();
       // **関連する道路の状態を更新**
        foreach (Road road in roads)
        {
            road.UpdateRoadState(currentSignal);
        }
    }

    private void UpdateSignalColor()
    {
        if (_aBcurrentSignal == SignalState.Red)
        {
            _sr.sprite = _redSignalSprite;
        }
        else
        {
            _sr.sprite = _blueSignalSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            ChangeSignal();
        }
    }
}
