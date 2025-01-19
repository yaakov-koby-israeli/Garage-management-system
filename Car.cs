namespace Ex03.GarageLogic
{
    public class Car
    {
        private eColorType m_ColorType = 0;

        private eDoorAmount m_DoorAmount = 0;

        public eColorType GetColorType()
        {
            return m_ColorType;
        }

        public eDoorAmount GetDoorAmount()
        {
            return m_DoorAmount;
        }

        public void SetColorType(eColorType i_ColorType)
        {
            m_ColorType = i_ColorType;
        }

        public void SetDoorAmount(eDoorAmount i_DoorAmount)
        {
            m_DoorAmount = i_DoorAmount;
        }
    }
}
