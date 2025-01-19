using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get { return m_MaxValue; }
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }

        public ValueOutOfRangeException(float i_MaxValue)
            : base(string.Format("The Value {0} is Already Maximum Level", i_MaxValue))
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = 0;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, float i_CurrentValue)
            : base(string.Format("Value {0} is out of range. Value should be between {1} and {2}",
                i_CurrentValue, i_MinValue, i_MaxValue))
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}