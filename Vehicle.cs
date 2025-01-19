using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        // כל מימושי המתודה האלה התקימו במחלקה היורשת
        protected string m_ModelName = null;

        protected string m_PlateNumber = null;

        protected float m_EnergyLevel = 0;

        protected Wheel[] m_WheelsArray = null;

        protected string m_VehicleOwner = null;

        protected string m_OwnerPhoneNumber = null;

        private int m_WheelsAmount = 0;

        protected ArrayList m_Menu = new ArrayList();

        protected Dictionary<int, string> m_Questions = new Dictionary<int, string>();

        public abstract Dictionary<int, string> GetQuestions();

        public abstract bool AnswerQuestion(int i_QuestionId, float i_Answer);

        protected abstract void SetQuestions();

        public abstract ArrayList GetArrayListMenu();

        protected abstract void SetArrayListMenu();

        protected Vehicle()
        {
            SetQuestions();
        }

        public void SetModelName(string i_ModelName)
        {
            m_ModelName = i_ModelName;
        }

        public void SetOwnerNumber(string i_OwnerPhoneNumber)
        {
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public string GetOwnerNumber()
        {
            return m_OwnerPhoneNumber;
        }

        public void SetPlateNumber(string i_PlateNumber)
        {
            if (!IsValidSetNumber(i_PlateNumber))
            {
                throw new Exception("The number you entered is invalid. Please make sure the vehicle number contains only digits and is between 1 and 10 characters long");
            }
            m_PlateNumber = i_PlateNumber;
        }

        public bool IsValidSetNumber(string i_PlateNumber)
        {
            bool isValidSetNumber = true;

            if (string.IsNullOrEmpty(i_PlateNumber))
            {
                isValidSetNumber = false;
            }

            if (i_PlateNumber.Length < 1 || i_PlateNumber.Length > 10)
            {
                isValidSetNumber = false;
            }

            foreach (char c in i_PlateNumber)
            {
                if (!char.IsDigit(c))
                {
                    isValidSetNumber = false;
                    break;
                }
            }

            return isValidSetNumber;
        }

        public void SetWheelsAmount(int i_WheelsAmount)
        {
            m_WheelsAmount = i_WheelsAmount;
        }

        public void SetVehicleOwner(string i_VehicleOwner)
        {
            m_VehicleOwner = i_VehicleOwner;
        }

        public void SetEnergyLevel(float i_EnergyLevel)
        {
            if (! (i_EnergyLevel > 0 && i_EnergyLevel <= 100))
            {
                throw new Exception();
            }
            m_EnergyLevel = i_EnergyLevel;
        }

        public string GetPlateNumber()
        {
            return m_PlateNumber;   
        
        }

        public string GetModelName()
        {
            return m_ModelName;
        }

        public string GetVehicleOwner()
        {
            return m_VehicleOwner;
        }

        public Wheel[] GetWheelsArray()
        {
            return m_WheelsArray;
        }

        public void SetWheelsAtOnes(string i_ManufacturerName,float i_CurrentAirPressure)
        {
            if (m_WheelsArray[0].GetMaxAirPressure() < i_CurrentAirPressure)
            {
                throw new Exception("The air pressure you entered exceeds the maximum allowed pressure for the wheel. Please enter a valid value");
            }

            for (int i = 0; i < m_WheelsAmount; i++)
            {
                m_WheelsArray[i].SetCurrentAirPressure(i_CurrentAirPressure);

                m_WheelsArray[i].SetManufacturerName(i_ManufacturerName);
            }
        }

        public void InflateTiresToMax()
        {
            for (int i = 0; i < m_WheelsAmount; i++)
            {
                m_WheelsArray[i].Inflate();
            }
        }

        public void SetMaxAirPressure(float i_MaxAirPressure)
        {
            m_WheelsArray = new Wheel[m_WheelsAmount];

            for (int i = 0; i < m_WheelsAmount; i++)
            {
                m_WheelsArray[i] = new Wheel();  
                m_WheelsArray[i].SetMaxAirPressure(i_MaxAirPressure);
            }
        }

    }
}
