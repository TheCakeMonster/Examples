using System;
using System.Collections.Generic;

namespace ComplexJsonSerialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestBaseClassWithExtensionDataAndImplementations();
        }

        private static void TestNestedJson()
        {
            Template1Serialiser serialiser = new Template1Serialiser(new ValidationRuleSerialiser());
            Template1 template = new Template1();
            IList<IValidationRule> rules = new List<IValidationRule>();

            template.TemplateName = "Template1";
            template.TemplateDescription = "This is Template 1";
            rules.Add(new Required());
            rules.Add(new NoTrailingSlash());
            rules.Add(new CharacterSet());

            string serialisedTemplate = serialiser.Serialise(template, rules);
            Console.WriteLine(serialisedTemplate);

        }

        private static void TestExtensionData()
        {
            Template2Serialiser serialiser = new Template2Serialiser();
            Template2 template = new Template2();
            Template2Deserialised deserialisedTemplate;

            template.TemplateName = "Template2";
            template.TemplateDescription = "This is Template 2";
            template.ValidationRules.Add(new Required());
            template.ValidationRules.Add(new NoTrailingSlash());
            template.ValidationRules.Add(new CharacterSet() { CharacterSetCode = "SomeCode" });

            string serialisedTemplate = serialiser.Serialise(template);
            Console.WriteLine(serialisedTemplate);
            Console.WriteLine("Press Enter to continue ...");
            Console.ReadLine();

            deserialisedTemplate = serialiser.Deserialise(serialisedTemplate);
            Console.WriteLine("Deserialisation succeeded :-)");
            Console.WriteLine($"Validation Rules Json is: {deserialisedTemplate.ValidationRulesJson}");
        }

        private static void TestCustomJsonConverter()
        {
            Template3Serialiser serialiser = new Template3Serialiser();
            Template3 template = new Template3();
            Template3 deserialisedTemplate;

            template.TemplateName = "Template3";
            template.TemplateDescription = "This is Template 3";
            template.ValidationRules.Add(new Required());
            template.ValidationRules.Add(new NoTrailingSlash());
            template.ValidationRules.Add(new CharacterSet() { CharacterSetCode = "SomeCode" });

            string serialisedTemplate = serialiser.Serialise(template);
            Console.WriteLine(serialisedTemplate);
            Console.WriteLine("Press Enter to continue ...");
            Console.ReadLine();

            deserialisedTemplate = serialiser.Deserialise(serialisedTemplate);
            Console.WriteLine("Deserialisation succeeded :-)");
            Console.WriteLine($"{deserialisedTemplate.ValidationRules.Count} validation rules deserialised");
        }

        private static void TestBaseClassWithExtensionDataAndImplementations()
        {
            Template4Serialiser serialiser = new Template4Serialiser();
            Template4Export template = new Template4Export();
            Template4Import deserialisedTemplate;

            template.TemplateName = "Template4";
            template.TemplateDescription = "This is Template 4";
            template.ValidationRules.Add(new Required());
            template.ValidationRules.Add(new NoTrailingSlash());
            template.ValidationRules.Add(new CharacterSet() { CharacterSetCode = "SomeCode" });

            string serialisedTemplate = serialiser.Serialise(template);
            Console.WriteLine(serialisedTemplate);
            Console.WriteLine("Press Enter to continue ...");
            Console.ReadLine();

            deserialisedTemplate = serialiser.Deserialise(serialisedTemplate);
            Console.WriteLine("Deserialisation succeeded :-)");
            Console.WriteLine($"{deserialisedTemplate.ValidationRules.Count} validation rules deserialised");
        }
    }
}
