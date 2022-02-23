using System.Collections.Generic;
using NUnit.Framework;

namespace Json.Path.Tests
{
	public class NormalizationTests
	{
		public static IEnumerable<TestCaseData> NormalizationCases
		{
			get
			{
				yield return new TestCaseData("$.foo", "$['foo']");
				yield return new TestCaseData("$['foo']", "$['foo']");
				yield return new TestCaseData("$[\"foo\"]", "$['foo']");
				yield return new TestCaseData("$[3]", "$[3]");
				yield return new TestCaseData("$.foo[-1]", null);
				yield return new TestCaseData("$.foo[1:4]", null);
				yield return new TestCaseData("$.foo[1,4]", null);
				yield return new TestCaseData("$[?(@.foo == 'bar')]", null);
			}
		}

		[TestCaseSource(nameof(NormalizationCases))]
		public void Normalize(string pathText, string? normalized)
		{
			var path = JsonPath.Parse(pathText);
			var normalizeable = path.TryGetNormalizedString(out var actual);

			Assert.AreEqual(normalized != null, normalizeable);
			Assert.AreEqual(normalized, actual);
		}
	}
}
