using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class IsHungry : Conditional
    {
        private IUnitService unitService;

        public override void OnStart()
        {
            unitService = this.transform.parent.GetComponent<IUnitService>();
        }

        /// <summary>
        /// ������� �Ա�
        /// </summary>
        /// <returns></returns>
        public override TaskStatus OnUpdate()
        {
            return unitService.GetHungry() < 80 ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}