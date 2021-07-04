using Engine.Services;

namespace Engine.Models
{
    public class ArmourItemRating : BaseNotificationClass
    {
        private int _modifiedValue;
        public string Key { get; set; }
        public string Rating { get; set; }
        public string AttributeKey { get; set; }
        
        public int AttributeModifier { get; set; }
        public int BaseValue { get; set; }

        public int ArmourRating
        {
            get => _modifiedValue;
            set
            {
                _modifiedValue = value;
                OnPropertyChanged();
            }
        }


        public ArmourItemRating(string key, string rating, int baseValue, string attributeKey, int attributeModifierFromEntity)
        {
            Key = key;
            Rating = rating;
            BaseValue = baseValue;
            AttributeKey = attributeKey;
            AttributeModifier = attributeModifierFromEntity;
            _modifiedValue = BaseValue + AttributeModifier;
            ArmourRating = _modifiedValue;
        }

        public int GetAttributeModifier(LivingEntity entity, string attributeKey)
        {
            int entityAttributeModifier = entity.GetAttributeValueModifier(entity, attributeKey);
            return entityAttributeModifier;
        }
    }//make method to change armour.
}