namespace _3DEUX1.IMMOBILIER.TG.NewControls;

public class BoxSkiletonView : BoxView
{
    public BoxSkiletonView()
    {
        this.Dispatcher.StartTimer(TimeSpan.FromSeconds(1.5), () =>
        {
            this.FadeTo(0.5, 750, Easing.SinInOut).ContinueWith((x) =>
            {
                this.FadeTo(1, 750, Easing.SinInOut);
            });

            return true; //Fade In Fade out Animation everytime
        });
    }

}
