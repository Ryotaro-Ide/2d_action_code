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
    public PlayerBase _pb;
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
        _pb=GetComponent<PlayerBase>();
        _rb=GetComponent<Rigidbody2D>();
        _ep=FindObjectOfType<Energy_pb>();
        foreach (Transform child in transform)
       {
        if(child.gameObject.tag=="Umbrella"){
        _attackList.Add(child.gameObject);
        child.GetComponent<Collider2D>().enabled=false;
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
    public void OnAttack(InputAction.CallbackContext context){
        if(CanNotAttack()) return;
        if(_isAttacking&&!_isAttackCharging) return;
        _rb.velocity=Vector3.zero;
        
        
        if(_pb.IsJump){
            if(context.started){
                _anim.SetTrigger("AttackLAir");
                _attackList[5].GetComponent<IAttack>().Attack(); 
                _isAttacking=true;
            }
        
        }else if(_pb.IsDash){
            if(context.started){ //0秒後
                
                _anim.SetTrigger("AttackLD");
                _attackList[2].GetComponent<IAttack>().Attack();
                _isAttacking=true;
            }
        }else if(_pb.IsLookUp){
                _attackList[3].GetComponent<IAttack>().Attack();
                _isAttacking=true;
        }else if(_pb.IsSquat){
                _attackList[4].GetComponent<IAttack>().Attack();
                _isAttacking=true;        
        }else{
            if(context.started){ //0秒後
                _sr.color=new Color(1.0f,0.7f,1.0f);
                _anim.SetTrigger("AttackLSCharge");
                _isAttacking=true;
                _isAttackCharging=true;
            }else if(context.performed&&_isAttackCharging){ //2秒以降に離した
                _sr.color=new Color(1.0f,1.0f,1.0f);
                _anim.SetTrigger("AttackLS");
                _attackList[1].GetComponent<IAttack>().Attack();
                _isAttacking=true;
                _isAttackCharging=false;
            }else if(context.canceled&&_isAttackCharging){ //2秒以前に離した
                _sr.color=new Color(1.0f,1.0f,1.0f);
                _anim.SetTrigger("AttackL");
                _attackList[0].GetComponent<IAttack>().Attack();
                _isAttacking=true;
                _isAttackCharging=false;
            }
    }
    }
    public void OnAttackLS(InputAction.CallbackContext context){
       
    }
     private void EndAttack(){
        foreach (var attack in _attackList)
        {
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
        return _pb._energy-_energyConsume>=0;
    }
    public void AttackEnergyConsume(int _energyAttackPoint){
        _ep.ConsumeEnergy(_energyAttackPoint);
    }
    private bool CanNotAttack(){
        return _pb.IsGuard||_pb.IsBarrier||_pb._isKnockedBack;
    }
    public bool IsAttack{
        get => _isAttacking; 
        set{_isAttacking = value;}}
    
}
public interface IAttack
    {
        void Attack();
    }
