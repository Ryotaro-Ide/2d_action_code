using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AttackBase : MonoBehaviour
{
    protected Animator _anim;
    protected SpriteRenderer _sr;
    protected bool _isAttacking=false;
    protected bool _isAttackCharging=false;
    public PlayerBase _player;
    public GameObject _attackPlayer;
    public  Rigidbody2D _rb;
    [SerializeField,Header("攻撃一覧")]
    List<GameObject> _attackList=new List<GameObject>();
    private Energy_Player _ep;
    
    
    // Start is called before the first frame update
    protected virtual void Awake() {
        _isAttacking=false;
        _isAttackCharging=false;
        _attackPlayer=gameObject;
        _anim=GetComponent<Animator>();
        _sr=GetComponent<SpriteRenderer>();
        _player=GetComponent<PlayerBase>();
        _rb=GetComponent<Rigidbody2D>();
        _ep=FindObjectOfType<Energy_Player>();
        foreach (Transform child in transform)
       {
        if(child.gameObject.tag=="Umbrella"){
        _attackList.Add(child.gameObject);
        
        }
       }
    }
    void Start()
    {
       
        
       
    }
    
    // Update is called once per frame
    void Update()
    {
        AttackAreaFlip(); 
    }
    public void OnAttackL(InputAction.CallbackContext context){
        if(CanNotAttack()) return;
        _rb.velocity=Vector3.zero;
        
        
        if(_player.IsJump){
                StartAttack("AttackLAir",5,0);
        
            }else if(_player.IsDash){
                StartAttack("AttackLD",2,0);

            }else if(_player.IsLookUp){
                StartAttack("AttackLUp",3,0);
                
            }else if(_player.IsSquat){
                StartAttack("AttackLDown",4,0);
                      
            }else{
            if(context.started){ //0秒後
                _sr.color=new Color(1.0f,0.7f,1.0f);
                _anim.SetTrigger("AttackLSCharge");
                _isAttackCharging=true;
            }else if(context.performed&&_isAttackCharging){ //2秒以降に離した
                StartAttack("AttackLS",1,0);
                _sr.color=new Color(1.0f,1.0f,1.0f);
                _isAttackCharging=false;
            }else if(context.canceled&&_isAttackCharging){ //2秒以前に離した
                StartAttack("AttackL",0,0);
                _sr.color=new Color(1.0f,1.0f,1.0f);
                _isAttackCharging=false;
            }
    }
    }
    public void OnAttackR(InputAction.CallbackContext context){
        if(CanNotAttack()) return;
            if(_player.IsJump){
                //StartAttack("AttackRAir",5);
        
            }else if(_player.IsDash){
                StartAttack("AttackLD",8,2);

            }else if(context.started&&IsEnergyUse(1)){ //0秒後
                _sr.color=new Color(0f,1f,1f);
                _anim.SetTrigger("AttackLSCharge");
                _isAttackCharging=true;
            }else if(context.performed&&_isAttackCharging){ //秒以降に離した
                StartAttack("AttackLS",7,1);
                _sr.color=new Color(1f,1f,1f);
                _isAttackCharging=false;
            }else if(context.canceled&&_isAttackCharging){ //秒以前に離した
                StartAttack("AttackL",6,1);
                _sr.color=new Color(1f,1f,1f);
                _isAttackCharging=false;
            }
            
        }
        
    
    private void StartAttack(string triggerName,int attackNum,int energyCost){ //setTriggerの名前、攻撃番号、必要エナジー
        if(!IsEnergyUse(energyCost)) return;
        _isAttacking=true;
        _anim.SetTrigger(triggerName);
        _attackList[attackNum].GetComponent<IAttack>().Attack();
    }
    private void EndAttack(){
        foreach (var attack in _attackList)
        {
            if(attack.GetComponent<Collider2D>()!=null)
            attack.GetComponent<Collider2D>().enabled=false;
        }
        _isAttacking=false;
        
    }
    private void AttackAreaFlip(){
        foreach (var attack in _attackList)
        {
            Vector3 localScale = attack.transform.localScale;
            localScale.x = _sr.flipX ? -1 : 1;
            attack.transform.localScale = localScale;
        }
        
    }
    public bool IsEnergyUse(int _energyConsume){
        return _player._energy-_energyConsume>=0;
    }
    public void AttackEnergyConsume(int _energyAttackPoint){
        
        _ep.ConsumeEnergy(_energyAttackPoint);
    }
    private bool CanNotAttack(){
        return _player.IsGuard||_player.IsBarrier||_player.IsKnockedBack||(_isAttacking&&!_isAttackCharging);
    }
    public bool IsAttack{
        get => _isAttacking; 
        set{_isAttacking = value;}}
    public bool IsAttackCharging{
        get => _isAttackCharging; 
        set{_isAttackCharging = value;}}
    
}
public interface IAttack
    {
        void Attack();
    }