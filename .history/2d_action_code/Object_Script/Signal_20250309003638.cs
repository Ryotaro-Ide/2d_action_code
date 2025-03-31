using UnityEngine;
using System.Collections.Generic; // List を使うために必要

public class Signal : MonoBehaviour
{
    public enum SignalState { Red, Blue }
    public SignalState _currentSignal;
    [SerializeField] private SpriteRenderer[] _signals; //左と右の信号
    [SerializeField] private Sprite _redSignalSprite; // 赤信号のスプライト
    [SerializeField] private Sprite _blueSignalSprite; // 青信号のスプライト
    
    [SerializeField] private SignalRoad[] roads; // 管理する道路のリスト

    private void Awake()
    {
        
        // **初期状態をランダムに設定**
        roads=gameObject.GetComponentsInChildren<SignalRoad>();
        _currentSignal = (Random.value > 0.5f) ? SignalState.Red : SignalState.Blue;
        UpdateSignalColor();
        foreach (SignalRoad road in roads)
        {
            road.UpdateRoadState(_currentSignal); 
        }
    }

    // **信号を攻撃すると色が切り替わる**
    public void ChangeSignal()
    {
        _currentSignal = (_currentSignal == SignalState.Red) ? SignalState.Blue : SignalState.Red;
        UpdateSignalColor();

        // **関連する道路の状態を更新**
        foreach (SignalRoad road in roads)
        {
            road.UpdateRoadState(_currentSignal); 
        }
    }

    private void UpdateSignalColor()
    {
        if (_currentSignal == SignalState.Red) 
        {
            foreach(SpriteRenderer _sr in _signals){
                _sr.sprite=_redSignalSprite;
            }
        }
        else
        {
            foreach(SpriteRenderer _sr in _signals){
                _sr.sprite=_blueSignalSprite;
            }
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
