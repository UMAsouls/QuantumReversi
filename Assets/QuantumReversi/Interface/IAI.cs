

using Cysharp.Threading.Tasks;

public interface IAI
{

    public UniTask SetStone();

    public void ModelLoad();

    public void ModelDispose();
}
