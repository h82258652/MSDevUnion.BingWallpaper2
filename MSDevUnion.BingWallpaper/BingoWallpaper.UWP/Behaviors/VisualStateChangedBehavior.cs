using Microsoft.Xaml.Interactivity;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using WinRTXamlToolkit.AwaitableUI;

namespace BingoWallpaper.Uwp.Behaviors
{
    [ContentProperty(Name = nameof(Actions))]
    public class VisualStateChangedBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(nameof(Actions), typeof(ActionCollection), typeof(VisualStateChangedBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(nameof(From), typeof(string), typeof(VisualStateChangedBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(nameof(To), typeof(string), typeof(VisualStateChangedBehavior), new PropertyMetadata(null));

        public ActionCollection Actions
        {
            get
            {
                var actionCollection = (ActionCollection)GetValue(ActionsProperty);
                if (actionCollection == null)
                {
                    actionCollection = new ActionCollection();
                    SetValue(ActionsProperty, actionCollection);
                }
                return actionCollection;
            }
        }

        public string From
        {
            get
            {
                return (string)GetValue(FromProperty);
            }
            set
            {
                SetValue(FromProperty, value);
            }
        }

        public string To
        {
            get
            {
                return (string)GetValue(ToProperty);
            }
            set
            {
                SetValue(ToProperty, value);
            }
        }

        protected override async void OnAttached()
        {
            base.OnAttached();

            await Task.Delay(3000);
            await AssociatedObject.WaitForLoadedAsync();

            var visualStateGroups = VisualStateManager.GetVisualStateGroups(AssociatedObject).ToList();
            foreach (var visualStateGroup in visualStateGroups)
            {
                visualStateGroup.CurrentStateChanged += VisualStateGroup_CurrentStateChanged;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            var visualStateGroups = VisualStateManager.GetVisualStateGroups(AssociatedObject);
            foreach (var visualStateGroup in visualStateGroups)
            {
                visualStateGroup.CurrentStateChanged -= VisualStateGroup_CurrentStateChanged;
            }
        }

        private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            if ((From == null || e.OldState?.Name == From) && (To == null || e.NewState?.Name == To))
            {
                Interaction.ExecuteActions(AssociatedObject, Actions, e);
            }
        }
    }
}