namespace CrossCutting.DI;

public class AppSettings
{
    private readonly IConfiguration _configuration;

    public AppSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        string? connectionString = _configuration["CONN_STRING"];
        if(String.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        return connectionString;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value">out</param>
    /// <returns></returns>
    public bool TryGetKeyValue<T>(string key, out T value)
    {
        T? keyValue = _configuration.GetValue<T>(key);
        if (keyValue != null)
        {
            value = keyValue;
            return true;
        }
        value = default;
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Tipo a ser convertido</typeparam>
    /// <param name="key">Chave</param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException">Chave não encontrada</exception>
    public T GetRequiredValue<T>(string key)
    {
        if(!TryGetKeyValue(key, out T value))
        {
            throw new KeyNotFoundException(key);
        }
        return value;
    }

    public string GetRequiredString(string key)
    {
        return GetRequiredValue<string>(key);
    }
}
