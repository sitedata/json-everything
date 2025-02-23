﻿using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Json.More;
// ReSharper disable PossibleMultipleEnumeration

namespace Json.Logic.Rules;

[Operator("min")]
internal class MinRule : Rule
{
	private readonly List<Rule> _items;

	public MinRule(Rule a, params Rule[] more)
	{
		_items = new List<Rule> { a };
		_items.AddRange(more);
	}

	public override JsonElement Apply(JsonElement data)
	{
		var items = _items.Select(i => i.Apply(data)).Select(e => new { e.ValueKind, Value = e.Numberify() }).ToList();
		var nulls = items.Where(i => i.Value == null);
		if (nulls.Any())
			throw new JsonLogicException($"Cannot find min with {nulls.First().ValueKind}.");

		return items.Min(i => i.Value!.Value).AsJsonElement();
	}
}