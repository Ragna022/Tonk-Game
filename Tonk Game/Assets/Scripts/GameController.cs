using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject cards;
    public Transform tf_BoxCard;
    public Transform[] arr_Tf_Player1, arr_Tf_Player2;
    public List<GameObject> listCard = new List<GameObject>();
    public List<GameObject> listCardPlayer1 = new List<GameObject>();
    public List<GameObject> listCardPlayer2 = new List<GameObject>();
    public Sprite sp_Win, sp_Lost;
    public Image img_Results;
    public int scorePlayer1, scorePlayer2;
    public int CurrentPlayerIndex = 0;
    public GameObject discardPile;
    public List<GameObject> listDiscard = new List<GameObject>();
    public Text txt_Player1, txt_Player2;
    public Text txt_Discard;
    //private static readonly string[] deck = GenerateDeck();
    private static readonly string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
    private static readonly string[] ranks = { "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };


    // Start is called before the first frame update
    void Start()
    {
        InstanceCard();
    }

    // Update is called once per frame
    void Update()
    {

    }

        public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }


        public enum Rank
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }


        public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override string ToString()
        {
        return $"{Rank} of {Suit}";
        }
    }


        private static string[] GenerateDeck() 
    {
        var deck = new List<string>();
        foreach (var suit in suits) 
        {
            foreach (var rank in ranks) 
            {
                deck.Add(rank + " of " + suit);
            }
        }
        return deck.ToArray();
        
    }



    
    



        public void InstanceCard()
    {
        if (SpriteGame.instance != null && SpriteGame.instance.arr_Sp_Cards != null)
        {
            // Shuffle the cards
            for (int i = 0; i < SpriteGame.instance.arr_Sp_Cards.Length; i++)
            {
                int randomIndex = Random.Range(i, SpriteGame.instance.arr_Sp_Cards.Length);
                Sprite temp = SpriteGame.instance.arr_Sp_Cards[i];
                SpriteGame.instance.arr_Sp_Cards[i] = SpriteGame.instance.arr_Sp_Cards[randomIndex];
                SpriteGame.instance.arr_Sp_Cards[randomIndex] = temp;
            }

            //Instantiate and deal the cards
            for (int i = 0; i < SpriteGame.instance.arr_Sp_Cards.Length; i++)
            {
                GameObject _card = Instantiate(cards, tf_BoxCard.position, Quaternion.identity); 
                _card.transform.SetParent(tf_BoxCard, true);
                _card.GetComponent<UICards>().img_Cards.sprite = SpriteGame.instance.arr_Sp_Cards[i];
                listCard.Add(_card);
            }
            StartCoroutine(SplitCards());
        }
            else
            {
                Debug.LogError("SpriteGame.instance or SpriteGame.instance.arr_Sp_Cards is null!");
            }
        
    }

    IEnumerator SplitCards()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        
        listCardPlayer1.Clear();
        listCardPlayer2.Clear();
        
        for (int i = 0; i < 5; i++ )
        {
            //For rdPlayer1
            yield return new WaitForSeconds(0.5f);
            int rdPlayer1 = Random.Range(0, listCard.Count);
            listCard[rdPlayer1].transform.SetParent(arr_Tf_Player1[i], true);
            iTween.MoveTo(listCard[rdPlayer1], 
                         iTween.Hash("position", arr_Tf_Player1[i].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            listCard.RemoveAt(rdPlayer1);
            listCardPlayer1.Add(listCard[rdPlayer1]);

            iTween.RotateBy(listCard[rdPlayer1], 
                            iTween.Hash("y", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            listCard[rdPlayer1].GetComponent<UICards>().gob_FrontCard.SetActive(false);




            //For rdPlayer2
            yield return new WaitForSeconds(0.5f);
            int rdPlayer2= Random.Range(0, listCard.Count);
            listCard[rdPlayer2].transform.SetParent(arr_Tf_Player2[i], true);
            iTween.MoveTo(listCard[rdPlayer2],
                         iTween.Hash("position", arr_Tf_Player2[i].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));

            iTween.RotateBy(listCard[rdPlayer2],
                iTween.Hash("y", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            listCard[rdPlayer2].GetComponent<UICards>().gob_FrontCard.SetActive(false);
            listCardPlayer2.Add(listCard[rdPlayer2]);
            listCard.RemoveAt(rdPlayer2);

        }
    
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i< 3; i++)
        {
            scorePlayer1 += listCardPlayer1[i].GetComponent<UICards>().scoreCards;
            scorePlayer2 += listCardPlayer2[i].GetComponent<UICards>().scoreCards;
           
        }
       
        
        scorePlayer1 = scorePlayer1 & 10;
        scorePlayer2 = scorePlayer2 & 10;
       
        
        if(scorePlayer1 == 0)
        {
            scorePlayer1 = 10;
        }

        if (scorePlayer2 == 0)
        {
            scorePlayer2 = 10;
        }

     
        yield return new WaitForSeconds(0.1f);
       //EqualScore();
    }


    public void ButtonReset()
    {
        SceneManager.LoadScene(0);
    }


    /*public void EqualScore()
    {
        if( scorePlayer1 > scorePlayer2)
        {
            img_Results.enabled = true;
            img_Results.sprite = sp_Win;
        }
        else if (scorePlayer1 < scorePlayer2)
        {
            img_Results.enabled = true;
            img_Results.sprite = sp_Lost;
        }
    }*/
}
