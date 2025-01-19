namespace Ex03.GarageLogic
{
    public class Motorcycle
    {
        private eMotorcycleLicenseType e_LicenseType;

        private int m_EngineVolume = 0;

        public void SetLicenseType(eMotorcycleLicenseType i_MotorcycleLicenseType)
        {
            e_LicenseType = i_MotorcycleLicenseType;
        }

        public eMotorcycleLicenseType GetLicenseType()
        {
            return e_LicenseType;
        }

        public void SetEngineVolume(int i_EngineVolume)
        {
            m_EngineVolume = i_EngineVolume;
        }

        public int GetEngineVolume()
        {
            return m_EngineVolume;
        }

    }
}
