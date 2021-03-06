using System;
using System.Collections.Generic;
using System.Linq;
using KRPC.Service.Attributes;
using KRPC.Utils;
using KRPCSpaceCenter.ExtensionMethods;

namespace KRPCSpaceCenter.Services
{
    [KRPCEnum (Service = "SpaceCenter")]
    public enum VesselType
    {
        Ship,
        Station,
        Lander,
        Probe,
        Rover,
        Base,
        Debris
    }

    [KRPCEnum (Service = "SpaceCenter")]
    public enum VesselSituation
    {
        Docked,
        Escaping,
        Flying,
        Landed,
        Orbiting,
        PreLaunch,
        Splashed,
        SubOrbital
    }

    [KRPCClass (Service = "SpaceCenter")]
    public sealed class Vessel : Equatable<Vessel>
    {
        global::Vessel vessel;

        internal Vessel (global::Vessel vessel)
        {
            this.vessel = vessel;
            Orbit = new Orbit (vessel);
            Control = new Control (vessel);
            AutoPilot = new AutoPilot (vessel);
            Resources = new Resources (vessel);
        }

        public override bool Equals (Vessel other)
        {
            return vessel == other.vessel;
        }

        public override int GetHashCode ()
        {
            return vessel.GetHashCode ();
        }

        [KRPCProperty]
        public string Name {
            get { return vessel.vesselName; }
            set { vessel.vesselName = value; }
        }

        [KRPCProperty]
        public VesselType Type {
            get {
                return vessel.vesselType.ToVesselType ();
            }
            set {
                vessel.vesselType = value.FromVesselType ();
            }
        }

        [KRPCProperty]
        public VesselSituation Situation {
            get { return vessel.situation.ToVesselSituation (); }
        }

        [KRPCProperty]
        public double MET {
            get { return vessel.missionTime; }
        }

        [KRPCMethod]
        public Flight Flight (ReferenceFrame referenceFrame = ReferenceFrame.Orbital)
        {
            return new Flight (vessel, referenceFrame);
        }

        [KRPCProperty]
        public Vessel Target {
            get { throw new NotImplementedException (); }
            set { throw new NotImplementedException (); }
        }

        [KRPCProperty]
        public Orbit Orbit { get; private set; }

        [KRPCProperty]
        public Control Control { get; private set; }

        [KRPCProperty]
        public AutoPilot AutoPilot { get; private set; }

        [KRPCProperty]
        public Resources Resources { get; private set; }

        [KRPCProperty]
        public double Mass {
            get {
                return vessel.parts.Where (p => p.IsPhysicallySignificant ()).Select (p => p.TotalMass ()).Sum ();
            }
        }

        [KRPCProperty]
        public double DryMass {
            get {
                return vessel.parts.Where (p => p.IsPhysicallySignificant ()).Select (p => p.DryMass ()).Sum ();
            }
        }

        [KRPCProperty]
        public double CrossSectionalArea {
            get { return FlightGlobals.DragMultiplier * Mass; }
        }

        [KRPCProperty]
        public double DragCoefficient {
            get {
                // Mass-weighted average of max_drag for each part
                // Note: Uses Part.mass, so does not include the mass of resources
                return vessel.Parts.Select (p => p.maximum_drag * p.mass).Sum () /
                vessel.Parts.Select (p => p.mass).Sum ();
            }
        }
    }
}
