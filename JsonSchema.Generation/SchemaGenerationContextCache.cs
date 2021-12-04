using System;
using System.Collections.Generic;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Gets the contexts for the current run.
	/// </summary>
	public static class SchemaGenerationContextCache
	{
		[ThreadStatic]
		private static Dictionary<Type, SchemaGeneratorContext>? _cache;

		private static Dictionary<Type, SchemaGeneratorContext> Cache => _cache ??= new Dictionary<Type, SchemaGeneratorContext>();

		/// <summary>
		/// Gets or creates a <see cref="SchemaGeneratorContext"/> based on the given
		/// type and attribute set.
		/// </summary>
		/// <param name="type">The type to generate.</param>
		/// <param name="configuration">The generator configuration.</param>
		/// <returns>
		/// A generation context, from the cache if one exists with the specified
		/// type and attribute set; otherwise a new one.  New contexts are automatically
		/// cached.
		/// </returns>
		/// <remarks>
		/// Use this in your generator if it needs to create keywords with subschemas.
		/// </remarks>
		public static SchemaGeneratorContext Get(Type type, SchemaGeneratorConfiguration configuration)
		{
			if (!Cache.TryGetValue(type, out var context))
			{
				context = new SchemaGeneratorContext(type, configuration);
				Cache[type] = context;
				context.GenerateIntents();
			}

			return context;
		}

		internal static void Clear()
		{
			Cache.Clear();
		}
	}
}