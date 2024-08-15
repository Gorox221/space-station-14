using Content.Shared.DoAfter;
using Robust.Shared.Serialization;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.List;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Dictionary;

namespace Content.Shared.Fishing.Components;

[RegisterComponent]
public sealed partial class PondComponent : Component
{

    [DataField("fish", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string FishPrototype = "MobAdultSlimesGreen";

}

