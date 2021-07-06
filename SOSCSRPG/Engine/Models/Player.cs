﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties

        private int _experiencePoints;

        public int ExperiencePoints
        {
            get => _experiencePoints;
            private set
            {
                _experiencePoints = value;

                OnPropertyChanged();

                SetLevelAndMaximumHitPoints();
            }
        }

//        public new int ArmourRating { get; set; }

        public ObservableCollection<QuestStatus> Quests { get; } =
            new ObservableCollection<QuestStatus>();

        public ObservableCollection<Recipe> Recipes { get; } =
            new ObservableCollection<Recipe>();

        public new ObservableCollection<PlayerAttribute> Attributes { get; set; } =
            new ObservableCollection<PlayerAttribute>();

        #endregion

        public event EventHandler OnLeveledUp;

        public Player(string name, int experiencePoints,
            int maximumHitPoints, int currentHitPoints,
            ObservableCollection<PlayerAttribute> attributes, int gold, int armourRating) :
            base(name, maximumHitPoints, currentHitPoints, attributes, gold)
        {
            ExperiencePoints = experiencePoints;
            ArmourRating = armourRating;
            foreach (PlayerAttribute attribute in attributes)
            {
                Attributes.Add(attribute);
            }
        }

        

        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }

        private void SetLevelAndMaximumHitPoints()
        {
            int originalLevel = Level;

            Level = (ExperiencePoints / 100) + 1;

            if (Level != originalLevel)
            {
                MaximumHitPoints = Level * 10;
                //level * 1d6 + CON

                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}