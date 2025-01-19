using System.Collections;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private FuelSystem m_FuelSystem = null;

        private bool m_IsRefrigerated = false;

        private float m_CargoVolume = 0;

        public Truck(string i_PlateNumber)
        {
            m_FuelSystem = new FuelSystem();

            SetFuelType(eFuelType.Soler);

            SetMaxFuelLevel(125.0f);

            SetWheelsAmount(14);

            SetMaxAirPressure(29.0f);

            SetPlateNumber(i_PlateNumber);

            SetArrayListMenu();

        }

        public float GetMaxFuelLevel()
        {
            return m_FuelSystem.GetMaxFuelLevel();
        }
        public void SetIsRefrigerated(bool i_IsRefrigerated)
        {
            m_IsRefrigerated = i_IsRefrigerated;
        }

        public void SetCargoVolume(float i_CargoVolume)
        {
            m_CargoVolume = i_CargoVolume;
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

        public bool GetIsRefrigerated()
        {
            return m_IsRefrigerated;
        }

        public float GetCargoVolume()
        {
            return m_CargoVolume;
        }

        public float GetCurrentFuelLevel()
        {
            return m_FuelSystem.GetCurrentFuelLevel();
        }

        public eFuelType GetFuelType()
        {
            return m_FuelSystem.GetFuelType();
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
            m_Menu.Add("Vehicle status : "); // עד פה לכל כלי רכב
            m_Menu.Add("Current fuel level : ");
            m_Menu.Add("Fuel Type : ");
            m_Menu.Add("Is refrigrated : ");
            m_Menu.Add("Cargo volume : ");
        }

        public override ArrayList GetArrayListMenu()
        {
            return m_Menu;
        }

        protected override void SetQuestions()
        {
            m_Questions.Add(1, "What is the current fuel level in your vehicle?");

            m_Questions.Add(2, "Does the truck have refrigeration?\n Yes press: 1\n No Press: 0");

            m_Questions.Add(3, "Please Enter Truck Cargo Volume");
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

                    if (isRefrigerated((int)i_Answer))
                    {
                        isCorrectAnswer = true;
                    }

                    break;

                case 3:

                    if (checkCurrentCargoVolume(i_Answer))
                    {
                        isCorrectAnswer = true;
                    }

                    break;
            }

            return isCorrectAnswer;
        }

        private bool checkCurrentFuelLevel(float i_Answer)
        {
            const bool v_IsValid = true;

            if (i_Answer < 0 || i_Answer > m_FuelSystem.GetMaxFuelLevel())
            {
                throw new ValueOutOfRangeException(0, m_FuelSystem.GetMaxFuelLevel(), i_Answer);
            }

            return v_IsValid;
        }

        private bool checkCurrentCargoVolume(float i_Answer)
        {
            bool isValid = false;

            if (i_Answer >= 0)
            {
                m_CargoVolume = i_Answer;
                isValid = true;
            }

            return isValid;
        }

        private bool isRefrigerated(int i_Answer)
        {
            bool isValid = false;

            if (i_Answer == 0)
            {
                m_IsRefrigerated = false;
                isValid = true;
            }

            if (i_Answer == 1)
            {
                m_IsRefrigerated = true;
                isValid = true;
            }


            return isValid;
        }


    }
}
