using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName = null;

        private float m_CurrentAirPressure = 0;

        private float m_MaxAirPressure = 0;
        
        public void SetManufacturerName(string i_ManufacturerName)
        {
            m_ManufacturerName = i_ManufacturerName;
        }

        public string GetManufacturerName() 
        {
            return m_ManufacturerName;
        }

        public void SetCurrentAirPressure(float i_CurrentAirPressure)
        {
            if (i_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ArgumentException();  // זורק חריגה ללא טקסט
            }

            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public float GetCurrentAirPressure()
        {
            return m_CurrentAirPressure;
        }

        public void SetMaxAirPressure(float i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;    
        }

        public float GetMaxAirPressure()
        {
            return m_MaxAirPressure;
        }

        public void Inflate()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }
        
    }
}
