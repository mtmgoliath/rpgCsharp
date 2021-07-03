using Engine.Services;

namespace Engine.Models
{
    public class PlayerArmourRating : BaseNotificationClass
    {
        private int _modifiedValue;
        public string Key { get; }
        public string DisplayName { get; }
        public string AttributeKey { get; }
        
        public int AttributeModifier { get; set; }
        public int BaseValue { get; set; }

        public int ModifiedValue
        {
            get => _modifiedValue;
            set
            {
                _modifiedValue = value;
                OnPropertyChanged();
            }
        }


        public PlayerArmourRating(string key, string displayName, int baseValue, string attributeKey, int attributeModifier)
        {
            Key = key;
            DisplayName = displayName;
            AttributeKey = attributeKey;
            BaseValue = baseValue;
            AttributeModifier = attributeModifier;
            ModifiedValue = BaseValue + AttributeModifier;
        }

        private void SetAttributeModifier(LivingEntity entity, string attributeKey)
        {
            int entityAttributeModifier = entity.GetAttributeValueModifier(entity, attributeKey);
            AttributeModifier = entityAttributeModifier;
        }
    }//make method to change armour.
}