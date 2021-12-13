//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Globalization;
//using System.Runtime.CompilerServices;
//using System.Security.Cryptography;
//using System.Text;

//namespace SWI.SoftStock.ServerApps.DataModel2.Identity
//{
//    public class CustomPasswordHasher : IPasswordHasher<User>
//    {
//        public string HashPassword(User user, string password)
//        {
//            if (password == null)
//            {
//                throw new ArgumentNullException(nameof(password));
//            }

//            byte[] salt;
//            byte[] subkey;
//            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2Count))
//            {
//                salt = deriveBytes.Salt;
//                subkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
//            }

//            byte[] outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
//            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
//            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
//            return Convert.ToBase64String(outputBytes);
//        }

//        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
//        {
//            if (hashedPassword == null)
//            {
//                throw new ArgumentNullException(nameof(hashedPassword));
//            }
//            if (providedPassword == null)
//            {
//                throw new ArgumentNullException(nameof(providedPassword));
//            }

//            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

//            // Verify a version 0 (see comment above) password hash.

//            if (hashedPasswordBytes.Length != (1 + SaltSize + Pbkdf2SubkeyLength) || hashedPasswordBytes[0] != (byte)0x00)
//            {
//                // Wrong length or version header.
//                return PasswordVerificationResult.Failed;
//            }

//            var salt = new byte[SaltSize];
//            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
//            var storedSubkey = new byte[Pbkdf2SubkeyLength];
//            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, Pbkdf2SubkeyLength);

//            byte[] generatedSubkey;
//            using (var deriveBytes = new Rfc2898DeriveBytes(providedPassword, salt, Pbkdf2Count))
//            {
//                generatedSubkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
//            }
//            var result = ByteArraysEqual(storedSubkey, generatedSubkey);
        
//            return result ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
//        }

//        private const int Pbkdf2Count = 1000;
//        private const int Pbkdf2SubkeyLength = 256 / 8;
//        private const int SaltSize = 128 / 8;


//        private static string GenerateSalt(int byteLength = SaltSize)
//        {
//            var buff = new byte[byteLength];
//            using (var prng = new RNGCryptoServiceProvider())
//            {
//                prng.GetBytes(buff);
//            }

//            return Convert.ToBase64String(buff);
//        }

//        private static string Hash(string input, string algorithm = "sha256")
//        {
//            if (input == null)
//            {
//                throw new ArgumentNullException(nameof(input));
//            }

//            return Hash(Encoding.UTF8.GetBytes(input), algorithm);
//        }

//        private static string Hash(byte[] input, string algorithm = "sha256")
//        {
//            if (input == null)
//            {
//                throw new ArgumentNullException(nameof(input));
//            }

//            using (var alg = HashAlgorithm.Create(algorithm))
//            {
//                if (alg != null)
//                {
//                    var hashData = alg.ComputeHash(input);
//                    return BinaryToHex(hashData);
//                }
//                else
//                {
//                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "not supported hash alg", algorithm));
//                }
//            }
//        }

//        private static string SHA1(string input)
//        {
//            return Hash(input, "sha1");
//        }
//        private static string SHA256(string input)
//        {
//            return Hash(input, "sha256");
//        }

//        private static string BinaryToHex(byte[] data)
//        {
//            var hex = new char[data.Length * 2];

//            for (var iter = 0; iter < data.Length; iter++)
//            {
//                var hexChar = ((byte)(data[iter] >> 4));
//                hex[iter * 2] = (char)(hexChar > 9 ? hexChar + 0x37 : hexChar + 0x30);
//                hexChar = ((byte)(data[iter] & 0xF));
//                hex[iter * 2 + 1] = (char)(hexChar > 9 ? hexChar + 0x37 : hexChar + 0x30);
//            }
//            return new string(hex);
//        }

//        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
//        [MethodImpl(MethodImplOptions.NoOptimization)]
//        private static bool ByteArraysEqual(byte[] a, byte[] b)
//        {
//            if (object.ReferenceEquals(a, b))
//            {
//                return true;
//            }

//            if (a == null || b == null || a.Length != b.Length)
//            {
//                return false;
//            }

//            var areSame = true;
//            for (var i = 0; i < a.Length; i++)
//            {
//                areSame &= (a[i] == b[i]);
//            }
//            return areSame;
//        }

//    }
//}