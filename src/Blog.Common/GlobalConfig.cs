using System;

namespace Blog.Common
{
    public class GlobalConfig
    {
        #region 密钥
        public static readonly String rsaPublicKey = "AwEAAZXuy2AswTAmLyVJfTNZn5PhF3t63a0cjlVXkTP1LDLWU27geFn6pUHyHX1JOValIPwGBseX9Tdz/NAw0TckfCddZgHTC5V/JC3mzMjQDVNn3kU2KglEJiBYuk+IkQRcnTD1dOLQq4tjMWrP7zuPqliKfCwHpjv8qEks3keI/rLz";
        public static readonly String rsaPublicKey2 = "AwEAAZcVqKxd1wCyLt7gmTyFxCcl9OUDaj+ehgso+tvsFcpToxgBR7LBATBBEB1O5VbUjxxcKZTKsfZi9giRa3vZYXOzc9bcPw3l4vzrySY3N85CDItIkt38yO/mFwLX1MPpJDZCPUXdGn9WFFgKyXv3KyMqSpgtmaQuUaMGnob7pKJr";

        public static readonly String rsaPrivateKey = "gApCIGAgL8eflTIz6QEObMCtcfHc4jXp8kMcjq7DY5bG20aGzTU61PvCGQptbCLlgXA86VHWr8xvfyivbmEbefMMvU5vz/v5xRqj0f8IbwobTvqwDWqEpWV5mbqXwO6m7kn44MGejEczfVBk0lpqDwMMEeIRNKZw4/Vedkxlr0Nhle7LYCzBMCYvJUl9M1mfk+EXe3rdrRyOVVeRM/UsMtZTbuB4WfqlQfIdfUk5VqUg/AYGx5f1N3P80DDRNyR8J11mAdMLlX8kLebMyNANU2feRTYqCUQmIFi6T4iRBFydMPV04tCri2Mxas/vO4+qWIp8LAemO/yoSSzeR4j+svM=";
        public static readonly String rsaPrivateKey2 = "gA+cgCcos83RcOYRwh5kBLk2LasOTR0RcHCtxYIST8wYvqT9LjSVHIjTvZUHBnaQPyYN4Ilh371W9heQ4yYod5kNf/ODlBGtwdn6Ilzot5bQiPuoE7gJbRtN7RM5LeLnzwqFHUUW+m+CRo90lkB0kypDXPfmzTlpu3HhGXVhJPXFlxWorF3XALIu3uCZPIXEJyX05QNqP56GCyj62+wVylOjGAFHssEBMEEQHU7lVtSPHFwplMqx9mL2CJFre9lhc7Nz1tw/DeXi/OvJJjc3zkIMi0iS3fzI7+YXAtfUw+kkNkI9Rd0af1YUWArJe/crIypKmC2ZpC5Rowaehvukoms=";

        public static readonly String aesKey = "hVFfFzn";

        public static readonly String rsaStandardPublicKey =
           "BgIAAACkAABSU0ExAAgAAAEAAQAz8kKLeAGzYOq7NTk0EQtab6KDafkmHsP0XkTC3jKjSnO+Ztecu4g9nWiMpjLaHlaB14t2gm7a0PPx/Y/+En+R7o3HyQ1McuTZnq0xONfCMEjlWf31LJcaoejAeataibOR1yav0Ezuj4/NR8p/SGW5XVdpQsivcV64ElbiAw1+pYUchA+nlFIGGVJVx4IbRkXq+MiQseXAn56jwRaUqpWXaL3hEkgBOt0nPyg7K3V1ffSu+X6CzEDb9/XfoSzLG8K0UJcH8ONwOKC8amSX5FaLCo3nOcoG/aXZR4P5lUOf03kAQ02WbAeyyu+Nkrapu7qA39bTVOSKjpJzr2gWTTH2";

