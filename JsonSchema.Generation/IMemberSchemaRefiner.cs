using System;
using System.Collections.Generic;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Describes a schema generation refiner.
	/// </summary>
	/// <remarks>
	/// Refiners run after attributes have been processed, before the
	/// schema itself is created.  This is used to add customization
	/// logic.
	/// </remarks>
	public interface IMemberSchemaRefiner
	{
		/// <summary>
		/// Determines if the refiner should run.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		bool ShouldRun(SchemaGeneratorContext context, IEnumerable<Attribute> attributes);

		/// <summary>
		/// Runs the refiner.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="attributes"></param>
		void Run(SchemaGeneratorContext context, IEnumerable<Attribute> attributes);
	}
}