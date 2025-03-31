using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Player : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
    private GameObject hpIconPrefab; // アイコンのプレハブ
    private List<Color> rainbowColors = new List<Color>
    {
        Color.red ,new Color(1f, 0.5f, 0f),Color.yellow,Color.green,new Color(0.0f,1.0f,1.0f),Color.blue, new Color(0.56f, 0f, 1f)     // 紫
    };
    private int startHp; // 初期HP
    private List<GameObject> hpIcons = new List<GameObject>(); // HPアイコンのリスト
    public PlayerBase _player; // プレイヤーの移動スクリプト
    [SerializeField,Header("ダメージ無敵時間")]
    private float _interval=1.6f;
    [SerializeField,Header("ガード無敵時間")]
    private float _intervalGuard=0.8f;
    private PGuard _guard;
    private KnockBackC _kB;
    // Startメソッドは最初のフレームの前に1回呼び出されます
    void Awake()
    {

        _player = FindObjectOfType<PlayerBase>();
        _guard=FindObjectOfType<PGuard>();
        _kB=FindObjectOfType<KnockBackC>();
        startHp = _player._hp;
        CreateInitialHPIcons(startHp);
    }
    private void Update() {
        UpdateHPIcons(_player._hp);
    }

    // 初期HPのアイコンを生成するメソッド
    void CreateInitialHPIcons(int _hp)
    {
        for (int i = 0; i < _hp; i++)
        {
            GameObject hpObj = Instantiate(hpIconPrefab, transform);
            

            int colorIndex = i % rainbowColors.Count;
            hpObj.GetComponent<Image>().color = rainbowColors[colorIndex];
            hpIcons.Add(hpObj);
        }
    }

    
    public void DamageHP(int amount,GameObject enemy,GameObject player,bool isAttackDirect)
    {
        if(_player!=null){
        int damage=amount;
        if(_guard.IsDamageEase){
            GuardHP(amount,enemy,player,isAttackDirect);
            return;
        }else if(_player.IsBarrierExpand){
            BarrierHP(amount,enemy,player,isAttackDirect);
            return;
        }
        _player.HPChange(damage);

        if(_player._hp>0){
            _player.KnockBack(enemy);
            StartCoroutine(_kB.HitBlink(player,8));
            StartCoroutine(_kB.InvicibleTime(player));
        }else{
            _player.GameOver();
        }
        }
    }
    public void HealHP(int amount)
    {
       _player.HPChange(-amount);
    }
    // HPアイコンを更新するメソッド
    private void UpdateHPIcons(int currentHp)
    {
        for (int i = 0; i < hpIcons.Count; i++)
        {
            hpIcons[i].SetActive(i < currentHp);
        }
    }
    private void GuardHP(int damage,GameObject enemy,GameObject player,bool isAttackDirect){
        int damage= damage/3;
        _player.HPChange(damage);
        AfterDamageProcess(enemy,player,isAttackDirect);
        
    }
    private void BarrierHP(int damage,GameObject enemy,GameObject player,bool isAttackDirect){
        damage= damage*0;
        _player.HPChange(damage);
        AfterDamageProcess(enemy,player,isAttackDirect);
    }
    private void AfterDamageProcess(GameObject enemy,GameObject player,bool isAttackDirect){ //ガード、バリアのダメージ
        EnemyBase _enemyBase=enemy.GetComponent<EnemyBase>();
        
        if(_player._hp>0){
            _player.KnockBackGuard(enemy);
            if(isAttackDirect) _enemyBase.KnockBackGuardToEnemy(player);
            StartCoroutine(_kB.InvicibleTime(player));
        }else{
            _player.GameOver();
        }
    }
}
