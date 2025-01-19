using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        protected List<Dictionary<string, eVehicleStatus>> m_AllVehicleStatusInGarage = null;

        public static List<Vehicle> m_AllVehicleInGarage = null;

        public Garage()
        {
            m_AllVehicleStatusInGarage = new List<Dictionary<string, eVehicleStatus>>();
        }

        public void GetVehicleByPlateNumber(string i_PlateNumber, ref Vehicle io_Vehicle)
        {
            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber().Equals(i_PlateNumber))
                {
                    io_Vehicle = m_AllVehicleInGarage[i];
                    break;
                }
            }
        }

        public bool IsServicedBefore(string i_PlateNumber)
        {
            
            bool isServicedBefore = false;

            if (m_AllVehicleInGarage == null)
            {
                throw new Exception();
            }

            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    Dictionary<string, eVehicleStatus> current = new Dictionary<string, eVehicleStatus>
                    {
                        { i_PlateNumber , eVehicleStatus.InProgress}
                    };
                    m_AllVehicleStatusInGarage[i] = current; 
                    isServicedBefore = true;
                }
            }

            return isServicedBefore;
        }

        public bool CheckEnergyLevel(float i_EnergyLevel)
        {
            bool isValid = true;

            if (!(i_EnergyLevel > 0 && i_EnergyLevel <= 100))
            {
                isValid = false;
            }

            return isValid;
        }

        public void IsVehicleInGarage(string i_PlateNumber)
        {
            bool isVehicleInGarage = false;

            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    isVehicleInGarage = true;
                }
            }

            if (isVehicleInGarage == false)
            {
                throw new Exception("Vehicle with the provided license plate number is not found in the garage. Please check and try again.");
            } 

        }

        public void IsVehicleWithElectricSystem(string i_PlateNumber)
        {
            bool isVehicleWithElectricSystem = false;

            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    if (m_AllVehicleInGarage[i] is ElectricCar)
                    {
                        isVehicleWithElectricSystem = true;
                    }

                    if (m_AllVehicleInGarage[i] is ElectricMotorcycle)
                    {
                        isVehicleWithElectricSystem = true;
                    }
                }
            }

            if (isVehicleWithElectricSystem == false)
            {
                throw new Exception("The vehicle with the provided license plate does not have a electric system. Please verify the vehicle type and try again.");
            }
        }

        public void IsVehicleWithFuelSystem(string i_PlateNumber)
        {
            bool isVehicleWithFuelSystem = false;

            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {

                    if (m_AllVehicleInGarage[i] is FuelCar)
                    {
                        isVehicleWithFuelSystem = true;
                    }

                    if (m_AllVehicleInGarage[i] is FuelMotorcycle)
                    {
                        isVehicleWithFuelSystem = true;
                    }

                    if (m_AllVehicleInGarage[i] is Truck)
                    {
                        isVehicleWithFuelSystem = true;
                    }
                }
            }

            if (isVehicleWithFuelSystem == false)
            {
                throw new Exception("The vehicle with the provided license plate does not have a fuel system. Please verify the vehicle type and try again."); 
            }    

        }

        public static void SetVehicleToGarage(Vehicle i_Vehicle)
        {
            if (m_AllVehicleInGarage == null)
            {
                m_AllVehicleInGarage = new List<Vehicle>();
            }

            if (i_Vehicle == null)
            {
                throw new ArgumentNullException(nameof(i_Vehicle), "Cannot add null vehicle to garage");
            }

            m_AllVehicleInGarage.Add(i_Vehicle);
        }

        public void UpdateVehicleDetails(string i_PlateNumber, string i_ModelName, float i_EnergyLevel, string i_VehicleOwner, string i_OwnerPhoneNumber)
        {
            // פונקציה שמעדכנת את פרטי הכלי רכב שנותרים על ידי מספר רכב
            for (int i = 0; i < m_AllVehicleInGarage.Count; ++i)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    m_AllVehicleInGarage[i].SetEnergyLevel(i_EnergyLevel);

                    m_AllVehicleInGarage[i].SetModelName(i_ModelName);

                    m_AllVehicleInGarage[i].SetVehicleOwner(i_VehicleOwner);

                    m_AllVehicleInGarage[i].SetOwnerNumber(i_OwnerPhoneNumber);

                    Dictionary<string, eVehicleStatus> current = new Dictionary<string, eVehicleStatus>
                    {
                        { i_PlateNumber , eVehicleStatus.InProgress}
                    };

                    m_AllVehicleStatusInGarage.Add(current);
                }
            }
        }

        public void UpdateAllWheelsAtOnes(string i_PlateNumber, string i_ManufacturerName, float i_CurrentAirPressure)
        {
            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    m_AllVehicleInGarage[i].SetWheelsAtOnes(i_ManufacturerName, i_CurrentAirPressure);
                    break;
                }
            }
        }
        
        public List<string> GetLicensePlatesByStatus(eVehicleStatus i_FilterVehicleStatus)
        {
            List<string> licensePlate = new List<string>();

            switch (i_FilterVehicleStatus)
            {
                case eVehicleStatus.None:

                    foreach(Dictionary<string, eVehicleStatus> dictionary in m_AllVehicleStatusInGarage)
                    {
                        licensePlate.AddRange(dictionary.Keys);
                    }
                    break;

                case eVehicleStatus.Paid:

                    foreach (Dictionary<string, eVehicleStatus> dictionary in m_AllVehicleStatusInGarage)
                    {
                        foreach (KeyValuePair<string, eVehicleStatus> kvp in dictionary)
                        {
                            string plate = kvp.Key;
                            eVehicleStatus status = kvp.Value;

                            if (status == eVehicleStatus.Paid)
                            {
                                licensePlate.Add(plate);
                            }

                        }
                    }
                    break;

                case eVehicleStatus.InProgress:

                    foreach (Dictionary<string, eVehicleStatus> dictionary in m_AllVehicleStatusInGarage)
                    {
                        foreach (KeyValuePair<string, eVehicleStatus> kvp in dictionary)
                        {
                            string plate = kvp.Key;
                            eVehicleStatus status = kvp.Value;

                            if (status == eVehicleStatus.InProgress)
                            {
                                licensePlate.Add(plate);
                            }
                        }
                    }
                    break;

                case eVehicleStatus.Ready:

                    foreach (Dictionary<string, eVehicleStatus> dictionary in m_AllVehicleStatusInGarage)
                    {
                        foreach (KeyValuePair<string, eVehicleStatus> kvp in dictionary)
                        {
                            string plate = kvp.Key;
                            eVehicleStatus status = kvp.Value;

                            if (status == eVehicleStatus.Ready)
                            {
                                licensePlate.Add(plate);
                            }
                        }
                    }
                    break;

            }

            if (licensePlate.Count == 0)
            {
                throw new Exception(" Empty List !");
            }

            return licensePlate;
        }

        public void UpdateVehicleStatus(string i_PlateNumber, int i_NewStatus)
        {
            eVehicleStatus newStatus = eVehicleStatus.None;

            switch (i_NewStatus)
            {
                case 1:
                    newStatus = eVehicleStatus.InProgress;
                    break;

                case 2:
                    newStatus = eVehicleStatus.Ready;
                    break;

                case 3:
                    newStatus = eVehicleStatus.Paid;
                    break;
            }

            for (int i = 0; i < m_AllVehicleStatusInGarage.Count; i++)
            {
                if (m_AllVehicleStatusInGarage[i].ContainsKey(i_PlateNumber))
                {
                    Dictionary<string, eVehicleStatus> keyValuePairs = new Dictionary<string, eVehicleStatus>
                    {
                        { i_PlateNumber , newStatus }
                    };

                    if (m_AllVehicleStatusInGarage[i][i_PlateNumber] == newStatus)
                    {
                        throw new Exception($"Vehicle is already in {newStatus} status! Please choose a different status.");
                    }

                    m_AllVehicleStatusInGarage[i] = keyValuePairs;
                    break;
                }
            }
        }

        public void InflateTiresToMax(string i_PlateNumber) 
        {
            for (int i = 0; i < m_AllVehicleInGarage.Count; i++) 
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    m_AllVehicleInGarage[i].InflateTiresToMax();
                    break;
                }

            } 
        }

        public void RefuelVehicle(string i_PlateNumber, int i_FuelType, float i_AmountFuel)
        {
            eFuelType fuelType = eFuelType.Octan98;

            switch (i_FuelType)
            {
                case 0:
                    fuelType = eFuelType.Octan98;
                    break;

                case 1:
                    fuelType = eFuelType.Octan96;
                    break;

                case 2:
                    fuelType = eFuelType.Octan95;
                    break;

                case 3:
                    fuelType = eFuelType.Soler;
                    break;

            }

            // fuel car, fuel motorcycle, truck
            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {

                    if (m_AllVehicleInGarage[i] is FuelCar)
                    {
                        FuelCar fuelCar = m_AllVehicleInGarage[i] as FuelCar;
                        
                        if (fuelCar.GetCurrentFuelLevel() == fuelCar.GetMaxFuelLevel())
                        {
                            throw new ValueOutOfRangeException(fuelCar.GetMaxFuelLevel());
                        }

                        if (! (fuelType == fuelCar.GetFuelType()))
                        {
                            throw new Exception($"Vehicle with license plate {i_PlateNumber} requires {fuelCar.GetFuelType()} fuel type. Cannot refuel with {fuelType}.");
                        }

                        fuelCar.Refuel(i_AmountFuel);
                    }

                    if (m_AllVehicleInGarage[i] is FuelMotorcycle)
                    {
                        FuelMotorcycle fuelMotorcycle = m_AllVehicleInGarage[i] as FuelMotorcycle;

                        if (fuelMotorcycle.GetCurrentFuelLevel() == fuelMotorcycle.GetMaxFuelLevel())
                        {
                            throw new ValueOutOfRangeException(fuelMotorcycle.GetMaxFuelLevel());
                        }

                        if (!(fuelType == fuelMotorcycle.GetFuelType()))
                        {
                            throw new Exception($"Vehicle with license plate {i_PlateNumber} requires {fuelMotorcycle.GetFuelType()} fuel type. Cannot refuel with {fuelType}.");
                        }

                        fuelMotorcycle.Refuel(i_AmountFuel);
                    }

                    if (m_AllVehicleInGarage[i] is Truck)
                    {
                        Truck truck = m_AllVehicleInGarage[i] as Truck;

                        if (truck.GetCurrentFuelLevel() == truck.GetMaxFuelLevel())
                        {
                            throw new ValueOutOfRangeException(truck.GetMaxFuelLevel());
                        }

                        if (!(fuelType == truck.GetFuelType()))
                        {
                            throw new Exception($"Vehicle with license plate {i_PlateNumber} requires {truck.GetFuelType()} fuel type. Cannot refuel with {fuelType}.");
                        }

                        truck.Refuel(i_AmountFuel);
                    }  
                }
            }
        }

        public void ChargeElectricVehicle(string i_PlateNumber, float i_ChargingDurationMinutes)
        {
            float chargingDurationHours = i_ChargingDurationMinutes / 60.0f;

            for (int i = 0; i < m_AllVehicleInGarage.Count; i++) 
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {

                    if (m_AllVehicleInGarage[i] is ElectricCar)
                    {
                        ElectricCar electricCar = m_AllVehicleInGarage[i] as ElectricCar;

                        if (electricCar.GetRemainingBatteryTimeHours() == electricCar.GetMaxBatteryTimeHours())
                        {
                            throw new ValueOutOfRangeException(electricCar.GetMaxBatteryTimeHours());
                        }

                        electricCar.ChargeBattery(chargingDurationHours);
                    }

                    if (m_AllVehicleInGarage[i] is ElectricMotorcycle)
                    {
                        ElectricMotorcycle electricMotorcycle = m_AllVehicleInGarage[i] as ElectricMotorcycle;

                        if (electricMotorcycle.GetRemainingBatteryTimeHours() == electricMotorcycle.GetMaxBatteryTimeHours())
                        {
                            throw new ValueOutOfRangeException(electricMotorcycle.GetMaxBatteryTimeHours());
                        }

                        electricMotorcycle.ChargeBattery(chargingDurationHours);
                    }
                }
            }
        }

        public ArrayList GetVehicleDetailsByLicensePlate(string i_PlateNumber)
        {
            ArrayList vehicleData = new ArrayList();

            for (int i = 0; i < m_AllVehicleInGarage.Count; i++) 
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    vehicleData.Add(m_AllVehicleInGarage[i].GetPlateNumber());

                    vehicleData.Add(m_AllVehicleInGarage[i].GetModelName());

                    vehicleData.Add(m_AllVehicleInGarage[i].GetVehicleOwner());

                    vehicleData.Add(m_AllVehicleInGarage[i].GetOwnerNumber());

                    Wheel[] wheels = m_AllVehicleInGarage[i].GetWheelsArray();

                    vehicleData.Add(wheels[0].GetManufacturerName());

                    vehicleData.Add(wheels[0].GetCurrentAirPressure());

                    vehicleData.Add(wheels[0].GetMaxAirPressure());
                   
                   // סטטוס
                    Dictionary<string, eVehicleStatus> kvp = m_AllVehicleStatusInGarage[i];
                    foreach (eVehicleStatus status in kvp.Values)
                    {
                        vehicleData.Add(status);
                    }

                    if (m_AllVehicleInGarage[i] is FuelCar)
                    {
                        FuelCar fuelCar = m_AllVehicleInGarage[i] as FuelCar;

                        vehicleData.Add(fuelCar.GetCurrentFuelLevel());

                        vehicleData.Add(fuelCar.GetFuelType());

                        vehicleData.Add(fuelCar.GetColorType());

                        vehicleData.Add(fuelCar.GetDoorAmount());
                    }

                    if (m_AllVehicleInGarage[i] is ElectricCar)
                    {
                        ElectricCar electricCar = m_AllVehicleInGarage[i] as ElectricCar;

                        vehicleData.Add(electricCar.GetRemainingBatteryTimeHours());

                        vehicleData.Add(electricCar.GetColorType());

                        vehicleData.Add(electricCar.GetDoorAmount());
                    }

                    if (m_AllVehicleInGarage[i] is FuelMotorcycle)
                    {
                        FuelMotorcycle fuelMotorcycle = m_AllVehicleInGarage[i] as FuelMotorcycle;

                        vehicleData.Add(fuelMotorcycle.GetCurrentFuelLevel());
                        
                        vehicleData.Add(fuelMotorcycle.GetFuelType());

                        vehicleData.Add(fuelMotorcycle.GetLicenseType());

                        vehicleData.Add(fuelMotorcycle.GetEngineVolume());
                    }

                    if (m_AllVehicleInGarage[i] is ElectricMotorcycle)
                    {
                        ElectricMotorcycle electricMotorcycle = m_AllVehicleInGarage[i] as ElectricMotorcycle;

                        vehicleData.Add(electricMotorcycle.GetRemainingBatteryTimeHours());

                        vehicleData.Add(electricMotorcycle.GetLicenseType());

                        vehicleData.Add(electricMotorcycle.GetEngineVolume());
                    }

                    if (m_AllVehicleInGarage[i] is Truck)
                    {
                        Truck truck = m_AllVehicleInGarage[i] as Truck;

                        vehicleData.Add(truck.GetCurrentFuelLevel());

                        vehicleData.Add(truck.GetFuelType());

                        vehicleData.Add(truck.GetIsRefrigerated());

                        vehicleData.Add(truck.GetCargoVolume());
                    }
                }
            }

            return vehicleData;
        }

        public ArrayList GetMenuForVehicle(string i_PlateNumber)
        {
            ArrayList menuArrayList = new ArrayList();

            for (int i = 0; i < m_AllVehicleInGarage.Count; i++)
            {
                if (m_AllVehicleInGarage[i].GetPlateNumber() == i_PlateNumber)
                {
                    if (m_AllVehicleInGarage[i] is FuelCar)
                    {
                        FuelCar fuelCar = m_AllVehicleInGarage[i] as FuelCar;

                        menuArrayList = fuelCar.GetArrayListMenu();

                        break;
                    }

                    if (m_AllVehicleInGarage[i] is ElectricCar)
                    {
                        ElectricCar electicCar = m_AllVehicleInGarage[i] as ElectricCar;

                        menuArrayList = electicCar.GetArrayListMenu();

                        break;
                    }

                    if (m_AllVehicleInGarage[i] is ElectricMotorcycle)
                    {
                        ElectricMotorcycle electricMotorcycle = m_AllVehicleInGarage[i] as ElectricMotorcycle;

                        menuArrayList = electricMotorcycle.GetArrayListMenu();

                        break;
                    }

                    if (m_AllVehicleInGarage[i] is FuelMotorcycle)
                    {
                        FuelMotorcycle fuelMotorcycle = m_AllVehicleInGarage[i] as FuelMotorcycle;

                        menuArrayList = fuelMotorcycle.GetArrayListMenu();

                        break;
                    }

                    if (m_AllVehicleInGarage[i] is Truck)
                    {
                        Truck truck = m_AllVehicleInGarage[i] as Truck;

                        menuArrayList = truck.GetArrayListMenu();

                        break;
                    }

                }
            }
            
            return menuArrayList;

        }

        public void IsEmptyList()
        {
            if (m_AllVehicleInGarage == null)
            {
                throw new Exception();
            }
        }
    }
}
