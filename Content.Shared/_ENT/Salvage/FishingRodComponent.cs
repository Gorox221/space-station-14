using Content.Shared.DoAfter;
using Robust.Shared.Serialization;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared.Fishing.Components;

[RegisterComponent]
public sealed partial class FishingRodComponent : Component
{

    [ViewVariables, DataField("effect")]
    public EntityUid Effect { get; set; }
    
}

[Serializable, NetSerializable]
public sealed partial class FishingDoAfterEvent : SimpleDoAfterEvent
{

}
