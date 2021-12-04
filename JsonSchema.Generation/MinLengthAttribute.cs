using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `minimum` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MinLengthAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The minimum length.
		/// </summary>
		public uint Length { get; }

		/// <summary>
		/// Creates a new <see cref="MinLengthAttribute"/> instance.
		/// </summary>
		/// <param name="length">The value.</param>
		public MinLengthAttribute(uint length)
		{
			Length = length;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (context.Type != typeof(string)) yield break;

			yield return new MinLengthIntent(Length);
		}
	}
}