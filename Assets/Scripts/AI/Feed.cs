using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine.AI;

namespace AI
{
    public class Feed : Action
    {        

        public override TaskStatus OnUpdate()
        {
            if (GameManager.unitService != null)//&& GameManager.objectService != null)
            {  
                if (CanSeeObject.targetObject != null)
                {
                    var obj = CanSeeObject.targetObject.Value.GetComponent<ObjectService>();
                    ActionByObjectType(obj);                   
                    CanSeeObject.targetObject = null;
                    return TaskStatus.Success;
                }
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }

        /// <summary>
        /// 음식 타입에 따른 루틴
        /// </summary>
        private void ActionByObjectType(ObjectService objInfo)
        {
            FoodType foodType = objInfo.GetFoodType();

            switch (foodType)
            {
                case FoodType.Food:
                   // GameManager.unitService.SetHealth(GameManager.objectService.GetHealth());
                    GameManager.unitService.SetHungry(objInfo.GetHungry());
                    break;
                case FoodType.Drink:
                    GameManager.unitService.SetThirst(objInfo.GetThirst());
                    break;
            }
        }
    }
}