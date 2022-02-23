using System;

namespace Json.Path
{
	internal class ErrorSelector : ISelector
	{
		public string ErrorMessage { get; }

		public ErrorSelector(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public void Evaluate(EvaluationContext context)
		{
			throw new NotImplementedException();
		}

		public bool CanBeNormalized()
		{
			return false;
		}

		public string GetNormalizedString()
		{
			throw new NotImplementedException();
		}
	}
}