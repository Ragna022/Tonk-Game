using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGame : MonoBehaviour
{
    public static SpriteGame instance;
    public Sprite[] arr_Sp_Cards;

    private void Awake()
    {


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        /* If an instance of SpriteGame already exists, destroy this new instance.
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // If no instance exists, set this as the instance.
        instance = this;

        // Make sure the arr_Sp_Cards array is not null.
        if (arr_Sp_Cards == null)
        {
            Debug.LogError("arr_Sp_Cards is null!");
            return;
        }*/

    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
