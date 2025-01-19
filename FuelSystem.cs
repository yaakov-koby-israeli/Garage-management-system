using System;

namespace Ex03.GarageLogic
{
    public class FuelSystem 
    {
        private eFuelType e_FuelType; 

        private float m_CurrentFuelLevel = 0;

        private float m_MaxFuelLevel = 0;

        public void SetFuelType(eFuelType i_FuelType)
        {
            e_FuelType = i_FuelType;
        }

        public eFuelType GetFuelType()
        {
            return e_FuelType;
        }

        public void SetCurrentFuelLevel(float i_CurrentFuelLevel)
        {
            m_CurrentFuelLevel = i_CurrentFuelLevel;
        }

        public float GetCurrentFuelLevel()
        {
            return m_CurrentFuelLevel;
        }

        public void SetMaxFuelLevel(float i_MaxFuelLevel)
        {
            m_MaxFuelLevel = i_MaxFuelLevel;
        }
        public float GetMaxFuelLevel()
        {
            return m_MaxFuelLevel;
        }

        public void Refuel(float i_Refuel)
        {
            if (i_Refuel > m_MaxFuelLevel - m_CurrentFuelLevel)
            {
                throw new Exception("You are trying to refuel more than the allowed capacity."); // זריקת שגיאה 
            }
            
            m_CurrentFuelLevel+= i_Refuel;
        }
    }
}
