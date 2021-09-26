using System;
using System.Collections.Generic;
using System.Linq;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `title` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
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

		void IAttributeHandler.AddConstraints(SchemaGeneratorContext context, IEnumerable<Attribute> attributes)
		{
			var attribute = attributes.OfType<TitleAttribute>().FirstOrDefault();
			if (attribute == null) return;

			context.Intents.Add(new TitleIntent(attribute.Title));
		}
	}
}