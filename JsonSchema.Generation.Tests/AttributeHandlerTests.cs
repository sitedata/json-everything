using System;
using System.Collections.Generic;
using NUnit.Framework;

using static Json.Schema.Generation.Tests.AssertionExtensions;

namespace Json.Schema.Generation.Tests
{
	public class AttributeHandlerTests
	{
		[AttributeUsage(AttributeTargets.Property)]
		private class AttributeWithDirectHandler : Attribute, IAttributeHandler
		{
			public const uint MaxLength = 100;

			IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
			{
				yield return new Intents.MaxLengthIntent(MaxLength);
			}
		}

		[AttributeUsage(AttributeTargets.Property)]
		private class AttributeWithIndirectHandler : Attribute
		{
			public const uint MaxLength = 200;
		}

		private class CustomAttributeHandler : IAttributeHandler
		{
			IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
			{
				yield return new Intents.MaxLengthIntent(AttributeWithIndirectHandler.MaxLength);
			}
		}

		private class TypeWithCustomAttribute1
		{
			[AttributeWithDirectHandler]
			public string MyProperty { get; set; }
		}

		private class TypeWithCustomAttribute2
		{
			[AttributeWithIndirectHandler]
			public string MyProperty { get; set; }
		}

		[Test]
		public void DirectAttributeHandler()
		{
			JsonSchema expected = new JsonSchemaBuilder()
				.Type(SchemaValueType.Object)
				.Properties(
					("MyProperty", new JsonSchemaBuilder().Type(SchemaValueType.String).MaxLength(AttributeWithDirectHandler.MaxLength))
				);

			JsonSchema actual = new JsonSchemaBuilder().FromType<TypeWithCustomAttribute1>();

			 AssertEqual(expected, actual);
		}

		[Test]
		public void IndirectAttributeHandler()
		{
			AttributeHandler.RemoveHandler<CustomAttributeHandler>();
			AttributeHandler.AddHandler<CustomAttributeHandler>();

			JsonSchema expected = new JsonSchemaBuilder()
				.Type(SchemaValueType.Object)
				.Properties(
					("MyProperty", new JsonSchemaBuilder().Type(SchemaValueType.String).MaxLength(AttributeWithIndirectHandler.MaxLength))
				);

			JsonSchema actual = new JsonSchemaBuilder().FromType<TypeWithCustomAttribute2>();

			AssertEqual(expected, actual);
		}
	}
}
