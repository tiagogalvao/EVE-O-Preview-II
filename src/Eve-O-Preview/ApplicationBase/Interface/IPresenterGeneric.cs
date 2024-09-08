namespace EveOPreview.ApplicationBase.Interface
{
    public interface IPresenter<in TArgument>
    {
        void Run(TArgument args);
    }
}