using Fluid;
using Fluid.Parser;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System.Text;
using TemplateContext = Scriban.TemplateContext;

namespace Mediator.SourceGenerator;

public static class MediatorOptionsGenerator
{
    public static void Generate(Action<string, SourceText> addSource, string generatorVersion)
    {
        GenerateOptions(addSource, generatorVersion);
        GenerateOptionsAttribute(addSource, generatorVersion);
    }

    private static void GenerateOptions1(Action<string, SourceText> addSource, string generatorVersion)
    {
        var model = new { GeneratorVersion = generatorVersion };

        var file = @"resources/MediatorOptions.sbn-cs";
        var template = Template.Parse(EmbeddedResource.GetContent(file), file);
        var output = template.Render(model, member => member.Name);
        addSource("MediatorOptions.g.cs", SourceText.From(output, Encoding.UTF8));
    }

    private static void GenerateOptions(Action<string, SourceText> addSource, string generatorVersion)
    {
        var model = new { GeneratorVersion = generatorVersion };

        var file = "resources/MediatorOptions.liquid";
        var templateContent = EmbeddedResource.GetContent(file);
        var parser = new FluidParser();
        if (!parser.TryParse(templateContent, out var template, out var error))
            return;

        var context = new Fluid.TemplateContext(model);
        var output = template.Render(context);
        addSource("MediatorOptions.g.cs", SourceText.From(output, Encoding.UTF8));
    }

    private static void GenerateOptionsAttribute1(Action<string, SourceText> addSource, string generatorVersion)
    {
        var model = new { GeneratorVersion = generatorVersion };

        var file = @"resources/MediatorOptionsAttribute.sbn-cs";
        var template = Template.Parse(EmbeddedResource.GetContent(file), file);
        var output = template.Render(model, member => member.Name);
        addSource("MediatorOptionsAttribute.g.cs", SourceText.From(output, Encoding.UTF8));
    }

    private static void GenerateOptionsAttribute(Action<string, SourceText> addSource, string generatorVersion)
    {
        var model = new { GeneratorVersion = generatorVersion };

        var file = @"resources/MediatorOptionsAttribute.liquid";
        var templateContent = EmbeddedResource.GetContent(file);
        var parser = new FluidParser();
        if (!parser.TryParse(templateContent, out var template, out var error))
            return;

        var context = new Fluid.TemplateContext(model);
        var output = template.Render(context);
        addSource("MediatorOptionsAttribute.g.cs", SourceText.From(output, Encoding.UTF8));
    }
}
