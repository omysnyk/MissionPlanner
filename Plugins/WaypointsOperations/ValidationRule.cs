using System.Drawing;

namespace MissionActionsPlugin
{
    public struct ValidationRule
    {
        public int RuleId { get; set; }
        public int MaxAlt { get; set; }
        public bool MaxAltEnabled { get; set; }
        public int TargetAlt { get; set; }
        public int MinAlt { get; set; }
        public AltitudeValidationMode ValidationMode { get; set; }

        public Color RuleColor { get; set; }

        public double OverlimitDegree(double routeAltitude, double terrainAltitude)
        {
            if (ValidationMode == AltitudeValidationMode.ASL)
            {
                if (routeAltitude < MinAlt)
                {
                    return -1.0;
                }
            
                if (routeAltitude > MaxAlt && MaxAltEnabled)
                {
                    return 1.0;
                }
            
                if (routeAltitude < TargetAlt * 0.95)
                {
                    return -0.5;
                }
                
                return 0.0;
            }

            var elevation = routeAltitude - terrainAltitude;

            if (elevation < MinAlt)
            {
                return -1.0;
            }
            
            if (elevation >= MaxAlt && MaxAltEnabled)
            {
                return 1.0;
            }
            
            if (elevation < TargetAlt * 0.95)
            {
                return -0.5;
            }

            return 0.0;
        }
    }
}