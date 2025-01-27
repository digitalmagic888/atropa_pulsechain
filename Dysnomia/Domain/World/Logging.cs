﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dysnomia.Domain.Tare;

namespace Dysnomia.Domain.World
{
    static public class Logging
    {
        static private Tare Tau;

        static Logging()
        {
            Tau = new Tare();
        }

        static public void Add(Gram G)
        {
            Tau.Add(G);
        }

        static public void Log(MSG A)
        {
            foreach (Gram G in Tau) G(A);
        }

        static public void Log(String From, String Data, short Priority = 6)
        {
            byte[] A = Encoding.Default.GetBytes(From);
            byte[] B = Encoding.Default.GetBytes(Data);
            MSG C = new MSG(A, B, Priority);
            foreach (Gram G in Tau) G(C);
        }
    }
}
