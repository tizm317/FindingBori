using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 correctPosition;
    private SpriteRenderer _sprite;
    public int number;

    public bool isRightPlace;

    void Awake()
    {
        //before start
        targetPosition = transform.position;
        correctPosition = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime); // 0.05f -> 10f * time.deltaTime // speed / frame -> speed / second
        if(targetPosition == correctPosition)
        {
            //_sprite.color = Color.white;
            _sprite.color = new Color(19/255f, 26/255f, 132/255f);
            isRightPlace = true;
        }
        else
        {
            _sprite.color = Color.black;
            isRightPlace = false;
        }
    }
}
