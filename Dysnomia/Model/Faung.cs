﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dysnomia
{
    public class Faung
    {
        public Fa Rod;
        public Fa Cone;

        public BigInteger Phi, Eta, Mu, Xi;
        public BigInteger Sigma, Rho, Upsilon, Ohm, Pi, Omicron, Omega;
        public Mutex Delta = new Mutex();
        public short Chi;
        public Domain.Nit Nu = new Domain.Nit();

        public Faung()
        {
            bool Failed = true;
            while (Failed)
            {
                try
                {
                    Rod = new Fa();
                    Cone = new Fa();
                    OpenManifolds();
                    Failed = false;
                }
                catch (Exception E)
                {
                    continue;
                }
            }
        }

        public Faung(ref Fa Rod, ref Fa Cone, bool Refaung = true)
        {
            if (Refaung && (!Rod.Barn.IsZero || !Cone.Barn.IsZero))
                throw new Exception("Non Zero Barn");
            this.Rod = Rod;
            this.Cone = Cone;
            this.Cone.Kappa = -1;

            bool Failed = true;

            while (Failed)
            {
                try
                {
                    OpenManifolds();
                    Failed = false;
                }
                catch (Exception E)
                {
                    this.Cone.Nu = 0;
                    throw;
                }
            }
            Rod.Gamma++;
            Cone.Gamma++;
        }

        public Faung(BigInteger Rho, BigInteger Upsilon, BigInteger Ohm, BigInteger Xi)
        {
            bool Failed = true;

            while (Failed)
            {
                try
                {
                    Rod = new Fa(true);
                    Cone = new Fa(true);
                    FuseAndOpen(Rho, Upsilon, Ohm, Xi);
                    Failed = false;
                }
                catch (Exception E)
                {
                    throw;
                }
            }
        }

        public Faung(ref Fa Beta, BigInteger Rho, BigInteger Upsilon, BigInteger Ohm, BigInteger Xi, bool Phi = false)
        {
            Rod = Beta;
            if (!Rod.Barn.IsZero)
                throw new Exception("Non Zero Barn");

            bool Failed = true;

            while (Failed)
            {
                try
                {
                    Cone = new Fa(Phi);
                    Cone.Kappa = -1;
                    //if (Rho == Ohm) Rho = Math.Random();
                    FuseAndOpen(Rho, Upsilon, Ohm, Xi);
                    Failed = false;
                }
                catch (Exception E)
                {
                    throw;
                }
            }
            Rod.Gamma++;
        }

        public void FuseAndOpen(BigInteger Rho, BigInteger Upsilon, BigInteger Ohm, BigInteger Xi)
        {
            Cone.Fuse(Rho, Upsilon, Ohm);
            Cone.Tune();
            OpenManifolds(Xi);
        }

        public void OpenManifolds()
        {
            Xi = Cone.Mu(Cone.Signal, Rod.Channel, Math.Prime);
            OpenManifolds(Xi);
        }

        public void ConductorGenerate(BigInteger Xi)
        {
            Phi = Rod.Avail(Xi);
            Cone.Tau = Cone.Avail(Xi);

            Rod.Form(Cone.Tau);
            Cone.Form(Phi);

            Rod.Polarize();
            Cone.Polarize();
        }

        public void OpenManifolds(BigInteger Xi)
        {
            ConductorGenerate(Xi);

            Rod.Conjugate(ref Cone.Pole);
            Cone.Conjugate(ref Rod.Pole);

            if (Rod.Coordinate != Cone.Coordinate)
                throw new Exception("Bad Coordination");

            Cone.Conify();

            Eta = Rod.Saturate(Cone.Foundation, Cone.Channel);
            Mu = Cone.Saturate(Rod.Foundation, Rod.Channel);

            if (Rod.Element != Cone.Element)
                throw new Exception("Bad Element");

            Ratchet();

            Rod.Adduct(Cone.Dynamo);
            Cone.Adduct(Rod.Dynamo);

            Rod.Open();
            Cone.Open();

            if (!Cone.ManifoldCompare(ref Rod))
                throw new Exception("Bad Compare");
            this.Xi = Xi;
            Chi = 0;
        }

        public void Ratchet()
        {
            Rod.Bond();
            Cone.Bond();
        }

        public void Charge(BigInteger Signal)
        {
            if (Signal == 0) throw new Exception("Signal Zero");
            Sigma = Cone.Charge(Signal);
        }

        public void Induce()
        {
            Rho = Rod.Induce(Sigma);
        }

        public void Torque()
        {
            Upsilon = Cone.Torque(Rho);
        }

        public void Amplify()
        {
            Ohm = Cone.Amplify(Upsilon);
        }

        public void Sustain()
        {
            Pi = Cone.Sustain(Ohm);
        }

        public void React()
        {
            Rod.React(Pi, Cone.Channel);
            Cone.React(Pi, Rod.Channel);
            if (Cone.Kappa != Rod.Eta || Rod.Kappa != Cone.Eta)
                throw new Exception("Non Match");
            if (Rod.Eta == Rod.Kappa)
                throw new Exception("Parallel");
            if (Cone.Kappa <= 1 && Rod.Kappa <= 1)
                throw new Exception("Negative");
            Omicron = Cone.Kappa;
            Omega = Rod.Kappa;
        }
    }
}
