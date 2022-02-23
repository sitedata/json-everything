using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Json.Pointer;

namespace Json.Path
{
	internal class IndexSelector : SelectorBase
	{
		private readonly List<IIndexExpression>? _indices;

		public IndexSelector(IEnumerable<IIndexExpression>? indices)
		{
			_indices = indices?.ToList();
		}

		protected override IEnumerable<PathMatch> ProcessMatch(PathMatch match)
		{
			switch (match.Value.ValueKind)
			{
				case JsonValueKind.Array:
					var array = match.Value.EnumerateArray().ToArray();
					IEnumerable<int> indices;
					indices = _indices?.OfType<IArrayIndexExpression>()
						          .SelectMany(r => r.GetIndices(match.Value))
						          .Where(i => 0 <= i && i < array.Length)
						          .Distinct() ??
					          Enumerable.Range(0, array.Length);
					foreach (var index in indices)
					{
						yield return new PathMatch(array[index], match.Location.Combine(PointerSegment.Create(index.ToString())));
					}
					break;
				case JsonValueKind.Object:
					if (_indices != null)
					{
						var props = _indices.OfType<IObjectIndexExpression>()
							.SelectMany(r => r.GetProperties(match.Value))
							.Distinct();
						foreach (var prop in props)
						{
							if (!match.Value.TryGetProperty(prop, out var value)) continue;
							yield return new PathMatch(value, match.Location.Combine(PointerSegment.Create(prop)));
						}
					}
					else
					{
						foreach (var prop in match.Value.EnumerateObject())
						{
							yield return new PathMatch(prop.Value, match.Location.Combine(PointerSegment.Create(prop.Name)));
						}
					}
					break;
			}
		}

		public override string ToString()
		{
			return _indices == null ? "[*]" : $"[{string.Join(",", _indices.Select(r => r.ToString()))}]";
		}

		public override bool CanBeNormalized()
		{
			return _indices is {Count: 1} &&
			       (_indices[0] is PropertyNameIndex ||
			        (_indices[0] is SimpleIndex simple && simple.CanBeNormalized()));
		}

		public override string GetNormalizedString()
		{
			string NormalizeIndex(IIndexExpression index)
			{
				switch (index)
				{
					case PropertyNameIndex prop:
						return prop.Normalize();
					case SimpleIndex simple:
						return simple.ToString();
					default:
						throw new InvalidOperationException();
				}
			}

			return $"[{NormalizeIndex(_indices![0])}]";
		}
	}
}