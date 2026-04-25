using System;

namespace iE.Core.Common.UnitConversions
{
    public class ConverterService
    {
        public ConverterResult Convert(ConverterInput input)
        {
            Validate(input);

            double result = input.Category switch
            {
                "Length" => ConvertLength(input.Value, input.FromUnit, input.ToUnit),
                "Temperature" => ConvertTemperature(input.Value, input.FromUnit, input.ToUnit),
                "Area" => ConvertArea(input.Value, input.FromUnit, input.ToUnit),
                "Volume" => ConvertVolume(input.Value, input.FromUnit, input.ToUnit),
                "Weight" => ConvertWeight(input.Value, input.FromUnit, input.ToUnit),
                "Time" => ConvertTime(input.Value, input.FromUnit, input.ToUnit),
                "Pressure" => ConvertPressure(input.Value, input.FromUnit, input.ToUnit),
                _ => throw new Exception("Invalid category")
            };

            return new ConverterResult
            {
                Category = input.Category,
                InputValue = input.Value,
                ConvertedValue = Math.Round(result, 6),
                FromUnit = input.FromUnit,
                ToUnit = input.ToUnit,
                Display = $"{input.Value} {input.FromUnit} = {result:0.######} {input.ToUnit}"
            };
        }

        private void Validate(ConverterInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Category))
                throw new Exception("Category required");

            if (string.IsNullOrWhiteSpace(input.FromUnit))
                throw new Exception("FromUnit required");

