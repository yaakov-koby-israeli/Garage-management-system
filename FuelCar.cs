using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelCar : Vehicle
    {
        private FuelSystem m_FuelSystem = null;

        private Car m_Car = null;

        public FuelCar(string i_PlateNumber)
        {
            m_FuelSystem = new FuelSystem();

            m_Car = new Car();

            SetFuelType(eFuelType.Octan95);

            SetMaxFuelLevel(52.0f);

            SetWheelsAmount(5);

            SetMaxAirPressure(34.0f);

            SetPlateNumber(i_PlateNumber);

            SetArrayListMenu();
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

        public void SetColorType(eColorType i_ColorType)
        {
            m_Car.SetColorType(i_ColorType);
        }

        public void SetDoorAmount(eDoorAmount i_DoorAmount)
        {
            m_Car.SetDoorAmount(i_DoorAmount);
        }

        public void Refuel(float i_Refuel)
        {

            m_FuelSystem.Refuel(i_Refuel);
        }

        public float GetCurrentFuelLevel()
        {
            return m_FuelSystem.GetCurrentFuelLevel();
        }

        public float GetMaxFuelLevel()
        {
            return m_FuelSystem.GetMaxFuelLevel();
        }

        public eFuelType GetFuelType()
        {
            return m_FuelSystem.GetFuelType();
        }

        public eColorType GetColorType()
        {
            return m_Car.GetColorType();
        }

        public eDoorAmount GetDoorAmount()
        {
            return m_Car.GetDoorAmount();
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
            m_Menu.Add("Fuel type : ");
            m_Menu.Add("Color type : ");
            m_Menu.Add("Door amount : ");
        }

        public override ArrayList GetArrayListMenu()
        {
            return m_Menu;
        }

        protected override void SetQuestions()
        {
            // הוספת שאלות ספציפיות למכונית דלק
            this.m_Questions.Add(1, "Enter car color: for; Blue Enter 0, Black Enter 1, White Enter 2, Gray Enter 3,");

            this.m_Questions.Add(2, "Enter number of doors (2-5):");

            this.m_Questions.Add(3, "Enter current fuel level:");

        }

        public override bool AnswerQuestion(int i_QuestionId, float i_Answer)
        {
            bool isCorrectAnswer = false;

            switch (i_QuestionId)
            {
                case 1:

                    if (checkColorCar((int)i_Answer))
                    {
                        SetColorType((eColorType)(int)i_Answer);
                        isCorrectAnswer = true;
                    }

                    break;

                case 2:

                    if (checkDoorAmount((int)i_Answer))
                    {
                        SetColorType((eColorType)(int)i_Answer);
                        isCorrectAnswer = true;
                    }

                    break;
                case 3:

                    if (checkCurrentFuelLevel(i_Answer))
                    {
                        m_FuelSystem.SetCurrentFuelLevel(i_Answer);
                        isCorrectAnswer = true;
                    }

                    break;
            }

            return isCorrectAnswer;
        }

        public override Dictionary<int, string> GetQuestions()
        {
            return this.m_Questions;
        }

        private bool checkColorCar(int i_Answer)
        {
            bool isValid = Enum.IsDefined(typeof(eColorType), i_Answer);
            return isValid;
        }

        private bool checkDoorAmount(int i_Answer)
        {
            bool isValid = Enum.IsDefined(typeof(eDoorAmount), i_Answer);
            return isValid;
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
    }
}
