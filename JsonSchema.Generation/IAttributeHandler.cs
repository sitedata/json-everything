using System;
using System.Collections.Generic;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Defines requirements to handle converting an attribute to a keyword intent.
	/// </summary>
	public interface IAttributeHandler
	{
		/// <summary>
		/// Processes the type and any attributes (present on the context), and adds
		/// intents to the context.
		/// </summary>
		/// <param name="context">The generation context.</param>
		/// <param name="attributes">The list of attributes present to process.</param>
		void AddConstraints(SchemaGeneratorContext context, IEnumerable<Attribute> attributes);
	}
}