using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{   
    
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;
    private float _firstSpeed;
    [SerializeField,Header("ダッシュ加速度")]
    private float _dashAcceleration=1f;
    private float _time=0f;
    private float _TMPspeed=1f;
    [SerializeField, Header("ジャンプ速度")]
    private float _jumpSpeed;
    public int _hp;
    [HideInInspector]public int _maxHP;
    public int _energy;
    public int _maxEnergy;
    private Rigidbody2D _rb;
    protected bool _bJump;
    private bool _isSlopeWalk=false;
    public bool _isKnockedBack=false;
    private bool _isDash=false;
    private bool _isLookUp=false;
    private bool _isSquat=false;
    private bool _isGuard=false;
    private bool _isBarrier=false;
    private bool _isBarrierExpand=false;
    private bool _isOnLadder=false;
    private bool _isLadderMove=false;
    private bool _isUmbrellaOpened=false; 
    private static bool _isFirstLoad=true;
    private HP_Player _hpPlayer;
    private SpriteRenderer _sr;
    private BoxCollider2D _collider;
    private PlayerRespawn _pR;
    private PMove _pM;
    private PGuard _pG;
    private PBarrier _pB;
    private AttackBase _aB;
    private float _rayYOffset = 0.1f;
    private Animator _anim;
    public RuntimeAnimatorController _defaultController;
    public AnimatorOverrideController _overrideController;
    public bool IsJump{
        get=>_bJump;
        set{_bJump=value;}}
    public bool IsDash{
        get=>_isDash;}
    public bool IsLookUp{
        get=>_isLookUp;
        set{_isLookUp=value;}
    }
    public bool IsSquat{
        get=>_isSquat;
        set{_isSquat=value;}
    }
    public bool IsGuard{
        get=>_isGuard;
        set{_isGuard=value;}}

    public bool IsBarrier{
        get=>_isBarrier;
        set{_isBarrier=value;}}

    public bool IsBarrierExpand{
        get=>_isBarrierExpand;
        set{_isBarrierExpand=value;}}

    public bool IsOnLadder{
        get=>_isOnLadder;
        set{_isOnLadder=value;}}

    public bool IsLadderMove{
        get=>_isLadderMove;
        set{_isLadderMove=value;}}

    public bool IsUmbrellaOpened{
        get=>_isUmbrellaOpened;}

    
    void Awake()
    {
        _firstSpeed=_moveSpeed;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _collider=GetComponent<BoxCollider2D>();
        _anim=GetComponent<Animator>();
        _hpPlayer=FindObjectOfType<HP_Player>();
        _pR=GetComponent<PlayerRespawn>();
        _pM=GetComponent<PMove>();
        _pG=GetComponent<PGuard>();
        _pB=FindObjectOfType<PBarrier>();
        _aB=GetComponent<AttackBase>();
        _bJump = false;
        _anim.runtimeAnimatorController = _defaultController;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Enemy"),false);
        if(!_isFirstLoad){
            _hp=PlayerPrefs.GetInt("HP",7);
            _energy=PlayerPrefs.GetInt("Energy",5);
        }else{
            HPReturn(_maxHP);
            EnergyReturn(_maxEnergy);
            _isFirstLoad=false;

        }
    }

    void Update()
    {
        _moveSpeed=SpeedControl();
        _pM.Move(SpeedControl);
        
        if(_isDash&&!_isGuard&&!_aB.IsAttack){
            _time+=Time.deltaTime;
            _TMPspeed=_firstSpeed+_dashAcceleration*_time;
        }else{
            _time=1f;
            _TMPspeed=_firstSpeed;
        }
        
        HitFloor();
        FloatingFall(_isUmbrellaOpened);
        
      
    }
 
 


private void CheckSurface(string layerName, bool isSlope)
{
    int layerMask = LayerMask.GetMask(layerName);
    Vector2 rayOrigin = (Vector2)transform.position + _collider.offset + new Vector2(0, -_collider.size.y / 2.4f);
    RaycastHit2D rayHit = Physics2D.BoxCast(rayOrigin,new Vector3(_collider.size.x/2f, 0.08f, 1), 0.0f, Vector2.down, _rayYOffset, layerMask);
    if (rayHit.collider != null&&_rb.velocity.y<=0)
    {
        _bJump=false;
        if(!_aB.IsAttack) _anim.SetBool("isJump", false);
        if(_isDash){
            _anim.SetBool("isDash",true);
        }
        
        else
        {
            _isSlopeWalk = false;
        }
    }else if(rayHit.collider == null){
        
        _bJump=true;
    }
}
private void HitFloor()
{
    CheckSurface("Floor", false);
}


    private void OnDrawGizmos()
    {
        if (_collider == null) return;

        Gizmos.color = Color.blue;

        // BoxCollider2Dの底部中央を起点としたGizmoの開始位置
        Vector2 gizmoOrigin = (Vector2)transform.position + _collider.offset + new Vector2(0, -_collider.size.y / 2.4f);

        // GizmoでBoxCastと同じ範囲を表示
        Gizmos.DrawWireCube(gizmoOrigin + Vector2.down * _rayYOffset, new Vector3(_collider.size.x/2f, 0.08f, 1));
    }
    public void KnockBack(GameObject enemy){
        Debug.Log("pass");
        KnockBackC _knockback=FindObjectOfType<KnockBackC>();
        StartCoroutine(_knockback.KnockBack(gameObject,enemy,_isKnockedBack, newbool=>_isKnockedBack=newbool));
    }
    public void KnockBackGuard(GameObject enemy){
       EnemyBase _enemyBase=enemy.GetComponent<EnemyBase>();
        KnockBackC _knockback=FindObjectOfType<KnockBackC>();
        StartCoroutine(_knockback.KnockBackGuard(gameObject,enemy,_isKnockedBack,newbool=>_isKnockedBack=newbool));
        
    }

    
    public void HPChange(int amount){
        _hp-=amount;
        PlayerPrefs.SetInt("HP",_hp);
    }
    public void HPReturn(int amount){
        _hp=amount;
        PlayerPrefs.SetInt("HP",_hp);
    }
    public void EnergyChange(int amount){
        _energy-=amount;
        PlayerPrefs.SetInt("Energy",_energy);
    }
    public void EnergyReturn(int amount){
        _energy=amount;
        PlayerPrefs.SetInt("Energy",_energy);
    }
    public void GameOver(){
        
        _pR.Respawn(); // セーブポイントから復活
    }
    public void _OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || _bJump||_isGuard||_isBarrier||_aB.IsAttack||_isSquat||_isLookUp) return;
        _isLadderMove=false;
        _bJump=true;
        
        _isSlopeWalk=false;
        
        
        _anim.SetBool("isJump",true);
        _rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        
    }
    
    public void _OnDash(InputAction.CallbackContext context){
        
        if(context.performed){
            if(_aB.IsAttack||_bJump||!_pM.IsWalk||_isSquat||_isLookUp) return;
            _isDash=true;
            _anim.SetBool("isDash",true);
            
        }else if(context.canceled){
            _isDash=false;
            _anim.SetBool("isDash",false);
        }    
        
    }
    
   

    
    
    public void _OnUmbrellaSwitch(InputAction.CallbackContext context)
{
    if (_aB.IsAttack) return;

    _isUmbrellaOpened = !_isUmbrellaOpened;

    // 現在のAnimatorパラメータを保存
    AnimatorControllerParameter[] parameters = _anim.parameters;
    Dictionary<string, object> paramValues = new Dictionary<string, object>();

    foreach (var param in parameters)
    {
        if (param.type == AnimatorControllerParameterType.Bool)
            paramValues[param.name] = _anim.GetBool(param.name);
        else if (param.type == AnimatorControllerParameterType.Float)
            paramValues[param.name] = _anim.GetFloat(param.name);
        else if (param.type == AnimatorControllerParameterType.Int)
            paramValues[param.name] = _anim.GetInteger(param.name);
    }

    // AnimatorController を切り替え
    _anim.runtimeAnimatorController = _isUmbrellaOpened ? _overrideController : _defaultController;

    // パラメータを復元
    foreach (var param in paramValues)
    {
        if (param.Value is bool)
            _anim.SetBool(param.Key, (bool)param.Value);
        else if (param.Value is float)
            _anim.SetFloat(param.Key, (float)param.Value);
        else if (param.Value is int)
            _anim.SetInteger(param.Key, (int)param.Value);
    }
}

    private void FloatingFall(bool _isUmbrellaOpened)
{
    if(_isLadderMove){
        _rb.gravityScale=0;
    }
    else if (_isUmbrellaOpened && _rb.velocity.y < 0 && _bJump) // 傘が開いており、下降中でジャンプ状態
    {
        _rb.gravityScale = 0.5f;      // 重力を軽くしてゆっくり落下
        _rb.drag = 2.0f;              // 空気抵抗を増加させて浮遊感を出す
    }
    else if (_isUmbrellaOpened && _rb.velocity.y >= 0 && _bJump) // 傘が開いており、上昇中
    {
        _rb.gravityScale = 2.0f;      // 上昇時の重力を少し強める
        _rb.drag = 0.5f;              // 空気抵抗を無効化
    }
    else // 傘が閉じているとき、または地面についているとき
    {
        _rb.gravityScale = 2.0f;      // 通常の重力
        _rb.drag = 0.0f;              // 通常の空気抵抗
        
    }
}
    private float SpeedControl(){
        if(_isGuard||_isBarrier){ //ガード
            return 0f;
        }else if((_isUmbrellaOpened && _rb.velocity.y >= 0 && _bJump)||_isSquat){ //傘開け上昇中orしゃがみ
            return _firstSpeed/1.4f;
        }else if(_isDash&&!_isUmbrellaOpened){ //傘閉じダッシュ
            return Mathf.Min(_firstSpeed*3.0f,_TMPspeed);
        }else if(_isDash&&_isUmbrellaOpened&&!_bJump){ //傘開けダッシュ
            return Mathf.Min(_firstSpeed*1.4f,_TMPspeed);
        }
        return _firstSpeed;
    }
    private void AnimParameterReset(){
        foreach (AnimatorControllerParameter param in _anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool&&param.name!="isOpen")
            {
                _anim.SetBool(param.name, false);
            }
        }
    }
   private bool IsMove(){

        return GetComponent<PMove>().IsWalk||_bJump;
   }
    
   
    
}