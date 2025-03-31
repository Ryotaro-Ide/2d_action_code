using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Load_Stage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnLoad();
    }
    private void OnLoad(){
        if(Input.GetKeyDown(KeyCode.Y)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
