using CarKilometerTrack.Core;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

namespace CarKilometerTrack.Model
{
    public class Car:MainModel
    {
        public string LicensePlate {  get; set; }
        public string Brand {  get; set; }

        public string Model { get; set; }

        public int Kilometer { get; set; }

        public int PeriodicKilometer { get; set; }

        public int LastMaintenance { get; set; }

        public bool IsActive { get; set; }=true;

        public int PeriodicInspection { get; set; }

        public DateTime Inspection { get; set; }

        public DateTime Insurance { get; set; }

        public bool InUse {  get; set; } =false;

        public int? InUseUserId { get; set; }
        public User? InUseUser { get; set; }

        public string? UseNote { get; set; }

        public ICollection<Note> Notes { get; } = new List<Note>();

        public ICollection<Log> Logs { get; } = new List<Log>();
    }
}
