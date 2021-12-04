using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies an `exclusiveMaximum` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ExclusiveMaximumAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The exclusive maximum.
		/// </summary>
		public decimal Value { get; }

		/// <summary>
		/// Creates a new <see cref="ExclusiveMaximumAttribute"/> instance.
		/// </summary>
		/// <param name="value">The value.</param>
		public ExclusiveMaximumAttribute(double value)
		{
			Value = Convert.ToDecimal(value);
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (!context.Type.IsNumber()) yield break;

			yield return new ExclusiveMaximumIntent(Value);
		}
	}
}