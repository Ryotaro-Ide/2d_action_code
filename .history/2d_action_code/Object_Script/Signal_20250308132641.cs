using UnityEngine;

public class Signal : MonoBehaviour
{
    public enum SignalState { Red, Blue }
    public SignalState _currentSignal;
    [SerializeField] private Sprite _redSignalSprite; // 赤信号のスプライト
    [SerializeField] private Sprite _blueSignalSprite; // 青信号のスプライト
    private SpriteRenderer _sr;

    public SignalRoad _road; // 関連する道路オブジェクト（Inspector で設定）

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
        _road.UpdateRoadState(_currentSignal); // 道路の状態を更新
    }

    private void UpdateSignalColor()
    {
        if (_currentSignal == SignalState.Red)
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
