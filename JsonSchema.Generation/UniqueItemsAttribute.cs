using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `uniqueItems` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class UniqueItemsAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// Whether the items should be unique.
		/// </summary>
		public bool Value { get; }

		/// <summary>
		/// Creates a new <see cref="UniqueItemsAttribute"/> instance.
		/// </summary>
		/// <param name="value">The value.</param>
		public UniqueItemsAttribute(bool value)
		{
			Value = value;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (!context.Type.IsArray() || context.Type == typeof(string)) yield break;

			yield return new UniqueItemsIntent(Value);
		}
	}
}