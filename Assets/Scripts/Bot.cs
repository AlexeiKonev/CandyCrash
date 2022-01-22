using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    public float angel;

    public int column, row;
    public int targetX, targetY;
    public int lastX, lastY;
    public bool isMatched;
    public Bot Target;
    Vector2 firstClick, lastClick , tempPosition;
    private float swipeResist = 0.5f;
    private BotDispatcher bd;



    void Start()
    {
        bd = FindObjectOfType<BotDispatcher>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;
        targetX = column;
        targetY = row;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else 
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            bd.botArray[column, row] = this;

        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else 
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            bd.botArray[column, row] = this;

        }

    }

    private void OnMouseDown()
    {
        firstClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void OnMouseUp()
    {
        lastClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Calculate();
    }

    public void Calculate()
    {
        if(Mathf.Abs(lastClick.y - firstClick.y) > swipeResist || Mathf.Abs(lastClick.x - firstClick.x) > swipeResist){
            angel = Mathf.Atan2(lastClick.y - firstClick.y, lastClick.x - firstClick.x) * 180 / Mathf.PI;
            MoveBot();
        }
    }

    public void MoveBot()
    {
        if (angel > -45 && angel <= 45)
        {
            //Right Swipe
            bd.MoveRight(transform.position);

        }
        else if (angel > 45 && angel <= 135)
        {
            //Up Swipe
            bd.MoveUp(transform.position);
        }
        else if (angel > 135 || angel <= -135)
        {
            //Left Swipe
            bd.MoveLeft(transform.position);
        }
        else if (angel < -45 && angel >= -135)
        {
            //Down Swipe
            bd.MoveDown(transform.position);
        }
        StartCoroutine(Chek());

    }

    public IEnumerator Chek()
    {
      yield return  new WaitForSeconds(0.10f);
        bd.CheckMatched();
        if (!isMatched && !Target.isMatched)
        {
            StartCoroutine(GoToLastPos());
            StartCoroutine(Target.GoToLastPos());
        }
        else
        {
            yield return new WaitForSeconds(0.20f);
            bd.DestroyMatchBot();
        }          

    }

    public IEnumerator GoToLastPos()
    {
        yield return new WaitForSeconds(0.1f);
        column = lastX;
        row = lastY;

    }
        

    public void SetLastPos()
    {
        lastX = column;
        lastY = row;
    }

    

}

