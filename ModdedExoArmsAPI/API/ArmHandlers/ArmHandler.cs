﻿using UnityEngine;

namespace ModdedArmsHelper.API.ArmHandlers
{
    public abstract class ArmHandler : MonoBehaviour
    {
        public abstract void Awake();
        public abstract void Start();

        private Vehicle _vehicle = null;
        public Vehicle Vehicle
        {
            get
            {
                if (_vehicle == null)
                {
                    _vehicle = GetComponentInParent<Vehicle>();
                }

                return _vehicle;
            }
        }

        private SeaMoth _seamoth = null;
        public SeaMoth Seamoth
        {
            get
            {
                if (_seamoth == null)
                {
                    _seamoth = GetComponentInParent<SeaMoth>();
                }

                return _seamoth;
            }
        }

        private Exosuit _exosuit = null;
        public Exosuit Exosuit
        {
            get
            {
                if (_exosuit == null)
                {
                    _exosuit = GetComponentInParent<Exosuit>();
                }

                return _exosuit;
            }
        }       

        private Animator _animator = null;
        public Animator Animator
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponent<Animator>();
                }

                return _animator;
            }
        }

        private EnergyInterface _energyInterface = null;
        public EnergyInterface EnergyInterface
        {
            get
            {
                if (_energyInterface == null)
                {
                    _energyInterface = GetComponentInParent<EnergyInterface>();
                }

                return _energyInterface;
            }
        }

        private ArmTag _armTag = null;
        public ArmTag ArmTag
        {
            get
            {
                if (_armTag == null)
                {
                    _armTag = GetComponent<ArmTag>();
                }

                return _armTag;
            }
        }

    }
}
