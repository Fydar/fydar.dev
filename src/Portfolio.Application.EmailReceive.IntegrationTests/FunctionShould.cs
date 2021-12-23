
using Amazon.Lambda.SimpleEmailEvents;
using Amazon.Lambda.SimpleEmailEvents.Actions;
using Amazon.Lambda.TestUtilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Application.EmailReceive.IntegrationTests
{
	[TestFixture(TestOf = typeof(Function))]
	public class FunctionShould
	{
		[Test, Parallelizable]
		public async Task RunSuccessfully()
		{
			// Arrange
			var function = new Function();
			var context = new TestLambdaContext();
			var sesEvent = new SimpleEmailEvent<LambdaReceiptAction>()
			{
				Records = new List<SimpleEmailEvent<LambdaReceiptAction>.SimpleEmailRecord<LambdaReceiptAction>>()
				{
					/* new SimpleEmailEvent<LambdaReceiptAction>.SimpleEmailRecord<LambdaReceiptAction>()
					{
						Ses = new SimpleEmailEvent<LambdaReceiptAction>.SimpleEmailService<LambdaReceiptAction>()
						{
							Mail = new SimpleEmailEvent<LambdaReceiptAction>.SimpleEmailMessage()
							{
								
							}
						}
					}
					*/
				}
			};

			// Act
			string upperCase = await function.FunctionHandler(sesEvent, context);

			// Assert
			Assert.AreEqual("CONTINUE", upperCase);
		}
	}
}
