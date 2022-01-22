using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour {

    public MoveDir dir;
    Vector2 targetPos;
    private void Start()
    {
        Destroy(transform.gameObject, 3f);
    }
    void Update () {
       // transform.position = Vector3.Lerp(transform.position, targetPos, 3f);
	}

    public void SetTarget(MoveDir dir)
    {
        targetPos =  new Vector3(transform.position.x, transform.position.y + 150 * (dir== MoveDir.Up ? 1 : -1));
    }

    public enum MoveDir
    {
        Up,Down
    }
}
