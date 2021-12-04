using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Json.Schema.Generation.Intents;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Handler for the <see cref="JsonNumberHandlingAttribute"/>.
	/// </summary>
	public class JsonNumberHandlingAttributeHandler : IAttributeHandler
	{
		private readonly JsonNumberHandlingAttribute _attribute;

		internal JsonNumberHandlingAttributeHandler(JsonNumberHandlingAttribute attribute)
		{
			_attribute = attribute;
		}

		/// <summary>
		/// Processes the type and any attributes (present on the context), and adds
		/// intents to the context.
		/// </summary>
		/// <param name="context">The generation context.</param>
		public IEnumerable<ISchemaKeywordIntent> GetConstraints(SchemaGeneratorContext context)
		{
			if (!context.Type.IsNumber()) yield break;

			var typeIntent = context.Intents.OfType<TypeIntent>().FirstOrDefault();

			var existingType = typeIntent?.Type ?? default;

			if (_attribute.Handling.HasFlag(JsonNumberHandling.AllowReadingFromString))
			{
				context.Intents.Remove(typeIntent!);
				yield return new TypeIntent(existingType | SchemaValueType.String);
			}

			if (_attribute.Handling.HasFlag(JsonNumberHandling.AllowNamedFloatingPointLiterals))
			{
				var currentSchema = context.Intents.ToList();
				context.Intents.Clear();
				yield return new AnyOfIntent(currentSchema,
					new ISchemaKeywordIntent[] {new EnumIntent("NaN", "Infinity", "-Infinity")}
				);
			}
		}
	}
}