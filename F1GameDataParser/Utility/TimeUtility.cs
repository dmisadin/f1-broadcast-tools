namespace F1GameDataParser.Utility
{
    public static class TimeUtility
    {
        public static string MillisecondsToGap(long milliseconds)
        {
            if (milliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milliseconds), "Milliseconds cannot be negative.");
            }

            long totalSeconds = milliseconds / 1000;
            int remainderMilliseconds = (int)(milliseconds % 1000);

            if (totalSeconds < 60)
            {
                return $"{totalSeconds}.{remainderMilliseconds:D3}";
            }

            long minutes = totalSeconds / 60;
            int seconds = (int)(totalSeconds % 60);

            return $"{minutes}:{seconds:D2}.{remainderMilliseconds:D3}";
        }
    }
}
