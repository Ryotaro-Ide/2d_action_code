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
    private PlayerBase _player; // プレイヤーの移動スクリプト
    [SerializeField,Header("ダメージ無敵時間")]
    private float _interval=1.6f;
    [SerializeField,Header("ガード無敵時間")]
    private float _intervalGuard=0.8f;
    private PGuard _guard;
    private KnockBackC _kB;
    // Startメソッドは最初のフレームの前に1回呼び出されます
    void Start()
    {

        _player = FindObjectOfType<PlayerBase>();
        GameObject obj=GameObject.Find("Player");
        _guard=obj.GetComponent<PGuard>();
        _kB=FindObjectOfType<KnockBackC>();
        startHp = _player._hp;
        CreateInitialHPIcons(startHp);
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
        int damage=amount;
        if(_guard.IsDamageEase){
            GuardHP(amount,enemy,player,isAttackDirect);
            return;
        }else if(_player.IsBarrierExpand){
            BarrierHP(amount,enemy,player,isAttackDirect);
            return;
        }
        _player._hp -= damage;
        UpdateHPIcons(_player._hp);
        if(_player._hp>0){
            _player.KnockBack(enemy);
            StartCoroutine(_kB.HitBlink(player,8));
            StartCoroutine(_kB.InvicibleTime(player));
        }else{
            _player.GameOver();
        }
        
    }
    public void HealHP(int amount)
    {
        _player._hp += amount;
        UpdateHPIcons(_player._hp);
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
        _player._hp -= damage/3;
        AfterDamageProcess(enemy,player,isAttackDirect);
        
    }
    private void BarrierHP(int damage,GameObject enemy,GameObject player,bool isAttackDirect){
        _player._hp -= damage*0;
        AfterDamageProcess(enemy,player,isAttackDirect);
    }
    private void AfterDamageProcess(GameObject enemy,GameObject player,bool isAttackDirect){ //ガード、バリアのダメージ
        EnemyBase _enemyBase=enemy.GetComponent<EnemyBase>();
        UpdateHPIcons(_player._hp);
        if(_player._hp>0){
            _player.KnockBackGuard(enemy);
            if(isAttackDirect) _enemyBase.KnockBackGuardToEnemy(player);
            StartCoroutine(_kB.InvicibleTime(player));
        }else{
            _player.GameOver();
        }
    }
}
