using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `maxLength` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MaxLengthAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The maximum length.
		/// </summary>
		public uint Length { get; }

		/// <summary>
		/// Creates a new <see cref="MaxLengthAttribute"/> instance.
		/// </summary>
		/// <param name="length">The value.</param>
		public MaxLengthAttribute(uint length)
		{
			Length = length;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (context.Type != typeof(string)) yield break;

			yield return new MaxLengthIntent(Length);
		}
	}
}