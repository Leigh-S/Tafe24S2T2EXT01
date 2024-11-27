using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class CurrencyConverter : Page
	{
		public CurrencyConverter()
		{
			this.InitializeComponent();
			InitializeCurrencyComboBoxes();
		}
				private void InitializeCurrencyComboBoxes()
				{
					string[] currencies = { 
						"USD - United States Dollar", 
						"EUR - Euro", 
						"GBP - British Pound", 
						"INR - Indian Rupee" 
					};
					FromCurrency.ItemsSource = currencies;
					ToCurrency.ItemsSource = currencies;
		}

		private void ConvertButton_Click(object sender, RoutedEventArgs e)
		{
			if (FromCurrency.SelectedItem == null || ToCurrency.SelectedItem == null || 
				string.IsNullOrEmpty(AmountToConvert.Text))
			{
				ResultText.Text = "Please fill in all fields";
				return;
			}

			if (!double.TryParse(AmountToConvert.Text, out double amount))
			{
				ResultText.Text = "Please enter a valid number";
				return;
			}

			// Add conversion logic here
			double result = PerformConversion(amount);
			ResultText.Text = $"{amount} {FromCurrency.SelectedItem} = {result:F2} {ToCurrency.SelectedItem}";
		}

				private double PerformConversion(double amount)
				{
					// Extract just the currency code (first 3 characters) from the selection
					string from = FromCurrency.SelectedItem.ToString().Substring(0, 3);
					string to = ToCurrency.SelectedItem.ToString().Substring(0, 3);

					double usdAmount = ConvertToUSD(amount, from);
					return ConvertFromUSD(usdAmount, to);
				}

		private double ConvertToUSD(double amount, string fromCurrency)
		{
			switch (fromCurrency)
			{
				case "USD": return amount;
				case "EUR": return amount * 1.1739732;
				case "GBP": return amount * 1.371907;
				case "INR": return amount * 0.011492628;
				default: return 0;
			}
		}

		private double ConvertFromUSD(double usdAmount, string toCurrency)
		{
			switch (toCurrency)
			{
				case "USD": return usdAmount;
				case "EUR": return usdAmount * 0.85189982;
				case "GBP": return usdAmount * 0.72872436;
				case "INR": return usdAmount * 74.257327;
				default: return 0;
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Calculator.MainMenu));
		}
	}
}	