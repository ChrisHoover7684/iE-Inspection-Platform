using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace iE.Core.Inspection.StiSp001
{
    public class StiSp001Service
    {
        public StiSp001ScheduleResult CalculateSchedule(StiSp001ScheduleInput input)
        {
            var capacity = GetCapacityInGallons(input);
            var result = new StiSp001ScheduleResult
            {
                CapacityGallons = capacity
            };

            if (input.IsPortableContainer)
            {
                if (capacity > 0 && capacity < 55)
                {
                    result.ValidationWarning = "The standard's scope for portable containers is specifically those >= 55 gal.";
                }

                result.DeterminedCategory = "Portable Container";
                result.ResultColor = "Default";
                result.RawSchedule = DetermineSchedule(0, capacity, input);
                result.FormattedSchedule = FormatSchedule(result.RawSchedule, input);
                return result;
            }

            if (capacity > 75000)
                throw new Exception("Tanks with capacity > 75,000 gallons are outside the scope of STI-SP001. Please use API 653.");

            bool hasSpillControl = input.SpillControlCount > 0;
            bool hasCrdm = input.CrdmCount > 0;

            int category = hasSpillControl && hasCrdm ? 1 : hasSpillControl ? 2 : 3;

            result.Category = category;
            result.DeterminedCategory = $"Category {category}";
            result.ShowRegulatoryWarning = category == 3;
            result.ResultColor = category switch
            {
                1 => "LightGreen",
                2 => "LightYellow",
                3 => "LightCoral",
                _ => "Default"
            };

            result.RawSchedule = DetermineSchedule(category, capacity, input);
            result.FormattedSchedule = FormatSchedule(result.RawSchedule, input);

            return result;
        }

        public StiSp001ThicknessResult EvaluateThickness(StiSp001ThicknessInput input)
        {
            if (input.OriginalThickness <= 0 || input.MeasuredThickness <= 0)
                throw new Exception("Original and measured thickness must be greater than zero.");

            int effectiveCategory = input.IsDoubleWall ? 1 : input.Category;
            if (effectiveCategory < 1 || effectiveCategory > 3)
                throw new Exception("Category must be 1, 2, or 3.");

            double d75 = input.OriginalThickness * 0.75;
            double d50 = input.OriginalThickness * 0.50;
            double d25 = input.OriginalThickness * 0.25;

            bool areaRequired = false;
            if (effectiveCategory == 1 && input.MeasuredThickness >= d25 && input.MeasuredThickness < d50)
                areaRequired = true;
            else if (effectiveCategory == 2 && input.MeasuredThickness >= d50 && input.MeasuredThickness < d75)
                areaRequired = true;

            double area = input.AffectedAreaSquareInches ?? 0;
            if (areaRequired && area <= 0)
                throw new Exception("Affected area is required and must be greater than zero for this thickness range/category.");

            bool repairRequired = false;
            string reason = string.Empty;

            switch (effectiveCategory)
            {
                case 3:
                    if (input.MeasuredThickness < d75)
                    {
                        repairRequired = true;
                        reason = "Measured thickness is less than 75% of original thickness at any point.";
                    }
                    break;
                case 2:
                    if (input.MeasuredThickness < d50)
                    {
                        repairRequired = true;
                        reason = "Measured thickness is less than 50% of original thickness at any point.";
                    }
                    else if (input.MeasuredThickness < d75 && area > 3.0)
                    {
                        repairRequired = true;
                        reason = "More than 3 sq. in. in a 1 sq. ft. area is less than 75% of original thickness.";
                    }
                    break;
                case 1:
                    if (input.MeasuredThickness < d25)
                    {
                        repairRequired = true;
                        reason = "Measured thickness is less than 25% of original thickness at any point.";
                    }
                    else if (input.MeasuredThickness < d50 && area > 3.0)
                    {
                        repairRequired = true;
                        reason = "More than 3 sq. in. in a 1 sq. ft. area is less than 50% of original thickness.";
                    }
                    break;
            }

            var message = new StringBuilder();
            if (repairRequired)
            {
                message.AppendLine($"REPAIR/REPLACE REQUIRED per SP001 10.3.{effectiveCategory}.");
                message.AppendLine($"Condition: {reason}");
                message.Append("The next inspection must be scheduled within 5 years.");
            }
            else
            {
                message.AppendLine("NO REPAIR REQUIRED.");
                message.Append($"Thickness and area are within acceptable limits for Category {effectiveCategory}");
                message.Append(input.IsDoubleWall ? " (Double-Wall Secondary Tank)." : ".");
            }

            return new StiSp001ThicknessResult
            {
                AreaRequired = areaRequired,
                RepairRequired = repairRequired,
                EffectiveCategory = effectiveCategory,
                Result = message.ToString(),
                ResultColor = repairRequired ? "Red" : "Green"
            };
        }

        public StiSp001ContainmentResult CalculateContainment(StiSp001ContainmentInput input)
        {
            if (input.DikeLengthFeet <= 0 || input.DikeWidthFeet <= 0 || input.DikeHeightFeet <= 0 || input.LargestTankVolumeGallons <= 0)
                throw new Exception("All dimensions and largest tank volume must be greater than zero.");

            const double GallonsPerCubicFoot = 7.48052;
            double available = input.DikeLengthFeet * input.DikeWidthFeet * input.DikeHeightFeet * GallonsPerCubicFoot;
            double required = input.LargestTankVolumeGallons * 1.10;
            bool isPass = available >= required;

            string result = isPass ? "CONTAINMENT PASS:\n" : "CONTAINMENT FAILURE:\n";
            result += $"Available Capacity: {FormatDouble(available, 1)} gal\n";
            result += $"Required Capacity (110% of largest tank): {FormatDouble(required, 1)} gal";

            return new StiSp001ContainmentResult
            {
                Pass = isPass,
                AvailableCapacityGallons = Math.Round(available, 1),
                RequiredCapacityGallons = Math.Round(required, 1),
                Result = result,
                ResultColor = isPass ? "Green" : "Red"
            };
        }

        private static double GetCapacityInGallons(StiSp001ScheduleInput input)
        {
            if (input.UseDirectCapacity)
            {
                if (!input.Capacity.HasValue)
                {
                    if (input.IsPortableContainer) return 0;
                    throw new Exception("Please enter a valid, positive numeric capacity.");
                }

                if (input.Capacity.Value <= 0)
                    throw new Exception("Please enter a valid, positive numeric capacity.");

                return input.CapacityUnit switch
                {
                    "Barrels" => input.Capacity.Value * 42,
                    "Liters" => input.Capacity.Value * 0.264172,
                    _ => input.Capacity.Value
                };
            }

            if (!input.DiameterFeet.HasValue || !input.HeightFeet.HasValue || input.DiameterFeet.Value <= 0 || input.HeightFeet.Value <= 0)
                throw new Exception("Please enter valid, positive dimensions for the cylinder.");

            double radius = input.DiameterFeet.Value / 2.0;
            double volumeCubicFeet = Math.PI * Math.Pow(radius, 2) * input.HeightFeet.Value;
            return volumeCubicFeet * 7.48052;
        }

        private static string FormatDouble(double value, int precision)
        {
            return value.ToString("F" + precision, CultureInfo.InvariantCulture);
        }

        private static string DetermineSchedule(int category, double capacityGallons, StiSp001ScheduleInput input)
        {
            if (input.IsPortableContainer)
            {
                return input.PortableMaterial switch
                {
                    "Plastic" => "P** (Plastic: Recertify every 7 years)",
                    "Steel" => "P** (Steel: Recertify every 12 years)",
                    "Stainless Steel" => "P** (Stainless Steel: Recertify every 17 years)",
                    _ => "P** (Unknown material)"
                };
            }

            bool verySmall = capacityGallons <= 1100;
            bool small = capacityGallons > 1100 && capacityGallons <= 5000;
            bool medium = capacityGallons > 5000 && capacityGallons <= 30000;
            bool large = capacityGallons > 30000 && capacityGallons <= 75000;

            return category switch
            {
                1 => "P",
                2 when verySmall => "P",
                2 when small => "P, E&L(10)",
                2 when medium => "[P, E(10), I(20)] or [P, E(5), L(10)]",
                2 when large => "P, E&L(5), I(15)",
                3 when verySmall => "P, E&L(10)",
                3 when small => "[P, E&L(5), I(10)] or [P, L(2), E(5)]",
                3 when medium => "[P, E&L(5), I(10)] or [P, L(1), E(5)]",
                3 when large => "P, E&L(5), I(10)",
                _ => "No schedule found for this configuration."
            };
        }

        private static string FormatSchedule(string schedule, StiSp001ScheduleInput input)
        {
            var resultBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(schedule)) return string.Empty;
            if (schedule.Contains("scope") || schedule.Contains("No schedule")) return schedule;

            if (schedule.StartsWith("P**"))
            {
                Match match = Regex.Match(schedule, @"\(([^)]+)\)");
                resultBuilder.AppendLine("- Periodic Inspection: Monthly Visual");
                if (match.Success)
                {
                    resultBuilder.AppendLine("- Special DOT Recertification:");
                    resultBuilder.AppendLine($"  ({match.Groups[1].Value})");
                }
                return resultBuilder.ToString();
            }

            if (schedule.StartsWith("["))
            {
                schedule = schedule.Trim('[', ']');
                string[] options = schedule.Split(new[] { " or " }, StringSplitOptions.None);
                resultBuilder.AppendLine("OPTION 1:");
                resultBuilder.Append(FormatSimpleSchedule(options[0], input));
                if (options.Length > 1)
                {
                    resultBuilder.AppendLine("\n--- OR ---\n");
                    resultBuilder.AppendLine("OPTION 2:");
                    resultBuilder.Append(FormatSimpleSchedule(options[1], input));
                }
                return resultBuilder.ToString();
            }

            return FormatSimpleSchedule(schedule, input);
        }

        private static string FormatSimpleSchedule(string simpleSchedule, StiSp001ScheduleInput input)
        {
            var lines = new StringBuilder();
            string[] parts = simpleSchedule.Split(',').Select(p => p.Trim()).ToArray();

            foreach (var part in parts)
            {
                if (part == "P")
                {
                    lines.AppendLine("- Periodic Inspection: Monthly Visual");
                    continue;
                }

                var match = Regex.Match(part, @"([A-Z&]+)\(([\d\.]+)\)");
                if (!match.Success) continue;

                string types = match.Groups[1].Value;
                if (!double.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double years))
                    continue;

                if (types.Contains("E"))
                {
                    DateTime dueDate = input.LastExternalDate.AddYears((int)years);
                    lines.AppendLine($"- Formal External (E): {years} years --> Next Due: {dueDate:MM/dd/yyyy}");
                }
                if (types.Contains("I"))
                {
                    DateTime dueDate = input.LastInternalDate.AddYears((int)years);
                    lines.AppendLine($"- Formal Internal (I): {years} years --> Next Due: {dueDate:MM/dd/yyyy}");
                }
                if (types.Contains("L"))
                {
                    DateTime dueDate = input.LastLeakTestDate.AddYears((int)years);
                    lines.AppendLine($"- Leak Test (L): {years} years --> Next Due: {dueDate:MM/dd/yyyy}");
                }
            }

            return lines.ToString();
        }
    }
}
