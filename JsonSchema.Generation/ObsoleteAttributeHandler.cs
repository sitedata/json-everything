using System;
using System.Collections.Generic;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation;

/// <summary>
/// Handler for the <see cref="ObsoleteAttribute"/>.
/// </summary>
public class ObsoleteAttributeHandler : IAttributeHandler
{
#pragma warning disable IDE0060 // Remove unused parameter
	// ReSharper disable once UnusedParameter.Local
	internal ObsoleteAttributeHandler(ObsoleteAttribute attribute)
#pragma warning restore IDE0060 // Remove unused parameter
	{
	}

	/// <summary>
	/// Processes the type and any attributes (present on the context), and adds
	/// intents to the context.
	/// </summary>
	/// <param name="context">The generation context.</param>
	public IEnumerable<ISchemaKeywordIntent> GetConstraints(SchemaGeneratorContext context)
	{
		yield return new DeprecatedIntent(true);
	}
}