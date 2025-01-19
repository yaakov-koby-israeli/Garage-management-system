using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Vehicle
    {
        private ElectricSystem m_ElectricSystem = null;

        private Car m_Car = null;

        public ElectricCar(string i_PlateNumber)
        {
            m_ElectricSystem = new ElectricSystem();

            m_Car = new Car();

            SetMaxBatteryTimeHours(5.4f);

            SetWheelsAmount(5);

            SetMaxAirPressure(34.0f);

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

        public void SetColorType(eColorType i_ColorType)
        {
            m_Car.SetColorType(i_ColorType);
        }

        public void SetDoorAmount(eDoorAmount i_DoorAmount)
        {
            m_Car.SetDoorAmount(i_DoorAmount);
        }

        public void SetRemainingBatteryTimeHours(float i_RemainingBatteryTimeHours)
        {
            m_ElectricSystem.SetRemainingBatteryTimeHours(i_RemainingBatteryTimeHours);
        }

        public float GetRemainingBatteryTimeHours()
        {
            return m_ElectricSystem.GetRemainingBatteryTimeHours();
        }

        public eColorType GetColorType()
        {
            return m_Car.GetColorType();
        }

        public eDoorAmount GetDoorAmount()
        {
            return m_Car.GetDoorAmount();
        }

        public void ChargeBattery(float i_ChargingDurationHours)
        {
            m_ElectricSystem.Charging(i_ChargingDurationHours); 
        }

        protected override void SetArrayListMenu()
        {
            m_Menu.Add("Plate number :");
            m_Menu.Add("Model name :" );
            m_Menu.Add("Owner name : ");
            m_Menu.Add("Phone number : ");
            m_Menu.Add("Manufacturer name : ");
            m_Menu.Add("Current air pressure : ");
            m_Menu.Add("Max air pressure : ");
            m_Menu.Add("Vehicle status : "); // עד פה לכל כלי רכב
            m_Menu.Add("Remaining battery : ");
            m_Menu.Add("Color type : ");
            m_Menu.Add("Door amount : ");
        }

        public override ArrayList GetArrayListMenu()
        {
            return m_Menu;
        }

        protected override void SetQuestions()
        {
            this.m_Questions.Add(1, "Enter car color: for Blue Enter: 0, Black Enter: 1, White Enter: 2, Gray Enter: 3:");

            this.m_Questions.Add(2, "Enter number of doors (2-5):");

            this.m_Questions.Add(3, "What is the current battery time in hours in your vehicle?");
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
                        SetDoorAmount((eDoorAmount)(int)i_Answer);
                        isCorrectAnswer = true;
                    }

                    break;
                case 3:

                    if (checkCurrentBatteryLevel(i_Answer))
                    {
                        m_ElectricSystem.SetRemainingBatteryTimeHours(i_Answer);
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

        private bool checkCurrentBatteryLevel(float i_Answer)
        {
            const bool v_IsValid = true;

            if (i_Answer < 0 || i_Answer > m_ElectricSystem.GetMaxBatteryTimeHours())
            {
                throw new ValueOutOfRangeException(0, m_ElectricSystem.GetMaxBatteryTimeHours(), i_Answer);
            }

            return v_IsValid;
        }
    }
}
