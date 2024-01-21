using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DuckAnimation : MonoBehaviour
{
    public List<Sprite> nSprites;
    public List<Sprite> eSprites;
    public List<Sprite> sSprites;

    public List<Sprite> sIdleSprites;
    public List<Sprite> eIdleSprites;
    public List<Sprite> nIdleSprites;
    private List<Sprite> selectedSprites;
    private SpriteRenderer spriteRenderer;
    public float frameRate;
    private float changeX;
    public float changeCutoff;
    private float changeY;

    private int facing = 1;// 1 for east, 2 for north, 3 for south

    private Vector3 lastPosition;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectedSprites = eSprites;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spriteRenderer.flipX && changeX < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && changeX > 0f)
        {
            spriteRenderer.flipX = false;
        }

        if (Mathf.Abs(changeX) > changeCutoff && Mathf.Abs(changeY) > changeCutoff)
        {
            SetSprite();
        }
        else
        {
            if (facing == 1)
            {
                selectedSprites = eIdleSprites;
            }
            else if (facing == 2)
            {
                selectedSprites = nIdleSprites;
            }
            else if (facing == 3)
            {
                selectedSprites = sIdleSprites;
            }
        }

        if (transform.position != lastPosition)
        {
            changeX = transform.position.x - lastPosition.x;
            changeY = transform.position.y - lastPosition.y;
            direction = (transform.position - lastPosition).normalized;
        }

        lastPosition = transform.position;

        int frame = (int)((Time.time * frameRate) % 3);

        spriteRenderer.sprite = selectedSprites[frame];
    }

    void FixedUpdate()
    {

    }
    void SetSprite()
    {
        if (Mathf.Abs(changeY) > Mathf.Abs(changeX) && changeY > 0)
        {
            selectedSprites = nSprites;
            facing = 2;

        }
        else if (Mathf.Abs(changeY) > Mathf.Abs(changeX) && changeY < 0)
        {
            selectedSprites = sSprites;
            facing = 3;
        }
        else if (Mathf.Abs(changeY) < Mathf.Abs(changeX))
        {
            selectedSprites = eSprites;
            facing = 1;
        }
    }
}
