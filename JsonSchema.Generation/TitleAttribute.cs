using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `title` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Property)]
	public class TitleAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The title.
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Creates a new <see cref="TitleAttribute"/> instance.
		/// </summary>
		/// <param name="title">The value.</param>
		public TitleAttribute(string title)
		{
			Title = title;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			yield return new TitleIntent(Title);
		}
	}
}