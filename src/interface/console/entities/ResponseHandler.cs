namespace ConsoleFrontend
{
    static class Response
    {
        static public T? HandleNullableSuccess<T>(Func<T> func)
        {
            try
            {
                var result = func();
                Console.WriteLine("Aktion Erfolgreich!");
                return result;
            }
            catch (AccessDeniedException)
            {
                Console.WriteLine("Zugriff verweigert");
                return default(T);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed with {ex.Message}");
                return default(T);
            }
        }
        static public bool HandleSuccess<T>(Func<T> func)
        {
            try
            {
                var result = func();
                Console.WriteLine("Aktion Erfolgreich!");
                return true;
            }
            catch (AccessDeniedException)
            {
                Console.WriteLine("Zugriff verweigert");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed with {ex.Message}");
                return false;
            }
        }
        static public bool HandleSuccess(Action func)
        {
            try
            {
                func();
                Console.WriteLine("Aktion Erfolgreich!");
                return true;
            }
            catch (AccessDeniedException)
            {
                Console.WriteLine("Zugriff verweigert");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed with {ex.Message}");
                return false;
            }
        }
    }
}