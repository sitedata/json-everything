using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `maximum` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MaximumAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The maximum.
		/// </summary>
		public decimal Value { get; }

		/// <summary>
		/// Creates a new <see cref="MaximumAttribute"/> instance.
		/// </summary>
		/// <param name="value">The value.</param>
		public MaximumAttribute(double value)
		{
			Value = Convert.ToDecimal(value);
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (!context.Type.IsNumber()) yield break;

			yield return new MaximumIntent(Value);
		}
	}
}