using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackC : MonoBehaviour
{
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public IEnumerator KnockBackMe(GameObject player,GameObject enemy){
        PlayerBase _pB=player.GetComponent<PlayerBase>();
        _pB.IsKnockedBack=true;
        Animator _anim=player.GetComponent<Animator>();
        Rigidbody2D _rb=player.GetComponent<Rigidbody2D>();
        _anim.enabled = false;
        Vector2 trEnemy = enemy.transform.position;
        Vector2 tr = player.transform.position;
        Vector2 knockbackDirection = (tr - trEnemy).normalized;
    
        
        _rb.AddForce(new Vector2( knockbackDirection.x* 5, 3), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        _rb.velocity=Vector2.zero;
        _anim.enabled=true;
        _pB.IsKnockedBack=false;
        
    }

    public IEnumerator KnockBackGuardMe(GameObject player,GameObject enemy){
        PlayerBase _pB=player.GetComponent<PlayerBase>();
        _pB.IsKnockedBack=true;
        Animator _anim=player.GetComponent<Animator>();
        Rigidbody2D _rb=player.GetComponent<Rigidbody2D>();
        _anim.enabled = false;
        Vector2 trEnemy = enemy.transform.position;
        Vector2 tr = player.transform.position;
        Vector2 knockbackDirection = (tr - trEnemy).normalized;
        

        _rb.AddForce(knockbackDirection * 5, ForceMode2D.Impulse);
    

        yield return new WaitForSeconds(0.4f);
        _rb.velocity=Vector2.zero;
        _anim.enabled=true;
        _pB.IsKnockedBack=false;
        
}
private void KnockBackMeCommonMethod(GameObject player,GameObject enemy){
    
}
    public IEnumerator KnockBackEnemy(GameObject player,GameObject enemy){
        
        EnemyBase _eB=enemy.GetComponent<EnemyBase>();
        _eB._isKnockedBack=true;
        Animator _anim=enemy.GetComponent<Animator>();
        Rigidbody2D _rb=enemy.GetComponent<Rigidbody2D>();
        _anim.enabled=false;
        Vector2 trMe=player.transform.position;
        Vector2 tr=enemy.transform.position;
        Vector2 trDistance=(tr-trMe).normalized;
        _rb.AddForce(new Vector2( trDistance.x* 5, 3), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        _rb.velocity=Vector2.zero;
        _anim.enabled=true;
        _eB._isKnockedBack=false;
        
    }
     

    
public IEnumerator KnockBackGuardEnemy(GameObject player,GameObject enemy){
    EnemyBase _eB=enemy.GetComponent<EnemyBase>();
        _eB._isKnockedBack=true;
        Animator _anim=enemy.GetComponent<Animator>();
        Rigidbody2D _rb=enemy.GetComponent<Rigidbody2D>();
        _anim.enabled=false;
        Vector2 trMe=player.transform.position;
        Vector2 tr=enemy.transform.position;
        Vector2 knockbackDirection=(tr-trMe).normalized;
    _rb.AddForce(knockbackDirection * 4, ForceMode2D.Impulse);

    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
    
    yield return new WaitForSeconds(0.4f);
        _rb.velocity=Vector2.zero;
        _anim.enabled=true;
        _eB._isKnockedBack=false;
}
 public IEnumerator HitBlink(GameObject charactor,int n){
        SpriteRenderer _sr=charactor.GetComponent<SpriteRenderer>();
        float interval=0.1f;
        for(int i=0; i<n; i++){
        yield return new WaitForSeconds(interval);
        _sr.enabled=false;
        yield return new WaitForSeconds(interval);
        _sr.enabled=true;
        }
    }


}
