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

        public static string? MillisecondsToDifference(long? milliseconds, byte decimalPlaces = 3)
        {
            if (!milliseconds.HasValue)
                return null;

            bool isNegative = milliseconds < 0;
            int decimalPlacesDivisor = (int)Math.Pow(10, decimalPlaces);
            long absMilliseconds = Math.Abs(milliseconds.Value);

            long totalSeconds = absMilliseconds / decimalPlacesDivisor;
            int remainderMilliseconds = (int)(absMilliseconds % decimalPlacesDivisor);

            string result;

            if (totalSeconds < 60)
            {
                result = $"{totalSeconds}.{remainderMilliseconds:D3}";
            }
            else
            {
                long minutes = totalSeconds / 60;
                int seconds = (int)(totalSeconds % 60);

                result = $"{minutes}:{seconds:D2}.{remainderMilliseconds.ToString($"D{decimalPlaces}")}";
            }

            return isNegative ? $"-{result}" : $"+{result}";
        }
    }
}
