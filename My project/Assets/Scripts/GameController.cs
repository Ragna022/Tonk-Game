using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject cards;
    public Transform tf_BoxCard;
    public Transform[] arr_Tf_Player, arr_Tf_AI;
    public List<GameObject> listCard = new List<GameObject>();
    public List<GameObject> listCardPlayer = new List<GameObject>();
    public List<GameObject> listCardAI = new List<GameObject>();
    public Sprite sp_Win, sp_Lost;
    public Image img_Results;
    public int scorePlayer, scoreAI;
    // Start is called before the first frame update
    void Start()
    {
        InstanceCard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstanceCard()
    {
        for ( int i = 0; i < SpriteGame.instance.arr_Sp_Cards.Length; i++ )
        {
            GameObject _card = Instantiate(cards, tf_BoxCard.position, Quaternion.identity); 
            _card.transform.SetParent(tf_BoxCard, true);
            _card.GetComponent<UICards>().img_Cards.sprite = SpriteGame.instance.arr_Sp_Cards[i];
            listCard.Add(_card);
        }
        Debug.Log("arr_Sp_Cards is " + SpriteGame.instance.arr_Sp_Cards);

        if (SpriteGame.instance != null && SpriteGame.instance.arr_Sp_Cards != null)
        {
            // Your code here
            for (int i = 0; i < SpriteGame.instance.arr_Sp_Cards.Length; i++) ;
        }
        else
        {
            Debug.LogError("SpriteGame.instance or SpriteGame.instance.arr_Sp_Cards is null!");
        }
        StartCoroutine(SplitCards());
    }

    IEnumerator SplitCards()
    {
        scoreAI = 0;
        scorePlayer = 0;
        listCardPlayer.Clear();
        listCardAI.Clear();
        for (int i = 0; i < 3; i++ )
        {
            yield return new WaitForSeconds(0.5f);
            int rdPlayer = Random.Range(0, listCard.Count);
            listCard[rdPlayer].transform.SetParent(arr_Tf_Player[i], true);
            iTween.MoveTo(listCard[rdPlayer], 
                         iTween.Hash("position", arr_Tf_Player[i].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            //listCard.RemoveAt(rdPlayer);
            listCardPlayer.Add(listCard[rdPlayer]);

            iTween.RotateBy(listCard[rdPlayer], 
                            iTween.Hash("y", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            listCard[rdPlayer].GetComponent<UICards>().gob_FrontCard.SetActive(false);


            yield return new WaitForSeconds(0.5f);
            int rdAI = Random.Range(0, listCard.Count);
            listCard[rdAI].transform.SetParent(arr_Tf_AI[i], true);
            iTween.MoveTo(listCard[rdAI],
                         iTween.Hash("position", arr_Tf_AI[i].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            
            


            iTween.RotateBy(listCard[rdAI],
                iTween.Hash("y", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            listCard[rdAI].GetComponent<UICards>().gob_FrontCard.SetActive(false);
            listCardAI.Add(listCard[rdAI]);
            //listCard.RemoveAt(rdAI);
           


        }

        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i< 3; i++)
        {
            scoreAI += listCardAI[i].GetComponent<UICards>().scoreCards;
            scoreAI += listCardPlayer[i].GetComponent<UICards>().scoreCards;
        }
       
        
        scoreAI = scoreAI & 10;
        scorePlayer = scorePlayer & 10;
        
        if(scoreAI == 0)
        {
            scoreAI = 10;
        }

        if (scorePlayer == 0)
        {
            scorePlayer = 10;
        }
        yield return new WaitForSeconds(0.1f);
        EqualScore();
    }


    public void ButtonReset()
    {
        SceneManager.LoadScene(0);
    }


    public void EqualScore()
    {
        if( scorePlayer > scoreAI)
        {
            img_Results.enabled = true;
            img_Results.sprite = sp_Win;
        }
        else if (scorePlayer < scoreAI)
        {
            img_Results.enabled = true;
            img_Results.sprite = sp_Lost;
        }
    }
}
