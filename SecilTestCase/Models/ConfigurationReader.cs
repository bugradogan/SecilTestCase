using SecilTestCase.Models;
using SecilTestCase.Models.Repository;
using System.Timers;

public class ConfigurationReader
{
    private readonly IConfigurationRepository _repository;
    private readonly System.Timers.Timer _timer;
    private List<ConfigurationItem> _configurations;

    public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
    {
        _repository = new MongoConfigurationRepository(connectionString);
        _configurations = _repository.GetConfigurations(applicationName);

        _timer = new System.Timers.Timer(refreshTimerIntervalInMs);
        _timer.Elapsed += (sender, e) => RefreshConfigurations(applicationName);
        _timer.AutoReset = true;
        _timer.Start();
    }

    public T GetValue<T>(string key)
    {
        var configuration = _configurations.FirstOrDefault(c => c.Name == key && c.IsActive == 1);
        if (configuration == null)
            throw new KeyNotFoundException("Configuration key not found or inactive");

        if(configuration.Type == "string")
			return (T)Convert.ChangeType(configuration.Value, typeof(string));
		else if (configuration.Type == "bool")
        {
			int val = int.Parse(configuration.Value);           
            bool active = val == 1 ? true : false;
			return (T)Convert.ChangeType(active, typeof(bool));
		}			
        else if(configuration.Type == "double")
			return (T)Convert.ChangeType(configuration.Value, typeof(double));
        else
			return (T)Convert.ChangeType(configuration.Value, typeof(int));
	}   

    public List<ConfigurationItem> GetConfigurationItems()
    {
        return _configurations;
    }
    public List<ConfigurationItem> GetAllConfigurationItems()
    {       
        return _repository.GetAllConfigurations();
    }

    private void RefreshConfigurations(string applicationName)
    {
        var updatedConfigurations = _repository.GetConfigurations(applicationName);
        Console.WriteLine("Reflesh DB");
        foreach (var updatedConfig in updatedConfigurations)
        {
            var existingConfig = _configurations.FirstOrDefault(c => c.Id == updatedConfig.Id);
            if (existingConfig == null)
            {
                _configurations.Add(updatedConfig);
            }
            else
            {
                existingConfig.Value = updatedConfig.Value;
            }
        }
    }
}
