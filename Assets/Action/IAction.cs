using Drone;
using UnityEngine;

namespace Action
{
    public interface IAction
    {
        string GetDescription();
        void TakeAction(AbsDrone drone);
        Color GetBackgroundColor();
    }
}