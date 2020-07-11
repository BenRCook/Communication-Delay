using System;
using Action;
using System.Collections.Generic;
using TileLocation;
using UnityEngine;

namespace Drone
{
    using HexLocation = Tuple<int, int, int>;
    public class Drone : MonoBehaviour, IDrone
    {
        [SerializeField] private long health;
        [SerializeField] private long movementLeft;
        [SerializeField] private Queue<IAction> actions;
        [SerializeField] private HexDirection facing;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void MoveTo(HexLocation newLocation)
        {
            throw new NotImplementedException();
        }

        public void LaserAttack(HexDirection direction)
        {
            throw new NotImplementedException();
        }

        public void KineticAttack(IDrone target)
        {
            throw new NotImplementedException();
        }

        public void MissileAttack(IDrone target)
        {
            throw new NotImplementedException();
        }

        public HexLocation GetLocation()
        {
            throw new NotImplementedException();
        }

        public bool IsVisibleFrom(HexLocation location, HexDirection direction)
        {
            throw new NotImplementedException();
        }

        public int DistanceFrom(HexLocation location)
        {
            throw new NotImplementedException();
        }
    }
}
