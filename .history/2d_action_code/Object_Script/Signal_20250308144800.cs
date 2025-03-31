using UnityEngine;
using System.Collections.Generic; // List を使うために必要

public class Signal : MonoBehaviour
{
    public enum SignalState { Red, Blue }
    public SignalState _currentSignal;
    
    [SerializeField] private Sprite _redSignalSprite; // 赤信号のスプライト
    [SerializeField] private Sprite _blueSignalSprite; // 青信号のスプライト
    private SpriteRenderer _sr;

    [SerializeField] private List<SignalRoad> roads; // 管理する道路のリスト

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        // **初期状態をランダムに設定**
        roads=GetComponentsInChildren<SignalRoad>();
        _currentSignal = (Random.value > 0.5f) ? SignalState.Red : SignalState.Blue;
        UpdateSignalColor();
    }

    // **信号を攻撃すると色が切り替わる**
    public void ChangeSignal()
    {
        _currentSignal = (_currentSignal == SignalState.Red) ? SignalState.Blue : SignalState.Red;
        UpdateSignalColor();

        // **関連する道路の状態を更新**
        foreach (SignalRoad road in roads)
        {
            road.UpdateRoadState(_currentSignal); // 修正
        }
    }

    private void UpdateSignalColor()
    {
        if (_currentSignal == SignalState.Red) // 修正
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
        if (other.CompareTag("Umbrella"))
        {
            ChangeSignal();
        }
    }
}
