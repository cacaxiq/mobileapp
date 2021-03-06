﻿using System;

namespace Toggl.Multivac
{
    public static class Ensure
    {
        public static class Argument
        {
            public static void IsNotNull<T>(T value, string argumentName)
            {
                #pragma warning disable RECS0017 // Possible compare of value type with 'null'
                if (value != null) return;
                #pragma warning restore RECS0017 // Possible compare of value type with 'null'

                throw new ArgumentNullException(argumentName);
            }

            public static void IsNotNullOrWhiteSpaceString(string value, string argumentName)
            {
                IsNotNull(value, argumentName);

                if (!string.IsNullOrWhiteSpace(value)) return;

                throw new ArgumentException("String cannot be empty.", argumentName);
            }

            public static void IsNotNullOrEmpty(string value, string argumentName)
            {
                if (!string.IsNullOrEmpty(value)) return;

                throw new ArgumentException("String cannot be null or empty.", argumentName);
            }

            public static void IsNotZero(long value, string argumentName)
            {
                if (value != 0) return;

                throw new ArgumentException("Long cannot be zero.", argumentName);
            }

            public static void IsAbsoluteUri(Uri uri, string argumentName)
            {
                IsNotNull(uri, argumentName);

                if (uri.IsAbsoluteUri) return;

                throw new ArgumentException("Uri must be absolute.", argumentName);
            }
        }
    }
}
