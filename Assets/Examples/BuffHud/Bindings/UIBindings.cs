using System;
using UniRx;
using UnityEngine.UI;

namespace Assets.Examples.BuffHud.Bindings
{
    public static class UIExtensions
    {
        public static IDisposable BindTextTo(this Text label, IReactiveProperty<string> textProperty)
        {
            return textProperty.DistinctUntilChanged()
                .Subscribe(x => label.text = x);
        }
    }
}