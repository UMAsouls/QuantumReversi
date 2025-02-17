

using Cysharp.Threading.Tasks;

//AIクラスのインターフェース
public interface IAI
{

    public UniTask SetStone();

    public void ModelLoad();

    public void ModelDispose();
}
