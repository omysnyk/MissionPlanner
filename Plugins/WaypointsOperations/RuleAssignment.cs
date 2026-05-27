namespace MissionActionsPlugin
{
    public struct RuleAssignment
    {
        public ValidationRule Rule { get; set; }
        public int SegmentStart { get; set; }
        public int SegmentEnd { get; set; }
    }
}