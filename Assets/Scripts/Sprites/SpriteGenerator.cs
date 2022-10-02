using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;   
    void Start()
    {
        //Create array of colours to pick from
        Color[] colorArray = new Color[6];
        colorArray[0] = new Color (0.83f, 0.26f, 0.26f); //Red
        colorArray[1] = new Color (1.0f, 0.7f, 0.0f); //Yellow
        colorArray[2] = new Color (0.56f, 0.41f, 0.83f); 
        colorArray[3] = new Color (0.29f, 0.69f, 0.87f);
        colorArray[4] = new Color (0.62f, 0.83f, 0.31f);
        colorArray[5] = new Color (1.0f, 1.0f, 1.0f); 

        //Create arrays with paths for all relevant sprites.
        string[] bodySprites = {"Sprites/Body-Checker", "Sprites/Body-Stripes", "Sprites/Body-Accessory-Shoulders"};
        string[] headSprites = {"Sprites/Head-Afro", "Sprites/Head-2", "Sprites/Head-Messy", "Sprites/Head-long"};
        string[] armSprites = {"Sprites/Arms", "Sprites/Arms-Checker1", "Sprites/Arms-Checker2", "Sprites/Arms-Stripes1", "Sprites/Arms-Stripes2", "Sprites/Arms-Gloves"};
        //Pick a random colour for the body
        Color color = GenerateColor(colorArray);
        //Body
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Get's sprite renderer from current game object
        newSprite = Resources.Load<Sprite>("Sprites/Body-Solid"); //Loads sprite
        spriteRenderer.sprite = newSprite; //Sets new sprite to game objectusing spriteRenderer
        transform.localScale = new Vector2(1.5f, 1.5f); //Scales up sprite
        spriteRenderer.color = color;
        //Every enemy has a solid body with a body acessory overlayed onto it

        //BODY ACCESSORY:
        //Pick random body
        int bodyRandomNumber = Random.Range(0, bodySprites.Length);
        //Get random colour
        Color colorBody = GenerateColor(colorArray);

        //Get parentgameobject
        GameObject ParentGameObject = gameObject;
        //Get Body Acessory from parent
        GameObject bodyAcessory = ParentGameObject.transform.GetChild (0).gameObject;
        spriteRenderer = bodyAcessory.GetComponent<SpriteRenderer>();
        newSprite = Resources.Load<Sprite>(bodySprites[bodyRandomNumber]); //Loads sprite
        spriteRenderer.sprite = newSprite; //Sets new sprite to game objectusing spriteRenderer
        spriteRenderer.color = colorBody;

        //HEAD
        //Pick random head
        int randomNumber = Random.Range(0, headSprites.Length); //Random number within array bounds
        //Get random colour
        Color colorHead = GenerateColor(colorArray);
        GameObject head = ParentGameObject.transform.GetChild (1).gameObject; //Get head child from parent
        spriteRenderer = head.GetComponent<SpriteRenderer>(); //Get the spriteRenderer
        newSprite = Resources.Load<Sprite>(headSprites[randomNumber]); //Loads sprite
        spriteRenderer.sprite = newSprite; //Apply new sprite
        spriteRenderer.color = colorHead; //Apply new colour

        //ARMS
        //1/4 chance to have normal arms, otherwise give arms that correspond to body accessory
        randomNumber = Random.Range(0, 2);
        GameObject arm = ParentGameObject.transform.GetChild (2).gameObject;
        spriteRenderer = arm.GetComponent<SpriteRenderer>();
        if (randomNumber == 0)
        {
            newSprite = Resources.Load<Sprite>(armSprites[0]); //Loads normal arm sprite
            spriteRenderer.sprite = newSprite; //Apply new sprite
            applyGloves(ParentGameObject, armSprites, color, colorBody);
        }
        else
        {
            if (bodyRandomNumber == 0) //Body is checkers
            {
                applyArms(ParentGameObject, armSprites, color, colorBody, 1); //1 is the position of the first checkered arm sprite in armSprites
            }
            else if (bodyRandomNumber == 1) 
            {
                applyArms(ParentGameObject, armSprites, color, colorBody, 3);
            }
            else if (bodyRandomNumber == 2) //The little shoulder accessorys
            {
                randomNumber = Random.Range(0, 2); //Since there is no pattern we can use any arm sprite
                if (randomNumber == 0)
                {
                    applyArms(ParentGameObject, armSprites, color, colorBody, 1);
                }
                else if (randomNumber == 1)
                {
                    applyArms(ParentGameObject, armSprites, color, colorBody, 3);
                }
                else
                {
                    newSprite = Resources.Load<Sprite>(armSprites[0]); //Loads normal arm sprite
                    spriteRenderer.sprite = newSprite; //Apply new sprite
                    applyGloves(ParentGameObject, armSprites, color, colorBody);
                }
            }
        }

        GameObject nose = ParentGameObject.transform.GetChild (5).gameObject;
        Color noseColour = GenerateColor(colorArray);
        spriteRenderer = nose.GetComponent<SpriteRenderer>();
        spriteRenderer.color = noseColour; //Apply new colour
    }
    private Color GenerateColor(Color[] colorArray)
    {
        int lengthOfArray = colorArray.Length;
        Color randomColor = new Color();
        int randomNumber = Random.Range(0, lengthOfArray);
        randomColor = colorArray[randomNumber];
        return randomColor;
    }

    private void applyArms(GameObject ParentGameObject, string[] armSprites, Color color, Color colorBody, int num)
    {
        newSprite = Resources.Load<Sprite>(armSprites[num]); //Num is the position of the desired arm sprite in the array. 
        GameObject arm = ParentGameObject.transform.GetChild (2).gameObject;
        spriteRenderer = arm.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite; //Apply new sprite
        spriteRenderer.color = color; //Apply new colour

        newSprite = Resources.Load<Sprite>(armSprites[num+1]); //Num+1 to get to the second half of the desired arm sprite
        arm = ParentGameObject.transform.GetChild (3).gameObject;
        spriteRenderer = arm.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite; //Apply new sprite
        spriteRenderer.color = colorBody; //Apply new colour

        applyGloves(ParentGameObject, armSprites, color, colorBody);
    }
    private void applyGloves(GameObject ParentGameObject, string[] armSprites, Color color, Color colorBody)
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0) //Apply gloves
        {
            newSprite = Resources.Load<Sprite>(armSprites[5]); 
            GameObject arm = ParentGameObject.transform.GetChild (4).gameObject; //Get glove object
            spriteRenderer = arm.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = newSprite; //Apply new sprite
            

            randomNumber = Random.Range(0, 1);
            if (randomNumber == 0) //Pick random colour for the glove while staying consistent with body colours
            {
                spriteRenderer.color = colorBody; //Apply new colour
            }
            else
            {
                spriteRenderer.color = color; //Apply new colour
            }
        }
    }
}

