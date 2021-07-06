using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Engine.Services;
using Newtonsoft.Json;
using Engine.ViewModels;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        #region Properties

        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;
        private int _level;
        private int _armourRating;
        //private string _actionTaken;
        private GameItem _currentWeapon;
        private GameItem _currentArmour;
        private GameItem _currentShield;
        private GameItem _currentConsumable;
        private Inventory _inventory;

        public ObservableCollection<PlayerAttribute> Attributes { get; } =
            new ObservableCollection<PlayerAttribute>();

        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int CurrentHitPoints
        {
            get => _currentHitPoints;
            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int MaximumHitPoints
        {
            get => _maximumHitPoints;
            protected set
            {
                _maximumHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int Gold
        {
            get => _gold;
            private set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get => _level;
            protected set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        public int ArmourRating
        {
            get => _armourRating;
            
            set
            {
                _armourRating = value;
                OnPropertyChanged();
            }
        }

        public string ActionTaken { get; set; }

        public Inventory Inventory
        {
            get => _inventory;
            private set
            {
                _inventory = value;
                OnPropertyChanged();
            }
        }

        public GameItem CurrentWeapon
        {
            get => _currentWeapon;
            set
            {
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentWeapon = value;

                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }

                OnPropertyChanged();
            }
        }

        public GameItem CurrentArmour
        {
            get => _currentArmour;
            set
            {
                if (_currentArmour != null)
                {
                    _currentArmour.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentArmour = value;
                _armourRating = _currentArmour.BaseDefense + GetAttributeValueModifier(this, "DEX");

                if (_currentArmour != null)
                {
                    _currentArmour.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }

                OnPropertyChanged();
            }
        }

        public GameItem CurrentShield
        {
            get => _currentShield;
            set
            {
                if (_currentShield!= null)
                {
                    _currentShield.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentShield = value;
                _armourRating += _currentShield.BaseDefense;

                if (_currentShield != null)
                {
                    _currentArmour.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }

                OnPropertyChanged();
            }
        }

        public GameItem CurrentConsumable
        {
            get => _currentConsumable;
            set
            {
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentConsumable = value;

                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }

                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public bool IsAlive => CurrentHitPoints > 0;
        [JsonIgnore]
        public bool IsDead => !IsAlive;

        #endregion

        public event EventHandler<string> OnActionPerformed;
        public event EventHandler OnKilled;

        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints,
                               ObservableCollection<PlayerAttribute> attributes, int gold, int level = 1, int armourRating = 10)
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;

            foreach (PlayerAttribute attribute in attributes)
            {
                Attributes.Add(attribute);
            }

            Inventory = new Inventory();
        }

        public void UseCurrentWeaponOn(LivingEntity target)
        {
            CurrentWeapon.PerformAction(this, target);
        }

        public void UseBlockAction(LivingEntity target)
        {
            CurrentShield.PerformAction(this, target);
        }


        public void UseCurrentConsumable()
        {
            CurrentConsumable.PerformAction(this, this);

            RemoveItemFromInventory(CurrentConsumable);
        }

        public void UseEquipItemAction()
        {
            CurrentArmour.PerformAction(this, this);
        }

        public void TakeDamage(int hitPointsOfDamage)
        {
            CurrentHitPoints -= hitPointsOfDamage;

            if (IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointsToHeal)
        {
            CurrentHitPoints += hitPointsToHeal;

            if (CurrentHitPoints > MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public void CompletelyHeal()
        {
            CurrentHitPoints = MaximumHitPoints;
        }

        public void ReceiveGold(int amountOfGold)
        {
            Gold += amountOfGold;
        }

        public void SpendGold(int amountOfGold)
        {
            if (amountOfGold > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amountOfGold} gold");
            }

            Gold -= amountOfGold;
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory = Inventory.AddItem(item);
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory = Inventory.RemoveItem(item);
        }

        public void RemoveItemsFromInventory(IEnumerable<ItemQuantity> itemQuantities)
        {
            Inventory = Inventory.RemoveItems(itemQuantities);
        }

        public int GetAttributeModifiedValue(string key)
        {
            var tempModValue = 0;
            foreach (PlayerAttribute attribute in Attributes)
            {
                if (attribute.Key == key)
                {
                    tempModValue = attribute.ModifiedValue;
                }

            }
            return tempModValue;
        }

        //method to get int modifier based on attributes
        //called in AttackWithWeapon class
        public int GetAttributeValueModifier(LivingEntity entity, string key)
        {
            int entityAttributeValue = entity.GetAttributeModifiedValue(key);
            int attributeValueModifier = 0;

            switch (entityAttributeValue)
            {
                case 1:
                    attributeValueModifier = -5;
                    break;
                case 2:
                    attributeValueModifier = -4;
                    break;
                case 3:
                    attributeValueModifier = -4;
                    break;
                case 4:
                    attributeValueModifier = -3;
                    break;
                case 5:
                    attributeValueModifier = -3;
                    break;
                case 6:
                    attributeValueModifier = -2;
                    break;
                case 7:
                    attributeValueModifier = -2;
                    break;
                case 8:
                    attributeValueModifier = -1;
                    break;
                case 9:
                    attributeValueModifier = -1;
                    break;
                case 10:
                    attributeValueModifier = 0;
                    break;
                case 11:
                    attributeValueModifier = 0;
                    break;
                case 12:
                    attributeValueModifier = 1;
                    break;
                case 13:
                    attributeValueModifier = 1;
                    break;
                case 14:
                    attributeValueModifier = 2;
                    break;
                case 15:
                    attributeValueModifier = 2;
                    break;
                case 16:
                    attributeValueModifier = 3;
                    break;
                case 17:
                    attributeValueModifier = 3;
                    break;
                case 18:
                    attributeValueModifier = 4;
                    break;
                case 19:
                    attributeValueModifier = 4;
                    break;
                case 20:
                    attributeValueModifier = 5;
                    break;
                case 21:
                    attributeValueModifier = 5;
                    break;
            }

            return attributeValueModifier;
        }

        #region Private functions

        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }

        private void RaiseActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }

        #endregion
    }
}