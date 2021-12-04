using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Json.Schema.Generation
{
	/// <summary>
	/// Adds attribute-related schema elements.
	/// </summary>
	public static class AttributeHandler
	{
		private static readonly Dictionary<Type, Func<Attribute, IAttributeHandler>> _handlers = new();

		static AttributeHandler()
		{
			AddHandler<JsonNumberHandlingAttributeHandler>();
			AddHandler<ObsoleteAttributeHandler>();
		}

		/// <summary>
		/// Adds a handler for a custom attribute that cannot be made to implement <see cref="IAttributeHandler"/>.
		/// </summary>
		/// <typeparam name="T">The handler type.</typeparam>
		public static void AddHandler<T>()
			where T : IAttributeHandler
		{
			if (_handlers.Any(h => h.GetType() == typeof(T))) return;

			var constructor = typeof(T).GetConstructors()
				.FirstOrDefault(x =>
				{
					var parameters = x.GetParameters();
					if (parameters.Length != 1) return false;
					var parameter = parameters[0];
					return typeof(Attribute).IsAssignableFrom(parameter.ParameterType);
				});
			if (constructor == null) return;

			_handlers[constructor.GetParameters()[0].ParameterType] = x => (IAttributeHandler) constructor.Invoke(new object[] {x});
		}

		/// <summary>
		/// Removes a handler type.
		/// </summary>
		/// <typeparam name="T">The handler type.</typeparam>
		public static void RemoveHandler<T>()
			where T : IAttributeHandler
		{
			_handlers.Remove(typeof(T));
		}

		internal static void HandleAttributes(SchemaGeneratorContext context)
		{
			var attributes = context.Type.GetCustomAttributes().ToList();
			var selfHandling = attributes.Where(x => x is IAttributeHandler).ToList();
			var customHandling = attributes.Except(selfHandling);
			var customHandlers = _handlers.Join(customHandling,
					h => h.Key,
					a => a.GetType(),
					(h, a) => h.Value(a))
				.ToList();
			var handlers = selfHandling.Cast<IAttributeHandler>().Concat(customHandlers);

			context.Intents.AddRange(handlers.SelectMany(x => x.GetConstraints(context)));
		}

		internal static IEnumerable<ISchemaKeywordIntent> HandleExtraAttribute(SchemaGeneratorContext context, Attribute attribute)
		{
			if (attribute is IAttributeHandler handler) return handler.GetConstraints(context);

			if (!_handlers.TryGetValue(attribute.GetType(), out var handlerFunc)) return Enumerable.Empty<ISchemaKeywordIntent>();

			return handlerFunc(attribute).GetConstraints(context);
		}
	}
}