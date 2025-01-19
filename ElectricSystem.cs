using System;

namespace Ex03.GarageLogic
{
    public class ElectricSystem
    {
        private float m_RemainingBatteryTimeHours = 0;

        private float m_MaxBatteryTimeHours = 0;

        public void SetRemainingBatteryTimeHours(float i_RemainingBatteryTimeHours)
        {
            m_RemainingBatteryTimeHours = i_RemainingBatteryTimeHours;
        }

        public float GetRemainingBatteryTimeHours()
        { 
            return m_RemainingBatteryTimeHours;
        }



        public void SetMaxBatteryTimeHours(float i_MaxBatteryTimeHours)
        {
            m_MaxBatteryTimeHours = i_MaxBatteryTimeHours;
        }

        public float GetMaxBatteryTimeHours()
        {
            return m_MaxBatteryTimeHours;
        }

        public void Charging(float i_ChargingDurationHours)
        {
            if (i_ChargingDurationHours > m_MaxBatteryTimeHours - m_RemainingBatteryTimeHours)
            {
                throw new Exception("You are trying to charge more than the allowed capacity. Please enter a valid charge amount."); // זריקת שגיאה 
            }

            m_RemainingBatteryTimeHours += i_ChargingDurationHours;
        }
    }
}
