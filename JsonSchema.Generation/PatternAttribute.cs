using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Applies a `pattern` keyword.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class PatternAttribute : Attribute, IAttributeHandler
	{
		/// <summary>
		/// The regular expression pattern.
		/// </summary>
		public string Value { get; }

		/// <summary>
		/// Creates a new <see cref="PatternAttribute"/> instance.
		/// </summary>
		/// <param name="value">The value.</param>
		public PatternAttribute([RegexPattern] string value)
		{
			Value = value;
		}

		IEnumerable<ISchemaKeywordIntent> IAttributeHandler.GetConstraints(SchemaGeneratorContext context)
		{
			if (context.Type != typeof(string)) yield break;

			yield return new PatternIntent(Value);
		}
	}
}