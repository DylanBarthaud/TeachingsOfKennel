using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public abstract class DogBase : MonoBehaviour, IHasId, ISpawnsButtons
{
    [SerializeField] protected int breedId;
    [Header("Display:")]
    [SerializeField] protected string dogName; 
    [TextArea]
    [SerializeField] protected string description;
    [SerializeField] protected Sprite sprite;
    [SerializeField] private Animator animator; 
    [Header("Stats:")]
    [SerializeField] private float baseDogFaith; 
    [SerializeField] private float baseDogSpeed;
    [SerializeField] private float baseBarkStrength;
    [SerializeField] private float baseBarkSpeed;
    protected int packId;

    protected float dogFaith;
    protected float dogSpeed;
    protected float barkStrength;
    protected float barkSpeed;

    private UiManager uiManager;

    private ButtonDataStruct buttonData; 
    private DogDataStruct dogData;

    Vector2 movePos;
    Vector3 scale;
    private SpriteRenderer spriteRenderer; 
    bool isFlipped; 

    private void Awake() {
        buttonData = new ButtonDataStruct() {
            text = dogName,
            sprite = this.sprite,
            eventParent = this,
            transformParent = null
        };
 
        dogData = new DogDataStruct()
        {
            icon = sprite,
            breed = dogName,
            description = this.description,
            faith = baseDogFaith,
            barkStrength = this.baseBarkStrength,
            barkSpeed = this.baseBarkSpeed, 
        };

        ResetStatsToBase();

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>(); 
        uiManager = UiManager.instance; 
        movePos = transform.position;
        scale = transform.localScale; 
    }

    private void Update(){
        Vector2 newMovePos = new Vector2(movePos.x, movePos.y);
        transform.position = Vector2.MoveTowards(transform.position, newMovePos, dogSpeed * Time.deltaTime);

        if(isFlipped && transform.position.x < newMovePos.x){
            transform.localScale = scale;
            isFlipped = false;
        }
        else if (!isFlipped && transform.position.x > newMovePos.x){
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            isFlipped = true;
        }

        spriteRenderer.sortingOrder = Mathf.RoundToInt(-((transform.position.y + (transform.position.x / 10)) * 100)); 

        if (animator == null){
            return; 
        }

        if(new Vector2(transform.position.x, transform.position.y) == newMovePos){
            animator.SetBool("IsMoving", false);
        }
        else { animator.SetBool("IsMoving", true); }
    }

    public void MoveDogGraphic(Vector2 movePos){
        this.movePos = movePos;
    }

    public IEnumerator StartBark(DogPack dogPack, DogPack target){
        yield return new WaitForSeconds(barkSpeed);

        if (dogPack.GetFaith() > 0){
            Bark(dogPack, target);

            if (animator != null)
            {
                animator.SetBool("IsBarking", true);
                yield return new WaitForSeconds(0.05f);
                animator.SetBool("IsBarking", false);
            }

            AudioSource audioSource = transform.GetComponent<AudioSource>();
            audioSource.Play();

        }
        dogPack.TickBarks(target);
    }
    // Abstract functions
    public abstract void Bark(DogPack dogPack, DogPack target);

    // Display dogs current stats 
    public void OnButtonClick(GameObject buttonObj){
        uiManager.SetDogStatWindow(dogData);
        uiManager.ActivateWindow(uiManager.dogStats);
    }

    // Getters
    public float GetBaseFaith(){ 
        return baseDogFaith; 
    }

    public float GetBaseSpeed(){
        return baseDogSpeed; 
    }

    public float GetBaseBarkStrength(){
        return baseBarkStrength;
    }

    public float GetBaseBarkSpeed(){
        return baseBarkSpeed; 
    }

    public float GetBarkSpeed(){
        return barkSpeed;
    }

    public int GetBreedId(){
        return breedId; 
    }
   
    public int GetId(){
        return packId;
    }

    public Sprite GetSprite(){
        return sprite;
    }

    public ButtonDataStruct GetButtonData() { 
        return buttonData;
    }

    //Setters
    public void SetId(int id){
        packId = id;
    }

    public void SetBarkSpeed(float x){
        barkSpeed = x; 
    }

    public void AddToBarkSpeed(float x){
        barkSpeed -= x; 
    }

    public void SetBarkStrength(float x){  
        barkStrength = x;
    }

    public void AddToBarkStrength(float x){
        barkStrength += x; 
    }

    public void SetFaith(float x){
        dogFaith = x; 
    }

    public void AddToFaith(float x){
        dogFaith += x; 
    }

    public void ResetStatsToBase(){
        dogFaith = baseDogFaith;
        dogSpeed = baseDogSpeed;
        barkStrength = baseBarkStrength;
        barkSpeed = baseBarkSpeed;
    }

    public void SetScale(Vector3 scale){
        this.scale = scale;
        transform.localScale = scale; 
    }
}

