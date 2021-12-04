using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies an `maxItems` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MaxItemsAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The maximum number of items.
		/// </summary>
		public uint Value { get; }

		/// <summary>
		/// Creates a new <see cref="MaxItemsAttribute"/> instance.
		/// </summary>
		/// <param name="value">The value.</param>
		public MaxItemsAttribute(uint value)
		{
			Value = value;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (!context.Type.IsArray()) yield break;

			yield return new MaxItemsIntent(Value);
		}
	}
}