        public static readonly String rsastandardPrivateKey = "BwIAAACkAABSU0EyAAgAAAEAAQAz8kKLeAGzYOq7NTk0EQtab6KDafkmHsP0XkTC3jKjSnO+Ztecu4g9nWiMpjLaHlaB14t2gm7a0PPx/Y/+En+R7o3HyQ1McuTZnq0xONfCMEjlWf31LJcaoejAeataibOR1yav0Ezuj4/NR8p/SGW5XVdpQsivcV64ElbiAw1+pYUchA+nlFIGGVJVx4IbRkXq+MiQseXAn56jwRaUqpWXaL3hEkgBOt0nPyg7K3V1ffSu+X6CzEDb9/XfoSzLG8K0UJcH8ONwOKC8amSX5FaLCo3nOcoG/aXZR4P5lUOf03kAQ02WbAeyyu+Nkrapu7qA39bTVOSKjpJzr2gWTTH2mx9JnvyMgEDcFMVD8LczzIhnj6RsY6NHqm0WrJfoIxapYyS8o3nLfqNKQn+OVqp48iKVuOQmqFFnzdxW41tfqd5lkn5zw/T+Oe7Nodt03GDRdeIbGGCzzOvWtbpf990PEM9WxRHDW34ji+LsSyF3ItWpunGfFYVRg5hRBI5ZhftJPTMQ10IxLIQ7MnjJM9yog0CcbLiM5dALZxhQPq5l4xyXqz+sqg5m9zjiFYBS1J0OMdo6ThdOWhp3I+QVkITUXcQUmEO6yI1YjxVCNLaIGc0z5FxZI2AMr2Cf+0+yJ72u/GvbsknoG5s2jV1PzpyzvX9mLycngcqI5j1XBKmT+sM0UBU3Lrz7tYMvZevdim8fmoOZhAGH7zzWWgaFAtoXFbmOR6eu/ms9Swl6zi6ZehxeyJSOMa26pC8u0XgyXMW5OS+F7MPv0Q83J0fWkfcVDvjtDCpAXzVGALpxcZzV3evocNz1oLbTppVldmRiRPZ3ys1wyG6Cx71y7kLArBjVGbVFE13ezghikAxkX0fWz6m5i3JbZtApU14QQnLTGQKYnWVEQIjha2ze0Y0gdMm3jxECdO40iWrvFcioNPpprxDhJMWp4s0uv6xCQ/EzlIWgN+52on3n2OLYDQQY8ry+tg9zSZfqFs3qdhoCenmBiWdu3GhnbwwLdGP1NWq00UkT5y+6pjV8WX/WQtlFH91gVnQaFPp7f86oMhrWS9WiKmwg1yczXXamhUyXvS3L3Kad5wwf/NRAlveDRxAbNIYA30tr6/t+p2XdW7y172ArKU1VDRDWYssEqtRoZ4hGrKDQJT/VRCJp5UmaMiAvjIKDjYwbG06X932cr6Dx3zI0YglvnnGwt7gbyvZ3B8XB/tDcjBjX0XJ/Ho285lN3jEvIAHClCay+oQukD6CBfo9Ln7/DcamCbCArQEiBpUv2iTRimWMQD8EWYrbwofr98rEKWPWhsnQcn01FYHsKA5eZyxstHNQi2olv4IbpiRcAiJu1gBSTWNKeqW3VN+zt6vbVo9v3+W+tJEttN+RSJC8zrJ902iB3Sfh791rYiCSHNZfhuNyWzew8/F2ksMLkMGYOgC2J3KnLZ8j4XCvqEarFs9Epe86liL8ok7qMbVev1fYfMvw5+UuDVysPdUZCFm95hZYmtXMaUkOxlQaXNXOKMxLmVGvnYjcMdgTcrh7oIgE=";

