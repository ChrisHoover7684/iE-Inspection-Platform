using iE.Core.Equipment.Tanks.Data;
using iE.Core.Equipment.Tanks.Models;

namespace iE.Core.Equipment.Tanks.Services
{
    public class TankShellService
    {
        public TankShellResult Calculate(TankShellInput input)
        {
           
            var material = TankMaterialRepository.Get(input.Material);

            if (material == null)
                throw new Exception($"Tank material '{input.Material}' was not found.");

            if (material.IsSpecialCase)
                throw new Exception($"Tank material '{input.Material}' is a special case and requires manual Y/T input.");

            var result = new TankShellResult();

            double totalLiquidHeight = input.Courses.Sum(c => c.Height);
            double heightFromBottom = 0;

            double governingThickness = 0;
            int governingCourse = 0;

            for (int i = 0; i < input.Courses.Count; i++)
            {
                int courseNumber = i + 1;
                var course = input.Courses[i];

                double hCourse = totalLiquidHeight - heightFromBottom;
                if (hCourse < 1.0)
                    hCourse = 1.0;

                var stress = GetAllowableStress(
                    courseNumber,
                    material.YieldStrength,
                    material.TensileStrength,
                    false,
                    input.ApiEdition
                );

                double allowableStress = stress.Stress * course.JointEfficiency;

                double tCalculated =
                    (2.6 * input.Diameter * (hCourse - 1) * input.SpecificGravity)
                    / allowableStress;

                double tWithMinimum = Math.Max(tCalculated, 0.1);
                double tFinal = tWithMinimum + input.CorrosionAllowance;

                bool governedByLowerCourse = false;
                string governingNote = "Self-governing";

                if (tFinal > governingThickness)
                {
                    governingThickness = tFinal;
                    governingCourse = courseNumber;
                }
                else
                {
                    governedByLowerCourse = true;
                    governingNote = $"Governed by Course {governingCourse}";
                }

                // HYDROTEST STRESS
                var hydroStress = GetAllowableStress(
                    courseNumber,
                    material.YieldStrength,
                    material.TensileStrength,
                    true,
                    input.ApiEdition
                );

                double hydroAllowableStress = hydroStress.Stress * course.JointEfficiency;

                // DEFAULTS
                double hydroTestHeight = 0;
                double maxProductHeight = 0;
                string status = "PASS";

                // CHECK ACTUAL THICKNESS
                if (course.ActualThickness > 0)
                {
                    if (course.ActualThickness < tFinal)
                    {
                        status = "FAIL - Below minimum thickness";
                    }

                    // HYDROTEST HEIGHT (ft)
                    hydroTestHeight =
                        (hydroAllowableStress * course.ActualThickness) /
                        (2.6 * input.Diameter);

                    // MAX PRODUCT HEIGHT (ft)
                    maxProductHeight =
                        (allowableStress * course.ActualThickness) /
                        (2.6 * input.Diameter * input.SpecificGravity);
                }
                else
                {
                    status = "NO ACTUAL THICKNESS PROVIDED";
                }
                result.Courses.Add(new CourseResult
                {
                    CourseNumber = courseNumber,
                    CourseStressGroup = courseNumber <= 2 ? "Lower two courses" : "Upper courses",

                    CalculatedThickness = Math.Round(tFinal, 3),
                    RequiredThickness = Math.Round(governingThickness, 3),
                    GoverningRequiredThickness = Math.Round(governingThickness, 3),
                    IsGovernedByLowerCourse = governedByLowerCourse,
                    GoverningNote = governingNote,

                    ActualThickness = course.ActualThickness,

                    DesignStress = Math.Round(allowableStress, 0),
                    YieldFactor = stress.YieldFactor,
                    TensileFactor = stress.TensileFactor,
                    YieldBasedStress = Math.Round(stress.YieldBasedStress, 0),
                    TensileBasedStress = Math.Round(stress.TensileBasedStress, 0),
                    StressBasis = $"min({stress.YieldFactor:0.###}Y, {stress.TensileFactor:0.###}T) × E",

                    HydroTestStress = Math.Round(hydroAllowableStress, 0),
                    HydroTestHeight = Math.Round(hydroTestHeight, 2),
                    MaxProductHeight = Math.Round(maxProductHeight, 2),

                    Status = status
                });

                heightFromBottom += course.Height;
            }

            result.MaxFillHeight = totalLiquidHeight;
            return result;
        }

        private static TankStressResult GetAllowableStress(
            int courseNumber,
            double yieldStrength,
            double tensileStrength,
            bool isHydrotest,
            string apiEdition)
        {
            double yieldFactor;
            double tensileFactor;

            if (isHydrotest)
            {
                yieldFactor = courseNumber <= 2 ? 0.88 : 0.90;
                tensileFactor = courseNumber <= 2 ? 0.472 : 0.519;
            }
            else
            {
                if (courseNumber <= 2)
                {
                    yieldFactor = 0.80;
                    tensileFactor = 0.429;
                }
                else
                {
                    bool is7thOrLater = apiEdition == "7th and Later (1980-Present)";
                    yieldFactor = is7thOrLater ? 0.88 : 0.83;
                    tensileFactor = 0.472;
                }
            }

            double yieldBased = yieldFactor * yieldStrength;
            double tensileBased = tensileFactor * tensileStrength;

            return new TankStressResult
            {
                Stress = Math.Min(yieldBased, tensileBased),
                YieldFactor = yieldFactor,
                TensileFactor = tensileFactor,
                YieldBasedStress = yieldBased,
                TensileBasedStress = tensileBased
            };
        }
    }

    public class TankStressResult
    {
        public double Stress { get; set; }
        public double YieldFactor { get; set; }
        public double TensileFactor { get; set; }
        public double YieldBasedStress { get; set; }
        public double TensileBasedStress { get; set; }
    }
}