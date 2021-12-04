using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `description` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Property)]
	public class DescriptionAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The description.
		/// </summary>
		public string Desription { get; }

		/// <summary>
		/// Creates a new <see cref="DescriptionAttribute"/> instance.
		/// </summary>
		/// <param name="description">The value.</param>
		public DescriptionAttribute(string description)
		{
			Desription = description;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			yield return new DescriptionIntent(Desription);
		}
	}
}