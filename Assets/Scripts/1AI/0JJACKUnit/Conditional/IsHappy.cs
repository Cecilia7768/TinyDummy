using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class IsHappy : Conditional
    {
        private IUnitService unitService;

        public override void OnStart()
        {
            unitService = this.transform.parent.GetComponent<IUnitService>();
        }

        /// <summary>
        /// �ູ�� �����ϸ� ��� ����
        /// </summary>
        /// <returns></returns>
        public override TaskStatus OnUpdate()
        {
            return unitService.GetHappiness() >= 100? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}