namespace Daybreak.Common.Features.PotLoot;

public readonly record struct PotBreakContext(
    int X,
    int Y,
    int Style
);

public readonly record struct PotLootContext(
    int X,
    int Y,
    int X2,
    int Y2,
    int Style,
    bool AboveRockLayer,
    bool AboveUnderworldLayer
);

public readonly record struct PotLootContextWithCoinMult(
    int X,
    int Y,
    int X2,
    int Y2,
    int Style,
    bool AboveRockLayer,
    bool AboveUnderworldLayer,
    float CoinMult
);

public readonly record struct PotItemDrop(
    int ItemType,
    int Stack = 1
);
