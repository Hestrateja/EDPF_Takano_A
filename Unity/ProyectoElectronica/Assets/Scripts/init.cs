using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
