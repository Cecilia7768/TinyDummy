using Definition;
public interface IObjectService 
{
    public float GetHealth();
    public float GetHungry();
    public float GetThirst();
    public FoodType GetFoodType();


    // 아래는 쓸일없을거 같기도
    //public void SetHealth(float health);
    //public void SetHungry(float hungry);
    //public void SetThirst(float thirst);
}
