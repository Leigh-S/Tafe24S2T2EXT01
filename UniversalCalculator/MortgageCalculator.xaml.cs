using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
			// Attach event handlers for input validation
			PrincipalBorrowTextBox.BeforeTextChanging += TextBox_BeforeTextChanging;
			YearsTextBox.BeforeTextChanging += TextBox_BeforeTextChanging;
			MonthsTextBox.BeforeTextChanging += TextBox_BeforeTextChanging;
			YearlyInterestRateTextBox.BeforeTextChanging += TextBox_BeforeTextChanging;
		}

		private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
		{
			// Ensure only numeric input
			args.Cancel = !IsTextNumeric(args.NewText);
		}

		private bool IsTextNumeric(string text)
		{
			foreach (char c in text)
			{
				if (!char.IsDigit(c) && c != '.')
				{
					return false;
				}
			}
			return true;
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			// Get input values
			double principal = double.Parse(PrincipalBorrowTextBox.Text);
			int years = int.Parse(YearsTextBox.Text);
			int months = int.Parse(MonthsTextBox.Text);
			double yearlyInterestRate = double.Parse(YearlyInterestRateTextBox.Text) / 100;

			// Calculate monthly interest rate
			double monthlyInterestRate = yearlyInterestRate / 12;
			MonthlyInterestRateTextBlock.Text = monthlyInterestRate.ToString("P");

			// Calculate number of months
			int totalMonths = (years * 12) + months;

			// Calculate monthly repayment 
			double monthlyRepayment = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths)) / (Math.Pow(1 + monthlyInterestRate, totalMonths) - 1);
			MonthlyRepaymentsTextBlock.Text = monthlyRepayment.ToString("C");
		}
	}
}