        public static readonly string exponent = "AQAB";
        public static readonly string exponent2 = "AQAB";
        //0cIsj+UyFPoEDb9IJkcxiFnsVmbFTMqMy6uFla3CAhClxT3Y3sFCwgclnYQUygclzNzaUHwgm6/8RUSsY94JDQ==
        public static readonly string modules =
            "le7LYCzBMCYvJUl9M1mfk+EXe3rdrRyOVVeRM/UsMtZTbuB4WfqlQfIdfUk5VqUg/AYGx5f1N3P80DDRNyR8J11mAdMLlX8kLebMyNANU2feRTYqCUQmIFi6T4iRBFydMPV04tCri2Mxas/vO4+qWIp8LAemO/yoSSzeR4j+svM=";
        public static readonly string modules2 =
            "lxWorF3XALIu3uCZPIXEJyX05QNqP56GCyj62+wVylOjGAFHssEBMEEQHU7lVtSPHFwplMqx9mL2CJFre9lhc7Nz1tw/DeXi/OvJJjc3zkIMi0iS3fzI7+YXAtfUw+kkNkI9Rd0af1YUWArJe/crIypKmC2ZpC5Rowaehvukoms=";

        public static readonly string d =
            "CkIgYCAvx5+VMjPpAQ5swK1x8dziNenyQxyOrsNjlsbbRobNNTrU+8IZCm1sIuWBcDzpUdavzG9/KK9uYRt58wy9Tm/P+/nFGqPR/whvChtO+rANaoSlZXmZupfA7qbuSfjgwZ6MRzN9UGTSWmoPAwwR4hE0pnDj9V52TGWvQ2E=";
        public static readonly string d2 =
            "D5yAJyizzdFw5hHCHmQEuTYtqw5NHRFwcK3FghJPzBi+pP0uNJUciNO9lQcGdpA/Jg3giWHfvVb2F5DjJih3mQ1/84OUEa3B2foiXOi3ltCI+6gTuAltG03tEzkt4ufPCoUdRRb6b4JGj3SWQHSTKkNc9+bNOWm7ceEZdWEk9cU=";

        public static readonly string dp =
            "H/pKTdvLqCC02NTdb7+w6pDQmBOnOt8ZDnCa20tN4fXPqZT3eanJVPO0KeY465UQwMmvhxP2srDVDWaFp87xgQ==";
        public static readonly string dp2 =
           "FxQ0I+h+gszHtPOJiTOxcO5avUfOdS1z/4LWsXYTTz2deBJH7OkWaSMprpwJt4abGwHyI3HYMak5lLN9kAT0mQ==";

        public static readonly string dq =
            "qNyJFxBTm0mYbkBl51v2sdk5jajbvbyfQGYCdwsCWCqAT4pI6pKeUatSWC71RUXroC0ZWuFvF0Nge+L6nTZCaQ==";
        public static readonly string dq2 =
            "Ro2/VYy2kQlJXLwFsvS1y/w6B6xGK+oIelBKavBt5iRwQ3zUfUQWjAPVMzk07Ptf2vhbLePE4h/XK3+1QFc5cw==";

        public static readonly string inverseQ =
            "HettPdvnPN0SFHsYkI3Zk1Pxh69CzId2A44Hcp3McJt+yw08k+HkaDaIhTWYHhqzzeT6VAGN09LC0yXXyv/ifg==";
        public static readonly string inverseQ2 =
            "CNI19jgasO2ULNnOK+k0AiU9GtuO6NFLSAutICR/K2+z5BllcnPspdFPeUSvDW+MCDgGs5hJD/906rMEEoa9hw==";

        public static readonly string pp =
            "wcLk7TWfJWexE+wTzS+7Udb8bGiBGea4XiVVHCtjiGs1wsKtCFx5J1B/8XoB76sVuW41ah4+Q5Yl0/dcvdYvVw==";
        public static readonly string pp2 =
            "xyxnkDx9yPXE3LaG3kNgO1L8JQhrO8zMlvw7NNEq/fDr5dT8ITSiKBn8Jjf4kaRCqr8/FgvaobmWE/4wQeNvHQ==";

        public static readonly string q =
            "xhfbEGb83399dMEHiqA/HfWWCBgqMDCrZ48HlYe++zE+nTAgWwp2CzT2n0WKNxhRfAx4m8cKbG5FAem5AqvDxQ==";
        public static readonly string q2 =
            "wjDaVJ0Ohbk3rm8LX+rDcfmip+VmR4qkhUgjeJtYIoPx2bcrk4D10dD8mmo/DD++Lx1XjP4P9C5qqumicUJ5Jw==";

        #endregion
    }

}
