using System.Globalization;

namespace F1GameDataParser.Utility
{
    public static class TimeUtility
    {
        public static string MillisecondsToTime(long milliseconds, byte decimalPlaces = 3)
        {
            if (milliseconds < 0)
                throw new ArgumentOutOfRangeException(nameof(milliseconds), "Milliseconds cannot be negative.");

            double totalSeconds = milliseconds / 1000.0;
            string format = "F" + decimalPlaces;

            if (totalSeconds < 60)
            {
                return totalSeconds.ToString(format, CultureInfo.InvariantCulture);
            }

            long minutes = (long)(totalSeconds / 60);
            double remainingSeconds = totalSeconds % 60;

            return $"{minutes}:{remainingSeconds.ToString($"00.{new string('0', decimalPlaces)}", CultureInfo.InvariantCulture)}";
        }


        public static string? MillisecondsToDifference(long? milliseconds, byte decimalPlaces = 3)
        {
            if (!milliseconds.HasValue)
                return null;

            bool isNegative = milliseconds.Value < 0;
            double totalSeconds = Math.Abs(milliseconds.Value) / 1000.0;

            string format = "F" + decimalPlaces;
            string result;

            if (totalSeconds < 60)
            {
                result = totalSeconds.ToString(format, CultureInfo.InvariantCulture);
            }
            else
            {
                long minutes = (long)(totalSeconds / 60);
                double remainingSeconds = totalSeconds % 60;

                string secondsFormatted = remainingSeconds.ToString($"00.{new string('0', decimalPlaces)}",
                                                                    CultureInfo.InvariantCulture);

                result = $"{minutes}:{secondsFormatted}";
            }

            return (isNegative ? "-" : "+") + result;
        }

        public static string SecondsToTime(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            int minutes = (int)time.TotalMinutes;
            int secs = time.Seconds;

            return $"{minutes}:{secs:D2}";
        }
    }
}
