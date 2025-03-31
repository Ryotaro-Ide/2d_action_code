using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackC : MonoBehaviour
{
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

     public IEnumerator KnockBack(GameObject receiver,GameObject server,bool _isKnockedBack,System.Action<bool> callback){ //敵と反対側に吹っ飛ぶ
        Animator _anim=receiver.GetComponent<Animator>();
        Rigidbody2D _rb=receiver.GetComponent<Rigidbody2D>();
        _anim.enabled=false;
        _isKnockedBack=true;
        callback(_isKnockedBack);
        Vector2 trEnemy=server.transform.position;
        Vector2 tr=receiver.transform.position;
        Vector2 trDistance=(tr-trEnemy).normalized;
        if(tr.x<=trEnemy.x){
            _rb.AddForce(new Vector2( trDistance.x* 5, 3), ForceMode2D.Impulse);
        }else{
            _rb.AddForce(new Vector2( trDistance.x* 5, 3), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.5f);
        _rb.velocity=Vector2.zero;
        _anim.enabled=true;
        _isKnockedBack=false;
        callback(_isKnockedBack);
    }

    public IEnumerator KnockBackGuard(GameObject player,GameObject enemy,bool _isKnockedBack,System.Action<bool> callback){
        Animator _anim=player.GetComponent<Animator>();
        Rigidbody2D _rb=player.GetComponent<Rigidbody2D>();
        _isKnockedBack = true;
        _anim.enabled = false;
        callback(_isKnockedBack);
        Vector2 trEnemy = enemy.transform.position;
        Vector2 tr = player.transform.position;
        Vector2 knockbackDirection = (tr - trEnemy).normalized;

        _rb.AddForce(knockbackDirection * 5, ForceMode2D.Impulse);
    

        yield return new WaitForSeconds(0.4f);
        _rb.velocity=Vector2.zero;
        _anim.enabled=true;
        _isKnockedBack=false;
        callback(_isKnockedBack);
}
public IEnumerator KnockBackGuardToEnemy(GameObject enemy,GameObject player,bool _isKnockedBack,System.Action<bool> callback){
    Animator _anim=enemy.GetComponent<Animator>();
    Rigidbody2D _rb=enemy.GetComponent<Rigidbody2D>();
    _isKnockedBack = true;
    _anim.enabled = false;
    callback(_isKnockedBack);
    Vector2 trplayer = player.transform.position;
    Vector2 tr = enemy.transform.position;
    Vector2 knockbackDirection = (tr - trplayer).normalized;
    _rb.AddForce(knockbackDirection * 4, ForceMode2D.Impulse);

    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
    
    yield return new WaitForSeconds(0.4f);
        _rb.velocity=Vector2.zero;
        _anim.enabled=true;
        _isKnockedBack=false;
        callback(_isKnockedBack);
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
