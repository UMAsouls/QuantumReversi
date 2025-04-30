

//実際の石のタイプ
//白に変わる確率で区別
public enum StoneType
{
    TEN,
    THIRTY,
    SEVENTY,
    NINETY
}

//盤面に見えている石のタイプ
//プレイヤーの石、何もない、CPUの石
public enum WatchedStoneType
{
    PlayerSTONE,
    NONE,
    CPSTONE
}