            if (string.IsNullOrWhiteSpace(input.ToUnit))
                throw new Exception("ToUnit required");
        }
        private double ConvertVolume(double value, string from, string to)
        {
            // Convert everything TO cubic inches
            double in3 = from switch
            {
                "Cubic Inches" => value,
                "Cubic Feet" => value * 1728.0,
                "Cubic Yards" => value * 46656.0,
                "Cubic Centimeters" => value * 0.0610237441,
                "Cubic Meters" => value * 61023.7441,
                "Liters" => value * 61.0237441,
                "Milliliters" => value * 0.0610237441,
                "Gallons (US)" => value * 231.0,
                "Quarts (US)" => value * 57.75,
                "Pints (US)" => value * 28.875,
                "Cups (US)" => value * 14.4375,
                "Fluid Ounces (US)" => value * 1.8046875,
                "Tablespoons (US)" => value * 0.90234375,
                "Teaspoons (US)" => value * 0.30078125,
                "Barrels (Oil)" => value * 9702.0,
                _ => throw new Exception($"Unsupported volume unit: {from}")
            };

            // Convert FROM cubic inches to target
            return to switch
            {
                "Cubic Inches" => in3,
                "Cubic Feet" => in3 / 1728.0,
                "Cubic Yards" => in3 / 46656.0,
                "Cubic Centimeters" => in3 / 0.0610237441,
                "Cubic Meters" => in3 / 61023.7441,
                "Liters" => in3 / 61.0237441,
                "Milliliters" => in3 / 0.0610237441,
                "Gallons (US)" => in3 / 231.0,
                "Quarts (US)" => in3 / 57.75,
                "Pints (US)" => in3 / 28.875,
                "Cups (US)" => in3 / 14.4375,
                "Fluid Ounces (US)" => in3 / 1.8046875,
                "Tablespoons (US)" => in3 / 0.90234375,
                "Teaspoons (US)" => in3 / 0.30078125,
                "Barrels (Oil)" => in3 / 9702.0,
                _ => throw new Exception($"Unsupported volume unit: {to}")
            };
        }
        private double ConvertPressure(double value, string from, string to)
        {
            // Convert everything TO PSI (base unit)
            double psi = from switch
            {
                "PSI" => value,
                "Pa" => value * 0.0001450377377,
                "kPa" => value * 0.1450377377,
                "MPa" => value * 145.0377377,
                "bar" => value * 14.5037738,
                "atm" => value * 14.6959,
                "mmHg" => value * 0.0193368,
                "Torr" => value * 0.0193368,
                "inWC" => value * 0.0360912,
                "inHg" => value * 0.491154,
                _ => throw new Exception($"Unsupported pressure unit: {from}")
            };

            // Convert FROM PSI to target
            return to switch
            {
                "PSI" => psi,
                "Pa" => psi / 0.0001450377377,
                "kPa" => psi / 0.1450377377,
                "MPa" => psi / 145.0377377,
                "bar" => psi / 14.5037738,
                "atm" => psi / 14.6959,
                "mmHg" => psi / 0.0193368,
                "Torr" => psi / 0.0193368,
                "inWC" => psi / 0.0360912,
                "inHg" => psi / 0.491154,
                _ => throw new Exception($"Unsupported pressure unit: {to}")
            };
        }
        private double ConvertWeight(double value, string from, string to)
        {
            // Convert everything TO pounds (lb)
            double pounds = from switch
            {
                "Ounces" => value / 16.0,
                "Pounds" => value,
                "Short Tons (US)" => value * 2000.0,
                "Milligrams" => value / 453592.37 / 1000.0,
                "Grams" => value / 453.59237,
                "Kilograms" => value * 2.20462262,
                "Metric Tons" => value * 2204.62262,
                _ => throw new Exception($"Unsupported weight unit: {from}")
            };

            // Convert FROM pounds to target
            return to switch
            {
                "Ounces" => pounds * 16.0,
                "Pounds" => pounds,
                "Short Tons (US)" => pounds / 2000.0,
                "Milligrams" => pounds * 453592.37 * 1000.0,
                "Grams" => pounds * 453.59237,
                "Kilograms" => pounds / 2.20462262,
                "Metric Tons" => pounds / 2204.62262,
                _ => throw new Exception($"Unsupported weight unit: {to}")
            };
        }

        private double ConvertTime(double value, string from, string to)
        {
            // Convert everything TO hours first
            double hours = from switch
            {
                "Nanoseconds" => value / 3600000000000.0,
                "Microseconds" => value / 3600000000.0,
                "Milliseconds" => value / 3600000.0,
                "Seconds" => value / 3600.0,
                "Minutes" => value / 60.0,
                "Hours" => value,
                "Days" => value * 24.0,
                "Weeks" => value * 168.0,
                "Months (avg)" => value * 730.5,
                "Years (avg)" => value * 8766.0,
                "Decades" => value * 87660.0,
                "Centuries" => value * 876600.0,
                "Quarters (avg)" => value * 2191.5,
                _ => throw new Exception($"Unsupported time unit: {from}")
            };

            // Convert FROM hours to target
            return to switch
            {
                "Nanoseconds" => hours * 3600000000000.0,
                "Microseconds" => hours * 3600000000.0,
                "Milliseconds" => hours * 3600000.0,
                "Seconds" => hours * 3600.0,
                "Minutes" => hours * 60.0,
                "Hours" => hours,
                "Days" => hours / 24.0,
                "Weeks" => hours / 168.0,
                "Months (avg)" => hours / 730.5,
                "Years (avg)" => hours / 8766.0,
                "Decades" => hours / 87660.0,
                "Centuries" => hours / 876600.0,
                "Quarters (avg)" => hours / 2191.5,
                _ => throw new Exception($"Unsupported time unit: {to}")
            };

        }
        private double ConvertArea(double value, string from, string to)
        {
            // Convert everything TO square feet
            double ft2 = from switch
            {
                "Square Inches" => value / 144.0,
                "Square Feet" => value,
                "Square Yards" => value * 9.0,
                "Square Miles" => value * 27878400.0,
                "Acres" => value * 43560.0,
                "Square Millimeters" => value / 92903.04,
                "Square Centimeters" => value / 929.0304,
                "Square Meters" => value * 10.7639104167,
                "Square Kilometers" => value * 10763910.4167,
                "Hectares" => value * 107639.104167,
                _ => throw new Exception($"Unsupported area unit: {from}")
            };

            // Convert FROM square feet to target
            return to switch
            {
                "Square Inches" => ft2 * 144.0,
                "Square Feet" => ft2,
                "Square Yards" => ft2 / 9.0,
                "Square Miles" => ft2 / 27878400.0,
                "Acres" => ft2 / 43560.0,
                "Square Millimeters" => ft2 * 92903.04,
                "Square Centimeters" => ft2 * 929.0304,
                "Square Meters" => ft2 / 10.7639104167,
                "Square Kilometers" => ft2 / 10763910.4167,
                "Hectares" => ft2 / 107639.104167,
                _ => throw new Exception($"Unsupported area unit: {to}")
            };
        }

        private double ConvertTemperature(double value, string from, string to)
        {
            // Convert everything TO Celsius first
            double celsius = from switch
            {
                "Celsius" => value,
                "Fahrenheit" => (value - 32.0) * 5.0 / 9.0,
                "Kelvin" => value - 273.15,
                _ => throw new Exception($"Unsupported temperature unit: {from}")
            };

            if (from == "Kelvin" && value < 0)
                throw new Exception("Kelvin cannot be below zero.");

            // Convert FROM Celsius to target
            return to switch
            {
                "Celsius" => celsius,
                "Fahrenheit" => (celsius * 9.0 / 5.0) + 32.0,
                "Kelvin" => celsius + 273.15,
                _ => throw new Exception($"Unsupported temperature unit: {to}")
            };
        }
        private double ConvertLength(double value, string from, string to)
        {
            // Convert everything TO inches
            double inches = from switch
            {
                "Inches" => value,
                "Feet" => value * 12.0,
                "Yards" => value * 36.0,
                "Miles" => value * 63360.0,
                "Millimeters" => value / 25.4,
                "Centimeters" => value / 2.54,
                "Meters" => value * 39.37007874,
                "Kilometers" => value * 39370.07874,
                _ => throw new Exception($"Unsupported length unit: {from}")
            };

            // Convert FROM inches to target
            return to switch
            {
                "Inches" => inches,
                "Feet" => inches / 12.0,
                "Yards" => inches / 36.0,
                "Miles" => inches / 63360.0,
                "Millimeters" => inches * 25.4,
                "Centimeters" => inches * 2.54,
                "Meters" => inches / 39.37007874,
                "Kilometers" => inches / 39370.07874,
                _ => throw new Exception($"Unsupported length unit: {to}")
            };
        }

        // 👇 COPY your EXACT methods here (no changes needed)
    }
}