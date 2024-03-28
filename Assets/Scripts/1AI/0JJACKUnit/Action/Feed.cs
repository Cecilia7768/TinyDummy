using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class Feed : Action
    {
        private IUnitService unitService;
        private NavMeshAgent agent;

        public override void OnStart()
        {
            unitService = this.transform.parent.GetComponent<IUnitService>();
            agent = GetComponent<NavMeshAgent>();
        }
        public override TaskStatus OnUpdate()
        {
            if (unitService != null)
            {
                if (CanSeeObject.targetObject != null)
                {
                    GameObject tartget;
                    if (CanSeeObject.targetObject.Value.tag == "Food")
                        tartget = CanSeeObject.targetObject.Value;
                    else
                        tartget = CanSeeObject.targetObject.Value.transform.parent.gameObject;

                    var obj = tartget.GetComponent<ObjectService>();
                    if (obj != null)
                    {
                        ActionByObjectType(obj);
                        CanSeeObject.targetObject = null;
                        agent.isStopped = false;
                        return TaskStatus.Success;
                    }
                }
               // return TaskStatus.Success;
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