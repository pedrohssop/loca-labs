using System;
using System.IO;

namespace LocaLabs.Infra.Templates
{
    public interface ITemplateContentService
    {
        string GetTemplateContent(Domain.Enums.Templates template);
    }

    public class LocalFileTemplateContentService : ITemplateContentService
    {
        private static string TemplatesFolder = "Templates/Source";

        public string GetTemplateContent(Domain.Enums.Templates template)
        {
            var appPath = AppDomain.CurrentDomain.RelativeSearchPath;
            var path = Path.Combine(appPath, TemplatesFolder, $"{template}.txt");

            return File.ReadAllText(path);
        }
    }
}