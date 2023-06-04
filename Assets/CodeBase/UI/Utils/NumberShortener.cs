namespace UI {
    public static class NumberShortener {
        public static string ShorterString(this int number) {
            return number switch {
                >= 1000000000 => (number / 1000000000f).ToString("F2") + "B",
                >= 1000000 => (number / 1000000f).ToString("F2") + "M",
                >= 1000 => (number / 1000f).ToString("F2") + "K",
                _ => number.ToString()
            };
        }
    }
}