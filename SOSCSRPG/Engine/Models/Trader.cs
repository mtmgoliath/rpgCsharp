using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Trader : LivingEntity
    {
        public int ID { get; }

        public new ObservableCollection<PlayerAttribute> Attributes { get; set; } =
            new ObservableCollection<PlayerAttribute>();

        public Trader(int id, string name, ObservableCollection<PlayerAttribute> attributes) : base(name, 9999, 9999, attributes, 9999)
        {
            ID = id;
            Attributes = attributes;
        }
    }
}