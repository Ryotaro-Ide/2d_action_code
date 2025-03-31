using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int _hp;
    private float d=0.1f;
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;
    [SerializeField,Header("基礎攻撃力")]
    private int _attackPoint;
    private Rigidbody2D _rigid; 
    private HP_Player _hpPlayer;
    private PlayerBase _player;
    private KnockBackC _kB;
    private IMove _iMove;
    public bool _CanKnockBack; //ノックバック可能かどうか
    public bool _isKnockedBack=false; //現在ノックバックしているかどうか
    private bool _isActive = false; // 画面に映っているかどうか
    private Camera mainCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        _rigid=GetComponent<Rigidbody2D>();
        _hpPlayer=FindObjectOfType<HP_Player>();
        _player=FindObjectOfType<PlayerBase>();
        _kB=FindObjectOfType<KnockBackC>();
        _iMove=GetComponent<IMove>();
        mainCamera=Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        _isActive = IsVisibleFromCamera();

        if (_isActive && !_isKnockedBack && _player != null)
        {
            _iMove.Move(_moveSpeed);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.tag=="Player"){
            _hpPlayer.DamageHP(_attackPoint, gameObject,collision.gameObject,true);
        
        
    }
    }
    
    public void TakeDamage(int attackPoint,GameObject player){ //攻撃を受けた
        _hp-=attackPoint;
        if(_hp>0){ //ノックバック、アニメーション停止、点滅など
            if(_CanKnockBack) StartCoroutine(_kB.KnockBackEnemy(player,gameObject));
            StartCoroutine(_kB.HitBlink(gameObject,3)); //対象、点滅回数
        }else{ //死亡アニメーション
            Destroy(gameObject);
        }
    }
    public void KnockBackGuardToEnemy(GameObject player){ //ガードを受けた
        StartCoroutine(_kB.KnockBackGuardEnemy(gameObject,player,_isKnockedBack));
    }
    private bool IsVisibleFromCamera()
    {
        if (mainCamera == null) return false;

        // **オブジェクトの位置をカメラの Viewport 空間に変換**
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        // **視界内なら true（0〜1の範囲内ならカメラに映っている）**
        return viewPos.x > 0-d && viewPos.x < 1+d && viewPos.y > 0-d && viewPos.y < 1+d && viewPos.z > 0;
    }
}
public interface IMove{
    void Move(float _moveSpeed);
}