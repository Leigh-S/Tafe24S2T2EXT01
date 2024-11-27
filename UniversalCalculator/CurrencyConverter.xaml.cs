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
			string[] currencies = { "USD", "EUR", "GBP", "AUD", "CAD", "JPY", "NZD" };
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
			// Placeholder conversion logic - replace with actual conversion rates
			return amount * 1.5; // Example conversion rate
		}
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Calculator.MainMenu));
		}
		}
	}