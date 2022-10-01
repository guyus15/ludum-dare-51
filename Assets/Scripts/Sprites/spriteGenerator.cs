using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;   
    void Start()
    {
        //Create array of colours to pick from
        Color[] colorArray = new Color[5];
        colorArray[0] = new Color (0.83f, 0.26f, 0.26f); //Red
        colorArray[1] = new Color (1.0f, 0.7f, 0.0f); //Yellow
        colorArray[2] = new Color (0.56f, 0.41f, 0.83f); 
        colorArray[3] = new Color (0.29f, 0.69f, 0.87f);
        colorArray[4] = new Color (0.62f, 0.83f, 0.31f); 

        //Create arrays with paths for all relevant sprites.
        string[] bodySprites = {"Sprites/Body-Checkers", "Sprites/Body-Stripes", "Sprites/Body-Accessory-Shoulders"};
        string[] headSprites = {"Sprites/Head-Afro", "Sprites/Head-2", "Sprites/Head-Messy", "Sprites/Head-long"};

        //Pick a random colour for the body
        Color color = GenerateColor(colorArray);
        //Body
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Get's sprite renderer from current game object
        newSprite = Resources.Load<Sprite>("Sprites/Body-Solid"); //Loads sprite
        spriteRenderer.sprite = newSprite; //Sets new sprite to game objectusing spriteRenderer
        transform.localScale = new Vector2(1.5f, 1.5f); //Scales up sprite
        spriteRenderer.color = color;

        //Pick random body
        int randomNumber = Random.Range(0, bodySprites.Length-1);
        //Get random colour
        Color colorBody = GenerateColor(colorArray);

        //Get parentgameobject
        GameObject ParentGameObject = gameObject;
        //Get Body Acessory from parent
        GameObject bodyAcessory = ParentGameObject.transform.GetChild (0).gameObject;
        spriteRenderer = bodyAcessory.GetComponent<SpriteRenderer>();
        newSprite = Resources.Load<Sprite>(bodySprites[randomNumber]); //Loads sprite
        spriteRenderer.sprite = newSprite; //Sets new sprite to game objectusing spriteRenderer
        spriteRenderer.color = colorBody;


        //Pick random body
        randomNumber = Random.Range(0, headSprites.Length-1); //Random number within array bounds
        //Get random colour
        Color colorHead = GenerateColor(colorArray);
        GameObject head = ParentGameObject.transform.GetChild (1).gameObject; //Get head child from parent
        spriteRenderer = head.GetComponent<SpriteRenderer>(); //Get the spriteRenderer
        newSprite = Resources.Load<Sprite>(headSprites[randomNumber]); //Loads sprite
        spriteRenderer.sprite = newSprite; //Apply new sprite
        spriteRenderer.color = colorHead; //Apply new colour
    }
    private Color GenerateColor(Color[] colorArray)
    {
        int lengthOfArray = colorArray.Length;
        Color randomColor = new Color();
        int randomNumber = Random.Range(0, lengthOfArray-1);
        randomColor = colorArray[randomNumber];
        return randomColor;
    }

}
