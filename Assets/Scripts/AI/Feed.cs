using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Definition;
using UnityEngine;

namespace AI
{
    public class Feed : Action
    {
        private IUnitService unitService;

        public override void OnStart()
        {
            unitService = this.gameObject.GetComponent<IUnitService>();
        }
        public override TaskStatus OnUpdate()
        {
            if (unitService != null)
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
                    CanSeeObject.targetObject.Value.SetActive(false);
                    unitService.SetHungry(objInfo.GetHungry());
                    break;
                case FoodType.Drink:
                    unitService.SetThirst(objInfo.GetThirst());
                    break;
            }
        }
    }
}