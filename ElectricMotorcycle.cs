using System;
using System.Collections;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Vehicle
    {
        private ElectricSystem m_ElectricSystem = null;

        private Motorcycle m_Motorcycle = null;

        public ElectricMotorcycle(string i_PlateNumber)
        {
            m_ElectricSystem = new ElectricSystem();

            m_Motorcycle = new Motorcycle();

            SetMaxBatteryTimeHours(2.9f);

            SetWheelsAmount(2);

            SetMaxAirPressure(32.0f);

            SetPlateNumber(i_PlateNumber);

            SetArrayListMenu();

        }

        public float GetMaxBatteryTimeHours()
        {
            return m_ElectricSystem.GetMaxBatteryTimeHours();
        }

        public void SetMaxBatteryTimeHours(float i_MaxBatteryTimeHours)
        {
            m_ElectricSystem.SetMaxBatteryTimeHours(i_MaxBatteryTimeHours);
        }

        public void SetRemainingBatteryTimeHours(float i_RemainingBatteryTimeHours)
        {
            m_ElectricSystem.SetRemainingBatteryTimeHours(i_RemainingBatteryTimeHours);
        }

        public void SetLicenseType(eMotorcycleLicenseType i_MotorcycleLicenseType)
        {
            m_Motorcycle.SetLicenseType(i_MotorcycleLicenseType);
        }

        public void SetEngineVolume(int i_EngineVolume)
        {
            m_Motorcycle.SetEngineVolume(i_EngineVolume);
        }

        public float GetRemainingBatteryTimeHours()
        {
            return m_ElectricSystem.GetRemainingBatteryTimeHours();
        }

        public eMotorcycleLicenseType GetLicenseType()
        {
            return m_Motorcycle.GetLicenseType();
        }

        public int GetEngineVolume()
        {
            return m_Motorcycle.GetEngineVolume();
        }

        public void ChargeBattery(float i_ChargingDurationHours)
        {
            m_ElectricSystem.Charging(i_ChargingDurationHours);
        }

        protected override void SetArrayListMenu()
        {
            m_Menu.Add("Plate number :");
            m_Menu.Add("Model name :");
            m_Menu.Add("Owner name : ");
            m_Menu.Add("Phone number : ");
            m_Menu.Add("Manufacturer name : ");
            m_Menu.Add("Current air pressure : ");
            m_Menu.Add("Max air pressure : ");
            m_Menu.Add("Vehicle status : "); // עד פה לכל כלי רכב
            m_Menu.Add("Remaining battery : ");
            m_Menu.Add("License type : ");
            m_Menu.Add("Engine volume : ");
        }

        public override ArrayList GetArrayListMenu()
        {
            return m_Menu;
        }

        protected override void SetQuestions()
        {
            m_Questions.Add(1, "What is the current battery time in hours in your Motor ?");

            m_Questions.Add(2, "What is your license type? \n" +
                               "Press 0: A1\n" +
                               "Press 1: A2\n" +
                               "Press 2: B1\n" +
                               "Press 3: B2\n");

            m_Questions.Add(3, "What is your engine volume?");

        }

        public override Dictionary<int, string> GetQuestions()
        {
            return this.m_Questions;
        }

        public override bool AnswerQuestion(int i_QuestionId, float i_Answer)
        {
            bool isCorrectAnswer = false;

            switch (i_QuestionId)
            {
                case 1:

                    if (checkCurrentBatteryLevel(i_Answer))
                    {
                        m_ElectricSystem.SetRemainingBatteryTimeHours(i_Answer);
                        isCorrectAnswer = true;
                    }

                    break;

                case 2:

                    if (checkLicenseType((int)i_Answer))
                    {
                        SetLicenseType((eMotorcycleLicenseType)(int)i_Answer);
                        isCorrectAnswer = true;
                    }

                    break;

                case 3:
                    if (i_Answer >= 50)
                    {
                        SetEngineVolume((int)i_Answer);
                        isCorrectAnswer = true;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, 1200, i_Answer); //להוסיף ערך מקסימלי לרכב מסוג אופנוע
                    }
                    break;
            }

            return isCorrectAnswer;


        }

        private bool checkCurrentBatteryLevel(float i_Answer)
        {
            const bool v_IsValid = true;

            if (i_Answer < 0 || i_Answer > m_ElectricSystem.GetMaxBatteryTimeHours())
            {
                throw new ValueOutOfRangeException(0, m_ElectricSystem.GetMaxBatteryTimeHours(), i_Answer);
            }

            return v_IsValid;
        }

        private bool checkLicenseType(int i_Answer)
        {
            bool isValid = Enum.IsDefined(typeof(eMotorcycleLicenseType), i_Answer);
            return isValid;
        }
    }



}

