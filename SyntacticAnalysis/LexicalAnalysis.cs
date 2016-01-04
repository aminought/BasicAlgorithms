using System;
using System.Collections.Generic;

namespace SyntacticAnalysis
{
	namespace LexicalAnalysis
	{
		public enum TypeOfToken
		{
			Operand,
			UnaryLeftOperator,
			UnaryRightOperator,
			BinaryOperator,
			LeftBracket,
			RightBracker,
			Equals
		}

		public class Token
		{
			public TypeOfToken TypeOfToken;

			public string Value { get; set; }
		}

		public class LexicalAnalysis
		{
			public List<Token> Analyze (string expression)
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
							token.TypeOfToken = TypeOfToken.UnaryLeftOperator;
						}

						while (i++ < expression.Length - 1) {
							char nextChar = expression [i];
							if (Char.IsLetterOrDigit (nextChar)) {
								token.Value += Char.ToString (nextChar);
							} else {
								--i;
								break;
							}
						}
					} else { // example: *, /, -
						if (currentChar == '!') {
							token.TypeOfToken = TypeOfToken.UnaryRightOperator;
						} else if (currentChar == '(') {
							token.TypeOfToken = TypeOfToken.LeftBracket;
						} else if (currentChar == ')') {
							token.TypeOfToken = TypeOfToken.RightBracker;
						} else {
							token.TypeOfToken = TypeOfToken.BinaryOperator;
						}
					}

					tokens.Add (token);
				}
				return tokens;
			}
		}
	}
}
