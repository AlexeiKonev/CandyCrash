using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDispatcher : MonoBehaviour
{

    public  Bot[,] botArray;
    public GameObject DestroyParticle;
    public GameObject HelpParticle;
    private  List<Bot> BotMatch = new List<Bot>();
    private  Bot curBot, targetBot;
    private  int wight, hidth;
    private Board board;
    private UiController uiController;
    private AudioSource audio;
    public void Start()
    {
           audio = GameObject.FindGameObjectWithTag("PufSound").GetComponent<AudioSource>();
           uiController = FindObjectOfType<UiController>();
           board = FindObjectOfType<Board>();
    }

    public  void ArrayInstatiate(int x, int y)
    {
        botArray = new Bot[x, y];
        wight = x;
        hidth = y;
    }

    public  void MoveUp(Vector2 pos)
    {
        if (pos.y == hidth) return;

        curBot = botArray[(int)pos.x, (int)pos.y];
        targetBot = botArray[(int)pos.x, (int)pos.y + 1];

        curBot.Target = targetBot;
        curBot.SetLastPos();
        targetBot.SetLastPos();

        curBot.row = (int)pos.y + 1;
        targetBot.row = (int)pos.y;

    }

    public  void MoveDown(Vector2 pos)
    {
        if (pos.y == 0) return;


        curBot = botArray[(int)pos.x, (int)pos.y];
        targetBot = botArray[(int)pos.x, (int)pos.y - 1];

        curBot.Target = targetBot;
        curBot.SetLastPos();
        targetBot.SetLastPos();

        curBot.row = (int)pos.y - 1;
        targetBot.row = (int)pos.y;

    }

    public  void MoveLeft(Vector2 pos)
    {
        if (pos.x == 0) return;


        curBot = botArray[(int)pos.x, (int)pos.y];
        targetBot = botArray[(int)pos.x - 1, (int)pos.y];

        curBot.Target = targetBot;
        curBot.SetLastPos();
        targetBot.SetLastPos();

        curBot.column = (int)pos.x - 1;
        targetBot.column = (int)pos.x;

    }

    public  void MoveRight(Vector2 pos)
    {
        if (pos.x == wight) return;


        curBot = botArray[(int)pos.x, (int)pos.y];
        targetBot = botArray[(int)pos.x + 1, (int)pos.y];

        curBot.Target = targetBot;
        curBot.SetLastPos();
        targetBot.SetLastPos();

        curBot.column = (int)pos.x + 1;
        targetBot.column = (int)pos.x;


    }

    public  void CheckMatched()
    {
        Bot curBot, leftBot, rightBot, upBot, downBot;
        for (int y = 0; y <= hidth - 1; y++)
        {
            for (int x = 0; x <= wight - 1; x++)
            {
                curBot = botArray[x, y];
                if (curBot == null) continue;

                if (x != 0 && x < wight - 1)
                {
                    leftBot = botArray[x - 1, y];
                    rightBot = botArray[x + 1, y];

                    if (leftBot == null || rightBot == null) continue;

                    if (leftBot.tag == curBot.tag && rightBot.tag == curBot.tag)
                    {
                        leftBot.isMatched = true;
                        rightBot.isMatched = true;
                        curBot.isMatched = true;

                        BotMatch.Add(leftBot);
                        BotMatch.Add(rightBot);
                        BotMatch.Add(curBot);

                        leftBot.gameObject.GetComponent<SpriteRenderer>().color = new Color(220f, 255f, 255f);
                        rightBot.gameObject.GetComponent<SpriteRenderer>().color = new Color(220f, 255f, 255f);
                        curBot.gameObject.GetComponent<SpriteRenderer>().color = new Color(220f, 255f, 255f);
                    }

                }

                if (y != 0 && y < hidth - 1)
                {

                    downBot = botArray[x, y - 1];
                    upBot = botArray[x, y + 1];

                    if (downBot == null || upBot == null) continue;

                    if (downBot.tag == curBot.tag && upBot.tag == curBot.tag)
                    {
                        downBot.isMatched = true;
                        upBot.isMatched = true;
                        curBot.isMatched = true;

                        BotMatch.Add(downBot);
                        BotMatch.Add(upBot);
                        BotMatch.Add(curBot);

                        downBot.gameObject.GetComponent<SpriteRenderer>().color = new Color(220f, 255f, 255f);
                        upBot.gameObject.GetComponent<SpriteRenderer>().color = new Color(220f, 255f, 255f);
                        curBot.gameObject.GetComponent<SpriteRenderer>().color = new Color(220f, 255f, 255f);
                    }

                }
            }
        }
    }

    public  void DestroyMatchBot()
    {
        uiController.AddTime();
        while (BotMatch.Count > 0)
        {
          GameObject gm = Instantiate(DestroyParticle, new Vector2( BotMatch[0].column, BotMatch[0].row) ,Quaternion.identity);
            Destroy(gm, 1f);
            botArray[BotMatch[0].column, BotMatch[0].row] = null;

            Destroy(BotMatch[0].gameObject);
            BotMatch.RemoveAt(0);
        }
        audio.Play();
        StartCoroutine(BotMoveDown());

    }

    private  IEnumerator BotMoveDown()
    {
        yield return new WaitForSeconds(0.25f);
        int nullBot = 0;
        for (int x = 0; x <= wight - 1; x++)
        {
            for (int y = 0; y <= hidth - 1; y++)
            {
            
                if (botArray[x, y] == null)
                {
                    nullBot++;
                }
                else if (nullBot > 0)
                {
                    botArray[x, y].row -= nullBot;
                    botArray[x, y] = null;
                }
            }
            nullBot = 0;
        }
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(FillBoard());

    }   

    private void CreateBot()
    {
        for (int y = 0; y <= hidth - 1; y++)
        {
            for (int x = 0; x <= wight - 1; x++)
            {
                if (botArray[x, y] == null)
                {
                    int randNum = Random.Range(0, board.botlist.Length);
                    Vector2 transformBot = new Vector2(x, y);
                    GameObject bot = Instantiate(board.botlist[randNum], transformBot, Quaternion.identity) as GameObject;
                    botArray[x, y] = bot.GetComponent<Bot>();
                }
            }
        }
    }

    private bool CheckMatchesOnBoard()
    {
        for (int y = 0; y <= hidth - 1; y++)
        {
            for (int x = 0; x <= wight - 1; x++)
            {
                if (botArray[x, y].isMatched) return true;
            }
        }
        return false;
    }

    public IEnumerator FillBoard()
    {
        CreateBot();
        yield return new WaitForSeconds(0.35f);
        CheckMatched();
        while (CheckMatchesOnBoard())
        {
            yield return new WaitForSeconds(0.35f);
            DestroyMatchBot();
        }
        board.CheckAbility();
        if(board.ListAbilityMatch.Count == 0)
        {
            for (int y = 0; y <= hidth - 1; y++)
            {
                for (int x = 0; x <= wight - 1; x++)
                {
                    Destroy(botArray[x, y]);
                    botArray[x, y] = null;

                }
            }
            board.FillBoard();
            board.CheckAbility();
        }

    }

    public void HelpPlayer()
    {
        if (board.ListAbilityMatch.Count != 0)
        {
            uiController.MinusTime();
            foreach (Bot bot in board.ListAbilityMatch)
            {
                GameObject gm = Instantiate(HelpParticle, bot.transform.position, Quaternion.identity);
                Destroy(gm, 1f);
            }
        }
    }

    


}
