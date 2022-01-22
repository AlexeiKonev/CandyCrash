using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [Header("Board Size")]
    public int width;
    public int hight;
    [Header("Bot Options ")] 
    public GameObject[] botlist;

    private Bot  leftBot1, leftBot2, downBot1, downBot2 , rigthBot1 , rigthBot2 , topBot1 ,topBot2;
    private BotDispatcher bd;
    public List<Bot> ListAbilityMatch;

    void Start () {
        bd = FindObjectOfType<BotDispatcher>();
        bd.ArrayInstatiate(width, hight);
        ListAbilityMatch = new List<Bot>();
        FillBoard();

        CheckAbility();
    }

    public void FillBoard()
    {
        for (int y = 0; y < hight; y++)
        {

            for (int x = 0; x < width; x++)
            {
                int randNum = Random.Range(0, botlist.Length);

                int iteration = 0;
                while (IsMathc(x, y, botlist[randNum]) && iteration < 100)
                {
                    randNum = Random.Range(0, botlist.Length);
                    iteration++;

                }

                Vector2 transformBot = new Vector2(x, y);
                GameObject bot = Instantiate(botlist[randNum], transformBot, Quaternion.identity) as GameObject;
                bot.transform.SetParent(gameObject.transform);
                bot.name = "( y - " + y + ", x - " + x + ")";
                bd.botArray[x, y] = bot.GetComponent<Bot>();
            }
        }
    }

    private bool IsMathc(int x, int y, GameObject bot)
    {

        if (x >= 2)
        {
            leftBot1 = bd.botArray[x - 1, y];
            leftBot2 = bd.botArray[x - 2, y];
            if (leftBot1.tag == bot.tag && bot.tag == leftBot2.tag)
            {
                return true;
            }
        }

        if (y >= 2)
        {
            downBot1 = bd.botArray[x , y - 1];
            downBot2 = bd.botArray[x , y - 2];
            if (downBot1.tag == bot.tag && bot.tag == downBot2.tag)
            {
                return true;
            }
        }

        return false;
    }

    public void BotCreate()
    {
        for (int y = 0; y < hight - 1; y++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                if (bd.botArray[x, y] == null)
                {
                    int randNum = Random.Range(0, botlist.Length);
                    Vector2 transformBot = new Vector2(x, y);
                    GameObject bot = Instantiate(botlist[randNum], transformBot, Quaternion.identity) as GameObject;
                    bd.botArray[x, y] = bot.GetComponent<Bot>();
                }
            }
        }
    }

    public void CheckAbility()
    {
        ListAbilityMatch.Clear();
           Bot curBot;
        for (int y = 0; y < hight - 1; y++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                curBot = bd.botArray[x, y];
                if (x < width - 2)
                {
                    rigthBot1 = bd.botArray[x + 1, y];
                    if (curBot.tag == rigthBot1.tag)
                    {
                        if (x < width - 3)
                        {
                            rigthBot2 = bd.botArray[x + 3, y];
                            if (rigthBot1.tag == rigthBot2.tag)
                            {
                                

                                ListAbilityMatch.Add(curBot);
                                ListAbilityMatch.Add(rigthBot1);
                                ListAbilityMatch.Add(rigthBot2);
                                return;
                            }
                        }

                        if (y != 0)
                        {
                            downBot1 = bd.botArray[x + 2, y - 1];
                            if (rigthBot1.tag == downBot1.tag)
                            {
                                
                                ListAbilityMatch.Add(curBot);
                                ListAbilityMatch.Add(rigthBot1);
                                ListAbilityMatch.Add(downBot1);
                                return;
                            }
                        }
                        if (y < hight - 2)
                        {
                            topBot1 = bd.botArray[x + 2, y + 1];
                            if (rigthBot1.tag == topBot1.tag)
                            {
                                
                                ListAbilityMatch.Add(curBot);
                                ListAbilityMatch.Add(rigthBot1);
                                ListAbilityMatch.Add(topBot1);
                                return;
                            }
                        }
                    }
                    else
                    {
                        rigthBot2 = bd.botArray[x + 2, y];
                        if (curBot.tag == rigthBot2.tag)
                        {
                            if (y < hight - 2)
                            {
                                topBot1 = bd.botArray[x + 1, y + 1];
                                if (rigthBot2.tag == topBot1.tag)
                                {
                                    
                                    ListAbilityMatch.Add(curBot);
                                    ListAbilityMatch.Add(rigthBot2);
                                    ListAbilityMatch.Add(topBot1);
                                    return;
                                }
                            }

                            if ((y != 0))
                            {
                                downBot1 = bd.botArray[x + 1, y - 1];
                                if (rigthBot2.tag == downBot1.tag)
                                {
                                    
                                    ListAbilityMatch.Add(curBot);
                                    ListAbilityMatch.Add(rigthBot2);
                                    ListAbilityMatch.Add(downBot1);
                                    return;
                                }
                            }
                        }
                    }
                }

                if (y < hight - 2)
                {
                    topBot1 = bd.botArray[x, y + 1];
                    if (curBot.tag == topBot1.tag)
                    {
                        if (y < hight - 3)
                        {
                            topBot2 = bd.botArray[x, y + 3];
                            if (topBot1.tag == topBot2.tag)
                            {
                                
                                ListAbilityMatch.Add(curBot);
                                ListAbilityMatch.Add(topBot1);
                                ListAbilityMatch.Add(topBot2);
                                return;
                            }
                        }

                        if (x != 0)
                        {
                            leftBot1 = bd.botArray[x - 1, y + 2];
                            if (topBot1.tag == leftBot1.tag)
                            {
                                
                                ListAbilityMatch.Add(curBot);
                                ListAbilityMatch.Add(topBot1);
                                ListAbilityMatch.Add(leftBot1);
                                return;
                            }
                        }
                        if (x < width - 2)
                        {
                            rigthBot1 = bd.botArray[x + 1, y + 2];
                            if (topBot1.tag == rigthBot1.tag)
                            {
                                
                                ListAbilityMatch.Add(curBot);
                                ListAbilityMatch.Add(topBot1);
                                ListAbilityMatch.Add(rigthBot1);
                                return;
                            }
                        }
                    }
                    else
                    {
                        topBot2 = bd.botArray[x, y + 2];
                        if (curBot.tag == topBot2.tag)
                        {
                            if (x < width - 2)
                            {
                                rigthBot1 = bd.botArray[x + 1, y + 1];
                                if (topBot2.tag == rigthBot1.tag)
                                {
                                    
                                    ListAbilityMatch.Add(curBot);
                                    ListAbilityMatch.Add(topBot2);
                                    ListAbilityMatch.Add(rigthBot1);
                                    return;
                                }
                            }

                            if ((x != 0))
                            {
                                leftBot1 = bd.botArray[x - 1, y + 1];
                                if (topBot2.tag == leftBot1.tag)
                                {

                                    
                                    ListAbilityMatch.Add(curBot);
                                    ListAbilityMatch.Add(topBot2);
                                    ListAbilityMatch.Add(leftBot1);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }



}


