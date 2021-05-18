using HandlebarsDotNet;
using LocaLabs.Domain.Abstractions;

namespace LocaLabs.Infra.Templates
{
    public class HandlebarTemplateGenerator : ITemplateGenerator
    {
        public HandlebarTemplateGenerator(ITemplateContentService contentService)
        {
            ContentService = contentService;
        }

        ITemplateContentService ContentService { get; }

        public string Generate(Domain.Enums.Templates template, object dataSource)
        {
            var text = ContentService.GetTemplateContent(template);
            var handlerCompiled = Handlebars.Compile(text);

            return handlerCompiled(dataSource);
        }
    }
}