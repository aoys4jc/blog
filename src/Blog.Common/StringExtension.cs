﻿using System;

namespace Blog.Common
{
    public static class StringExtension
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
