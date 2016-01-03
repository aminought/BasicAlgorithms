using System;
using System.Collections.Generic;

namespace SyntacticAnalysis
{
	public enum TypeOfToken
	{
		Operator,
		Operand
	}

	public class Token
	{
		public TypeOfToken TypeOfToken;

		public string Value { get; set; }
	}

	public class LexicalAnalysis
	{
		public List<Token> Analize (string expression)
		{
			var tokens = new List<Token> ();
			for (int i = 0; i < expression.Length; ++i) {
				var currentChar = expression [i];
				var token = new Token ();
				token.Value = Char.ToString (currentChar);

				if (Char.IsLetterOrDigit (currentChar)) { // example: 234, cos, sin, 345.3
					if (Char.IsDigit (currentChar) || currentChar == '.' || currentChar == ',') {
						token.TypeOfToken = TypeOfToken.Operand;
					} else {
						token.TypeOfToken = TypeOfToken.Operator;
					}

					int j = i;
					while (j++ < expression.Length) {
						char nextChar = expression [j];
						if (Char.IsDigit (nextChar)) {
							token.Value += Char.ToString (nextChar);
						} else {
							i = --j;
							break;
						}
					}
				} else { // example: *, /, -
					token.TypeOfToken = TypeOfToken.Operator;
				}

				tokens.Add (token);
			}
			return tokens;
		}
	}
}
