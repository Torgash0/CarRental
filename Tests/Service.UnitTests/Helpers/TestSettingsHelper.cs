using Service.Settings;

namespace Service.UnitTests.Helpers;

public static class TestSettingsHelper
{
    public static CarRentalSettings GetSettings()
    {
        return CarRentalSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}
