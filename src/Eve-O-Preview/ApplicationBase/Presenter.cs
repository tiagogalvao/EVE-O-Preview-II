using EveOPreview.ApplicationBase.Interface;

namespace EveOPreview
{
    public abstract class Presenter<TView> : IPresenter where TView : IView
    {
        // Properties are used instead of fields so the code remains CLS compliant
        // 'protected readonly' fields would result in non-CLS compliant code
        protected TView View { get; private set; }

        protected Presenter(TView view)
        {
            View = view;
        }

        public void Run()
        {
            View.Show();
        }
    }
}