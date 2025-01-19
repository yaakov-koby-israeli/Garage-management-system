using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Vehicle
    {
        private FuelSystem m_FuelSystem = null;

        private Motorcycle m_Motorcycle = null;


        public FuelMotorcycle(string i_PlateNumber)
        {
            m_FuelSystem = new FuelSystem();

            m_Motorcycle = new Motorcycle();

            SetFuelType(eFuelType.Octan98);

            SetMaxFuelLevel(6.2f);

            SetWheelsAmount(2);

            SetMaxAirPressure(32.0f);

            SetPlateNumber(i_PlateNumber);

            SetArrayListMenu();


        }

        public float GetMaxFuelLevel()
        {
            return m_FuelSystem.GetMaxFuelLevel();
        }

        public void SetFuelType(eFuelType i_FuelType)
        {
            m_FuelSystem.SetFuelType(i_FuelType);
        }

        public void SetMaxFuelLevel(float i_MaxFuelLevel)
        {
            m_FuelSystem.SetMaxFuelLevel(i_MaxFuelLevel);
        }

        public void SetCurrentFuelLevel(float i_CurrentFuelLevel)
        {
            m_FuelSystem.SetCurrentFuelLevel(i_CurrentFuelLevel);
        }

        public void SetLicenseType(eMotorcycleLicenseType i_MotorcycleLicenseType)
        {
            m_Motorcycle.SetLicenseType(i_MotorcycleLicenseType);
        }

        public void SetEngineVolume(int i_EngineVolume)
        {
            m_Motorcycle.SetEngineVolume(i_EngineVolume);
        }

        public float GetCurrentFuelLevel()
        {
            return m_FuelSystem.GetCurrentFuelLevel();
        }

        public eFuelType GetFuelType()
        {
            return m_FuelSystem.GetFuelType();
        }

        public eMotorcycleLicenseType GetLicenseType()
        {
            return m_Motorcycle.GetLicenseType();
        }

        public int GetEngineVolume()
        {
            return m_Motorcycle.GetEngineVolume();
        }

        public void Refuel(float i_Refuel)
        {
            m_FuelSystem.Refuel(i_Refuel);
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
            m_Menu.Add("Vehicle status : "); 
            m_Menu.Add("Current fuel level : ");
            m_Menu.Add("Fuel type : ");
            m_Menu.Add("License type : ");
            m_Menu.Add("Engine volume : ");
        }

        public override ArrayList GetArrayListMenu()
        {
            return m_Menu;
        }

        protected override void SetQuestions()
        {
            m_Questions.Add(1, "What is the current fuel level in your vehicle?");

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

                    if (checkCurrentFuelLevel(i_Answer))
                    {
                        m_FuelSystem.SetCurrentFuelLevel(i_Answer);
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
                    break;
            }

            return isCorrectAnswer;

        }

        private bool checkCurrentFuelLevel(float i_Answer)
        {
            const bool isValid = true;

            if (i_Answer < 0 || i_Answer > m_FuelSystem.GetMaxFuelLevel())
            {
                throw new ValueOutOfRangeException(0, m_FuelSystem.GetMaxFuelLevel(), i_Answer);
            }

            return isValid;
            
        }

        private bool checkLicenseType(int i_Answer)
        {
            bool isValid = Enum.IsDefined(typeof(eMotorcycleLicenseType), i_Answer);
            return isValid;
        }

    }


}
