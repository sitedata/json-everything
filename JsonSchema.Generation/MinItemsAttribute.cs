using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `minItems` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MinItemsAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The minimum number of items.
		/// </summary>
		public uint Value { get; }

		/// <summary>
		/// Creates a new <see cref="MinItemsAttribute"/> instance.
		/// </summary>
		/// <param name="value">The value.</param>
		public MinItemsAttribute(uint value)
		{
			Value = value;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (!context.Type.IsArray()) yield break;

			yield return new MinItemsIntent(Value);
		}
	}
}