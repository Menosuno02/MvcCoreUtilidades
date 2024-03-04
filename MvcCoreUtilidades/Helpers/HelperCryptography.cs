using System.Security.Cryptography;
using System.Text;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        // Tendremos una propiedad donde almacenaremos el SALT
        // dinámico que vamos a generar
        public static string Salt { get; set; }

        // Cada vez que realicemos un cifrado se genera un SALT distinto
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++)
            {
                // Un número entre los caracteres ASCII
                int aleatorio = random.Next(1, 255);
                char letra = Convert.ToChar(aleatorio);
                salt += letra;
            }
            return salt;
        }

        public static string EncriptarContenido
            (string contenido, bool comparar)
        {
            // password123
            // password123###@@@~€#~21U392
            if (comparar == false)
            {
                // El usuario quiere cifrar, por lo que generamos nuevo
                // SALT y lo guardamos en la propiedad
                Salt = GenerateSalt();
            }
            // El SALT se utiliza en múltiples lugares. Es decir, lo podemos
            // incluir al final, al inicio, con un insert...
            string contenidoSalt = contenido + Salt;
            // Creamos el objeto para cifrar el contenido
            SHA256 sha256 = SHA256.Create();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            // Convertimos a bytes nuestro contenido + salt
            salida = encoding.GetBytes(contenidoSalt);
            // Ciframos el contenido N iteraciones
            for (int i = 1; i <= 255; i++)
            {
                // Cifrado sobre cifrado
                salida = sha256.ComputeHash(salida);
            }
            // Debemos limpiar el objeto del cifrado
            sha256.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        // Vamos a crear un método static para convertir
        // un contenido y cifrarlo y devovlemos un texto cifrado
        public static string EncriptarTextoBasico(string contenido)
        {
            // Necesitamos un array de bytes para convertir el
            // texto a byte[]
            byte[] entrada;
            // Al cifrar nos devolverá un array de bytes con la salida cifrada
            byte[] salida;
            // Necesitamos una clase que nos permite convertir de string a
            // byte[] y viceversa
            UnicodeEncoding encoding = new UnicodeEncoding();
            // Necesitamos el objeto para cifrar contenido
            SHA1Managed managed = new SHA1Managed();
            // SHA1 sha = SHA1.Create();
            entrada = encoding.GetBytes(contenido);
            // El objeto SHA1 recibe un array de bytes e internamente
            // modifica cada elemento devolviendo otro array de bytes
            salida = managed.ComputeHash(entrada);
            // Convertimos bytes[] a string
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
