namespace FirstWebApp.Models
{
    public class FieldViewDataModel : GameInfo
    {
        public bool IsReadOnlyView { get; private set; }

        public FieldViewDataModel(GameInfo gameInfo, bool isReadOnlyView) : base(gameInfo) 
        {
            IsReadOnlyView = isReadOnlyView;
        }
    }
}
