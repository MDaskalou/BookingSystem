namespace BookingSystem.Application.Common
{
    // Generisk klass som standardiserar resultat från MediatR-handlers (lyckat eller misslyckat)
    public class Result<T>
    {
        // Indikerar om operationen lyckades (true) eller misslyckades (false)
        public bool Success { get; private set; }

        // Felmeddelande om något gick fel, annars null
        public string? Error { get; private set; }

        // Returnerat värde från operationen om den lyckades, annars null
        public T? Value { get; private set; }

        // Privat konstruktor - endast tillgänglig inom klassen
        private Result(bool success, T? value = default, string? error = null)
        {
            Success = success;
            Value = value;
            Error = error;
        }

        // Skapar ett lyckat resultat med ett värde (ex. en användare)
        public static Result<T> Ok(T value) => new Result<T>(true, value);

        // Skapar ett misslyckat resultat med ett felmeddelande
        public static Result<T> Fail(string error) => new Result<T>(false, default, error);
    }
}