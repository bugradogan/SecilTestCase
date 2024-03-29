namespace SecilTestCase.Models.Repository
{
    public interface IConfigurationRepository
    {
        List<ConfigurationItem> GetConfigurations(string applicationName);
        List<ConfigurationItem> GetAllConfigurations();
        void UpdateConfiguration(ConfigurationItem item);
    }
}
