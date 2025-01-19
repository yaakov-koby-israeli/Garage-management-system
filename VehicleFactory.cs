namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        // יצירה של כלי רכב
        public void AddNewVehicle(string i_PlateNumber, eVehicleType i_VehicleType)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    FuelCar fuelCar = new FuelCar(i_PlateNumber);
                    Garage.SetVehicleToGarage(fuelCar);
                    break;

                case eVehicleType.ElectricCar:
                    ElectricCar electricCar = new ElectricCar(i_PlateNumber);
                    Garage.SetVehicleToGarage(electricCar);
                    break;

                case eVehicleType.FuelMotorcycle:
                    FuelMotorcycle fuelMotorcycle = new FuelMotorcycle(i_PlateNumber);
                    Garage.SetVehicleToGarage(fuelMotorcycle);
                    break;

                case eVehicleType.ElectricMotorcycle:
                    ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle(i_PlateNumber);
                    Garage.SetVehicleToGarage(electricMotorcycle);
                    break;

                case eVehicleType.Truck:
                    Truck truck = new Truck(i_PlateNumber);
                    Garage.SetVehicleToGarage(truck);
                    break;
            }

        }
    }
}
