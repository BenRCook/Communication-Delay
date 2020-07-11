using Drone;
using UnityEngine;

namespace Action
{
    public interface IAction
    {
        string GetDescription();
        IDrone TakeAction(IDrone drone);
        Color GetBackgroundColor();
    }
}