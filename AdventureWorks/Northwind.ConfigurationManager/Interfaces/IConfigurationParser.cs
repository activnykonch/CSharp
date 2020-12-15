namespace Northwind.ConfigurationManager.Interfaces
{
    public interface IConfigurationParser<out T>
    {
        T Parse();
    }
}
