using Ex03.GarageLogic;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        public Garage m_MyGarage;

        VehicleFactory m_MyFactory;

        public UserInterface()
        {
            m_MyGarage = new Garage();

            m_MyFactory = new VehicleFactory();

        }

        public void WelcomeToGarage()
        {
            Console.WriteLine("Welcome to Koby and Mor Garage Management system !");

            Console.WriteLine();

            Console.WriteLine("Press Q to Exit any key to continue.");

            string userExit = Console.ReadLine();

            while (userExit.ToLower() != "q")
            {
                MainMenu();

                Console.WriteLine("Press Q to Exit or any other key to return to menu.");

                userExit = Console.ReadLine();

                Console.Clear();

            }
        }

        public void MainMenu()
        {
            bool isCorrectInput = false;

            int userChoice = 0;

            while (!isCorrectInput)
            {
                Console.WriteLine("=================Menu=================");
                Console.WriteLine("Press 1: To Enter A New Car");
                Console.WriteLine("Press 2: To Show Car List By License Plate");
                Console.WriteLine("Press 3: To Change Car Status");
                Console.WriteLine("Press 4: To Inflate Tire To Max");
                Console.WriteLine("Press 5: To Fuel Vehicle Powered By Gasoline");
                Console.WriteLine("Press 6: To Charge Electric Vehicle");
                Console.WriteLine("Press 7: To Show All Car Detail");
                Console.WriteLine("=======================================");

                string userInput = Console.ReadLine();

                try
                {
                    userChoice = int.Parse(userInput);

                    if (userChoice >= 1 && userChoice <= 7)
                    {
                        isCorrectInput = true;
                    }
                    else
                    {
                        Console.Clear();

                        Console.WriteLine("Error: Please enter a number between 1 and 7.");

                        Console.WriteLine();
                    }
                }

                catch (FormatException)
                {

                    Console.Clear();

                    Console.WriteLine("Error: Input is not a valid number.");

                    Console.WriteLine();

                }
                catch (ArgumentNullException)
                {

                    Console.Clear();

                    Console.WriteLine("Error: No input was provided.");

                    Console.WriteLine();

                }
                catch (OverflowException)
                {

                    Console.Clear();

                    Console.WriteLine("Error: The number is too large or too small.");

                    Console.WriteLine();

                }
            }

            FuncByUserChoiceInvoker(userChoice);

        }

        public void FuncByUserChoiceInvoker(int i_UserChoice)
        {
            switch (i_UserChoice)
            {
                case 1:
                    {
                        AddNewVehicle();

                        break;
                    }

                case 2:
                    {
                        GetVehicleList();

                        break;
                    }

                case 3:
                    {
                        ChangeVehicleStatus();

                        break;
                    }

                case 4:
                    {
                        InflateWheels();

                        break;
                    }

                case 5:
                    {
                        RefuelVehicle();

                        break;
                    }

                case 6:
                    {
                        ChargeElectricVehicle();

                        break;
                    }

                case 7:
                    {
                        ShowVehicleDetails();

                        break;
                    }
            }
        }

        public void AddNewVehicle()
        {
            string userVehicleLicensePlate = null;

            Console.Clear();
            Console.WriteLine("=============== Add New Vehicle =================");
            Console.WriteLine();

            Console.WriteLine("Please Enter Car License Plate:");

            userVehicleLicensePlate = Console.ReadLine();

            ValidateUserVehicleLicensePlateInput(ref userVehicleLicensePlate);

            Console.WriteLine($"Your Car Plate Is:{userVehicleLicensePlate}");

            Console.WriteLine();

            try
            {
                if (m_MyGarage.IsServicedBefore(userVehicleLicensePlate))
                {
                    Console.Clear();
                    Console.WriteLine($"The vehicle with license plate {userVehicleLicensePlate} has been serviced in the past and is now set to 'In Progress' status for the new service.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            

            Console.WriteLine("Please Choose Vehicle Type:");

            eVehicleType e_CurrentUserVehicleType = ChooseCarType();

            m_MyFactory.AddNewVehicle(userVehicleLicensePlate, e_CurrentUserVehicleType);

            Console.WriteLine($"Your Car: {userVehicleLicensePlate} Type: {e_CurrentUserVehicleType} Added !");

            GetVehicleDetailsByType(userVehicleLicensePlate);

        }

        public eVehicleType ChooseCarType()
        {
            Console.WriteLine();

            foreach (eVehicleType type in Enum.GetValues(typeof(eVehicleType)))
            {
                Console.WriteLine($"Press {(int)type}: For {type} ");
            }

            string userChoice = Console.ReadLine();

            int numberUserVehicleChoice = 0;

            CheckUserVehicleTypeChoice(ref userChoice, out numberUserVehicleChoice);

            return (eVehicleType)numberUserVehicleChoice;
        }

        public void CheckUserVehicleTypeChoice(ref string io_UserChoice, out int o_NumberUserVehicleChoice)
        {

            int enumSize = Enum.GetValues(typeof(eVehicleType)).Length;

            bool isValidUserVehicleChoice = false;

            int currentUserChoice = 0;

            while (!isValidUserVehicleChoice)
            {

                try
                {
                    currentUserChoice = int.Parse(io_UserChoice);

                    if (currentUserChoice >= 0 && currentUserChoice <= enumSize - 1)
                    {
                        isValidUserVehicleChoice = true;

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Error: Please enter a number between 0 and {enumSize - 1}.");
                    }

                }
                catch (FormatException)
                {

                    Console.WriteLine("Error: Input is not a valid number.");

                    Console.WriteLine();

                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Error: No input was provided.");

                    Console.WriteLine();

                }
                catch (OverflowException)
                {

                    Console.WriteLine("Error: The number is too large or too small.");

                    Console.WriteLine();

                }

                io_UserChoice = Console.ReadLine();
            }

            o_NumberUserVehicleChoice = currentUserChoice;

        }

        public void GetVehicleDetailsByType(string i_CurrentUserLicensePlate)
        {

            Console.Clear();
            Console.WriteLine("===================== Add Vehicle Details ======================");
            Console.WriteLine();

            GetVehicleDetails(i_CurrentUserLicensePlate);

            Vehicle currentVehicle = null;
            m_MyGarage.GetVehicleByPlateNumber(i_CurrentUserLicensePlate, ref currentVehicle);

            var questions = currentVehicle.GetQuestions();

            foreach (var question in questions)
            {
                bool validAnswer = false;

                while (!validAnswer)
                {
                    Console.WriteLine(question.Value);
                    string answer = Console.ReadLine();
                    float convertedAnswerToFloat = 0;

                    if (CheckValidInput(answer, out convertedAnswerToFloat))
                    {
                        try
                        {

                            if (currentVehicle.AnswerQuestion(question.Key, convertedAnswerToFloat))
                            {
                                validAnswer = true; 
                            }

                            else
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                            }

                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.WriteLine($"Please enter a value between {ex.MinValue} and {ex.MaxValue}");
                        }
                    }
                }
            }
        }

        public bool CheckValidInput(string i_Answer, out float i_ConvertedAnswerToFloat)
        {
            bool isValid = true;

            float checkingAnswer = 0;

            try
            {
                checkingAnswer = float.Parse(i_Answer);

            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Input is not correct");
                isValid = false;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error: No input was provided.");
                isValid = false;

            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: The number is too large or too small for a float.");
                isValid = false;

            }

            i_ConvertedAnswerToFloat = checkingAnswer;
            return isValid;
        }

        public void GetVehicleDetails(string i_CurrentUserLicensePlate)
        {
            Console.WriteLine("Please Enter Car Owner Name");
            string currentUserName = Console.ReadLine();

            CheckValidNames(ref currentUserName);
           
            Console.WriteLine("Please Enter Owner Phone Number Name");
            string currentPhoneNumber = Console.ReadLine();

            CheckPhoneNumber(ref currentPhoneNumber);

            Console.WriteLine("Please Enter Your Car Model Name");
            string currentModelName = Console.ReadLine();

            CheckValidNames(ref currentModelName);

            Console.WriteLine("Please Enter Your Car Energy Level");

            string currentCarEnergyLevel = Console.ReadLine();
            float convertedCurrentCarEnergyLevelInFloat = 0;

            ValidateCarEnergyLevelInput(ref currentCarEnergyLevel, out convertedCurrentCarEnergyLevelInFloat);

            m_MyGarage.UpdateVehicleDetails(i_CurrentUserLicensePlate, currentModelName,
                convertedCurrentCarEnergyLevelInFloat, currentUserName, currentPhoneNumber);

            bool isContinue = true;

            Console.Clear();

            // wheels

            Console.WriteLine("==========Wheels==========");
            Console.WriteLine("Please Enter Manufacturer Name");
            string manufacturerName = Console.ReadLine();

            CheckValidNames(ref manufacturerName);

            
            while (isContinue)
            {
                Console.WriteLine("Please Enter Current Air Pressure");
                string currAirPressure = Console.ReadLine(); // להמיר ל-float

                float currentAirPressure = 0;


                while (!float.TryParse(currAirPressure, out currentAirPressure))
                {
                    Console.WriteLine("Please Enter Current Air Pressure");

                    currAirPressure = Console.ReadLine();
                   
                }

                try
                {
                    m_MyGarage.UpdateAllWheelsAtOnes(i_CurrentUserLicensePlate, manufacturerName, currentAirPressure);
                    isContinue = false;
                }

                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                }
            }
        }

        public void CheckPhoneNumber(ref string io_OwnerPhoneNumberToCheck)
        {
            bool isValidPhoneNumber = false;

            while (!isValidPhoneNumber)
            {
                if (ValidPhoneNumber(io_OwnerPhoneNumberToCheck))
                {
                    isValidPhoneNumber = true;
                }
                else
                {

                    Console.WriteLine("Error: Phone Number Length Must be 10! please enter again:");

                    io_OwnerPhoneNumberToCheck = Console.ReadLine();
                }
            }
        }

        public bool ValidPhoneNumber(string i_OwnerPhoneNumberToCheck)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(i_OwnerPhoneNumberToCheck) || i_OwnerPhoneNumberToCheck.Length != 10)
            {
                isValid = false;
            }
            else
            {
                foreach (char currentChar in i_OwnerPhoneNumberToCheck)
                {
                    if (!char.IsDigit(currentChar) || char.IsWhiteSpace(currentChar))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        public void CheckValidNames(ref string io_CurrentName)
        {
            bool isValidName = false;

            while (!isValidName)
            {

                if (!IsCarOwnerNameContainOnlyLetters(io_CurrentName))
                {
                    Console.WriteLine("The Name Must Contain Only Letters, Please Enter Again: ");

                    io_CurrentName = Console.ReadLine();
                }
                else
                {
                    isValidName = true;
                }
            }
        }

        public void ValidateCarEnergyLevelInput(ref string io_CurrentCarEnergyLevel, out float i_ConvertedCurrentCarEnergyLevelInFloat)
        {
            bool isValid = false;

            float checkingCarEnergyLevel = 0;

            while (!isValid)
            {
                try
                {
                    checkingCarEnergyLevel = float.Parse(io_CurrentCarEnergyLevel);

                    if (m_MyGarage.CheckEnergyLevel(checkingCarEnergyLevel))
                    {
                        isValid = true;

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Error: Please Enter Correct Energy Level Value.");
                    }
                }

                catch (FormatException)
                {
                    Console.WriteLine("Error: Input is not a valid number.");
                    Console.WriteLine();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Error: No input was provided.");
                    Console.WriteLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Error: The number is too large or too small for a float.");
                    Console.WriteLine();
                }

                io_CurrentCarEnergyLevel = Console.ReadLine();
            }

            i_ConvertedCurrentCarEnergyLevelInFloat = checkingCarEnergyLevel;
        }

        public bool IsCarOwnerNameContainOnlyLetters(string i_StringToCheck)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(i_StringToCheck) || i_StringToCheck.Length < 1 || i_StringToCheck.Length > 15)
            {

                isValid = false;
            }
            else
            {
                foreach (char currentChar in i_StringToCheck)
                {
                    if (char.IsDigit(currentChar) || char.IsWhiteSpace(currentChar))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        public void ValidateUserVehicleLicensePlateInput(ref string io_UserVehicleLicensePlateToCheck)
        {
            bool isValidUserLicensePlate = false;

            while (!isValidUserLicensePlate)
            {
                if (IsPlateStringContainOnlyNumbers(io_UserVehicleLicensePlateToCheck))
                {
                    isValidUserLicensePlate = true;
                }
                else
                {

                    Console.WriteLine("Error: License plate must contain only numbers (1-10 digits)");

                    io_UserVehicleLicensePlateToCheck = Console.ReadLine();
                }
            }
        }

        public bool IsPlateStringContainOnlyNumbers(string i_StringToCheck)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(i_StringToCheck) || i_StringToCheck.Length < 1 || i_StringToCheck.Length > 10)
            {
                isValid = false;
            }
            else
            {
                foreach (char currentChar in i_StringToCheck)
                {
                    if (!char.IsDigit(currentChar) || char.IsWhiteSpace(currentChar))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        public void GetVehicleList()
        {
            Console.Clear();

            Console.WriteLine("============ Show Vehicle List ============");
            Console.WriteLine();

            Console.WriteLine("Please choose if you want to Filter or not");

            foreach (eVehicleStatus type in Enum.GetValues(typeof(eVehicleStatus)))
            {
                Console.WriteLine($"Press {(int)type}: For {type} ");
            }

            string userChoice = Console.ReadLine();

            int numberUserStatusChoice = 0;

            CheckUserStatusChoice(ref userChoice, out numberUserStatusChoice);

            Console.Clear();

            Console.WriteLine("============ Show Vehicle List ============");
            Console.WriteLine();

            try
            {
                List<string> licensePlates =
                    m_MyGarage.GetLicensePlatesByStatus((eVehicleStatus)numberUserStatusChoice);

                foreach (string plate in licensePlates)
                {
                    Console.WriteLine($"{plate}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error ! {ex.Message}");
            }
        }

        public void CheckUserStatusChoice(ref string io_UserChoice, out int o_NumberUserstatusChoice)
        {

            int enumSize = Enum.GetValues(typeof(eVehicleStatus)).Length;

            bool isValidUserVehicleChoice = false;

            int currentUserChoice = 0;

            while (!isValidUserVehicleChoice)
            {

                try
                {
                    currentUserChoice = int.Parse(io_UserChoice);

                    if (currentUserChoice >= 0 && currentUserChoice <= enumSize - 1)
                    {
                        isValidUserVehicleChoice = true;

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Error: Please enter a number between 0 and {enumSize - 1}.");
                    }

                }
                catch (FormatException)
                {

                    Console.WriteLine("Error: Input is not a valid number.");

                    Console.WriteLine();

                }
                catch (ArgumentNullException)
                {

                    Console.WriteLine("Error: No input was provided.");

                    Console.WriteLine();

                }
                catch (OverflowException)
                {
                    Console.WriteLine("Error: The number is too large or too small.");

                    Console.WriteLine();

                }

                io_UserChoice = Console.ReadLine();
            }

            o_NumberUserstatusChoice = currentUserChoice;

        }

        public void ChangeVehicleStatus()
        {
            bool isContinue = true;

            string userVehicleLicensePlate = null;

            Console.Clear();

            try
            {
                m_MyGarage.IsEmptyList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No Data available !!!");
                return;
            }

            while (isContinue)
            {
                Console.WriteLine("========== Change Vehicle Status ==========");
                Console.WriteLine("Press q to return to Menu");
                Console.WriteLine();
                Console.WriteLine("Please Enter Car License Plate:");

                userVehicleLicensePlate = Console.ReadLine();

                if (userVehicleLicensePlate.Equals("q"))
                {
                    return;
                }

                ValidateUserVehicleLicensePlateInput(ref userVehicleLicensePlate);

                try
                {
                    m_MyGarage.IsVehicleInGarage(userVehicleLicensePlate);
                }
                catch (Exception ex)
                {
                    Console.Clear() ;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    Console.WriteLine();
                    continue; 
                }

                Console.WriteLine($"Your Car Plate Is:{userVehicleLicensePlate}");

                Console.WriteLine("===========================================");

                isContinue = false;

            }

            isContinue = true;

            Console.Clear();

            while (isContinue)
            {
                int statusChoice = 0;

                Console.WriteLine("========== Change Vehicle Status ==========");

                Console.WriteLine("By which status would you like to view all the vehicles? ");

                Console.WriteLine("Press 1 : In progress ");

                Console.WriteLine("Press 2 : Ready ");

                Console.WriteLine("Press 3 : Paid ");

                Console.WriteLine("===========================================");

                try
                {
                    string userInput = Console.ReadLine();

                    if (string.IsNullOrEmpty(userInput))
                    {
                        throw new Exception("You must enter a value!");
                    }

                    if (!int.TryParse(userInput, out statusChoice))
                    {
                        throw new Exception($"'{userInput}' is not a valid number!");
                    }

                    if (statusChoice < 1 || statusChoice > 3)
                    {
                        throw new Exception($"Number {statusChoice} is not in valid range (1-3)!");
                    }
                    try
                    {
                        m_MyGarage.UpdateVehicleStatus(userVehicleLicensePlate, statusChoice);
                        PrintUpdateStatusToVehicle(userVehicleLicensePlate, statusChoice);
                        isContinue = false;

                    }
                    catch (Exception garageEx)
                    {
                        Console.Clear();
                        Console.WriteLine($"Error: {garageEx.Message}");
                        Console.WriteLine("Press enter to continue...");
                        Console.WriteLine();
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear ();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    Console.WriteLine();
                    continue;
                }
            }
        }

        public void PrintUpdateStatusToVehicle(string i_PlateNumber, int i_CurrentStatus)
        {
            string currentStatus = null;

            switch (i_CurrentStatus)
            {
                case 1:
                    currentStatus = "InProgress";
                    break;
                case 2:
                    currentStatus = "Ready";
                    break;
                case 3:
                    currentStatus = "Paid";
                    break;
            }

            Console.Clear();

            Console.WriteLine($"Vehicle with license plate {i_PlateNumber} successfully changed to {currentStatus} status!");
        }
        
        public void InflateWheels()
        {
            bool isContinue = true;

            string userVehicleLicensePlate = null;

            Console.Clear();

            try
            {
                m_MyGarage.IsEmptyList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No Data available !!!");
                return;
            }

            while (isContinue)
            {
                Console.WriteLine("============ Inflate Wheels To Max ============");
                Console.WriteLine("Press q to return to Menu");
                Console.WriteLine();
                Console.WriteLine("Please Enter Car License Plate:");

                userVehicleLicensePlate = Console.ReadLine();

                if (userVehicleLicensePlate.Equals("q"))
                {
                    return;
                }

                ValidateUserVehicleLicensePlateInput(ref userVehicleLicensePlate);

                try
                {
                    m_MyGarage.IsVehicleInGarage(userVehicleLicensePlate);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine($"Your Car Plate Is:{userVehicleLicensePlate}");

                Console.WriteLine("===========================================");

                isContinue = false;

            }

            Console.Clear();

            Console.WriteLine("========== Inflat Wheels To Max ==========");

            Console.WriteLine();

            m_MyGarage.InflateTiresToMax(userVehicleLicensePlate);

            Console.WriteLine($"Vehicle with license plate {userVehicleLicensePlate} successfully inflated to maximum air pressure!");

            Console.WriteLine();

            Console.WriteLine("===========================================");

            
        }

        public void RefuelVehicle()
        {
            
            bool isContinue = true;
            float amountFuel = 0;
            int fuelChoice = 0;

            string userVehicleLicensePlate = null;

            Console.Clear();

            try
            {
                m_MyGarage.IsEmptyList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No Data available !!!");
                return;
            }

            while (isContinue)
            {
                
                Console.WriteLine("========== Welcome To Ventura & Israeli Gas Station ==========");
                Console.WriteLine("Press q to return to Menu");
                Console.WriteLine();
                Console.WriteLine("Please Enter Car License Plate:");

                userVehicleLicensePlate = Console.ReadLine();

                if (userVehicleLicensePlate.Equals("q"))
                {
                    return;
                }

                ValidateUserVehicleLicensePlateInput(ref userVehicleLicensePlate);

                try
                {
                    m_MyGarage.IsVehicleInGarage(userVehicleLicensePlate);
                    m_MyGarage.IsVehicleWithFuelSystem(userVehicleLicensePlate);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    continue;
                }

                Console.WriteLine("===========================================");

                isContinue = false;

            }

            Console.Clear();

            isContinue = true;

            while (isContinue)
            {
                Console.WriteLine($"Your Car Plate Is:{userVehicleLicensePlate}");

                Console.WriteLine("========== Welcome To Ventura & Israeli Gas Station ==========");

                Console.WriteLine("Please select the type of fuel:");

                Console.WriteLine("Press 0: Octan 98");

                Console.WriteLine("Press 1: Octan 96");

                Console.WriteLine("Press 2: Octan 95");

                Console.WriteLine("Press 3: Soler");

                Console.WriteLine("==============================================================");

                try
                {
                    string userChoiceFuelType = Console.ReadLine();

                    if (string.IsNullOrEmpty(userChoiceFuelType))
                    {
                        throw new Exception("You must enter a value!");
                    }

                    if (!int.TryParse(userChoiceFuelType, out fuelChoice))
                    {
                        throw new Exception($"'{userChoiceFuelType}' is not a valid number!");
                    }

                    if (fuelChoice < 0 || fuelChoice > 3)
                    {
                        throw new Exception($"Number {fuelChoice} is not in valid range (0-3)!");
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    continue;
                }

                Console.WriteLine("========== Welcome To Ventura & Israeli Gas Station ==========");

                Console.Write("Please enter the amount of liters to refuel: ");

                Console.WriteLine("==============================================================");

                try
                {
                    string userChoiceAmountFuel = Console.ReadLine();

                    if (string.IsNullOrEmpty(userChoiceAmountFuel))
                    {
                        throw new Exception("You must enter a value!");
                    }

                    if (!float.TryParse(userChoiceAmountFuel, out amountFuel))
                    {
                        throw new Exception($"'{userChoiceAmountFuel}' is not a valid number!");
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    continue;
                }

                try
                {
                    m_MyGarage.RefuelVehicle(userVehicleLicensePlate, fuelChoice, amountFuel);
                    Console.WriteLine(
                        $"Refueling completed successfully for vehicle {userVehicleLicensePlate}. Your fuel tank has been updated!"); // להציג הודעה למסך כאשר התדלוק אצליח
                    isContinue = false;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    continue;
                }
            }
        }

        public void ChargeElectricVehicle()
        {
            
            bool isContinue = true;    
            string userChoice = null;
            float minutesCharging = 0;
            string userVehicleLicensePlate = null;

            Console.Clear();

            try
            {
                m_MyGarage.IsEmptyList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No Data available !!!");
                return;
            }

            while (isContinue)
            {
                Console.WriteLine();
                Console.WriteLine("========== Welcome To Ventura & Israeli Gas Station ==========");
                Console.WriteLine("Press q to Return to menu");
                Console.WriteLine();
                Console.WriteLine("Please Enter Car License Plate:");


                userVehicleLicensePlate = Console.ReadLine();

                if (userVehicleLicensePlate.Equals("q"))
                {
                    return;
                }

                ValidateUserVehicleLicensePlateInput(ref userVehicleLicensePlate);

                try
                {
                    m_MyGarage.IsVehicleInGarage(userVehicleLicensePlate);
                    m_MyGarage.IsVehicleWithElectricSystem(userVehicleLicensePlate);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    continue;
                }

                Console.WriteLine("===========================================");

                isContinue = false;
            }

            Console.Clear();

            isContinue = true ;

            while (isContinue)
            {
                Console.WriteLine("========== Welcome To Ventura & Israeli Gas Station ==========");

                Console.Write("Please enter the number of minutes you'd like to charge the electric vehicle");

                Console.WriteLine("==============================================================");

                try
                {
                    userChoice = Console.ReadLine();

                    if (string.IsNullOrEmpty(userChoice))
                    {
                        throw new Exception("You must enter a value!");
                    }

                    if (!float.TryParse(userChoice, out minutesCharging))
                    {
                        throw new Exception($"'{userChoice}' is not a valid number!");
                    }

                    try
                    {
                        m_MyGarage.ChargeElectricVehicle(userVehicleLicensePlate, minutesCharging);
                        Console.Clear();
                        Console.WriteLine($"Charging completed successfully for vehicle {userVehicleLicensePlate}. Your electric battery has been updated!");
                        isContinue = false; 
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine("Press enter to continue...");
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    continue;
                }
            }
        }
        
        public void ShowVehicleDetails()
        {
            
            bool isContinue = true;
            string userVehicleLicensePlate = null;

            Console.Clear();

            try
            {
                m_MyGarage.IsEmptyList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No Data available !!!");
                return;
            }

            while (isContinue)
            {
                Console.Clear();
                Console.WriteLine("============= Show All Car Detail =============");
                Console.WriteLine("Press q to return to Menu");
                Console.WriteLine();
                Console.WriteLine("Please Enter Car License Plate:");

                userVehicleLicensePlate = Console.ReadLine();

                if (userVehicleLicensePlate.Equals("q"))
                {
                    return;
                }

                ValidateUserVehicleLicensePlateInput(ref userVehicleLicensePlate);

                try
                {
                    m_MyGarage.IsVehicleInGarage(userVehicleLicensePlate);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press enter to continue...");
                    continue;
                }

                Console.WriteLine("===========================================");

                isContinue = false;
            }

            ArrayList arrayList = m_MyGarage.GetVehicleDetailsByLicensePlate(userVehicleLicensePlate);

            ArrayList menuArrayList = m_MyGarage.GetMenuForVehicle(userVehicleLicensePlate);

            for (int i = 0; i < arrayList.Count; i++)
            {
                Console.WriteLine($"{menuArrayList[i]} {arrayList[i]}");
            }
        }
    }
}