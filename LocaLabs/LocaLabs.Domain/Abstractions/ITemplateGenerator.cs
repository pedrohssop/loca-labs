using LocaLabs.Domain.Enums;

namespace LocaLabs.Domain.Abstractions
{
    public interface ITemplateGenerator
    {
        /// <summary>
        /// Generate the string based on a registred template
        /// </summary>
        /// <param name="template">the template identifier</param>
        /// <param name="dataSource">the data source</param>
        /// <returns>the generated string</returns>
        string Generate(Templates template, object dataSource);
    }
}