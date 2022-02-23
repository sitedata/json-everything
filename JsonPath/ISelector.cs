namespace Json.Path
{
	internal interface ISelector
	{
		void Evaluate(EvaluationContext context);
		bool CanBeNormalized();
		string GetNormalizedString();
	}
}