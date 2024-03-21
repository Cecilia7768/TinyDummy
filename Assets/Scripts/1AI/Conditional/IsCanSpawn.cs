using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class IsCanSpawn : Conditional
    {
        private IUnitService unitService;

        public override void OnStart()
        {
            unitService = this.transform.parent.GetComponent<IUnitService>();
        }
        public override TaskStatus OnUpdate()
        {
            if(JjackStandard.MaleAdultCount > 0 && unitService.GetGender() == GenderType.Female) 
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

    }
}