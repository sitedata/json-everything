using System;
using System.Collections.Generic;

namespace Json.Path
{
	internal abstract class SelectorBase : ISelector
	{
		public virtual void Evaluate(EvaluationContext context)
		{
			var toProcess = new List<PathMatch>(context.Current);
			context.Current.Clear();
			foreach (var match in toProcess)
			{
				context.Current.AddRange(ProcessMatch(match));
			}
		}

		public virtual bool CanBeNormalized()
		{
			return false;
		}

		public virtual string GetNormalizedString()
		{
			throw new NotImplementedException();
		}

		protected abstract IEnumerable<PathMatch> ProcessMatch(PathMatch match);
	}
}